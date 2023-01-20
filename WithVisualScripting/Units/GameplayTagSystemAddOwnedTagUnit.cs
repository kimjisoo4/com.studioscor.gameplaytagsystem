#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.VisualScripting
{
    [UnitTitle("Add Owned GameplayTag")]
    [UnitSubtitle("GameplayTagSystemComponent")]
    [UnitCategory("StudioScor\\GameplayTagSystem")]
    public class GameplayTagSystemAddOwnedTagUnit : GameplayTagSystemSingleTagUnit
    {
        protected override ControlOutput OnFlow(Flow flow)
        {
            var gameplayTagSystemComponent = flow.GetValue<GameplayTagSystemComponent>(GameplayTagSystemComponent);
            var gameplayTag = flow.GetValue<GameplayTag>(GameplayTag);

            gameplayTagSystemComponent.AddOwnedTag(gameplayTag);

            return Exit;
        }
    }
}

#endif