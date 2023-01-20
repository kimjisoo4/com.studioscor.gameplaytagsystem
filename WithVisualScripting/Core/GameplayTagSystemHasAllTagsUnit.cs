#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.VisualScripting
{

    public abstract class GameplayTagSystemHasAllTagsUnit : Unit
    {
        [DoNotSerialize]
        [NullMeansSelf]
        [PortLabel("Target")]
        [PortLabelHidden]
        public ValueInput Target;

        [PortLabel("Tags")]
        [DoNotSerialize]
        public ValueInput Tags;

        [DoNotSerialize]
        [PortLabel("IsContain")]
        [PortLabelHidden]
        public ValueOutput IsContain;

        protected override void Definition()
        {
            Target = ValueInput<GameplayTagSystemComponent>(nameof(Target), null).NullMeansSelf();
            Tags = ValueInput<GameplayTag[]>(nameof(Tags), null);

            IsContain = ValueOutput<bool>(nameof(IsContain), CheckContainTag);
        }

        private bool CheckContainTag(Flow flow)
        {
            var gameplayTagComponent = flow.GetValue<GameplayTagSystemComponent>(Target);
            var tags = flow.GetValue<GameplayTag[]>(Tags);

            return ContainTag(gameplayTagComponent, tags);
        }

        protected abstract bool ContainTag(GameplayTagSystemComponent gameplayTagSystemComponent, GameplayTag[] gameplayTags);
    }
}

#endif