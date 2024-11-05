#if SCOR_ENABLE_BEHAVIOR
using System;
using Unity.Behavior;
using UnityEngine;

namespace StudioScor.GameplayTagSystem.Behavior
{
    [Serializable, Unity.Properties.GeneratePropertyBag]
    [NodeDescription(name: "Remove Gameplay Tag", story: "[Self] Remove [GameplayTag] Tag in [Type] ", category: "Action/StudioScor/GameplayTagSystem", id: "playersystem_removegameplaytag")]
    public class RemoveGameplayTagAction : GameplayTagSystemAction
    {
        public enum EType
        {
            Owned,
            Block,
        }

        [SerializeReference] public BlackboardVariable<GameplayTagSO> GameplayTag;
        [SerializeReference] public BlackboardVariable<EType> Type = new(EType.Owned);

        protected override Status OnStart()
        {
            if (base.OnStart() == Status.Failure)
                return Status.Failure;

            if (Type.Value == EType.Owned)
            {
                _gameplayTagSystem.RemoveOwnedTag(GameplayTag.Value);
            }
            else
            {
                _gameplayTagSystem.RemoveBlockTag(GameplayTag.Value);
            }

            return Status.Success;

        }
    }

}

#endif