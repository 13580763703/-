using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Equipment : Item
{
    /// <summary>
    /// ����
    /// </summary>
    public int Strength;
    /// <summary>
    /// ����
    /// </summary>
    public int Intellect;
    /// <summary>
    /// ����
    /// </summary>
    public int Agility;
    /// <summary>
    /// ����
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
                equipType = "ͷ��";
                break;
            case EquipmentType.Neck:
                equipType = "����";
                break;
            case EquipmentType.Chest:
                equipType = "�ؼ�";
                break;
            case EquipmentType.Ring:
                equipType = "��ָ";
                break;
            case EquipmentType.Shoulder:
                equipType = "����";
                break;
            case EquipmentType.Leg:
                equipType = "����";
                break;
            case EquipmentType.Boots:
                equipType = "ѥ��";
                break;
            case EquipmentType.Trinket:
                equipType = "��Ʒ";
                break;
            case EquipmentType.Belt:
                equipType = "����";
                break;
            case EquipmentType.Bracer:
                equipType = "����";
                break;
            case EquipmentType.Glove:
                equipType = "����";
                break;
        }
        newtext = string.Format("{0}\n<color=blue>{1}</color>\n<color=orange><size=10>����:{2}\n����:{3}\n����:{4}\n����:{5}\n</size></color>", text,equipType,Strength,Intellect,Agility,Stamina);
        return newtext;
    }
}
