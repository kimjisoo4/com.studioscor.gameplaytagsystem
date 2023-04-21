#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.VisualScripting
{
    [UnitTitle("Trigger GameplayTag")]
    [UnitSubtitle("GameplayTagSystem Unit")]
    [UnitCategory("StudioScor\\GameplayTagSystem")]
    public class GameplayTagSystemTriggerTagUnit : GameplayTagSystemFlowUnit
    {
        [DoNotSerialize]
        [PortLabel("GameplayTag")]
        public ValueInput GameplayTag { get; private set; }

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
            
            if(_UseList)
            {
                var triggerTags = flow.GetValue<GameplayTag[]>(GameplayTag);

                gameplayTagSystem.TriggerTags(triggerTags);
            }
            else
            {
                var triggerTag = flow.GetValue<GameplayTag>(GameplayTag);

                gameplayTagSystem.TriggerTag(triggerTag);
            }

            return Exit;
        }
    }
}

#endif