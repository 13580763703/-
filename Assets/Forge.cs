using Defective.JSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Forge : Inventory
{
    #region
    private static Forge _instance;
    public static Forge Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = GameObject.Find("ForgePanel").GetComponent<Forge>();
            }
            return _instance;
        }
    }
    #endregion


    private List<Formula> formulaList;
    public override void Start()
    {
        base.Start();
        ParseFormulaJson();
    }

    void ParseFormulaJson()
    {
        formulaList = new List<Formula>();
        TextAsset formulaText = Resources.Load<TextAsset>("Formulas");
        string formulasJson = formulaText.text;
        JSONObject jo = new JSONObject(formulasJson);
        foreach(JSONObject temp in jo.list)
        {
            int Item1ID = (int)temp["Item1ID"].intValue;
            int Item1Amount = (int)temp["Item1Amount"].intValue;
            int Item2ID = (int)temp["Item2ID"].intValue;
            int Item2Amount = (int)temp["Item2Amount"].intValue;
            int ResID = (int)temp["ResID"].intValue;
            Formula formula = new Formula(Item1ID, Item1Amount, Item2ID, Item2Amount, ResID);
            formulaList.Add(formula);
        }
        //Debug.Log(formulaList[1].ResID);
    }
    
    public void ForgeItem()
    {
        List<int> haveMaterialIDList = new List<int>();
        foreach (Slot slot in slotLists)
        {
            if (slot.transform.childCount > 0)
            {
                ItemUI currentItemUI = slot.transform.GetChild(0).GetComponent<ItemUI>();
                for (int i = 0; i < currentItemUI.Amount; i++)
                {
                    haveMaterialIDList.Add(currentItemUI.Item.ID);
                }
            }
        }
        Formula matchedFormula = null;
        foreach(Formula formula in formulaList)
        {
            bool isMatch = formula.Match(haveMaterialIDList);
            if (isMatch)
            {
                matchedFormula = formula;
                break;
            }
        }
        if(matchedFormula != null)
        {
            Knapsack.Instance.StoreItem(matchedFormula.ResID);
            foreach(int id in matchedFormula.NeedList)
            {
                foreach(Slot slot in slotLists)
                {
                    if (slot.transform.childCount > 0)
                    {
                        ItemUI itemUI = slot.transform.GetChild(0).GetComponent<ItemUI>();
                        if(itemUI.Item.ID == id&& itemUI.Amount >= 0)
                        {
                            itemUI.ReduceAmount();
                            DestroyImmediate(itemUI.gameObject);
                            break;
                        }
                    }
                }
            }
        }
       
    }


}

