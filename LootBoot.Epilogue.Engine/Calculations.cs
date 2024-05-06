namespace LootBoot.Epilogue.Engine;
public static class Calculations
{
    public static class DateTime
    {
        public static bool IsSeconds(ref System.DateTime? dateTime, double seconds)
        {
            if (dateTime is not null)
            {
                if ((System.DateTime.Now - dateTime.Value).TotalSeconds >= seconds)
                {
                    dateTime = null;
                    return true;
                }
            }
            else
            {
                dateTime = System.DateTime.Now;
            }
            return false;
        }
    }
    public static class Percentage
    {
        public static double From(double current, double maximum) => current / maximum;
        public static double Of(double percentage, double maximum) => maximum * percentage;
        public static double Of(double percentage, double minimum, double maximum) => (maximum - minimum) * percentage + minimum;
    }
    public static class Geometry
    {
        public static class Coordinates
        {
            public static class Screen
            {
                public static Point PositionToWorld(Point screenPosition)
                {
                    double screenX = screenPosition.X;
                    double screenY = screenPosition.Y;
                    double viewportWidth = Viewport.Geometry.Size.Value.Width;
                    double viewportHeight = Viewport.Geometry.Size.Value.Height;
                    double viewportLeft = Viewport.Geometry.Position.Value.X - viewportWidth / 2;
                    double viewportTop = Viewport.Geometry.Position.Value.Y - viewportHeight / 2;
                    double worldX = screenX + viewportLeft;
                    double worldY = screenY + viewportTop;
                    return new Point(worldX, worldY);
                }
                public static Point PositionFromPerspective(Point worldPosition, double perspectiveScale)
                {
                    if (perspectiveScale == 1)
                    {
                        return worldPosition.ToScreen();
                    }
                    Point viewportWorldPosition = Viewport.Geometry.Position;
                    double xDiffrence = worldPosition.X - viewportWorldPosition.X;
                    double yDiffrence = worldPosition.Y - viewportWorldPosition.Y;
                    double xScaled = xDiffrence * perspectiveScale;
                    double yScaled = yDiffrence * perspectiveScale;
                    double x = viewportWorldPosition.X + xScaled;
                    double y = viewportWorldPosition.Y + yScaled;
                    Point perspectiveWorldPosition = new(x, y);
                    return perspectiveWorldPosition.ToScreen();
                }
            }
            public static class World
            {
                public static Point PositionToScreen(Point worldPosition)
                {
                    double worldX = worldPosition.X;
                    double worldY = worldPosition.Y;
                    double viewportWidth = Viewport.Geometry.Size.Value.Width;
                    double viewportHeight = Viewport.Geometry.Size.Value.Height;
                    double viewportLeft = Viewport.Geometry.Position.Value.X - viewportWidth / 2;
                    double viewportTop = Viewport.Geometry.Position.Value.Y - viewportHeight / 2;
                    double screenX = worldX - viewportLeft;
                    double screenY = worldY - viewportTop;
                    return new Point(screenX, screenY);
                }
                public static Point PositionFromPerspective(Point screenPosition, double perspectiveScale)
                {
                    Point worldPosition = screenPosition.ToWorld();
                    if (perspectiveScale == 1)
                    {
                        return worldPosition;
                    }
                    Point viewportWorldPosition = Viewport.Geometry.Position;
                    double x = worldPosition.X - viewportWorldPosition.X;
                    double y = worldPosition.Y - viewportWorldPosition.Y;
                    double xScaled = x / perspectiveScale;
                    double yScaled = y / perspectiveScale;
                    double xDiffrence = viewportWorldPosition.X + xScaled;
                    double yDiffrence = viewportWorldPosition.Y + yScaled;
                    return new(xDiffrence, yDiffrence);
                }
            }
        }
        public static class Position
        {
            public static Point ToTarget(Point position, Point target, Speed speed)
            {
                if (speed == 0)
                {
                    return position;
                }
                double speedDistance = speed.Distance;
                Vector vector = target - position;
                double distance = Distance.ToTarget(position, target);
                Point unitVector = new(vector.X / distance, vector.Y / distance);
                return distance < speedDistance ? target : new Point(position.X + unitVector.X * speedDistance, position.Y + unitVector.Y * speedDistance);
            }
            public static Point FromAngle(Point position, Engine.Angle angle, Point origin)
            {
                double cos = Math.Cos(angle.Radians);
                double sin = Math.Sin(angle.Radians);
                double distanceX = position.X - origin.X;
                double distanceY = position.Y - origin.Y;
                double x = cos * distanceX - sin * distanceY + origin.X;
                double y = sin * distanceX + cos * distanceY + origin.Y;
                return new(x, y);
            }
            public static Point AtAngle(Point position, Engine.Angle angle, Speed speed)
            {
                double distance = speed.Distance;
                return AtAngle(position, angle, distance);
            }
            public static Point AtAngle(Point position, Engine.Angle angle, double distance)
            {
                if (distance == 0)
                {
                    return position;
                }
                double sin = Math.Sin(angle.Radians);
                double x = position.X + sin * distance;
                double cos = Math.Cos(angle.Radians);
                double y = position.Y + cos * -distance;
                return new Point(x, y);
            }
        }
        public static class Angle
        {
            public static double ToRadians(double degrees) => degrees * (Math.PI / 180);
            public static double ToDegrees(double radians)
            {
                double degrees = radians * (180 / Math.PI);
                return Clamp(degrees);
            }
            public static double Clamp(double degrees) => degrees < 0 ? Clamp(degrees + 360) : Math.Abs(degrees % 360);
            public static Engine.Angle ToTarget(Point position, Point target)
            {
                Vector vector = target - position;
                vector.Normalize();
                double radians = Math.Acos(-vector.Y);
                return new Engine.Angle() { Radians = vector.X > 0 ? radians : -radians };
            }
            public static Engine.Angle ToAngle(Engine.Angle angle, Engine.Angle target, Speed speed) => ToAngle(angle, target, speed.Distance);
            public static Engine.Angle ToAngle(Engine.Angle angle, Engine.Angle target, double distance)
            {
                if (distance == 0 || angle == target)
                {
                    return angle;
                }
                distance = Math.Abs(distance);
                double diffrence = 180 - Math.Abs(Math.Abs((double)(angle - target)) - 180);
                double addDiffrence = 180 - Math.Abs(Math.Abs((double)(angle + distance - target)) - 180);
                double subDiffrence = 180 - Math.Abs(Math.Abs((double)(angle - distance - target)) - 180);
                double degress;
                if (addDiffrence < diffrence)
                {
                    degress = angle + distance;
                }
                else if (subDiffrence < diffrence)
                {
                    degress = angle - distance;
                }
                else
                {
                    degress = target;
                }
                return degress;
            }
        }
        public static class Distance
        {
            public static double ToTarget(Point position, Point target)
            {
                Vector vector = target - position;
                return Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            }
        }
        public static class Bounds
        {
            public static Rect AtAngle(Rect bounds, Engine.Angle angle)
            {
                Point origin = ToCenter(bounds);
                Point topLeft = Position.FromAngle(bounds.TopLeft, angle, origin);
                Point topRight = Position.FromAngle(bounds.TopRight, angle, origin);
                Point bottomLeft = Position.FromAngle(bounds.BottomLeft, angle, origin);
                Point bottomRight = Position.FromAngle(bounds.BottomRight, angle, origin);
                double minX = Math.Min(Math.Min(topLeft.X, topRight.X), Math.Min(bottomLeft.X, bottomRight.X));
                double minY = Math.Min(Math.Min(topLeft.Y, topRight.Y), Math.Min(bottomLeft.Y, bottomRight.Y));
                double maxX = Math.Max(Math.Max(topLeft.X, topRight.X), Math.Max(bottomLeft.X, bottomRight.X));
                double maxY = Math.Max(Math.Max(topLeft.Y, topRight.Y), Math.Max(bottomLeft.Y, bottomRight.Y));
                double width = maxX - minX;
                double height = maxY - minY;
                return new Rect(new Point(minX, minY), new Size(width, height));
            }
            public static Point ToCenter(Rect bounds) => new(bounds.Left + bounds.Width / 2, bounds.Top + bounds.Height / 2);
            public static Rect ToExpanded(Rect bounds, Offset offset) => new Rect(new Point(bounds.X - offset.Left, bounds.Y - offset.Top), new Size(bounds.Width + offset.Left + offset.Right, bounds.Height + offset.Top + offset.Bottom));
            public static (Rect topLeft, Rect top, Rect topRight, Rect right, Rect bottomRight, Rect bottom, Rect bottomLeft, Rect left) GetExtended(Rect inside, Offset offset, Offset area)
            {
                double xLeft = inside.Left - offset.Left - area.Left;
                double xRight = inside.Left + inside.Width + offset.Right;
                double yTop = inside.Top - offset.Top - area.Top;
                double yBottom = inside.Bottom + offset.Bottom;
                double xHorizontal = inside.Left - offset.Left - Math.Min(area.Left, 0);
                double yVertical = inside.Top - offset.Top - Math.Min(area.Top, 0);
                double horizontalWidth = inside.Width + offset.Left + offset.Right + Math.Min(area.Left, 0) + Math.Min(area.Right, 0);
                double verticalHeight = inside.Height + offset.Top + offset.Bottom + Math.Min(area.Top, 0) + Math.Min(area.Bottom, 0);
                Rect topLeft = new Rect(xLeft, yTop, Math.Max(area.Left, 0), Math.Max(area.Top, 0));
                Rect top = new Rect(xHorizontal, yTop, Math.Max(horizontalWidth, 0), Math.Max(area.Top, 0));
                Rect topRight = new Rect(xRight, yTop, Math.Max(area.Right, 0), Math.Max(area.Top, 0));
                Rect right = new Rect(xRight, yVertical, Math.Max(area.Right, 0), Math.Max(verticalHeight, 0));
                Rect bottomRight = new Rect(xRight, yBottom, Math.Max(area.Right, 0), Math.Max(area.Bottom, 0));
                Rect bottom = new Rect(xHorizontal, yBottom, Math.Max(horizontalWidth, 0), Math.Max(area.Bottom, 0));
                Rect bottomLeft = new Rect(xLeft, yBottom, Math.Max(area.Left, 0), Math.Max(area.Bottom, 0));
                Rect left = new Rect(xLeft, yVertical, Math.Max(area.Left, 0), Math.Max(verticalHeight, 0));
                return (topLeft, top, topRight, right, bottomRight, bottom, bottomLeft, left);
            }
        }
    }
}

