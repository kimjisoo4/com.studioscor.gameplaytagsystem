#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.Extend.VisualScripting
{
    [UnitTitle("On Added OwnedTag")]
    [UnitSubtitle("Event")]
    [UnitCategory("Events\\StudioScor\\GameplayTagSystem")]
    public class GameplayTagSystemAddOwnedTagEvent : GameplayTagSystemEventUnit
    {
        protected override string HookName => GameplayTagSystemWithVisualScripting.ADD_OWNED_TAG;
    }
}

#endif