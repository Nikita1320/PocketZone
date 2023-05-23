using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] private Animator footAnimator;
    [SerializeField] private Animator handAnimator;
    [SerializeField] private Animator generalAnimator;

    public void MoveAnimation(bool isMoving)
    {
        footAnimator.SetBool("IsMoving", isMoving);
    }
    public void ShootAnimation()
    {
        handAnimator.ResetTrigger("Shoot");
        handAnimator.SetTrigger("Shoot");
    }
    public void ReloadAnimation(bool isReloading)
    {
        handAnimator.SetBool("Reload", isReloading);
    }
    public void DieAnimation()
    {
        footAnimator.enabled = false;
        handAnimator.enabled = false;
        generalAnimator.enabled = true;
    }
}
