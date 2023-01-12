
#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem
{
    [Descriptor(typeof(GameplayTagSystemRemoveBlockTagEvent))]
    public sealed class GameplayTagSystemRemoveBlockTagEventDescriptor : UnitDescriptor<GameplayTagSystemRemoveBlockTagEvent>
    {
        public GameplayTagSystemRemoveBlockTagEventDescriptor(GameplayTagSystemRemoveBlockTagEvent target) : base(target)
        {
        }

        protected override EditorTexture DefaultIcon()
        {
            return PathUtility.Load("T_RemoveBlockTag_D");
        }
        protected override EditorTexture DefinedIcon()
        {
            return PathUtility.Load("T_RemoveBlockTag_D");
        }
    }
}

#endif