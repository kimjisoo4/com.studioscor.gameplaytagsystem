
#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.Extend.VisualScripting.Editor
{
    [Descriptor(typeof(GameplayTagSystemSubtractBlockTagEvent))]
    public sealed class GameplayTagSystemSubtractBlockTagEventDescriptor : UnitDescriptor<GameplayTagSystemSubtractBlockTagEvent>
    {
        public GameplayTagSystemSubtractBlockTagEventDescriptor(GameplayTagSystemSubtractBlockTagEvent target) : base(target)
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