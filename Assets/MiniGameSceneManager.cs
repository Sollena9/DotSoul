using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameSceneManager : MonoBehaviour
{

    public int sceneCheckNum;


    public void ChangeScene()
    {
        switch(sceneCheckNum)
        {
            case 0:
                SceneManager.LoadScene("RockGame");
                break;
            case 1:
                SceneManager.LoadScene("UpDownGame");
                break;
            case 2:
                SceneManager.LoadScene("MiniGameLobby");
                break;


        }
    }



    
}
