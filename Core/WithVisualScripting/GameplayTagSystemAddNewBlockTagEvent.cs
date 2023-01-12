#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem
{
    [UnitTitle("OnAddNewBlockTagEvent")]
    [UnitCategory("Events\\StudioScor\\GameplayTagSystem")]
    public class GameplayTagSystemAddNewBlockTagEvent : GameplayTagSystemCustomUnitEvent
    {
        protected override string EventName => GameplayTagSystemVisualScriptingEvent.ADD_NEW_BLOCK_TAG;
    }
}

#endif