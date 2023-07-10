using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Equipment;

public class Weapon : Item
{
    public int Damage { get; set; }
    public WeaponType WpType { get; set; }

    public Weapon(int id, string name, string description, ItemType type, ItemQuality quality, int capacity, int buyprice, int sellprice,string sprite, int damage,WeaponType wpType) : base(id, name, description, type, quality, capacity, buyprice, sellprice,sprite)
    {
        this.Damage = damage;
        this.WpType = wpType;

    }

    public enum WeaponType
    {
        None,
        MainHand,
        OffHand
    }

    public override string GetToolTipText()
    {
        string text = base.GetToolTipText();
        string newtext = "";
        string weapon = "";
        switch (WpType)
        {
            case WeaponType.MainHand:
                weapon = "Ö÷ÎäÆ÷";
                break;
            case WeaponType.OffHand:
                weapon = "¸±ÎäÆ÷";
                break;
        }
        newtext = string.Format("{0}\n<color=blue>{1}</color>\n<color=orange><size=10>¹¥»÷Á¦:{2}</size></color>\n", text,weapon,Damage);
        return newtext;
    }
}
