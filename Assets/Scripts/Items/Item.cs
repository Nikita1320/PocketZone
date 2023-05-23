using System;

[Serializable]
public class Item
{
    private ItemData itemData;
    public int Ammount { get; protected set; }
    public int MaxAmmount => itemData.MaxAmmount;

    public ItemData Data { get => itemData; protected set => itemData = value; }

    public bool IsFull => Ammount == MaxAmmount ? true : false;

    public Action ChangedAmmount { get; set; }

    public Item(ItemData itemData) 
    {
        this.itemData = itemData;
    }
    public Item(ItemData itemData, int ammount) : this(itemData)
    {
        Ammount = ammount;
        if (Ammount > MaxAmmount)
        {
            Ammount = MaxAmmount;
        }
    }
    public int AddToStack(int value)
    {
        if (IsFull)
        {
            return value;
        }
        if (Ammount + value > MaxAmmount)
        {
            var remaining = Ammount + value - MaxAmmount;
            Ammount = MaxAmmount;
            ChangedAmmount?.Invoke();
            return remaining;
        }
        else
        {
            Ammount += value;
            ChangedAmmount?.Invoke();
            return 0;
        }
    }
}
