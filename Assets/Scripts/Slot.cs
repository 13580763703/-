using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;

public class Slot : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerDownHandler
{
    public GameObject ItemPrefab;
    /// <summary>
    /// 把item放在自身下面,
    /// 如果下面已经有item了则amount++
    /// 如果没有,就根据Itemprefab去实例化一个item放在下面.
    /// </summary>
    public void StoreItem(Item item)
    {
        if (transform.childCount > 0)
        {
            //ItemUI itemAmount = transform.GetChild(0).GetComponent<ItemUI>();
            //itemAmount.AddAmount();
            transform.GetChild(0).GetComponent<ItemUI>().AddAmount();
        }
        else
        {
            GameObject itemGameObject = Instantiate(ItemPrefab) as GameObject;
            itemGameObject.transform.SetParent(this.transform);
            itemGameObject.transform.localPosition = Vector3.zero;
            itemGameObject.transform.GetComponent<ItemUI>().SetItem(item);
        }

    }
    /// <summary>
    /// 获得当前物品槽的物品类型
    /// </summary>
    /// <returns></returns>
    public Item.ItemType GetItemType()
    {
        return transform.GetChild(0).GetComponent<ItemUI>().Item.Type;
    }
    /// <summary>
    /// 得到当前物品的id
    /// </summary>
    /// <returns></returns>
    public int GetItemID()
    {
        return transform.GetChild(0).GetComponent<ItemUI>().Item.ID;
    }
    /// <summary>
    /// 检查同类型的item里面的物品个数是否大于额定数值.
    /// </summary>
    /// <returns></returns>
    public bool isFilled()
    {
        ItemUI item = transform.GetChild(0).GetComponent<ItemUI>();
        if(item.Amount >= item.Item.Capacity)
        {
            return true;
        }
        return false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (transform.childCount > 0)
        {
            string toolTipText = transform.GetChild(0).GetComponent<ItemUI>().Item.GetToolTipText();
            InventoryManager.Instance.ShowToolTip(toolTipText);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InventoryManager.Instance.HideToolTip();
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            if(transform.childCount > 0)
            {
                ItemUI currentItem = transform.GetChild(0).GetComponent<ItemUI>();
                if(currentItem.Item is Equipment || currentItem.Item is Weapon)
                {
                    currentItem.ReduceAmount(1);
                    Item item = currentItem.Item;
                    if (currentItem.Amount <= 0)
                    {
                        InventoryManager.Instance.HideToolTip();
                        DestroyImmediate(currentItem.gameObject);
                    }
                    CharacterPanel.Instance.PutOn(item);
                    
                }
            }
            else
            {

            }
        }

        if (eventData.button != PointerEventData.InputButton.Left) return;

        if(transform.childCount > 0)//在这个背包格子有物品存在时
        {
            ItemUI currentItem = transform.GetChild(0).GetComponent<ItemUI>();

            if(InventoryManager.Instance.IsPickedItem == false)//没选中任何物品时
            {
                if (Input.GetKey(KeyCode.LeftControl))//按下左CTRL时
                {
                    int amountPicked = (currentItem.Amount+1)/2;
                    InventoryManager.Instance.PickupItem(currentItem.Item, amountPicked);
                    int amountRemained = currentItem.Amount - amountPicked;
                    if(amountRemained > 0)
                    {
                        currentItem.SetAmount(amountRemained);
                    }
                    else
                    {
                        Destroy(currentItem.gameObject);
                    }
                }
                else
                {
                    InventoryManager.Instance.PickupItem(currentItem.Item, currentItem.Amount);//把当前物品槽的东西复制到pickedItem上
                    Destroy(currentItem.gameObject);//销毁当前物品
                }
            }
            else//选中物品时
            {
                ItemUI item = InventoryManager.Instance.PickedItem;//item代表此时选中的物品
                //int pickedItemAmount = item.Amount + currentItem.Amount;
                if(currentItem.Item.ID == item.Item.ID)
                {
                    if (Input.GetKey(KeyCode.LeftControl))
                    {
                        if (currentItem.Item.Capacity > currentItem.Amount)
                        {
                            InventoryManager.Instance.ReduceAmount();
                            currentItem.AddAmount();
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        int reduceAmount = currentItem.Item.Capacity - currentItem.Amount;
                        if (reduceAmount >= item.Amount)
                        {
                            currentItem.SetAmount(currentItem.Amount + item.Amount);
                            InventoryManager.Instance.ReduceAmount(item.Amount);
                        }
                        else
                        {
                            currentItem.AddAmount(reduceAmount);
                            item.ReduceAmount(reduceAmount);
                        }
                    }
                }
                else
                {
                    Item itemCurrent = currentItem.Item;
                    int amount = currentItem.Amount;
                    currentItem.SetItem(item.Item, item.Amount);
                    InventoryManager.Instance.PickedItem.SetItem(itemCurrent, amount);
                }
                //if (Input.GetKey(KeyCode.LeftControl))//按下左CTRL时
                //{
                //    if(currentItem.Item.ID == item.Item.ID && currentItem.Item.Capacity > currentItem.Amount)
                //    {
                //        InventoryManager.Instance.ReduceAmount();
                //        currentItem.AddAmount();
                //    }
                //    else
                //    {
                //        return;
                //    }
                //}
                //else//没有按下左CTRL时
                //{
                //    if (currentItem.Item.ID == item.Item.ID && item.Item.Capacity>= pickedItemAmount)
                //    {
                //        currentItem.SetAmount(pickedItemAmount);
                //        InventoryManager.Instance.IsPickedItem = false;
                //        InventoryManager.Instance.PickedItem.Hide();
                //    }
                //    else if (currentItem.Item.ID == item.Item.ID && item.Item.Capacity < pickedItemAmount)
                //    {
                //        int reduceAmount = item.Item.Capacity - currentItem.Amount;
                //        currentItem.SetAmount(item.Item.Capacity);
                //        InventoryManager.Instance.ReduceAmount(reduceAmount);
                //    }
                //    else
                //    {
                //        int amountPicked = currentItem.Amount;
                //        GameObject pickedGameobject = Instantiate(ItemPrefab) as GameObject;
                //        pickedGameobject.transform.SetParent(this.transform);
                //        pickedGameobject.transform.GetComponent<ItemUI>().SetItem(item.Item,item.Amount);
                //        pickedGameobject.transform.localPosition = Vector3.zero;
                //        InventoryManager.Instance.PickupItem(currentItem.Item, currentItem.Amount);
                //        Destroy(currentItem.gameObject);
                //    }
                //}
            }
        }
        else
        {
            if(InventoryManager.Instance.IsPickedItem == true)//如果在这个背包格子没有物品时,我手上选中了物品
            {
                if (Input.GetKey(KeyCode.LeftControl))//按下CTRL时
                {
                    this.StoreItem(InventoryManager.Instance.PickedItem.Item);
                    InventoryManager.Instance.ReduceAmount();
                }
                else
                {
                    for(int i = 0; i < InventoryManager.Instance.PickedItem.Amount; i++)
                    {
                        this.StoreItem(InventoryManager.Instance.PickedItem.Item);
                    }
                    InventoryManager.Instance.ReduceAmount(InventoryManager.Instance.PickedItem.Amount);
                }
            }
        }
    }
}
