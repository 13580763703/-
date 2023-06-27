using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : Item
{
    /// <summary>
    /// 力量
    /// </summary>
    public int Strength;
    /// <summary>
    /// 智力
    /// </summary>
    public int Intellect;
    /// <summary>
    /// 敏捷
    /// </summary>
    public int Agility;
    /// <summary>
    /// 体力
    /// </summary>
    public int Stamina;

    public EquipmentType EquipType { get; set; }

    public Equipment(int id, string name, string description, ItemType type, ItemQuality quality, int capacity, int buyprice, int sellprice,string sprite,int strength,int intellect,int agility,int stamina,EquipmentType equipType):base(id, name, description, type, quality, capacity, buyprice, sellprice,sprite)
    {
        this.Strength = strength;
        this.Intellect = intellect;
        this.Agility = agility;
        this.Stamina = stamina;
        this.EquipType = equipType;
    }

    public enum EquipmentType
    {
        Head,
        Neck,
        Ring,
        Shoulder,
        Leg,
        Boots,
        Trinket,
        OffHand,
        Belt,
        Bracer
    }
}
