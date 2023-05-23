using System;
using UnityEngine;

class Bullet:MonoBehaviour
{
    [SerializeField] private LayerMask collisionLayer;
    private LayerMask damagableLayer;
    private Action<Health> collidedWithTargetLayer;
    private float maxFlyingDistance;
    private Vector2 startPosition;
    private float bulletSpeed;

    private void Update()
    {
        if (maxFlyingDistance != 0)
        {
            if (Vector2.Distance(startPosition, transform.position) > maxFlyingDistance)
            {
                Destroy(gameObject);
            }
            transform.position += transform.right * bulletSpeed * Time.deltaTime;
        }
    }
    public virtual void SendBullet(LayerMask damagableLayer, float maxFlyingDistance, float bulletSpeed, Action<Health> getDamage)
    {
        startPosition = transform.position;
        this.damagableLayer = damagableLayer;
        this.maxFlyingDistance = maxFlyingDistance;
        this.bulletSpeed = bulletSpeed;
        collidedWithTargetLayer += getDamage;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (damagableLayer == (damagableLayer | (1 << collision.gameObject.layer)))
        {
            if (collision.TryGetComponent(out Health health))
            {
                collidedWithTargetLayer?.Invoke(health);
                Destroy(gameObject);
            }
        }
        else if (collisionLayer == (collisionLayer | (1 << collision.gameObject.layer)))
        {
            Destroy(gameObject);
        }
    }
}
