#if SCOR_ENABLE_BEHAVIOR
using StudioScor.Utilities.UnityBehavior;
using StudioScor.Utilities;
using Unity.Behavior;
using UnityEngine;

namespace StudioScor.GameplayTagSystem.Behavior
{
    public abstract class GameplayTagSystemCondition : BaseCondition
    {
        [SerializeReference] public BlackboardVariable<GameObject> Self;

        protected IGameplayTagSystem _gameplayTagSystem;

        public override void OnStart()
        {
            base.OnStart();

            var self = Self.Value;

            if (!self)
            {
                _gameplayTagSystem = null;
                LogError($"{nameof(Self).ToBold()} is Null.");
                return;
            }

            if (!self.TryGetGameplayTagSystem(out _gameplayTagSystem))
            {
                LogError($"{nameof(Self).ToBold()} is Not Has {nameof(IGameplayTagSystem).ToBold()}.");
                return;
            }
        }

        public override bool IsTrue()
        {
            var result = _gameplayTagSystem is not null;

            Log($"{nameof(Self).ToBold()} {(result ? $"Has {nameof(IGameplayTagSystem)}".ToColor(SUtility.STRING_COLOR_SUCCESS) : $"Not Has {nameof(IGameplayTagSystem).ToColor(SUtility.STRING_COLOR_FAIL)}").ToBold()}");

            return result;
        }
    }
}

#endif