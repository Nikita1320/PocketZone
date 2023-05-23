using System;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private int sizeInventory;
        [SerializeField] private ItemData[] itemDatas;
        [SerializeField] private ItemData[] defaultItems;
        [SerializeField] private List<Item> items = new();
        private ISaveService saveService;

        private static Inventory instance;

        public Action<Item> AddedItem { get; set; }
        public Action<Item> RemovedItem { get; set; }
        public static Inventory Instance => instance;

        public Item[] Items { get => items.ToArray(); }

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
            DontDestroyOnLoad(gameObject);

            saveService = new JsonSaveService();

            Dictionary<int, List<int>> loadData = new();
            saveService.Load("Inventory", out loadData, new Dictionary<int, List<int>>() 
            {
                { 0, new List<int>(){4} },
                { 1, new List<int>(){4} }
            });
            Initialize(loadData);
        }

        public int AddItem(ItemData itemData, int ammount)
        {
            var remaining = ammount;
            foreach (var item in items)
            {
                if (item.Data == itemData)
                {
                    remaining = item.AddToStack(ammount);
                    if (remaining == 0)
                    {
                        SaveItems();
                        return 0;
                    }
                }
            }

            if (sizeInventory > items.Count)
            {
                var newItem = new Item(itemData, remaining);
                items.Add(newItem);
                AddedItem?.Invoke(newItem);
                newItem.ChangedAmmount += SaveItems;
                SaveItems();
                return 0;
            }
            return remaining;
        }

        public void RemoveItem(Item item)
        {
            items.Remove(item);
            RemovedItem?.Invoke(item);
            SaveItems();
        }
        private void Initialize(Dictionary<int, List<int>> itemDatas)
        {
            foreach (var i in itemDatas)
            {
                var itemdata = GetItemDataObject(i.Key);
                foreach (var j in i.Value)
                {
                    var item = new Item(itemdata, j);
                    items.Add(item);
                    item.ChangedAmmount += SaveItems;
                }
            }
        }
        private void SaveItems()
        {
            var saveData = new Dictionary<int, List<int>>();
            foreach (var item in items)
            {
                if (saveData.ContainsKey(item.Data.Id))
                {
                    saveData[item.Data.Id].Add(item.Ammount);
                }
                else
                {
                    saveData.Add(item.Data.Id, new List<int>() { item.Ammount });
                }
            }
            saveService.Save("Inventory", saveData);
        }
        private ItemData GetItemDataObject(int id)
        {
            foreach (var item in itemDatas)
            {
                if (item.Id == id)
                {
                    return item;
                }
            }
            return null;
        }
    }
}
