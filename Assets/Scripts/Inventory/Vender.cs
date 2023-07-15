using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Vender : Inventory
{
    #region µ¥ÀýÄ£Ê½
    private static Vender _instance;
    public static Vender Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = GameObject.Find("VenderPanel").GetComponent<Vender>(); ;
            }
            return _instance;
        }
    }
    #endregion
    public int[] itemArray;
    private Player player;

    public override void Start()
    {
        base.Start();
        InitShop();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Hide();
    }

    public void InitShop()
    {
        foreach(var item in itemArray)
        {
            StoreItem(item);
        }
    }

    public void BuyItem(Item item)
    {
        bool isSuccess = player.ConsumeCoin(item.Buyprice);
        if (isSuccess)
        {
            Knapsack.Instance.StoreItem(item);
        }
    }

    public void SellItem()
    {
        int sellAmount = 1;
        if (Input.GetKey(KeyCode.LeftControl))
        {
            sellAmount = 1;
        }
        else
        {
            sellAmount = InventoryManager.Instance.PickedItem.Amount;
        }

        int coinAmount = InventoryManager.Instance.PickedItem.Item.Sellprice * sellAmount;
        player.EarnCoin(coinAmount);
        InventoryManager.Instance.ReduceAmount(sellAmount);
    }
}
