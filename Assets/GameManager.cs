    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }





    public bool GetItems(List<int> itemNum, List<int> itemCount)
    {
        for(int i = 0; i < itemNum.Count; i++)
        {
            Debug.Log(itemNum[i] + " // " + itemCount);
        }

        return false;

    }
}
