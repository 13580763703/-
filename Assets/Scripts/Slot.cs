using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
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
}
