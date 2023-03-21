#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.VisualScripting
{
    [UnitTitle("Trigger GameplayTag")]
    [UnitShortTitle("TriggerTag")]
    [UnitCategory("StudioScor\\GameplayTagSystem")]
    public class GameplayTagSystemTriggerTagUnit : GameplayTagSystemFlowUnit
    {
        [DoNotSerialize]
        [PortLabel("GameplayTag")]
        public ValueInput GameplayTag { get; private set; }

        [Serialize]
        [Inspectable]
        [InspectorToggleLeft]
        public bool UseList { get; set; } = false;

        protected override void Definition()
        {
            base.Definition();

            if(UseList)
                GameplayTag = ValueInput<GameplayTag[]>(nameof(GameplayTag), null);
            else
                GameplayTag = ValueInput<GameplayTag>(nameof(GameplayTag), null);

            Requirement(GameplayTag, Enter);
        }

        protected override ControlOutput OnFlow(Flow flow)
        {
            var gameplayTagSystem = flow.GetValue<IGameplayTagSystem>(Target);
            
            if(UseList)
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