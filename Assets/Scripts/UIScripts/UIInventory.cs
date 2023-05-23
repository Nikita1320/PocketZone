using InventorySystem;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private InventoryCell prefab;
    [SerializeField] private GameObject content;
    private List<InventoryCell> cells = new();
    private Inventory inventory;

    private void Start()
    {
        inventory = Inventory.Instance;
        inventory.AddedItem += AddCell;
        InstanceCells();
    }
    private void InstanceCells()
    {
        foreach (var item in inventory.Items)
        {
            AddCell(item);
        }
    }
    private void AddCell(Item item)
    {
        var cell = Instantiate(prefab, content.transform);
        cells.Add(cell);
        cell.Init(item);
        cell.RemovedButton.onClick.AddListener(() => RemoveCell(cell));
    }
    private void RemoveCell(InventoryCell inventoryCell)
    {
        inventory.RemoveItem(inventoryCell.ContainedItem);
        cells.Remove(inventoryCell);
        Destroy(inventoryCell.gameObject);
    }
    private void OnDestroy()
    {
        inventory.AddedItem -= AddCell;
    }
}
