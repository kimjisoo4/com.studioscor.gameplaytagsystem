#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.Extend.VisualScripting
{
    [UnitTitle("Grant GameplayTag")]
    [UnitSubtitle("GameplayTagSystem Unit")]
    [UnitCategory("StudioScor\\GameplayTagSystem")]
    public class GameplayTagSystemGrantTagUnit : GameplayTagSystemFlowUnit
    {
        [DoNotSerialize]
        [PortLabel("GameplayTag")]
        [PortLabelHidden]
        public ValueInput GameplayTag { get; private set; }

        [Serialize]
        [Inspectable]
        [UnitHeaderInspectable]
        [PortLabel("Container Type")]
        public EContainerType ContainerType { get; private set; } = EContainerType.Owned;

        [Serialize]
        [Inspectable]
        [UnitHeaderInspectable]
        [PortLabel("Structure Type")]
        public EStructureType StructureType { get; set; } = EStructureType.Target;

        private bool _UseList;

        protected override void Definition()
        {
            base.Definition();

            _UseList = StructureType.Equals(EStructureType.List);

            if (_UseList)
                GameplayTag = ValueInput<GameplayTag[]>(nameof(GameplayTag), null);
            else
                GameplayTag = ValueInput<GameplayTag>(nameof(GameplayTag), null);

            Requirement(GameplayTag, Enter);
        }

        protected override ControlOutput OnFlow(Flow flow)
        {
            var gameplayTagSystem = flow.GetValue<IGameplayTagSystem>(Target);

            if (_UseList)
            {
                var grantTags = flow.GetValue<GameplayTag[]>(GameplayTag);

                switch (ContainerType)
                {
                    case EContainerType.Owned:
                        gameplayTagSystem.AddOwnedTags(grantTags);
                        break;
                    case EContainerType.Block:
                        gameplayTagSystem.AddBlockTags(grantTags);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                var grantTag = flow.GetValue<GameplayTag>(GameplayTag);

                switch (ContainerType)
                {
                    case EContainerType.Owned:
                        gameplayTagSystem.AddOwnedTag(grantTag);
                        break;
                    case EContainerType.Block:
                        gameplayTagSystem.AddBlockTag(grantTag);
                        break;
                    default:
                        break;
                }
            }

            return Exit;
        }
    }
}

#endif