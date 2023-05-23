using UnityEngine;

public class EnemyHealth : Health
{
    [SerializeField] private Animator animator;
    protected override void Die()
    {
        animator.SetTrigger("Die");
        base.Die();
    }
}
