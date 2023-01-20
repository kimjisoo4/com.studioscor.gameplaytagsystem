#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.VisualScripting
{
    [UnitTitle("Remove Owned GameplayTags")]
    [UnitSubtitle("GameplayTagSystemComponent")]
    [UnitCategory("StudioScor\\GameplayTagSystem")]
    public class GameplayTagSystemRemoveOwnedTagsUnit : GameplayTagSystemMultiTagUnit
    {
        protected override ControlOutput OnFlow(Flow flow)
        {
            var gameplayTagSystemComponent = flow.GetValue<GameplayTagSystemComponent>(GameplayTagSystemComponent);
            var gameplayTags = flow.GetValue<GameplayTag[]>(GameplayTags);

            gameplayTagSystemComponent.RemoveOwnedTags(gameplayTags);

            return Exit;
        }
    }
}

#endif