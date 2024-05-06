namespace LootBoot.Epilogue.Engine.FluentBuilders;
public abstract class BehaviorWhenFluentBuilder
{
    public BehaviorWhenFluentBuilder(Controller controller, Type key) : this(controller, key, Controller.BehvaiorSequence.Triggers.None) { }
    public BehaviorWhenFluentBuilder(Controller controller, Type key, Controller.BehvaiorSequence.Triggers trigger)
    {
        Controller = controller;
        Key = key;
        Trigger = trigger;
    }
    protected TStep Map<TStep>(Action action) where TStep : BehaviorWhenFluentBuilder
    {
        Controller.FluentMap(Key, Trigger, action);
        TStep step = (TStep)Activator.CreateInstance(typeof(TStep), new object[] { Controller, Key, Trigger });
        return step;
    }
    private Controller Controller { get; }
    private Type Key { get; }
    private Controller.BehvaiorSequence.Triggers Trigger { get; }
    public class TriggerStep : BehaviorWhenFluentBuilder
    {
        public TriggerStep(Controller controller, Type key) : base(controller, key) { }
        public RunStep IsCreated() => new RunStep(Controller, Key, Controller.BehvaiorSequence.Triggers.Created);
        public RunStep IsDestroyed() => new RunStep(Controller, Key, Controller.BehvaiorSequence.Triggers.Destroyed);
        public RunStep IsCompleted() => new RunStep(Controller, Key, Controller.BehvaiorSequence.Triggers.Completed);
    }
    public class RunStep : BehaviorWhenFluentBuilder
    {
        public RunStep(Controller controller, Type key, Controller.BehvaiorSequence.Triggers trigger) : base(controller, key, trigger) { }
        public AndStep Run(Action action) => Map<AndStep>(action);
    }
    public class AndStep : BehaviorWhenFluentBuilder
    {
        public AndStep(Controller controller, Type key, Controller.BehvaiorSequence.Triggers trigger) : base(controller, key, trigger) { }
        public AndStep And(Action action) => Map<AndStep>(action);
    }
}
