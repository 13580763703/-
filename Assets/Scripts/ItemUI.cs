using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public Item Item { get; set; }
    public int Amount { get; set; }

    private Image itemImage;
    private TMP_Text amountText;

    private Image ItemImage
    {
        get
        {
            if (itemImage == null)
            {
                itemImage=GetComponent<Image>();
            }
            return itemImage;
        }
    }

    private TMP_Text AmountText
    {
        get
        {
            if(amountText == null)
            {
                amountText = GetComponentInChildren<TMP_Text>();
            }
            return amountText;
        }
        
    }

    public void SetItem(Item item,int Amount =1)
    {
        this.Item = item;
        this.Amount = Amount;
        ItemImage.sprite = Resources.Load<Sprite>(item.Sprite);
        AmountText.text = Amount.ToString();
    }

    public void AddAmount(int amount=1)
    {
        this.Amount+=amount;
        AmountText.text = Amount.ToString();
    }
}
