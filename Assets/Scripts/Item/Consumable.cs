using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : Item
{
    public int HP;
    public int MP;
    
    public Consumable(int id, string name, string description, ItemType type, ItemQuality quality, int capacity, int buyprice, int sellprice,string sprite,int hp,int mp) : base(id, name, description, type, quality, capacity, buyprice, sellprice,sprite)
    {
        this.HP = hp;
        this.MP = mp;
    }

    public override string ToString()
    {
        string s = "";
        s += ID.ToString();
        s += Type;
        s += Name;
        s += Description;
        s += Quality;
        s += Capacity;
        s += Buyprice;
        s += Sellprice;
        s += Sprite;
        s += HP;
        s += MP;
        return s;
    }
}
