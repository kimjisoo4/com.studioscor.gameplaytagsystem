#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.Extend.VisualScripting
{
    [UnitTitle("On Removed OwnedTag")]
    [UnitSubtitle("Event")]
    [UnitCategory("Events\\StudioScor\\GameplayTagSystem")]
    public class GameplayTagSystemRemoveOwnedTagEvent : GameplayTagSystemEventUnit
    {
        protected override string HookName => GameplayTagSystemWithVisualScripting.REMOVE_OWNED_TAG;
    }
}

#endif