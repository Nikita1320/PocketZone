using UnityEngine;

public class DeathLoot : MonoBehaviour
{
    [SerializeField] private ItemData itemData;
    [SerializeField] private int ammount;
    [SerializeField] private ItemConteiner prefab;
    private Health health;
    private void Start()
    {
        health = GetComponent<Health>();
        health.Died += InstanceLootConteiner;
    }
    private void InstanceLootConteiner()
    {
        var conteiner = Instantiate(prefab);
        conteiner.transform.position = transform.position;
        conteiner.Init(itemData, ammount);
    }
}
