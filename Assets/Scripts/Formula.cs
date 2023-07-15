using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Formula
{
    public int Item1ID { get; private set; }
    public int Item1Amount { get; private set; }
    public int Item2ID { get; private set; }
    public int Item2Amount { get; private set; }

    public int ResID { get; private set; }//锻造的物品

    private List<int> needList = new List<int>();
    public List<int> NeedList { get { return needList; } }

    public Formula(int item1ID, int item1Amount, int item2ID, int item2Amount, int resID)
    {
        Item1ID = item1ID;
        Item1Amount = item1Amount;
        Item2ID = item2ID;
        Item2Amount = item2Amount;
        ResID = resID;

        for(int i = 0; i < Item1Amount; i++)
        {
            needList.Add(Item1ID);
        }
        for(int i = 0; i < item2Amount; i++)
        {
            needList.Add(Item2ID);
        }
    }

    public bool Match(List<int> idList)
    {
        List<int> tempList = new List<int>(idList);
        foreach(int id in needList)
        {
            bool isSuccess = tempList.Remove(id);
            if (isSuccess == false)
            {
                return false;
            }
        }
        return true;
    }
}
