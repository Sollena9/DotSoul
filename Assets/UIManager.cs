using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public Vector3[] bonfireSelect = new Vector3[3];
    public GameObject bonfire;
    public GameObject bonfireUI;
    public bool bonfireUITrigger;





    public void MoveBonfireSeletor()
    {
        var selNum = bonfire.GetComponent<Bonfire>().selectNum;
        var fire = bonfireUI.transform.GetChild(0).gameObject;

        switch (selNum)
        {
            case 0:
                fire.gameObject.transform.position = bonfireSelect[0];
            break;

            case 1:
                fire.gameObject.transform.position = bonfireSelect[1];
            break;

            case 2:
                fire.gameObject.transform.position = bonfireSelect[2];
            break;

        }

    }


    public void ShowBonFireUI(int i)
    {

        if (!bonfireUITrigger && i == 1)
        {
            bonfireUI.SetActive(true);
            bonfireUITrigger = true;

        }

        else
        {
            bonfireUI.SetActive(false);
            bonfireUITrigger = false;
            MoveBonfireSeletor();

        }
    }

}
