
#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.Extend.VisualScripting.Editor
{
    [Descriptor(typeof(GameplayTagSystemSubtractOwnedTagEvent))]
    public sealed class GameplayTagSystemSubtractOwnedTagEventDescriptor : UnitDescriptor<GameplayTagSystemSubtractOwnedTagEvent>
    {
        public GameplayTagSystemSubtractOwnedTagEventDescriptor(GameplayTagSystemSubtractOwnedTagEvent target) : base(target)
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