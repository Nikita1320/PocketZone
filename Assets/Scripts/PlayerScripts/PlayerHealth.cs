using UnityEngine;

public class PlayerHealth : Health
{
    [SerializeField] private AnimatorController animator;
    protected override void Die()
    {
        animator.DieAnimation();
        base.Die();
    }
}
