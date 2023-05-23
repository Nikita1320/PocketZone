using System;
using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] protected float maxHP;
    [SerializeField] protected float currentHP;
    private bool isDie = false;
    public Action Died { get; set; }
    public Action ChangedHP { get; set; }
    public float CurrentHP { get => currentHP; }
    public float MaxHP { get => maxHP; }
    public bool IsDie { get => isDie; }

    private void Start()
    {
        currentHP = maxHP;
    }

    public virtual void TakeDamage(float damage)
    {
        if (isDie == false)
        {
            currentHP -= damage;
            ChangedHP?.Invoke();
            if (currentHP <= 0)
            {
                isDie = true;
                Die();
            }
        }
    }
    protected virtual void Die()
    {
        var listeners = GetComponents<IDieListener>();
        foreach (var listener in listeners)
        {
            listener.OnDie();
        }
        Died?.Invoke();
        StartCoroutine(DestroyDelay(3));
    }
    protected IEnumerator DestroyDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            Destroy(gameObject);
        }
    }
}
