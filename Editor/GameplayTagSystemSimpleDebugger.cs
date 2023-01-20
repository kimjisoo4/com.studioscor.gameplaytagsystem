using UnityEngine;
using System.Collections.Generic;

namespace StudioScor.GameplayTagSystem.Editor
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
            if(TryGetComponent(out GameplayTagSystemComponent gameplayTagSystemComponent))
            {
                UpdateTag(gameplayTagSystemComponent);

                gameplayTagSystemComponent.OnRemovedBlockTag += GameplayTagSystem_UpdateTag;
                gameplayTagSystemComponent.OnRemovedOwnedTag += GameplayTagSystem_UpdateTag;
                gameplayTagSystemComponent.OnGrantedBlockTag += GameplayTagSystem_UpdateTag;
                gameplayTagSystemComponent.OnGrantedOwnedTag += GameplayTagSystem_UpdateTag;
            }
            else
            {
                enabled = false;
            }
        }
        private void OnDisable()
        {
            _OwnedTags.Clear();
            _BlockTags.Clear();

            if (TryGetComponent(out GameplayTagSystemComponent gameplayTagSystemComponent))
            {
                gameplayTagSystemComponent.OnRemovedBlockTag -= GameplayTagSystem_UpdateTag;
                gameplayTagSystemComponent.OnRemovedOwnedTag -= GameplayTagSystem_UpdateTag;
                gameplayTagSystemComponent.OnGrantedBlockTag -= GameplayTagSystem_UpdateTag;
                gameplayTagSystemComponent.OnGrantedOwnedTag -= GameplayTagSystem_UpdateTag;
            }
        }

        private void GameplayTagSystem_UpdateTag(GameplayTagSystemComponent gameplayTagSystemComponent, GameplayTag changedTag)
        {
            UpdateTag(gameplayTagSystemComponent);
        }

        private void UpdateTag(GameplayTagSystemComponent gameplayTagSystemComponent)
        {
            _OwnedTags.Clear();
            _BlockTags.Clear();

            foreach (var tag in gameplayTagSystemComponent.OwnedTags)
            {
                if (tag.Value > 0)
                {
                    _OwnedTags.Add(tag.Key);
                }
            }

            foreach (var tag in gameplayTagSystemComponent.BlockTags)
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
