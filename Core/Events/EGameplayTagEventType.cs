namespace StudioScor.GameplayTagSystem
{
    public enum EGameplayTagType
    {
        Owned,
        Block,
    }
    public enum EContainType
    {
        Any,
        All,
    }
    public enum EGameplayTagEventType
    {
        ToggleOwned = 1,
        ToggleBlock = 2,
        AddOwned = 3,
        RemoveOwned = 4,
        AddBlock = 5,
        RemoveBlock = 6,
        Trigger = 0,
    }
}
