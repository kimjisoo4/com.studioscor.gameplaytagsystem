using System.Collections.Generic;
using UnityEngine;

namespace StudioScor.GameplayTagSystem
{
    [System.Serializable]
    public struct FConditionTags
    {
        [SerializeField] private GameplayTag[] requireds;
        [SerializeField] private GameplayTag[] obstacleds;

        public IReadOnlyCollection<GameplayTag> Requireds => requireds;
        public IReadOnlyCollection<GameplayTag> Obstacleds => obstacleds;
    }
}
