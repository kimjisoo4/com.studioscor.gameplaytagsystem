namespace KimScor.GameplayTagSystem
{
    [System.Serializable]
    public struct FCheckTags
    {
        public GameplayTag[] Require;
        public GameplayTag[] Ignore;
    }
}
