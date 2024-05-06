namespace LootBoot.Epilogue.Game.Behaviors.Weapons.Strategy;
public class HumanStrategyBehavior : SpacecraftBehavior
{
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
        Weapon.IsActivated = Input.IsKeyDown(LootBootEpilogue.Settings.Keybinding.ActivateWeapon);
        base.Update();
    }
}
