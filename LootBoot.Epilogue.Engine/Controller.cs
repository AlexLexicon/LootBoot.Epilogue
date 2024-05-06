namespace LootBoot.Epilogue.Engine;
public class Controller : Script
{
    public Controller(Script parent) : base(parent, GameSequencer.Permanence.Newest)
    {
        Behaviors = new GameSequencer<Behavior>();
        BehvaiorSetSequences = new();
        BehvaiorSetTriggerActions = new();
        RunningBehaviors = new();
    }
    public BehaviorSequenceFluentBuilder.TriggerStep Sequence<TBehavior>() where TBehavior : Behavior, new() => new BehaviorSequenceFluentBuilder.TriggerStep(this, typeof(TBehavior));
    public TBehavior Start<TBehavior>() where TBehavior : Behavior, new() => (TBehavior)Start(typeof(TBehavior));
    public void Complete<TBehavior>() where TBehavior : Behavior, new() => Complete(typeof(TBehavior));
    public void Complete()
    {
        HashSet<Behavior> previouslyRunningBehaviors = RunningBehaviors.ToHashSet();
        foreach (Behavior behavior in previouslyRunningBehaviors)
        {
            behavior.Complete();
        }
    }
    public void Stop<TBehavior>() where TBehavior : Behavior, new() => Stop(typeof(TBehavior));
    public void Stop()
    {
        HashSet<Behavior> previouslyRunningBehaviors = RunningBehaviors.ToHashSet();
        foreach (Behavior behavior in previouslyRunningBehaviors)
        {
            EndBehvaior(behavior);
        }
    }
    public BehaviorWhenFluentBuilder.TriggerStep When<TBehavior>() where TBehavior : Behavior, new() => new BehaviorWhenFluentBuilder.TriggerStep(this, typeof(TBehavior));
    protected virtual void BehaviorCreated(Behavior behavior) => BehaviorTrigger(behavior, BehvaiorSequence.Triggers.Created);
    protected virtual void BehaviorComplete(Behavior behavior) => BehaviorTrigger(behavior, BehvaiorSequence.Triggers.Completed);
    protected virtual void BehaviorDestroyed(Behavior behavior) => BehaviorTrigger(behavior, BehvaiorSequence.Triggers.Destroyed);
    private void BehaviorTrigger(Behavior behavior, BehvaiorSequence.Triggers trigger)
    {
        foreach (var BehaviorSetTriggersToActions in BehvaiorSetTriggerActions)
        {
            Type behaviorType = behavior.GetType();
            if (BehaviorSetTriggersToActions.Key == behaviorType)
            {
                var triggersToActions = BehaviorSetTriggersToActions.Value;
                if (triggersToActions.ContainsKey(trigger))
                {
                    HashSet<Action> actions = triggersToActions[trigger];
                    foreach (Action action in actions)
                    {
                        action.Invoke();
                    }
                }
                return;
            }
        }
    }
    protected override void Destroy()
    {
        if (Behaviors is not null)
        {
            foreach (Behavior behavior in Behaviors)
            {
                behavior.EngineDestroy();
            }
            Behaviors.Clear();
        }
        base.Destroy();
    }
    internal override void EngineUpdate()
    {
        if (Behaviors is not null)
        {
            Behaviors.UpdateFront();
            Update();
            Behaviors.UpdateBack();
        }
    }
    internal void FluentMap(Type whenBehaviorType, BehvaiorSequence.Triggers trigger, Action action)
    {
        if (action is not null)
        {
            if (BehvaiorSetTriggerActions.ContainsKey(whenBehaviorType))
            {
                Dictionary<BehvaiorSequence.Triggers, HashSet<Action>> triggersToActions = BehvaiorSetTriggerActions[whenBehaviorType];
                if (triggersToActions.ContainsKey(trigger))
                {
                    HashSet<Action> actions = triggersToActions[trigger];
                    actions.Add(action);
                    triggersToActions[trigger] = actions;
                }
                else
                {
                    HashSet<Action> actions = new()
                    {
                        action,
                    };
                    triggersToActions.Add(trigger, actions);
                }
                BehvaiorSetTriggerActions[whenBehaviorType] = triggersToActions;
            }
            else
            {
                Dictionary<BehvaiorSequence.Triggers, HashSet<Action>> triggersToActions = new()
                {
                    {
                        trigger,
                        new HashSet<Action>
                        {
                            action,
                        }
                    },
                };
                BehvaiorSetTriggerActions.Add(whenBehaviorType, triggersToActions);
            }
        }
    }
    internal void FluentMap(Type sequenceBehaviorType, Guid guid, BehvaiorSequence.Triggers trigger, Type startBehaviorType)
    {
        if (sequenceBehaviorType is null)
        {
            throw new ArgumentNullException(nameof(sequenceBehaviorType));
        }
        if (startBehaviorType is null)
        {
            throw new ArgumentNullException(nameof(startBehaviorType));
        }
        if (BehvaiorSetSequences.ContainsKey(sequenceBehaviorType))
        {
            Dictionary<Guid, BehvaiorSequence> sequences = BehvaiorSetSequences[sequenceBehaviorType];
            if (sequences.ContainsKey(guid))
            {
                BehvaiorSequence sequence = sequences[guid];
                sequence.Starts.Add(startBehaviorType);
                sequences[guid] = sequence;
            }
            else
            {
                sequences.Add(guid, new BehvaiorSequence
                {
                    Trigger = trigger,
                    Starts = new HashSet<Type>
                        {
                            startBehaviorType,
                        },
                });
            }
            BehvaiorSetSequences[sequenceBehaviorType] = sequences;
        }
        else
        {
            Dictionary<Guid, BehvaiorSequence> sequences = new()
            {
                {
                    guid,
                    new BehvaiorSequence
                    {
                        Trigger = trigger,
                        Starts = new HashSet<Type>
                            {
                                startBehaviorType,
                            }
                    }
                },
            };
            BehvaiorSetSequences.Add(sequenceBehaviorType, sequences);
        }
    }
    private Behavior Start(Type behvaiorType)
    {
        if (behvaiorType is null)
        {
            throw new ArgumentNullException(nameof(behvaiorType));
        }
        Behavior behavior;
        try
        {
            behavior = (Behavior)Activator.CreateInstance(behvaiorType);
        }
        catch (Exception exception)
        {
            throw new Exception($"Failed to create an instance of the behavior type {behvaiorType.Name}", exception);
        }
        behavior.OnCreate += () =>
        {
            BehaviorCreated(behavior);
            StartBehaviorTrigger(behavior, BehvaiorSequence.Triggers.Created);
        };
        behavior.OnDestroy += () =>
        {
            BehaviorDestroyed(behavior);
            EndBehvaior(behavior);
            StartBehaviorTrigger(behavior, BehvaiorSequence.Triggers.Destroyed);
        };
        behavior.OnComplete += () =>
        {
            BehaviorComplete(behavior);
            EndBehvaior(behavior);
            StartBehaviorTrigger(behavior, BehvaiorSequence.Triggers.Completed);
        };
        behavior.Source = Parent;
        RunningBehaviors.Add(behavior);
        Behaviors.EnqueueCreate(behavior);
        return behavior;
    }
    private void Complete(Type behvaiorType)
    {
        if (behvaiorType is null)
        {
            throw new ArgumentNullException(nameof(behvaiorType));
        }
        foreach (Behavior behavior in RunningBehaviors)
        {
            if (Reflecting.IsOrDerivedFrom(behavior?.GetType(), behvaiorType))
            {
                behavior.Complete();
            }
        }
    }
    private void Stop(Type behvaiorType)
    {
        if (behvaiorType is null)
        {
            throw new ArgumentNullException(nameof(behvaiorType));
        }
        foreach (Behavior behavior in RunningBehaviors)
        {
            if (Reflecting.IsOrDerivedFrom(behavior?.GetType(), behvaiorType))
            {
                EndBehvaior(behavior);
            }
        }
    }
    private void StartBehaviorTrigger(Behavior behavior, BehvaiorSequence.Triggers trigger)
    {
        if (behavior is null)
        {
            throw new ArgumentNullException(nameof(behavior));
        }
        Type behvaiorType = behavior.GetType();
        foreach (KeyValuePair<Type, Dictionary<Guid, BehvaiorSequence>> set in BehvaiorSetSequences)
        {
            if (set.Key == behvaiorType)
            {
                foreach (KeyValuePair<Guid, BehvaiorSequence> guidSequence in set.Value)
                {
                    if (guidSequence.Value.Trigger == trigger)
                    {
                        foreach (Type type in guidSequence.Value.Starts)
                        {
                            Start(type);
                        }
                    }
                }
            }
        }
    }
    private void EndBehvaior(Behavior behavior)
    {
        Behaviors.DequeueDestroy(behavior);
        if (RunningBehaviors.Contains(behavior))
        {
            RunningBehaviors.Remove(behavior);
        }
    }
    private GameSequencer<Behavior> Behaviors { get; }
    private Dictionary<Type, Dictionary<Guid, BehvaiorSequence>> BehvaiorSetSequences { get; }
    private Dictionary<Type, Dictionary<BehvaiorSequence.Triggers, HashSet<Action>>> BehvaiorSetTriggerActions { get; }
    private HashSet<Behavior> RunningBehaviors { get; }
    public struct BehvaiorSequence
    {
        public enum Triggers
        {
            None,
            Created,
            Destroyed,
            Completed,
        }
        public Triggers Trigger { get; init; }
        public HashSet<Type> Starts { get; init; }
    }
}
