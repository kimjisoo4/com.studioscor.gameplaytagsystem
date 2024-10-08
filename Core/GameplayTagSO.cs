using System;
using UnityEngine;

namespace StudioScor.GameplayTagSystem
{
    [CreateAssetMenu(fileName = "New GameplayTag", menuName = "StudioScor/GameplayTagSystem/New GameplayTag")]
    public class GameplayTagSO : ScriptableObject, IGameplayTag
    {
        public override bool Equals(object other)
        {
            if (ReferenceEquals(this, other))
                return true;

            if(other is IGameplayTag gameplayTag)
            {
                return string.Equals(name, gameplayTag.name, StringComparison.OrdinalIgnoreCase);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return name?.GetHashCode() ?? 0;
        }

        public static bool operator ==(GameplayTagSO lhs, IGameplayTag rhs)
        {
            if (ReferenceEquals(lhs, rhs))
                return true;

            if (rhs is null || lhs is null)
                return false;

            if (string.Equals(lhs.name, rhs.name, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        }
        public static bool operator !=(GameplayTagSO lhs, IGameplayTag rhs)
        {
            return !(lhs == rhs);
        }
    }
}
