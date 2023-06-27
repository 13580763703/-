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
        MainHand,
        OffHand
    }
}
