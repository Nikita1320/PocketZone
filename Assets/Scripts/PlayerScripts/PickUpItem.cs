using UnityEngine;
using InventorySystem;

public class PickUpItem : MonoBehaviour
{
    [SerializeField] private LayerMask pickUpItemLayer;
    [SerializeField] private Inventory inventory;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (pickUpItemLayer == (pickUpItemLayer | (1 << collision.gameObject.layer)))
        {
            var conteiner = collision.GetComponent<ItemConteiner>();
            var remainingAmmount = Inventory.Instance.AddItem(conteiner.ItemData, conteiner.Ammount);
            if (remainingAmmount == 0)
            {
                Destroy(collision.gameObject);
            }
            else
            {
                conteiner.Ammount = remainingAmmount;
            }
        }
    }
}
