
#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem
{
    [Descriptor(typeof(GameplayTagSystemAddNewBlockTagEvent))]
    public sealed class GameplayTagSystemAddNewBlockTagEventDescriptor : UnitDescriptor<GameplayTagSystemAddNewBlockTagEvent>
    {
        public GameplayTagSystemAddNewBlockTagEventDescriptor(GameplayTagSystemAddNewBlockTagEvent target) : base(target)
        {
        }

        protected override EditorTexture DefaultIcon()
        {
            return PathUtility.Load("T_AddNewBlockTag_D");
        }
        protected override EditorTexture DefinedIcon()
        {
            return PathUtility.Load("T_AddNewBlockTag_D");
        }
    }
}

#endif