

namespace StudioScor.GameplayTagSystem
{
    #region WIth Visual Scripting
#if SCOR_ENABLE_VISUALSCRIPTING
    [Unity.VisualScripting.Inspectable, Unity.VisualScripting.IncludeInSettings(true)]
#endif
    #endregion
    [System.Serializable]
    public struct FGameplayTags
    {
        #region WIth Visual Scripting
#if SCOR_ENABLE_VISUALSCRIPTING
        [Unity.VisualScripting.Inspectable]
#endif
        #endregion
        public GameplayTag[] Owneds;

        #region WIth Visual Scripting
#if SCOR_ENABLE_VISUALSCRIPTING
        [Unity.VisualScripting.Inspectable]
#endif
        #endregion
        public GameplayTag[] Blocks;
    }
}
