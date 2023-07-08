using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.Port;
using static Weapon;
/// <summary>
/// ≤ƒ¡œ¿‡
/// </summary>
public class Material : Item
{
    public Material(int id, string name, string description, ItemType type, ItemQuality quality, int capacity, int buyprice, int sellprice, string sprite):base(id, name, description, type, quality, capacity, buyprice, sellprice, sprite)
    {
        

    }
}
