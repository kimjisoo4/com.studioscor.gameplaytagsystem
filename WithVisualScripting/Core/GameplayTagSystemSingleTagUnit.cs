#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.VisualScripting
{
    public abstract class GameplayTagSystemSingleTagUnit : GameplayTagSystemFlowUnit
    {
        [DoNotSerialize]
        [PortLabel("GameplayTags")]
        [PortLabelHidden]
        public ValueInput GameplayTag;

        protected override void Definition()
        {
            base.Definition();

            GameplayTag = ValueInput<GameplayTag>(nameof(GameplayTag), null);

            Requirement(GameplayTag, Enter);
        }
    }
}

#endif