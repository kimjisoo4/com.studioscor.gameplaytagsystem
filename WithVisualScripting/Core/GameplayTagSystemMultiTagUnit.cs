#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.VisualScripting
{
    public abstract class GameplayTagSystemMultiTagUnit : GameplayTagSystemFlowUnit
    {
        [DoNotSerialize]
        [PortLabel("GameplayTag")]
        [PortLabelHidden]
        public ValueInput GameplayTags;

        protected override void Definition()
        {
            base.Definition();

            GameplayTags = ValueInput<GameplayTag[]>(nameof(GameplayTags), null);

            Requirement(GameplayTags, Enter);
        }
    }
}

#endif