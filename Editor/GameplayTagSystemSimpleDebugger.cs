using UnityEngine;
using System.Collections.Generic;

namespace StudioScor.GameplayTagSystem
{
    [AddComponentMenu("StudioScor/GameplayTagSystem/Debug/GameplayTagSystem Simple Debugger")]
    public class GameplayTagSystemSimpleDebugger : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField] private List<GameplayTag> _OwnedTags;
        [SerializeField] private List<GameplayTag> _BlockTags;

        private void Awake()
        {
            _OwnedTags = new();
            _BlockTags = new();
        }

        private void OnEnable()
        {
            if(TryGetComponent(out GameplayTagSystemComponent gameplayTagComponent))
            {
                UpdateTag(gameplayTagComponent);

                gameplayTagComponent.OnRemovedBlockTag += GameplayTagSystem_OnAddBlockTag;
                gameplayTagComponent.OnRemovedOwnedTag += GameplayTagSystem_OnAddBlockTag;
                gameplayTagComponent.OnAddedNewBlockTag += GameplayTagSystem_OnAddBlockTag;
                gameplayTagComponent.OnAddedNewOwnedTag += GameplayTagSystem_OnAddBlockTag;
            }
            else
            {
                enabled = false;
            }
        }

        private void GameplayTagSystem_OnAddBlockTag(GameplayTagSystemComponent gameplayTagComponent, GameplayTag changedTag)
        {
            UpdateTag(gameplayTagComponent);
        }

        private void UpdateTag(GameplayTagSystemComponent gameplayTagComponent)
        {
            _OwnedTags.Clear();
            _BlockTags.Clear();

            foreach (var tag in gameplayTagComponent.OwnedTags)
            {
                if (tag.Value > 0)
                {
                    _OwnedTags.Add(tag.Key);
                }
            }
            foreach (var tag in gameplayTagComponent.BlockTags)
            {
                if (tag.Value > 0)
                {
                    _BlockTags.Add(tag.Key);
                }
            }
        }
#endif
    }
}
