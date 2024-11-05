#if SCOR_ENABLE_BEHAVIOR
using StudioScor.Utilities.UnityBehavior;
using StudioScor.Utilities;
using Unity.Behavior;
using UnityEngine;

namespace StudioScor.GameplayTagSystem.Behavior
{
    public class GameplayTagSystemAction : BaseAction
    {
        [SerializeReference] public BlackboardVariable<GameObject> Self;

        protected IGameplayTagSystem _gameplayTagSystem;

        protected override Status OnStart()
        {
            var self = Self.Value;

            if (!self)
            {
                LogError($"{nameof(Self).ToBold()} is {"Null".ToBold().ToColor(SUtility.STRING_COLOR_FAIL)}.");
                return Status.Failure;
            }

            if (!self.TryGetGameplayTagSystem(out _gameplayTagSystem))
            {
                LogError($"{nameof(Self).ToBold()} is Not Has {nameof(_gameplayTagSystem).ToBold().ToColor(SUtility.STRING_COLOR_FAIL)}.");
                return Status.Failure;
            }

            return Status.Running;
        }
    }
}

#endif