using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemInfo : MonoBehaviour
{

    public List<int> itemNum = new List<int>();
    public List<int> itemCount = new List<int>();
    public bool check  = true;


    private void Start()
    {
        check = GameManager.instance.GetItems(itemNum, itemCount);

        Debug.Log(check);
    }

}
