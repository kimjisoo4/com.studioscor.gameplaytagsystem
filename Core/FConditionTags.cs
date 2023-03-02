using UnityEngine;
using System.Collections.Generic;

namespace StudioScor.GameplayTagSystem
{
    [System.Serializable]
    public struct FConditionTags
    {
        [SerializeField] private GameplayTag[] _Requireds;
        [SerializeField] private GameplayTag[] _Obstacleds;

        public IReadOnlyCollection<GameplayTag> Requireds => _Requireds;
        public IReadOnlyCollection<GameplayTag> Obstacleds => _Obstacleds;
    }
}
