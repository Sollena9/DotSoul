using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemInfo : MonoBehaviour
{

    public List<int> itemData = new List<int>();
    public List<int> itemCount = new List<int>();
    public bool check  = true;


    private void Start()
    {
        check = GameManager.instance.GetItems(itemData, itemCount);

        Debug.Log(check);
        //itemList ���̺귯��(Enemy����) ���� itemData�� �Է�
    }

}
