
#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.VisualScripting.Editor
{
    [Descriptor(typeof(GameplayTagSystemTriggerTagEvent))]
    public sealed class GameplayTagSystemTriggerTagEventDescriptor : UnitDescriptor<GameplayTagSystemTriggerTagEvent>
    {
        public GameplayTagSystemTriggerTagEventDescriptor(GameplayTagSystemTriggerTagEvent target) : base(target)
        {
        }
        
        protected override EditorTexture DefaultIcon()
        {
            return GameplayTagSystemPathUtility.Load("T_TriggerTag_D");
        }
        protected override EditorTexture DefinedIcon()
        {
            return GameplayTagSystemPathUtility.Load("T_TriggerTag_D");
        }
    }
}

#endif