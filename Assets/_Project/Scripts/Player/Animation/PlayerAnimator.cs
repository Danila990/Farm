using UnityEngine;

namespace ProjectCode
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        public void StartJumpAnimation()
        {
            _animator.SetBool("IsJump", true);
        }

        public void EndJumpAnimation()
        {
            _animator.SetBool("IsJump", false);
        }
    }
}