using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Slot[] slotLists;

    public virtual void Start()
    {
        slotLists = GetComponentsInChildren<Slot>();
    }

    public bool StoreItem(int id)
    {
        Item item = InventoryManager.Instance.GetItemById(id);
        return StoreItem(item);
    }

    public bool StoreItem(Item item)
    {
        if(item == null)
        {
            return false;
        }
        if (item.Capacity == 1)
        {
            Slot slot = FindEmptySlot();
            if(slot == null)
            {
                Debug.LogWarning("物品栏满了");
                return false;
            }
            else
            {
                slot.StoreItem(item);
                return true;
            }
        }
        else
        {
            Slot slot = FindSameTypeSlot(item);
            if(slot == null)
            {
                Slot emptySlot;
                emptySlot = FindEmptySlot();
                if(emptySlot != null)
                {
                    emptySlot.StoreItem(item);
                    return true;
                }
                else
                {
                    Debug.LogWarning("背包满了");
                    return false;
                }
            }
            else
            {
                slot.StoreItem(item);
                return true;
            }
        }
    }

    public Slot FindEmptySlot()
    {
        foreach(Slot slot in slotLists)
        {
            if (slot.transform.childCount == 0)
            {
               return slot;
            }
        }
        return null;
    }

    public Slot FindSameTypeSlot(Item item)
    {
        foreach(Slot slot in slotLists)
        {
            if(slot.transform.childCount>=1&& slot.GetItemID()==item.ID && slot.isFilled() == false)
            {
                return slot;
            }
        }
        return null;
    }
}
