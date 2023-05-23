using System.Collections;
using UnityEngine;

public class PlayerCombatSystem : MonoBehaviour,IDieListener
{
    [SerializeField] private AnimatorController animator;
    [SerializeField] private LayerMask damagableLayer;
    [SerializeField] private Rotater rotater;
    [SerializeField] private float trackingDistance;

    [Header("BulletSettings")]
    [SerializeField] private Transform spawnBulletPoint;
    [SerializeField] private Bullet bulletPref;
    [SerializeField] private float maxFlyingDistanceBullet;
    [SerializeField] private float bulletSpeed;

    [Header("FireSettings")]
    [SerializeField] private float damage;
    [SerializeField] private int sizeClip;
    [SerializeField] private float rateFire;
    [SerializeField] private float timeReloading;

    
    private GameObject nearestEnemy;
    private Coroutine detectedCoroutine;
    private Coroutine reloadingCoroutine;
    private float remainingRateFire;
    private int remainingBulletInClip;
    private bool isReloading;

    public LayerMask DamagableLayer { get => damagableLayer; }
    private void Start()
    {
        detectedCoroutine = StartCoroutine(DetectEnemyCoroutine());
    }
    private void Update()
    {
        if (remainingRateFire > 0)
        {
            remainingRateFire -= Time.deltaTime;
        }
        if (nearestEnemy != null)
        {
            rotater.Rotate(nearestEnemy.transform.position.x < transform.position.x, this);
        }
    }
    private IEnumerator DetectEnemyCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            DetectEnemy();
        }
    }
    private void DetectEnemy()
    {
        Collider2D[] detectedEnemy = Physics2D.OverlapCircleAll(transform.position, trackingDistance, damagableLayer);
        if (detectedEnemy.Length > 0)
        {
            rotater.BlockFlip(this);
            nearestEnemy = detectedEnemy[0].gameObject;
            for (int i = 1; i < detectedEnemy.Length; i++)
            {
                if (Vector2.Distance(detectedEnemy[i].transform.position, transform.position) < Vector2.Distance(nearestEnemy.transform.position, transform.position))
                {
                    nearestEnemy = detectedEnemy[i].gameObject;
                }
            }
        }
        else
        {
            nearestEnemy = null;
            rotater.UnblockFlip(this);
        }
    }
    public void Attack()
    {
        if (remainingBulletInClip > 0)
        {
            if (remainingRateFire <= 0 && isReloading == false)
            {
                animator.ShootAnimation();
                var bullet = Instantiate(bulletPref, spawnBulletPoint);
                bullet.transform.localPosition = Vector3.zero;
                bullet.transform.parent = null;
                bullet.SendBullet(damagableLayer, maxFlyingDistanceBullet, bulletSpeed, GetDamage);
                remainingRateFire = rateFire;
                remainingBulletInClip--;
            }
        }
        else
        {
            Reload();
        }
    }
    private void GetDamage(Health enemyHealth)
    {
        enemyHealth.TakeDamage(damage);
    }
    public void Reload()
    {
        if (isReloading == false)
        {
            animator.ReloadAnimation(true);
            reloadingCoroutine = StartCoroutine(Reloading());
        }
        isReloading = true;
    }
    private IEnumerator Reloading()
    {
        var remainingTImeReload = timeReloading;
        while (remainingTImeReload > 0)
        {
            yield return new WaitForSeconds(1);
            remainingTImeReload--;
        }
        FillClip();
    }
    public void FillClip()
    {
        remainingBulletInClip = sizeClip;
        animator.ReloadAnimation(false);
        isReloading = false;
    }

    public void OnDie()
    {
        StopAllCoroutines();
        enabled = false;
    }
}
