
#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.Extend.VisualScripting.Editor
{
    [Descriptor(typeof(GameplayTagSystemGrantBlockTagEvent))]
    public sealed class GameplayTagSystemAddNewBlockTagEventDescriptor : UnitDescriptor<GameplayTagSystemGrantBlockTagEvent>
    {
        public GameplayTagSystemAddNewBlockTagEventDescriptor(GameplayTagSystemGrantBlockTagEvent target) : base(target)
        {
        }

        protected override EditorTexture DefaultIcon()
        {
            return GameplayTagSystemPathUtilityWithVisualScripting.Load("T_Icon_GameplayTag_Event_Add_D");
        }
        protected override EditorTexture DefinedIcon()
        {
            return GameplayTagSystemPathUtilityWithVisualScripting.Load("T_Icon_GameplayTag_Event_Add_D");
        }
    }
}

#endif