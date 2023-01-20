#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.VisualScripting
{
    [UnitTitle("Trigger GameplayTags")]
    [UnitSubtitle("GameplayTagSystemComponent")]
    [UnitCategory("StudioScor\\GameplayTagSystem")]
    public class GameplayTagSystemTriggerGameplayTagsUnit : GameplayTagSystemMultiTagUnit
    {
        protected override ControlOutput OnFlow(Flow flow)
        {
            var gameplayTagSystemComponent = flow.GetValue<GameplayTagSystemComponent>(GameplayTagSystemComponent);
            var gameplayTag = flow.GetValue<GameplayTag[]>(GameplayTags);

            gameplayTagSystemComponent.TriggerTags(gameplayTag);

            return Exit;
        }
    }
}

#endif