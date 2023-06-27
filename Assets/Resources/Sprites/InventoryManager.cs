using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    #region ����ģʽ
    private static InventoryManager _instance;

    public static InventoryManager Instance
    {
        get 
        { 
            if (_instance == null)
            {
                //ֻ��ִ��һ��
                _instance = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
            }
            return _instance; 
        }
    }
    #endregion

    private List<Item> itemList;

    void ParseItemJson()
    {
        itemList = new List<Item>();
        TextAsset itemText = Resources.Load<TextAsset>("Items.json");
        string itemsJson = itemText.text;
    }
}
