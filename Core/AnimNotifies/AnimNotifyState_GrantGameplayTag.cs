using StudioScor.Utilities;
using UnityEngine;

namespace StudioScor.GameplayTagSystem
{
    public class AnimNotifyState_GrantGameplayTag : AnimNotifyStateBehaviour
    {
        [Header(" [ Grant Gameplay Tag ] ")]
        [SerializeField] private FGameplayTags _grantTags;

        protected override void OnEnterNotify(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

            var gameplayTagSystem = animator.transform.parent.GetGameplayTagSystem();

            gameplayTagSystem.AddGameplayTags(_grantTags);
        }

        protected override void OnExitNotify(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var gameplayTagSystem = animator.transform.parent.GetGameplayTagSystem();

            gameplayTagSystem.RemoveGameplayTags(_grantTags);
        }
    }
}
