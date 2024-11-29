#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.Extend.VisualScripting
{
    [UnitTitle("Check Contain Tag")]
    [UnitSubtitle("GameplayTagSystem Condition")]
    [UnitCategory("StudioScor\\GameplayTagSystem")]
    public class CheckContainTagUnit : GameplayTagSystemUnit
    {
        [Serialize]
        [Inspectable]
        [UnitHeaderInspectable]
        [PortLabel("Container Type")]
        public EContainerType ContainerType { get; private set; }

        [Serialize]
        [Inspectable]
        [UnitHeaderInspectable]
        [PortLabel("Contain Type")]
        public EContainType ContainType { get; private set; }

        [DoNotSerialize]
        [PortLabel("GameplayTag")]
        [PortLabelHidden]
        public ValueInput GameplayTag { get; private set; }

        [DoNotSerialize]
        [PortLabel("IsContain")]
        [PortLabelHidden]
        public ValueOutput IsContaine;


        protected override void Definition()
        {
            base.Definition();

            GameplayTag = ValueInput<GameplayTag>(nameof(GameplayTag), null);


            IsContaine = ValueOutput<bool>(nameof(IsContaine), CheckGameplayTags);

            Requirement(Target, IsContaine);
            Requirement(GameplayTag, IsContaine);
        }

        private bool CheckGameplayTags(Flow flow)
        {
            var gameplayTagSystem = flow.GetValue<IGameplayTagSystem>(Target);

            var tag = flow.GetValue<GameplayTag>(GameplayTag);

            if (!tag)
                return false;

            switch (ContainerType)
            {
                case EContainerType.Owned:
                    return gameplayTagSystem.ContainOwnedTag(tag);
                case EContainerType.Block:
                    return gameplayTagSystem.ContainBlockTag(tag);
                default:
                    return false;
            }
        }
    }
}
#endif