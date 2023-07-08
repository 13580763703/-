using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ItemUI : MonoBehaviour
{
    #region Data
    public Item Item { get; private set; }
    public int Amount { get;private set; }
    #endregion
    
    #region ItemUI
    private Image itemImage;
    private TMP_Text amountText;
    #endregion

    private float targetScale = 0.6f;
    private Vector3 animationScale = new Vector3(0.8f, 0.8f, 0.8f);
    private float smoothing = 4f;

    private void Update()
    {
        if(transform.localScale.x != targetScale)
        {
            float scale = Mathf.Lerp(transform.localScale.x, targetScale, smoothing*Time.deltaTime);
            transform.localScale = new Vector3(scale, scale, scale);
            if(Mathf.Abs(transform.localScale.x-targetScale) < .02f)
            {
                transform.localScale = new Vector3(targetScale, targetScale, targetScale);
            }
        }
    }

    private Image ItemImage
    {
        get
        {
            if (itemImage == null)
            {
                itemImage = GetComponent<Image>();
            }
            return itemImage;
        }
    }

    private TMP_Text AmountText
    {
        get
        {
            if (amountText == null)
            {
                amountText = GetComponentInChildren<TMP_Text>();
            }
            return amountText;
        }

    }

    public void SetItem(Item item, int amount = 1)
    {
        transform.localScale = animationScale;
        this.Item = item;
        this.Amount = amount;
        ItemImage.sprite = Resources.Load<Sprite>(item.Sprite);
        if (item.Capacity > 1)
        {
            AmountText.text = Amount.ToString();
        }
        else
        {
            AmountText.text = "";
        }
    }

    public void AddAmount(int amount = 1)
    {
        transform.localScale = animationScale;
        this.Amount += amount;
        //updateui
        if (Item.Capacity > 1)
        {
            AmountText.text = Amount.ToString();
        }
        else
        {
            AmountText.text = "";
        }
    }

    public void ReduceAmount(int amount = 1)
    {
        transform.localScale = animationScale;
        this.Amount -= amount;
            //updateui
        if (Item.Capacity > 1)
        {
            AmountText.text = Amount.ToString();
        }
        else
        {
            AmountText.text = "";
        }
    }

    public void SetAmount(int amount)
    {
        transform.localScale = animationScale;
        this.Amount = amount;
        AmountText.text = Amount.ToString();
    }

    public void SetLocalPosition(Vector3 position)
    {
        transform.localPosition = position;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
