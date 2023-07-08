using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            int id = Random.Range(1, 3);
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
    }
}
