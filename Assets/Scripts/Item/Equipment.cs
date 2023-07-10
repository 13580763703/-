using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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
        None,
        Head,
        Neck,
        Chest,
        Ring,
        Shoulder,
        Leg,
        Boots,
        Trinket,
        Belt,
        Bracer,
        Glove
    }

    public override string GetToolTipText()
    {
        string text = base.GetToolTipText();
        string newtext = "";
        string equipType = "";
        switch (EquipType)
        {
            case EquipmentType.Head:
                equipType = "头盔";
                break;
            case EquipmentType.Neck:
                equipType = "项链";
                break;
            case EquipmentType.Chest:
                equipType = "胸甲";
                break;
            case EquipmentType.Ring:
                equipType = "戒指";
                break;
            case EquipmentType.Shoulder:
                equipType = "护肩";
                break;
            case EquipmentType.Leg:
                equipType = "护腿";
                break;
            case EquipmentType.Boots:
                equipType = "靴子";
                break;
            case EquipmentType.Trinket:
                equipType = "饰品";
                break;
            case EquipmentType.Belt:
                equipType = "腰带";
                break;
            case EquipmentType.Bracer:
                equipType = "护腕";
                break;
            case EquipmentType.Glove:
                equipType = "手套";
                break;
        }
        newtext = string.Format("{0}\n<color=blue>{1}</color>\n<color=orange><size=10>力量:{2}\n智力:{3}\n敏捷:{4}\n体力:{5}\n</size></color>", text,equipType,Strength,Intellect,Agility,Stamina);
        return newtext;
    }
}
