
#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.VisualScripting.Editor
{
    [Descriptor(typeof(GameplayTagSystemAddBlockTagEvent))]
    public sealed class GameplayTagSystemAddBlockTagEventDescriptor : UnitDescriptor<GameplayTagSystemAddBlockTagEvent>
    {
        public GameplayTagSystemAddBlockTagEventDescriptor(GameplayTagSystemAddBlockTagEvent target) : base(target)
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