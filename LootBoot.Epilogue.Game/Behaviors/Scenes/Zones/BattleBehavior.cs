namespace LootBoot.Epilogue.Game.Behaviors.Scenes.Zones;
public class BattleBehavior : Behavior<Battle>
{
    protected const double BETWEEN_WAVE_DURATION_SECONDS = 5;
    private DateTime? betweenWaveTime;
    protected override void Create()
    {
        IsCompleting = false;
        CurrentWaveIndex = -1;
        SetWave();
        base.Create();
    }
    protected override void Update()
    {
        if (Source.IsActive)
        {
            if (!IsCompleting && CurrentWave.IsComplete)
            {
                SetWave();
                betweenWaveTime = null;
            }
            if (IsCompleting || !CurrentWave.IsStarted)
            {
                if (Calculations.DateTime.IsSeconds(ref betweenWaveTime, BETWEEN_WAVE_DURATION_SECONDS))
                {
                    if (IsCompleting)
                    {
                        Complete();
                    }
                    else
                    {
                        Source.StartCurrentWave();
                    }
                }
            }
            else
            {
                betweenWaveTime = null;
            }
        }
        base.Update();
    }
    protected void SetWave()
    {
        CurrentWaveIndex++;
        if (Source.SetCurrentWave(CurrentWaveIndex))
        {
            IsCompleting = true;
        }
    }
    protected bool IsCompleting { get; set; }
    protected Wave CurrentWave => Source.CurrentWave;
    protected int CurrentWaveIndex { get; set; }
}
