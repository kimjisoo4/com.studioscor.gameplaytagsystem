#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.Extend.VisualScripting
{
    [UnitTitle("Remove GameplayTag")]
    [UnitSubtitle("GameplayTagSystem Unit")]
    [UnitCategory("StudioScor\\GameplayTagSystem")]
    public class GameplayTagSystemRemoveTagUnit : GameplayTagSystemFlowUnit
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
                var removeTags = flow.GetValue<GameplayTag[]>(GameplayTag);

                switch (ContainerType)
                {
                    case EContainerType.Owned:
                        gameplayTagSystem.RemoveOwnedTags(removeTags);
                        break;
                    case EContainerType.Block:
                        gameplayTagSystem.RemoveBlockTags(removeTags);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                var removeTag = flow.GetValue<GameplayTag>(GameplayTag);

                switch (ContainerType)
                {
                    case EContainerType.Owned:
                        gameplayTagSystem.RemoveOwnedTag(removeTag);
                        break;
                    case EContainerType.Block:
                        gameplayTagSystem.RemoveBlockTag(removeTag);
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