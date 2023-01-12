#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem
{
    [UnitTitle("OnAddOwnedTagEvent")]
    [UnitCategory("Events\\StudioScor\\GameplayTagSystem")]
    public class GameplayTagSystemAddOwnedTagEvent : GameplayTagSystemCustomUnitEvent
    {
        protected override string EventName => GameplayTagSystemVisualScriptingEvent.ADD_OWNED_TAG;
    }
}

#endif