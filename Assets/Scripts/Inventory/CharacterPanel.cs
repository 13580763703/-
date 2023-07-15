using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterPanel : Inventory
{
    #region 单例模式
    private static CharacterPanel _instance;
    public static CharacterPanel Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("CharacterPanel").GetComponent<CharacterPanel>();
            }
            return _instance;
        }
    }
    #endregion
    private TMP_Text propertyText;
    private Player player;

    public override void Start()
    {
        base.Start();
        propertyText = transform.Find("PropertyPanel/Text").GetComponent<TMP_Text>();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        UpdatePropertyText();
        Hide();
    }


    public void PutOn(Item item)
    {
        Item exitItem = null;
        foreach(Slot slot in slotLists)
        {
            EquipmentSlot equipmentSlot = (EquipmentSlot)slot;
            if (equipmentSlot.IsRightItem(item))
            {
                if (equipmentSlot.transform.childCount > 0)
                {
                    ItemUI currentItem = equipmentSlot.transform.GetChild(0).GetComponent<ItemUI>();
                    exitItem = currentItem.Item;
                    currentItem.SetItem(item,1);
                    //equipmentSlot.transform.GetChild(0).GetComponent<ItemUI>().SetItem(item, 1);
                }
                else
                {
                    equipmentSlot.StoreItem(item);
                }
                break;
            }
        }
        if (exitItem != null)
        {
            Knapsack.Instance.StoreItem(exitItem);
        }
        UpdatePropertyText();
    }

    public void PutOff(Item item)
    {
        Knapsack.Instance.StoreItem(item);
        UpdatePropertyText();
    }

    private void UpdatePropertyText()
    {
        int strength = 0, intellect = 0, agility = 0, stamina = 0, damage = 0;
        foreach(EquipmentSlot slot in slotLists)
        {
            if (slot.transform.childCount > 0)
            {
                Item item = slot.transform.GetChild(0).GetComponent<ItemUI>().Item;
                if(item is Equipment)
                {
                    Equipment e = (Equipment)item;
                    strength += e.Strength;
                    intellect += e.Intellect;
                    agility += e.Agility;
                    stamina += e.Stamina;
                }
                else if(item is Weapon)
                {
                    Weapon w = (Weapon)item;
                    damage += w.Damage;
                }
            }
        }
        strength += player.BasicStrength;
        intellect += player.BasicIntellect;
        agility += player.BasicAgility;
        stamina += player.BasicStamina;
        damage += player.BasicDamage;
        string text = string.Format("力量:{0}\n智力:{1}\n敏捷:{2}\n体力:{3}\n攻击力:{4}", strength, intellect, agility, stamina, damage);
        propertyText.text = text;
    }


}
