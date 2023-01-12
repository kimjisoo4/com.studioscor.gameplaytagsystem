
#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem
{
    [Descriptor(typeof(GameplayTagSystemTriggerTagEvent))]
    public sealed class GameplayTagSystemTriggerTagEventDescriptor : UnitDescriptor<GameplayTagSystemTriggerTagEvent>
    {
        public GameplayTagSystemTriggerTagEventDescriptor(GameplayTagSystemTriggerTagEvent target) : base(target)
        {
        }
        
        protected override EditorTexture DefaultIcon()
        {
            return PathUtility.Load("T_TriggerTag_D");
        }
        protected override EditorTexture DefinedIcon()
        {
            return PathUtility.Load("T_TriggerTag_D");
        }
    }
}

#endif