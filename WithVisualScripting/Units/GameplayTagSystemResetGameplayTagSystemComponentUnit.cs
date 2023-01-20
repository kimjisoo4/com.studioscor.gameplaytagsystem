#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.VisualScripting
{
    [UnitTitle("Reset GameplayTagSystem Component")]
    [UnitSubtitle("GameplayTagSystemComponent")]
    [UnitCategory("StudioScor\\GameplayTagSystem")]
    public class GameplayTagSystemResetGameplayTagSystemComponentUnit : GameplayTagSystemFlowUnit
    {

        protected override ControlOutput OnFlow(Flow flow)
        {
            var gameplayTagSystemComponent = flow.GetValue<GameplayTagSystemComponent>(GameplayTagSystemComponent);
            
            gameplayTagSystemComponent.ResetGameplayTagSystem();

            return Exit;
        }
    }
}

#endif