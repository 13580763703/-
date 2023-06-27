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
}
    

