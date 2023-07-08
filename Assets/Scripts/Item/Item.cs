using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 物品基类
/// </summary>
public class Item
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ItemType Type { get; set; }
    public ItemQuality Quality { get; set; }
    public int Capacity { get; set; }
    public int Buyprice { get; set; }
    public int Sellprice { get; set; }
    public string Sprite { get; set; }

    public Item()
    {
        this.ID = -1;
    }

    public Item(int id, string name, string description, ItemType type, ItemQuality quality, int capacity, int buyprice, int sellprice, string sprite)
    {
        this.ID = id;
        this.Name = name;
        this.Description = description;
        this.Type = type;
        this.Quality = quality;
        this.Capacity = capacity;
        this.Buyprice = buyprice;
        this.Sellprice = sellprice;
        this.Sprite = sprite;
    }



    /// <summary>
    /// 物品类型
    /// </summary>
    public enum ItemType
    {
        Consumable,
        Equipment,
        Weapon,
        Material
    }
    /// <summary>
    /// 物品质量
    /// </summary>
    public enum ItemQuality
    {
        Common,
        Unmmon,
        Rare,
        Epic,
        Legendary,
        Artifact
    }
    /// <summary>
    /// 提示面板该显示什么内容
    /// </summary>
    /// <returns></returns>
    public virtual string GetToolTipText()
    {
        string color = "";
        switch (Quality)
        {
            case ItemQuality.Common:
                color = "white";
                break;
            case ItemQuality.Unmmon:
                color = "green";
                break;
            case ItemQuality.Rare:
                color = "blue";
                break;
            case ItemQuality.Epic:
                color = "purple";
                break;
            case ItemQuality.Legendary:
                color = "orange";
                break;
            case ItemQuality.Artifact:
                color = "red";
                break;
        }

        string text = string.Format("<color={4}>{0}</color>\n<size=10>说明:{1}</size>\n<size=10>购买价格:{2}  售出价格{3}</size>",Name,Description,Buyprice,Sellprice,color);

        return text;
    }
}
    

