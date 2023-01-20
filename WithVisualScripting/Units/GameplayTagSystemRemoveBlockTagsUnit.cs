#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.VisualScripting
{
    [UnitTitle("Remove Block GameplayTags")]
    [UnitSubtitle("GameplayTagSystemComponent")]
    [UnitCategory("StudioScor\\GameplayTagSystem")]
    public class GameplayTagSystemRemoveBlockTagsUnit : GameplayTagSystemMultiTagUnit
    {
        protected override ControlOutput OnFlow(Flow flow)
        {
            var gameplayTagSystemComponent = flow.GetValue<GameplayTagSystemComponent>(GameplayTagSystemComponent);
            var gameplayTags = flow.GetValue<GameplayTag[]>(GameplayTags);

            gameplayTagSystemComponent.RemoveBlockTags(gameplayTags);

            return Exit;
        }
    }
}

#endif