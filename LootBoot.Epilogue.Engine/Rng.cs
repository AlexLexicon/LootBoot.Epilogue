namespace LootBoot.Epilogue.Engine;
public static class Rng
{
    private static Random _rng;
    private static Random RNG => _rng ??= new Random();
    public static bool ToBoolean() => ToInteger(0, 1) == 0;
    public static int ToInteger(int min, int max) => RNG.Next(min, max + 1);
    public static double ToDouble(double min, double max) => RNG.NextDouble() * (max - min) + min;
    public static T SelectItem<T>(List<T> items) => SelectItem(items.ToArray());
    public static T SelectItem<T>(params T[] items) => (T)SelectItem(items.Cast<object>().ToArray());
    public static object SelectItem(List<object> items) => SelectItem(items.ToArray());
    public static object SelectItem(params object[] items)
    {
        if (items is null || items.Length <= 0)
        {
            return null;
        }
        int index = ToInteger(0, items.Length - 1);
        return items[index];
    }
    public static TEnum ToEnum<TEnum>() where TEnum : Enum
    {
        Array values = Enum.GetValues(typeof(TEnum));
        return (TEnum)values.GetValue(ToInteger(0, values.Length - 1));
    }
    public static Color ToColor() => new Color(ToInteger(0, 255), ToInteger(0, 255), ToInteger(0, 255));
    public static Size ToSize(double min, double max) => ToSize(min, max, min, max);
    public static Size ToSize(double minWidth, double maxWidth, double minHeight, double maxHeight) => new Size(ToDouble(minWidth, maxWidth), ToDouble(minHeight, maxHeight));
    public static double Negate(double value) => ToBoolean() ? value : -value;
    public static int Negate(int value) => ToBoolean() ? value : -value;
    public static class Percentage
    {
        public static bool Chance(double percentage) => percentage > 0 && ToDouble(0, 1) <= percentage;
        public static TResult Chances<TResult>(TResult noneOrRemainderResult, params (TResult result, double percentage)[] choices)
        {
            if (choices is null || choices.Length <= 0)
            {
                return noneOrRemainderResult;
            }
            List<(TResult result, double percentage)> list = new List<(TResult result, double percentage)>(choices);
            if (list.Count == 1)
            {
                (TResult result, double percentage) = list.First();
                return Chance(percentage) ? result : noneOrRemainderResult;
            }
            List<(TResult result, double percentage)> orderedList = list.OrderBy(c => c.percentage).ToList();
            for (int index = 1; index < orderedList.Count; index++)
            {
                (TResult result, double percentage) choice = orderedList[index];
                choice.percentage += orderedList[index - 1].percentage;
                orderedList[index] = choice;
            }
            double choosenPercentage = ToDouble(0, 1);
            for (int index = 0; index < orderedList.Count; index++)
            {
                (TResult result, double percentage) = orderedList[index];
                if (choosenPercentage < percentage)
                {
                    return result;
                }
            }
            return noneOrRemainderResult;
        }
    }
    public static class Distribution
    {
        public static class Approaching
        {
            public static double Maximum(double min, double max) => Probability(min, max, 0.5);
            public static double Median(double min, double max) => Probability(min, max, 1);
            public static double Minimum(double min, double max) => Probability(min, max, 2);
        }
        public static double Probability(double min, double max, double power)
        {
            double v = min + (max + 1 - min);
            double x = Math.Pow(RNG.NextDouble(), power);
            double d = v * x;
            double u = Math.Floor(d);
            return u + min;
        }
    }
    public static class Geomerty
    {
        public static double Angle() => ToDouble(0, 360);
        public static Point Inside(Rect rect)
        {
            double x = ToDouble(rect.Left, rect.Right);
            double y = ToDouble(rect.Top, rect.Bottom);
            return new Point(x, y);
        }
        public static Point Outside(Rect inside, Size size) => Outside(inside, size, new Offset());
        public static Point Outside(Rect inside, Size size, Offset area) => Outside(inside, new Offset(size.Width / 2, size.Height / 2), area);
        public static Point Outside(Rect inside, Offset offset, Offset area)
        {
            (Rect topLeft, Rect top, Rect topRight, Rect right, Rect bottomRight, Rect bottom, Rect bottomLeft, Rect left) = Calculations.Geometry.Bounds.GetExtended(inside, offset, area);
            bool isAllUnAvailable = isUnAvailable(topLeft) && isUnAvailable(top) && isUnAvailable(topRight) && isUnAvailable(right) && isUnAvailable(bottomRight) && isUnAvailable(bottom) && isUnAvailable(bottomLeft) && isUnAvailable(left);
            List<Rect> choices = new();
            Add(topLeft, isAllUnAvailable, choices);
            Add(top, isAllUnAvailable, choices);
            Add(topRight, isAllUnAvailable, choices);
            Add(right, isAllUnAvailable, choices);
            Add(bottomRight, isAllUnAvailable, choices);
            Add(bottom, isAllUnAvailable, choices);
            Add(bottomLeft, isAllUnAvailable, choices);
            Add(left, isAllUnAvailable, choices);
            if (choices.Count < 0)
            {
                throw new Exception("There is no possible choice point to generate for the provided inside, offset and area");
            }
            double totalArea = 0;
            foreach (Rect choice in choices)
            {
                totalArea += choice.GetArea(false);
            }
            double value = ToDouble(0, totalArea);
            double rectArea = 0;
            foreach (Rect choice in choices)
            {
                rectArea += choice.GetArea(false);
                if (value <= rectArea)
                {
                    return Inside(choice);
                }
            }
            throw new Exception("Failed to calculate the position based on area caculations");
            void Add(Rect rect, bool isAllUnAvailable, List<Rect> choices)
            {
                if (isAllUnAvailable || !isUnAvailable(rect))
                {
                    choices.Add(rect);
                }
            }
            bool isUnAvailable(Rect rect) => rect.Width == 0 || rect.Height == 0;
        }
    }
}
