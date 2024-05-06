namespace LootBoot.Epilogue.Engine.FluentBuilders;
public abstract class BehaviorSequenceFluentBuilder
{
    public BehaviorSequenceFluentBuilder(Controller controller, Type key) : this(controller, key, Controller.BehvaiorSequence.Triggers.None, Guid.NewGuid()) { }
    public BehaviorSequenceFluentBuilder(Controller controller, Type key, Controller.BehvaiorSequence.Triggers trigger, Guid guid)
    {
        Controller = controller;
        Key = key;
        Guid = guid;
        Trigger = trigger;
    }
    protected TStep Map<TStep, TBehavior>() where TBehavior : Behavior, new() where TStep : BehaviorSequenceFluentBuilder
    {
        Controller.FluentMap(Key, Guid, Trigger, typeof(TBehavior));
        TStep step = (TStep)Activator.CreateInstance(typeof(TStep), new object[] { Controller, Key, Trigger, Guid });
        return step;
    }
    private Controller Controller { get; }
    private Type Key { get; }
    private Guid Guid { get; }
    private Controller.BehvaiorSequence.Triggers Trigger { get; }
    public class TriggerStep : BehaviorSequenceFluentBuilder
    {
        public TriggerStep(Controller controller, Type key) : base(controller, key) { }
        public StartStep Create() => new StartStep(Controller, Key, Controller.BehvaiorSequence.Triggers.Created, Guid);
        public StartStep Destroy() => new StartStep(Controller, Key, Controller.BehvaiorSequence.Triggers.Destroyed, Guid);
        public StartStep Complete() => new StartStep(Controller, Key, Controller.BehvaiorSequence.Triggers.Completed, Guid);
    }
    public class StartStep : BehaviorSequenceFluentBuilder
    {
        public StartStep(Controller controller, Type key, Controller.BehvaiorSequence.Triggers trigger, Guid guid) : base(controller, key, trigger, guid) { }
        public AndStep Start<TBehavior>() where TBehavior : Behavior, new() => Map<AndStep, TBehavior>();
    }
    public class AndStep : BehaviorSequenceFluentBuilder
    {
        public AndStep(Controller controller, Type key, Controller.BehvaiorSequence.Triggers trigger, Guid guid) : base(controller, key, trigger, guid) { }
        public AndStep And<TBehavior>() where TBehavior : Behavior, new() => Map<AndStep, TBehavior>();
    }
}
