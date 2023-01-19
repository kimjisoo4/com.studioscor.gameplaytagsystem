
#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.VisualScripting.Editor
{
    [Descriptor(typeof(GameplayTagSystemGrantBlockTagEvent))]
    public sealed class GameplayTagSystemAddNewBlockTagEventDescriptor : UnitDescriptor<GameplayTagSystemGrantBlockTagEvent>
    {
        public GameplayTagSystemAddNewBlockTagEventDescriptor(GameplayTagSystemGrantBlockTagEvent target) : base(target)
        {
        }

        protected override EditorTexture DefaultIcon()
        {
            return GameplayTagSystemPathUtility.Load("T_AddNewBlockTag_D");
        }
        protected override EditorTexture DefinedIcon()
        {
            return GameplayTagSystemPathUtility.Load("T_AddNewBlockTag_D");
        }
    }
}

#endif