#if SCOR_ENABLE_BEHAVIOR
using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using StudioScor.Utilities;
using System.Collections.Generic;

namespace StudioScor.GameplayTagSystem.Behavior
{


    [Serializable, GeneratePropertyBag]
    [Condition(name: "Has GameplayTag",
        story: "[Self] Has [GameplayTag] Tag in [Type] ( UseDebug [UseDebugKey] )",
        category: "Conditions/StudioScor/GameplayTagSystem",
        id: "playersystem_hasgameplaytag")]
    public partial class HasGameplayTag : GameplayTagSystemCondition
    {
        public enum EType
        {
            Owned,
            Block,
        }

        [SerializeReference] public BlackboardVariable<GameplayTagSO> GameplayTag;
        [SerializeReference] public BlackboardVariable<EType> Type = new(EType.Owned);

        public override bool IsTrue()
        {
            if (!base.IsTrue())
                return false;

            bool result = true;

            if(Type.Value == EType.Owned)
            {
                result = _gameplayTagSystem.ContainOwnedTag(GameplayTag.Value);
            }
            else
            {
                result = _gameplayTagSystem.ContainBlockTag(GameplayTag.Value);
            }


            Log($"{nameof(Self).ToBold()} Is " +
                $"{(result ? $"Has {GameplayTag.Name} in {Type.Value}".ToColor(SUtility.STRING_COLOR_SUCCESS) : $"Not Has {GameplayTag.Name} in {Type.Value}".ToColor(SUtility.STRING_COLOR_FAIL)).ToBold()}");

            return result;
        }
    }

}

#endif