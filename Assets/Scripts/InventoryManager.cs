using Defective.JSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    #region 单例模式
    private static InventoryManager _instance;

    public static InventoryManager Instance
    {
        get 
        { 
            if (_instance == null)
            {
                //只会执行一次
                _instance = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
            }
            return _instance; 
        }
    }
    #endregion

    private List<Item> itemList;

    private void Start()
    {
        ParseItemJson();
    }
    /// <summary>
    /// 解析物品信息
    /// </summary>
    void ParseItemJson()
    {
        itemList = new List<Item>();
        TextAsset itemText = Resources.Load<TextAsset>("Items");
        string itemsJson = itemText.text;
        JSONObject j = new JSONObject(itemsJson);
        foreach(JSONObject temp in j.list)
        {
            string typeStr = temp["type"].stringValue;
            Item.ItemType type = (Item.ItemType)System.Enum.Parse(typeof(Item.ItemType), typeStr);
            
            int id = int.Parse(temp["id"].ToString());
            string name = temp["name"].stringValue;
            Item.ItemQuality quality = (Item.ItemQuality)System.Enum.Parse(typeof(Item.ItemQuality), temp["quality"].stringValue);
            string description = temp["description"].stringValue;
            int capacity = int.Parse(temp["capacity"].ToString());
            int buyPrice = int.Parse(temp["buyPrice"].ToString());
            int sellPrice = int.Parse(temp["sellPrice"].ToString());
            string sprite = temp["sprite"].stringValue;

            Item item = null;
            switch (type)
            {
                case Item.ItemType.Consumable:
                    int hp = int.Parse(temp["hp"].ToString());
                    int mp = int.Parse(temp["mp"].ToString());
                    item = new Consumable(id, name, description, type, quality, capacity, buyPrice, sellPrice, sprite, hp, mp);
                    break;
                case Item.ItemType.Equipment:
                    break;
                case Item.ItemType.Weapon:
                    break;
                case Item.ItemType.Material:
                    break;
            }
            itemList.Add(item);
            //Debug.Log(item);
        }
    }

    public Item GetItemById(int id)
    {
        foreach(Item item in itemList)
        {
            if (item.ID == id)
                return item;
        }
        return null;
    }
}
