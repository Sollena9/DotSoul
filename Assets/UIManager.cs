using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public GameObject[] bonfireSelect = new GameObject[3];
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
                fire.gameObject.transform.position = new Vector3(bonfireSelect[0].transform.position.x + 27f,
                    bonfireSelect[0].transform.position.y, bonfireSelect[0].transform.position.z);
            break;

            case 1:
                fire.gameObject.transform.position = new Vector3(bonfireSelect[1].transform.position.x + 27f,
                    bonfireSelect[1].transform.position.y, bonfireSelect[1].transform.position.z);
                break;

            case 2:
                fire.gameObject.transform.position = new Vector3(bonfireSelect[2].transform.position.x + 27f,
                     bonfireSelect[2].transform.position.y, bonfireSelect[2].transform.position.z);
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
