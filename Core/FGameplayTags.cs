using UnityEngine;
using System.Collections.Generic;

namespace StudioScor.GameplayTagSystem
{
    [System.Serializable]
    public struct FGameplayTags
    {
        [SerializeField] private GameplayTag[] _Owneds;
        [SerializeField] private GameplayTag[] _Blocks;

        public IReadOnlyCollection<GameplayTag> Owneds => _Owneds;
        public IReadOnlyCollection<GameplayTag> Blocks => _Blocks;
    }
}
