using System.Collections.Generic;
using UnityEngine;

namespace StudioScor.GameplayTagSystem
{
    [System.Serializable]
    public struct FGameplayTags
    {
        [SerializeField] private GameplayTag[] owneds;
        [SerializeField] private GameplayTag[] blocks;

        public readonly IReadOnlyCollection<GameplayTag> Owneds => owneds;
        public readonly IReadOnlyCollection<GameplayTag> Blocks => blocks;
    }
}
