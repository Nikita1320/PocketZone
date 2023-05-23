using UnityEngine;

public class ItemConteiner : MonoBehaviour
{
    [SerializeField] private ItemData itemData;
    [SerializeField] private int ammount;

    public int Ammount { get => ammount; set => ammount = value; }
    public ItemData ItemData { get => itemData; }

    public void Init(ItemData itemData, int ammount )
    {
        this.itemData = itemData;
        this.ammount = ammount;
    }
}
