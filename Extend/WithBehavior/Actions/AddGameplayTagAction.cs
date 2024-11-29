#if SCOR_ENABLE_BEHAVIOR
using System;
using Unity.Behavior;
using UnityEngine;

namespace StudioScor.GameplayTagSystem.Behavior
{
    [Serializable, Unity.Properties.GeneratePropertyBag]
    [NodeDescription(name: "Add Gameplay Tag", story: "[Self] Add [GameplayTag] Tag in [Type] ", category: "Action/StudioScor/GameplayTagSystem", id: "playersystem_addgameplaytag")]
    public class AddGameplayTagAction : GameplayTagSystemAction
    {
        public enum EType
        {
            Owned,
            Block,
        }

        [SerializeReference] public BlackboardVariable<GameplayTag> GameplayTag;
        [SerializeReference] public BlackboardVariable<EType> Type = new(EType.Owned);

        protected override Status OnStart()
        {
            if (base.OnStart() == Status.Failure)
                return Status.Failure;

            if (Type.Value == EType.Owned)
            {
                _gameplayTagSystem.AddOwnedTag(GameplayTag.Value);
            }
            else
            {
                _gameplayTagSystem.AddBlockTag(GameplayTag.Value);
            }

            return Status.Success;

        }
    }

}

#endif