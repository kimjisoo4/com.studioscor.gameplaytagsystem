#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.VisualScripting
{

    [UnitTitle("Trigger GameplayTag")]
    [UnitSubtitle("GameplayTagSystemComponent")]
    [UnitCategory("StudioScor\\GameplayTagSystem")]
    public class GameplayTagSystemTriggerGameplayTagUnit : GameplayTagSystemSingleTagUnit
    {
        protected override ControlOutput OnFlow(Flow flow)
        {
            var gameplayTagSystemComponent = flow.GetValue<GameplayTagSystemComponent>(GameplayTagSystemComponent);
            var gameplayTag = flow.GetValue<GameplayTag>(GameplayTag);

            gameplayTagSystemComponent.TriggerTag(gameplayTag);

            return Exit;
        }
    }
}

#endif