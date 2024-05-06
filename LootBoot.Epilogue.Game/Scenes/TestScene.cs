namespace LootBoot.Epilogue.Game.Scenes;
public class TestScene : Scene
{
    public const bool SHOW = false;
    public TestScene() : base((int)Configurations.Scenes.Layers.Primary) { }
    protected override void Create()
    {
        Viewport.Geometry.Size = new Size(550, 550);
        Viewport.Geometry.Position = new Point(5, 0);
        SpawnCenter(true);
        SpawnWatched(true);
        SimplePerspectiveTests(false);
        SpawnText(false);
        ActivateOnRKey();
        base.Create();
    }
    private void ActivateOnRKey()
    {
        SpawnPerspectiveTests(false);
        DestroyTesting(true);
    }
    private int destroyIndex = 0;
    private void DestroyTesting(bool isDo)
    {
        if (isDo)
        {
            if (IsUpdating && !Center00.IsDestroyed)
            {
                Center00.QueueDestroy();
            }
        }
    }
    private void SpawnItem(bool isDo)
    {
        if (isDo)
        {
            //new Scrap(this, new Point(0, 0), 45);
        }
    }
    private void SpawnTextActivate(bool isDo)
    {
        if (isDo)
        {
            TextEntity.Text = "Changed!";
            TextEntity.Geometry.Position = new PVPoint(0, 0);
            TextEntity.TestSprite.Color = Color.Yellow;
            TextEntity.TestSprite.EmSize = 20;
            TextEntity.TestSprite.PixelsPerDip = 20;
            TextEntity.TestSprite.TypeFace = "Tw Cen MT";
        }
    }
    private void SpawnText(bool isDo)
    {
        if (isDo)
        {
            TextEntity = new TextTestEntity(this, new()
            {
                Define = new()
                {
                    Position = new PVPoint(0.5, 0.5),
                    Size = new PVSize(0.015),
                },
                Style = new()
                {
                    Color = Color.White,
                    PerspectiveScale = 1,
                    ZIndex = 201,
                },
            })
            {
                Text = "Hello"
            };
        }
    }
    private void SpawnCenter(bool isDo)
    {
        if (isDo)
        {
            Center00 = new(this, new()
            {
                Define = new()
                {
                    Position = new PVPoint(0, 0),
                    Size = new PVSize(0.015),
                },
                Style = new()
                {
                    Contour = Sprite.Contours.Ellipse,
                    PerspectiveScale = 1,
                    ZIndex = 101,
                },
            });
        }
    }
    private void SpawnWatched(bool isDo)
    {
        if (isDo)
        {
            bool isAttached = false;
            if (isAttached)
            {
                Watched = new AttachedTestEntity(this);
            }
            else
            {
                Watched = new WatchEntity(this);
                Watched.Geometry.Angle = 90;
            }
            Viewport.Watched.Set(Watched.Geometry);
        }
    }
    private Rect inside;
    private void SpawnPerspectiveTests(bool isDo)
    {
        if (isDo)
        {
            if (InsideRect is not null)
            {
                //InsideRect.Destroy();
                InsideRect = null;
            }
            if (InsideRect is null)
            {
                Point insidePosition = Viewport.Geometry.Position;
                Size insideSize = new PVSize(0.2);
                inside = new Rect(new(insidePosition.X - insideSize.Width / 2, insidePosition.Y - insideSize.Height / 2), insideSize);
                InsideRect = new TestEntity(this, new()
                {
                    Define = new()
                    {
                        Position = inside.ToCenter(),
                        Size = inside.Size,
                    },
                    Style = new()
                    {
                        Color = Color.Pink,
                        ZIndex = 1,
                    },
                });
                ;
            }
            if (PerspectiveScaleTestEntities is null)
            {
                PerspectiveScaleTestEntities = new();
            }
            foreach (PerspectiveScaleTestEntity entity in PerspectiveScaleTestEntities)
            {
                //entity.Destroy();
            }
            double perspectiveScale = 0.75;
            Size size = new Size(32, 7);
            Point spawnScreenPosition;
            bool doSingluar = false;
            if (doSingluar)
            {
                Point position = inside.ToScreen().Location;
                spawnScreenPosition = new Point(position.X - size.Width / 2, position.Y - size.Height / 2);
                SpawnPerspectiveScaleSpawnTestEntity(0, perspectiveScale, spawnScreenPosition, size, 0);
            }
            else
            {
                PerspectiveScaleTestEntities.Clear();
                int total = 5;
                for (int count = 0; count < total; count++)
                {
                    Angle angle = 45 + 90;
                    bool doInsidePosition = false;
                    if (doInsidePosition)
                    {
                        spawnScreenPosition = inside.ToScreen().Rng().Inside();
                    }
                    else
                    {
                        Size angledSize = new Rect(new(), size).AtAngle(angle).Size;
                        spawnScreenPosition = inside.ToScreen().Rng().Outside(angledSize);
                    }
                    SpawnPerspectiveScaleSpawnTestEntity(count, perspectiveScale, spawnScreenPosition, size, angle);
                }
            }
        }
    }
    private void SpawnPerspectiveScaleSpawnTestEntity(int count, double perspectiveScale, Point spawnScreenPosition, Size size, Angle angle)
    {

        Point spawnWorldPosition = spawnScreenPosition.ToWorld();
        Point convertedSpawnPosition = Calculations.Geometry.Coordinates.World.PositionFromPerspective(spawnScreenPosition, perspectiveScale);
        PerspectiveScaleSpawnTestEntity entity = new(this, new()
        {
            Define = new()
            {
                Position = convertedSpawnPosition,
                Size = size,
                Angle = angle,
            },
            Style = new()
            {
                Color = Rng.ToColor(),
                PerspectiveScale = perspectiveScale,
                ZIndex = count + 2,
            }
        });
        entity.SpawnPositionShape.Geometry.Position = spawnWorldPosition;
        PerspectiveScaleTestEntities.Add(entity);
    }
    private void SimplePerspectiveTests(bool isDo)
    {
        if (isDo)
        {
            bool isMultiple = true;
            if (isMultiple)
            {
                PerspectiveScaleTestEntities = new();
                int total = 5;
                for (int count = 0; count < total; count++)
                {
                    int oneCount = count + 1;
                    PerspectiveScaleTestEntities.Add(new(this, new()
                    {
                        Define = new()
                        {
                            Position = new(),
                            Size = new PVSize(oneCount * 0.05),
                        },
                        Style = new()
                        {
                            Color = Rng.ToColor(),
                            PerspectiveScale = oneCount * 0.15,
                            ZIndex = oneCount,
                        },
                    }));
                }
            }
            else
            {
                int oneCount = 3;
                Point point = new PVPoint(oneCount * 0.2, 0);
                Watched.Geometry.Position = point;
                new PerspectiveScaleTestEntity(this, new()
                {
                    Define = new()
                    {
                        Position = point,
                        Size = new PVSize(0.05),
                    },
                    Style = new()
                    {
                        Color = Rng.ToColor(),
                        PerspectiveScale = oneCount * 0.15,
                        ZIndex = oneCount,
                    },
                });
            }
            SpawnPerspectiveEntity = new PerspectiveScaleTestEntity(this, new()
            {
                Define = new()
                {
                    Size = new PVSize(0.05),
                },
                Style = new()
                {
                    Color = Color.Red,
                    PerspectiveScale = 0.5,
                    ZIndex = 10,
                }
            });
            SpawnPerspectiveEntity.Geometry.Position = Calculations.Geometry.Coordinates.World.PositionFromPerspective(new Point(25, 25), SpawnPerspectiveEntity.Sprite.PerspectiveScale);
        }
    }
    private bool IsUpdating = false;
    protected override void Update()
    {
        IsUpdating = true;
        if (Input.IsKeyReleased(Key.R))
        {
            ActivateOnRKey();
        }
        if (Input.IsKeyReleased(Key.T))
        {
            SpawnTextActivate(true);
        }
        if (Input.IsKeyReleased(Key.I))
        {
            SpawnItem(true);
        }
        base.Update();
    }
    private WatchEntity Watched { get; set; }
    private TestEntity Center00 { get; set; }
    private TestEntity InsideRect { get; set; }
    private TextTestEntity TextEntity { get; set; }
    private PerspectiveScaleTestEntity SpawnPerspectiveEntity { get; set; }
    private List<PerspectiveScaleTestEntity> PerspectiveScaleTestEntities { get; set; }
    private class TextTestEntity : Entity
    {
        public TextTestEntity(Script parent, Construct construct) : this(parent, construct, null) { }
        public TextTestEntity(Script parent, Construct construct, Engine.Geometry baseGeometry) : base(parent, baseGeometry)
        {
            Geometry.Position = construct.Define.Position;
            Geometry.Size = construct.Define.Size;
            Geometry.Angle = construct.Define.Angle;
            Sprite = new Shape(this, Geometry)
            {
                Contour = construct.Style.Contour,
                Color = construct.Style.Color,
                Stroke = construct.Style.Stroke,
                StrokeThickness = construct.Style.StrokeThickness,
                PerspectiveScale = construct.Style.PerspectiveScale,
                Opacity = construct.Style.Opacity,
                ZIndex = construct.Style.ZIndex,
            };
            TestSprite = new Text(this, Geometry)
            {
                Contour = construct.Style.Contour,
                Color = construct.Style.Color,
                PerspectiveScale = construct.Style.PerspectiveScale,
                Opacity = construct.Style.Opacity,
                ZIndex = construct.Style.ZIndex,
            };
        }
        public string Text
        {
            get => TestSprite.Value;
            set => TestSprite.Value = value;
        }
        public Shape Sprite { get; }
        public Text TestSprite { get; }
    }
    private class AttachedTestEntity : WatchEntity
    {
        public AttachedTestEntity(Script parent) : base(parent)
        {
            Size size = new PVSize(0.05);
            double width = size.Width;
            double height = size.Height;
            Geometry.Size = size;
            Shape.Contour = Sprite.Contours.Rectangle;
            double largeFactor = 10;
            double smallFactor = 5;
            Left = SpawnAttached(new Point(-(width / 2), 0), new Size(width / largeFactor, height / smallFactor));
            Top = SpawnAttached(new Point(0, -(height / 2)), new Size(width / smallFactor, height / largeFactor));
            Right = SpawnAttached(new Point(width / 2, 0), new Size(width / largeFactor, height / smallFactor));
            Bottom = SpawnAttached(new Point(0, height / 2), new Size(width / smallFactor, height / largeFactor));
        }
        private TestEntity SpawnAttached(Point position, Size size)
        {
            var entity = new TestEntity(this, new()
            {
                Define = new()
                {
                    Position = position,
                    Size = size,
                },
                Style = new()
                {
                    Color = Rng.ToColor(),
                    ZIndex = Shape.ZIndex + 1,
                },
            }, Geometry);
            entity.Geometry.Size.IsDerivable = false;
            return entity;
        }
        public TestEntity Top { get; set; }
        public TestEntity Right { get; set; }
        public TestEntity Bottom { get; set; }
        public TestEntity Left { get; set; }
    }
    private class PerspectiveScaleSpawnTestEntity : PerspectiveScaleTestEntity
    {
        public PerspectiveScaleSpawnTestEntity(Script parent, Construct construct) : base(parent, construct)
        {
            SpawnPositionShape = new Shape(this, Geometry)
            {
                Contour = Engine.Sprite.Contours.Ellipse,
                Color = Color.Transparent,
                Stroke = construct.Style.Color,
                StrokeThickness = 2,
                PerspectiveScale = 1,
                Opacity = 0.5,
                ZIndex = construct.Style.ZIndex,
            };
            SpawnPositionShape.Geometry.Angle.IsDerivable = false;
            SpawnPositionShape.Geometry.Position.IsDerivable = false;
            SpawnPositionShape.Geometry.Size.IsDerivable = false;
            SpawnPositionShape.Geometry.Size = new Size(3, 3);
        }
        public Shape SpawnPositionShape { get; set; }
    }
    private class PerspectiveScaleTestEntity : TestEntity
    {
        private static Construct SetOpacity(Construct construct)
        {
            construct = new()
            {
                Define = new(construct.Define),
                Style = new(construct.Style)
                {
                    Opacity = 0.75,
                },
            };
            return construct;
        }
        public PerspectiveScaleTestEntity(Script parent, Construct construct) : base(parent, SetOpacity(construct)) => PerspectiveScale1 = new Shape(this, Geometry)
        {
            Contour = construct.Style.Contour,
            Color = Color.Transparent,
            Stroke = construct.Style.Color,
            StrokeThickness = 3,
            PerspectiveScale = 1,
            Opacity = 1,
            ZIndex = construct.Style.ZIndex,
        };
        public Shape PerspectiveScale1 { get; set; }
    }
    private class TestEntity : Entity
    {
        public TestEntity(Script parent, Construct construct) : this(parent, construct, null) { }
        public TestEntity(Script parent, Construct construct, Engine.Geometry baseGeometry) : base(parent, baseGeometry)
        {
            Geometry.Position = construct.Define.Position;
            Geometry.Size = construct.Define.Size;
            Geometry.Angle = construct.Define.Angle;
            Sprite = new Shape(this, Geometry)
            {
                Contour = construct.Style.Contour,
                Color = construct.Style.Color,
                Stroke = construct.Style.Stroke,
                StrokeThickness = construct.Style.StrokeThickness,
                PerspectiveScale = construct.Style.PerspectiveScale,
                Opacity = construct.Style.Opacity,
                ZIndex = construct.Style.ZIndex,
            };
        }
        public override void QueueDestroy()
        {
            base.QueueDestroy();
        }
        public Shape Sprite { get; }
    }
    private class WatchEntity : Entity
    {
        public WatchEntity(Script parent) : base(parent)
        {
            Geometry.Size = new PVSize(0.025);
            Shape = new Shape(this, Geometry)
            {
                Contour = Sprite.Contours.Triangle,
                Color = Color.White,
                Stroke = Color.Red,
                StrokeThickness = 0,
                PerspectiveScale = 1,
                Opacity = 0.85,
                ZIndex = 100,
            };
            Velocity.Set(new()
            {
                Acceleration = 30,
                Momentum = 30,
                Maximum = 300,
                Minimum = -300,
            });
            Rotation.Set(new()
            {
                Acceleration = 20,
                Momentum = 20,
                Maximum = 100,
                Minimum = -100,
            });
            Controller = new Controller(this);
        }
        protected override void Create()
        {
            Controller.Start<WatchBehavior>();
            base.Create();
        }
        public Shape Shape { get; set; }
        private class WatchBehavior : Behavior
        {
            protected override void Create()
            {
                Velocity = GetScript<Velocity>();
                Rotation = GetScript<Rotation>();
                base.Create();
            }
            protected override void Update()
            {
                Velocity.Throttle
                    = Input.IsKeyDown(LootBootEpilogue.Settings.Keybinding.MoveForwards)
                    ? 1
                    : Input.IsKeyDown(LootBootEpilogue.Settings.Keybinding.MoveBackwards)
                    ? -1
                    : 0;
                Rotation.Throttle
                    = Input.IsKeyDown(LootBootEpilogue.Settings.Keybinding.RotateClockwise)
                    ? 1
                    : Input.IsKeyDown(LootBootEpilogue.Settings.Keybinding.RotateCounterClockwise)
                    ? -1
                    : 0;
                base.Update();
            }
            private Velocity Velocity { get; set; }
            private Rotation Rotation { get; set; }
        }
    }
    private struct Construct
    {
        public Define Define { get; init; }
        public Style Style { get; init; }
    }
    private struct Define
    {
        public Define(Define define)
        {
            _position = define._position;
            _size = define._size;
            _angle = define._angle;
        }
        private Point? _position;
        public Point Position
        {
            get => _position is null ? new() : _position.Value;
            init => _position = value;
        }
        private Size? _size;
        public Size Size
        {
            get => _size is null ? new PVSize(0.15) : _size.Value;
            init => _size = value;
        }
        private Angle? _angle;
        public Angle Angle
        {
            get => _angle is null ? new() : _angle.Value;
            init => _angle = value;
        }
    }
    private struct Style
    {
        public Style(Style style)
        {
            _contour = style._contour;
            _color = style._color;
            _stroke = style._stroke;
            _strokeThickness = style._strokeThickness;
            _perspectiveScale = style._perspectiveScale;
            _opacity = style._opacity;
            _zIndex = style._zIndex;
        }
        private Sprite.Contours? _contour;
        public Sprite.Contours Contour
        {
            get => _contour is null ? Sprite.Contours.Rectangle : _contour.Value;
            init => _contour = value;
        }
        private Color? _color;
        public Color Color
        {
            get => _color is null ? Color.Red : _color.Value;
            init => _color = value;
        }
        private Color? _stroke;
        public Color Stroke
        {
            get => _stroke is null ? Color.White : _stroke.Value;
            init => _stroke = value;
        }
        private double? _strokeThickness;
        public double StrokeThickness
        {
            get => _strokeThickness is null ? 0 : _strokeThickness.Value;
            init => _strokeThickness = value;
        }
        private double? _perspectiveScale;
        public double PerspectiveScale
        {
            get => _perspectiveScale is null ? 1 : _perspectiveScale.Value;
            init => _perspectiveScale = value;
        }
        private double? _opacity;
        public double Opacity
        {
            get => _opacity is null ? 1 : _opacity.Value;
            init => _opacity = value;
        }
        private int? _zIndex;
        public int ZIndex
        {
            get => _zIndex is null ? 0 : _zIndex.Value;
            init => _zIndex = value;
        }
    }
}
