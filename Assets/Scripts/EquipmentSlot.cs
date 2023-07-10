using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentSlot : Slot
{
    public Equipment.EquipmentType equipType;
    public Weapon.WeaponType wpType;

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (InventoryManager.Instance.IsPickedItem)
        {
            ItemUI pickedItem = InventoryManager.Instance.PickedItem;
            if (transform.childCount > 0)
            {
                ItemUI currentItemUI = transform.GetChild(0).GetComponent<ItemUI>();
                if(IsRightItem(pickedItem.Item))
                {
                        //Item item = currentItemUI.Item;
                        //int amount = currentItemUI.Amount;
                        //currentItemUI.SetItem(pickedItem.Item, pickedItem.Amount);
                        //pickedItem.SetItem(item, amount);
                        InventoryManager.Instance.PickedItem.Exchange(currentItemUI);
                 }
            }
            else
            {
                if (IsRightItem(pickedItem.Item))
                {
                    this.StoreItem(pickedItem.Item);
                    InventoryManager.Instance.ReduceAmount(1);
                }
            }
        }
        else
        {
            if (transform.childCount > 0)
            {
                ItemUI currentItemUI = transform.GetChild(0).GetComponent<ItemUI>();
                InventoryManager.Instance.PickupItem(currentItemUI.Item, currentItemUI.Amount);
                Destroy(currentItemUI.gameObject);
            }
        }
    }

    public  bool IsRightItem(Item item)
    {
        if ((item is Equipment && ((Equipment)item).EquipType == this.equipType) || (item is Weapon && ((Weapon)item).WpType == this.wpType))
        {
            return true;
        }
        else return false;
    }
}
