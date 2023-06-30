using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
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
}
