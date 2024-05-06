namespace LootBoot.Epilogue.Game;
public class Settings
{
    public Settings()
    {
        Graphics = new GraphicSettings();
        Keybinding = new KeybindingSettings();
        Update();
    }
    public void Update()
    {
        if (Graphics.Apply)
        {
            //GameCore.EngineSettings.DoRegularDrawing = Graphics.DoRegularDrawing;
            //GameCore.EngineSettings.DoDebugDrawing = Graphics.DoDebugDrawing;
        }
        if (Keybinding.Apply)
        {
            //do keybinding updates
        }
        Graphics.Update();
        Keybinding.Update();
    }
    public GraphicSettings Graphics { get; set; }
    public KeybindingSettings Keybinding { get; set; }
    public class GraphicSettings : SettingsCollection
    {
        public GraphicSettings()
        {
            DoDebugDrawing = false;
            Apply = true;
        }
        private bool _doDebugDrawing;
        public bool DoDebugDrawing
        {
            get => _doDebugDrawing;
            set => Change(ref _doDebugDrawing, value);
        }
    }
    public class KeybindingSettings : SettingsCollection
    {
        public KeybindingSettings()
        {
            MoveForwards = Key.W;
            MoveBackwards = Key.S;
            RotateClockwise = Key.D;
            RotateCounterClockwise = Key.A;
            ActivateWeapon = Key.Space;
            DebugConsole = Key.OemTilde;
            Pause = Key.Escape;
            Apply = true;
        }
        private Key _moveForwards;
        public Key MoveForwards
        {
            get => _moveForwards;
            set => Change(ref _moveForwards, value);
        }
        private Key _moveBackwards;
        public Key MoveBackwards
        {
            get => _moveBackwards;
            set => Change(ref _moveBackwards, value);
        }
        private Key _rotateClockwise;
        public Key RotateClockwise
        {
            get => _rotateClockwise;
            set => Change(ref _rotateClockwise, value);
        }
        private Key _rotateCounterClockwise;
        public Key RotateCounterClockwise
        {
            get => _rotateCounterClockwise;
            set => Change(ref _rotateCounterClockwise, value);
        }
        private Key _activateWeapon;
        public Key ActivateWeapon
        {
            get => _activateWeapon;
            set => Change(ref _activateWeapon, value);
        }
        private Key _debugConsole;
        public Key DebugConsole
        {
            get => _debugConsole;
            set => Change(ref _debugConsole, value);
        }
        private Key _pause;
        public Key Pause
        {
            get => _pause;
            set => Change(ref _pause, value);
        }
    }
    public abstract class SettingsCollection
    {
        public virtual void Update() => Apply = false;
        protected virtual void Change<T>(ref T field, T value, bool invokeOnMatchingValue = true)
        {
            if (!invokeOnMatchingValue || !EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                Apply = true;
            }
        }
        [JsonIgnore]
        public bool Apply { get; set; }
    }
}
