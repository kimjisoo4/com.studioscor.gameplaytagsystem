
#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.VisualScripting.Editor
{
    [Descriptor(typeof(GameplayTagSystemRemoveBlockTagEvent))]
    public sealed class GameplayTagSystemRemoveBlockTagEventDescriptor : UnitDescriptor<GameplayTagSystemRemoveBlockTagEvent>
    {
        public GameplayTagSystemRemoveBlockTagEventDescriptor(GameplayTagSystemRemoveBlockTagEvent target) : base(target)
        {
        }

        protected override EditorTexture DefaultIcon()
        {
            return GameplayTagSystemPathUtilityWithVisualScripting.Load("T_Icon_GameplayTag_Event_Subtract_D");
        }
        protected override EditorTexture DefinedIcon()
        {
            return GameplayTagSystemPathUtilityWithVisualScripting.Load("T_Icon_GameplayTag_Event_Subtract_D");
        }
    }
}

#endif