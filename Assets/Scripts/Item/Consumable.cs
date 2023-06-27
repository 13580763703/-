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
}
