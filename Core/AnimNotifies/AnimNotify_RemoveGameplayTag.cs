using StudioScor.Utilities;
using UnityEngine;

namespace StudioScor.GameplayTagSystem
{
    public class AnimNotify_RemoveGameplayTag : AnimNotifyBehaviour
    {
        [Header(" [ Grant Gameplay Tag ] ")]
        [SerializeField] private FGameplayTags _removeTags;

        protected override void OnNotify(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var gameplayTagSystem = animator.transform.parent.GetGameplayTagSystem();

            gameplayTagSystem.RemoveGameplayTags(_removeTags);
        }
    }
}
