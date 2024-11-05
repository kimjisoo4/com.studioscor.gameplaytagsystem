#if SCOR_ENABLE_BEHAVIOR
using System;
using Unity.Behavior;
using UnityEngine;

namespace StudioScor.GameplayTagSystem.Behavior
{
    [Serializable, Unity.Properties.GeneratePropertyBag]
    [NodeDescription(name: "Trigger Gameplay Tag", story: "[Self] Trigger [GameplayTag] Tag", category: "Action/StudioScor/GameplayTagSystem", id: "playersystem_triggergameplaytag")]
    public class TriggerGameplayTagAction : GameplayTagSystemAction
    {
        [SerializeReference] public BlackboardVariable<GameplayTagSO> GameplayTag;

        protected override Status OnStart()
        {
            if (base.OnStart() == Status.Failure)
                return Status.Failure;

            _gameplayTagSystem.TriggerTag(GameplayTag.Value);

            return Status.Success;

        }
    }

}

#endif