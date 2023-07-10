using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPanel : Inventory
{
    #region µ¥ÀýÄ£Ê½
    private static CharacterPanel _instance;
    public static CharacterPanel Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("CharacterPanel").GetComponent<CharacterPanel>();
            }
            return _instance;
        }
    }
    #endregion


    public void PutOn(Item item)
    {
        Item exitItem = null;
        foreach(Slot slot in slotLists)
        {
            EquipmentSlot equipmentSlot = (EquipmentSlot)slot;
            if (equipmentSlot.IsRightItem(item))
            {
                if (equipmentSlot.transform.childCount > 0)
                {
                    ItemUI currentItem = equipmentSlot.transform.GetChild(0).GetComponent<ItemUI>();
                    exitItem = currentItem.Item;
                    currentItem.SetItem(item,1);
                    //equipmentSlot.transform.GetChild(0).GetComponent<ItemUI>().SetItem(item, 1);
                }
                else
                {
                    equipmentSlot.StoreItem(item);
                }
                break;
            }
        }
        if (exitItem != null)
        {
            Knapsack.Instance.StoreItem(exitItem);
        }
        
    }
}
