using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory system/Inventory")]
public class InventoryObject : ScriptableObject
{
    public List<InventorySlot> Container = new List<InventorySlot>();
    public void AddItem(ItemObject _item, int _amount)
    {
        bool hasItem = false;
        for (int i = 0; i < Container.Count; i++)
        {
            if(Container[i].item == _item)
            {
                Container[i].AddAmount(_amount);
                hasItem = true;
                break;
            }
        }
        if (!hasItem)
        {
            Container.Add(new InventorySlot(_item, _amount));
        }
    }

    public ItemObject GetItem(ItemObject item, int amount = 1)
    {
        foreach(InventorySlot obj in Container)
        {
            if (obj.item == item)
            {
                if(obj.amount >= amount)
                {
                    return obj.item;
                }
            }
        }
        return null;
    }

    public void RemoveItem(ItemObject item, int amount)
    {
        foreach (InventorySlot obj in Container)
        {
            if (obj.item == item)
            {
                obj.amount -= amount;
                if (obj.amount < 0)
                {
                    obj.amount = 0;
                }
            }
        }
    }
}

[System.Serializable]
public class InventorySlot
{
    public ItemObject item;
    public int amount;
    public InventorySlot(ItemObject _item, int _amount)
    {
        item = _item;
        amount = _amount;
    }
    public void AddAmount(int value)
    {
        amount += value;
    }
}