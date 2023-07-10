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
    /// ��item������������,
    /// ��������Ѿ���item����amount++
    /// ���û��,�͸���Itemprefabȥʵ����һ��item��������.
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
    /// ��õ�ǰ��Ʒ�۵���Ʒ����
    /// </summary>
    /// <returns></returns>
    public Item.ItemType GetItemType()
    {
        return transform.GetChild(0).GetComponent<ItemUI>().Item.Type;
    }
    /// <summary>
    /// �õ���ǰ��Ʒ��id
    /// </summary>
    /// <returns></returns>
    public int GetItemID()
    {
        return transform.GetChild(0).GetComponent<ItemUI>().Item.ID;
    }
    /// <summary>
    /// ���ͬ���͵�item�������Ʒ�����Ƿ���ڶ��ֵ.
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

        if(transform.childCount > 0)//�����������������Ʒ����ʱ
        {
            ItemUI currentItem = transform.GetChild(0).GetComponent<ItemUI>();

            if(InventoryManager.Instance.IsPickedItem == false)//ûѡ���κ���Ʒʱ
            {
                if (Input.GetKey(KeyCode.LeftControl))//������CTRLʱ
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
                    InventoryManager.Instance.PickupItem(currentItem.Item, currentItem.Amount);//�ѵ�ǰ��Ʒ�۵Ķ������Ƶ�pickedItem��
                    Destroy(currentItem.gameObject);//���ٵ�ǰ��Ʒ
                }
            }
            else//ѡ����Ʒʱ
            {
                ItemUI item = InventoryManager.Instance.PickedItem;//item�����ʱѡ�е���Ʒ
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
                //if (Input.GetKey(KeyCode.LeftControl))//������CTRLʱ
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
                //else//û�а�����CTRLʱ
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
            if(InventoryManager.Instance.IsPickedItem == true)//����������������û����Ʒʱ,������ѡ������Ʒ
            {
                if (Input.GetKey(KeyCode.LeftControl))//����CTRLʱ
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
