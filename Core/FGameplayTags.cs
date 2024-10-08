using System.Collections.Generic;
using UnityEngine;

namespace StudioScor.GameplayTagSystem
{
    [System.Serializable]
    public struct FGameplayTags
    {
        [SerializeField] private GameplayTagSO[] owneds;
        [SerializeField] private GameplayTagSO[] blocks;

        public readonly IReadOnlyCollection<GameplayTagSO> Owneds => owneds;
        public readonly IReadOnlyCollection<GameplayTagSO> Blocks => blocks;
    }
}
