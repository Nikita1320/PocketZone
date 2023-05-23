using UnityEngine;

[CreateAssetMenu(menuName = "Item/ItemData", fileName = "newItemData")]
public class ItemData: ScriptableObject
{
    [SerializeField] private int id;
    [SerializeField] private Sprite itemIcon;
    [SerializeField] private string nameItem;
    [SerializeField] private string description;
    [SerializeField] private int maxAmmount;
    public Sprite ItemIcon { get => itemIcon; }
    public string NameItem { get => nameItem; }
    public string Description { get => description; }
    public int Id { get => id; }
    public int MaxAmmount { get => maxAmmount; }
}
