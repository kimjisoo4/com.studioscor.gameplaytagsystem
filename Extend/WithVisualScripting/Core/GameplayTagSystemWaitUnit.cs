#if SCOR_ENABLE_VISUALSCRIPTING
using System.Collections;
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.VisualScripting
{
    public abstract class GameplayTagSystemWaitUnit : Unit
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
            Enter = ControlInputCoroutine(nameof(Enter), Await);
            Exit = ControlOutput(nameof(Exit));

            GameplayTagSystemComponent = ValueInput<GameplayTagSystemComponent>(nameof(GameplayTagSystemComponent), null).NullMeansSelf();

            Succession(Enter, Exit);
            Requirement(GameplayTagSystemComponent, Enter);
        }

        protected abstract IEnumerator Await(Flow arg);
    }
}

#endif