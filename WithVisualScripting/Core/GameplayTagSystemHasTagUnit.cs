#if SCOR_ENABLE_VISUALSCRIPTING
using System;
using System.Collections;
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.VisualScripting
{

    public abstract class GameplayTagSystemHasTagUnit : Unit
    {
        [DoNotSerialize]
        [NullMeansSelf]
        [PortLabel("Target")]
        [PortLabelHidden]
        public ValueInput Target;

        [PortLabel("Tags")]
        [DoNotSerialize]
        public ValueInput Tag;

        [DoNotSerialize]
        [PortLabel("IsContain")]
        [PortLabelHidden]
        public ValueOutput IsContain;

        protected override void Definition()
        {
            Target = ValueInput<GameplayTagSystemComponent>(nameof(Target), null).NullMeansSelf();
            Tag = ValueInput<GameplayTag>(nameof(Tag), null);

            IsContain = ValueOutput<bool>(nameof(IsContain), CheckContainTag);

            Requirement(Target, IsContain);
            Requirement(Tag, IsContain);
        }

        private bool CheckContainTag(Flow flow)
        {
            var gameplayTagComponent = flow.GetValue<GameplayTagSystemComponent>(Target);
            var tags = flow.GetValue<GameplayTag>(Tag);

            return ContainTag(gameplayTagComponent, tags);
        }

        protected abstract bool ContainTag(GameplayTagSystemComponent gameplayTagComponent, GameplayTag gameplayTag);
    }
}

#endif