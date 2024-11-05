using StudioScor.Utilities;
using UnityEngine;

namespace StudioScor.GameplayTagSystem
{
    public class AnimNotify_GrantGameplayTag : AnimNotifyBehaviour
    {
        [Header(" [ Grant Gameplay Tag ] ")]
        [SerializeField] private FGameplayTags _grantTags;

        protected override void OnNotify(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var gameplayTagSystem = animator.transform.parent.GetGameplayTagSystem();

            gameplayTagSystem.AddGameplayTags(_grantTags);
        }
    }
}
