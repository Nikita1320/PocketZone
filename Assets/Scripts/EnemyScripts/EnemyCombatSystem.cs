using UnityEngine;

public class EnemyCombatSystem : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private LayerMask damagableLayer;
    [SerializeField] private float damageRadius;
    private Animator animator;
    private bool isAttacking;

    public bool IsAttacking { get => isAttacking; }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void Attack()
    {
        isAttacking = true;
        animator.SetTrigger("Attack");
    }
    public void GetDamage()
    {
        Collider2D player = Physics2D.OverlapCircle(transform.position + transform.right, damageRadius, damagableLayer);

        if (player != null)
        {
            player.GetComponent<Health>().TakeDamage(damage);
        }
        isAttacking = false;
    }
}
