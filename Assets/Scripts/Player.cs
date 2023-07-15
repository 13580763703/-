using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    #region basic priperty
    private int basicStrength = 10;
    private int basicIntellect = 10;
    private int basicAgility = 10;
    private int basicStamina = 10;
    private int basicDamage = 10;

    public int BasicStrength
    {
        get { return basicStrength; }
    }

    public  int BasicIntellect
    {
        get { return basicIntellect; }
    }

    public int BasicAgility
    {
        get { return basicAgility; }
    }

    public int BasicStamina
    {
        get { return basicStamina; }
    }

    public int BasicDamage
    {
        get { return basicDamage; }
    }

    #endregion

    private int coinAmount = 100;
    private TMP_Text coinText;

    private void Start()
    {
        coinText = GameObject.Find("Coin").GetComponentInChildren<TMP_Text>();
        coinText.text = coinAmount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            int id = Random.Range(1, 18);
            Knapsack.Instance.StoreItem(id);
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Knapsack.Instance.DisplaySwitch();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Chest.Instance.DisplaySwitch();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            CharacterPanel.Instance.DisplaySwitch();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Vender.Instance.DisplaySwitch();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Forge.Instance.DisplaySwitch();
        }
    }
    /// <summary>
    /// Ïû·Ñ
    /// </summary>
    /// <param name="amount"></param>
    /// <returns></returns>
    public bool ConsumeCoin(int amount)
    {
        if(coinAmount >= amount)
        {
            this.coinAmount -= amount;
            coinText.text = coinAmount.ToString();
            return true;
        }
        return false;
    }
    /// <summary>
    /// ×¬Ç®
    /// </summary>
    /// <param name="amount"></param>
    public void EarnCoin(int amount)
    {
        this.coinAmount += amount;
        coinText.text = coinAmount.ToString();
    }
}
