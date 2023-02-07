#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.VisualScripting
{
    public abstract class GameplayTagSystemUnit : Unit
    {
        [DoNotSerialize]
        [NullMeansSelf]
        [PortLabel("Target")]
        [PortLabelHidden]
        public ValueInput GameplayTagSystemComponent;

        protected override void Definition()
        {
            GameplayTagSystemComponent = ValueInput<GameplayTagSystemComponent>(nameof(GameplayTagSystemComponent), null).NullMeansSelf();
        }
    }
}

#endif