using UnityEngine;

public class PlayerAnimatorUnity : MonoBehaviour
{
    [SerializeField] private Animator _unitAnimator;

    public void StartJumpAnimation()
    {
        _unitAnimator.SetBool("IsJump", true);
    }

    public void EndJumpAnimation()
    {
        _unitAnimator.SetBool("IsJump", false);
    }
}