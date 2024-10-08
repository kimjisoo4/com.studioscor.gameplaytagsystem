using System.Collections.Generic;
using UnityEngine;

namespace StudioScor.GameplayTagSystem
{
    [System.Serializable]
    public struct FConditionTags
    {
        [SerializeField] private GameplayTagSO[] requireds;
        [SerializeField] private GameplayTagSO[] obstacleds;

        public IReadOnlyCollection<GameplayTagSO> Requireds => requireds;
        public IReadOnlyCollection<GameplayTagSO> Obstacleds => obstacleds;
    }
}
