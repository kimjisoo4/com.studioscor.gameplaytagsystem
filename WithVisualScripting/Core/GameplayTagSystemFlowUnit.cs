#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.VisualScripting
{
    public abstract class GameplayTagSystemFlowUnit : Unit
    {
        [DoNotSerialize]
        [PortLabel("Enter")]
        [PortLabelHidden]
        public ControlInput Enter;

        [DoNotSerialize]
        [PortLabel("Exit")]
        [PortLabelHidden]
        public ControlOutput Exit;

        [DoNotSerialize]
        [NullMeansSelf]
        [PortLabel("Target")]
        [PortLabelHidden]
        public ValueInput GameplayTagSystemComponent;

        protected override void Definition()
        {
            Enter = ControlInput(nameof(Enter), OnFlow);

            Exit = ControlOutput(nameof(Exit));

            GameplayTagSystemComponent = ValueInput<GameplayTagSystemComponent>(nameof(GameplayTagSystemComponent), null).NullMeansSelf();

            Succession(Enter, Exit);
            Requirement(GameplayTagSystemComponent, Enter);
        }

        protected abstract ControlOutput OnFlow(Flow arg);
    }
}

#endif