
#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.Extend.VisualScripting.Editor
{
    [Descriptor(typeof(GameplayTagSystemUnit))]
    public sealed class GameplayTagSystemUnitDescriptor : UnitDescriptor<GameplayTagSystemUnit>
    {
        public GameplayTagSystemUnitDescriptor(GameplayTagSystemUnit target) : base(target)
        {
        }

        protected override EditorTexture DefaultIcon()
        {
            return GameplayTagSystemPathUtilityWithVisualScripting.Load("T_Icon_GameplayTagSystem_D");
        }
        protected override EditorTexture DefinedIcon()
        {
            return GameplayTagSystemPathUtilityWithVisualScripting.Load("T_Icon_GameplayTagSystem_D");
        }
    }
}

#endif