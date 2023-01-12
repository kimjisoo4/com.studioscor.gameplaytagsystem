#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem
{
    [UnitTitle("OnRemoveOwnedTagEvent")]
    [UnitCategory("Events\\StudioScor\\GameplayTagSystem")]
    public class GameplayTagSystemRemoveOwnedTagEvent : GameplayTagSystemCustomUnitEvent
    {
        protected override string EventName => GameplayTagSystemVisualScriptingEvent.REMOVE_OWNED_TAG;
    }
}

#endif