using System;
using UnityEngine;
using DG.Tweening;

namespace MyCode
{
    public class MoveUnitComponent : MonoBehaviour
    {
        public event Action OnMoveComplete;

        [SerializeField] private float _moveDuraction = 0.5f;

        private Tween _moveTween;

        public bool IsMoved => _moveTween != null && _moveTween.active;

        public void StartMove(Vector3 target)
        {
            if (IsMoved)
                return;

            _moveTween = transform.DOMove(target, _moveDuraction)
                .SetEase(Ease.Linear)
                .OnComplete(MoveComplete);
        }

        public void PauseMove()
        {
            if (IsMoved)
                _moveTween.Pause();
        }

        public void PlayMove()
        {
            if (IsMoved)
                _moveTween.Play();
        }

        private void MoveComplete()
        {
            _moveTween.Kill();
            OnMoveComplete?.Invoke();
        }

        private void OnDestroy()
        {
            _moveTween?.Kill();
        }
    }
}