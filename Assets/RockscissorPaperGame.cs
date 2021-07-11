using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RockscissorPaperGame : MonoBehaviour
{
    public Text sysText;
    public Text inputText;

    public int computerNum;

    // Start is called before the first frame update
   public void EnterTheGame()
    {
        if(inputText.text == "가위, 바위, 보")
        {
            sysText.text = "말좀들어라 제발";
        }


        else if (inputText.text == "가위" || inputText.text == "바위" || inputText.text == "보")
        {
            Debug.Log("shobu");
            Shobu();
        }

        else
        {
            sysText.text = "가위, 바위, 보 중에 하나만 입력이 가능합니다.";

        }

    }

    void Shobu()
    {
        computerNum = Random.Range(1, 4);

        if (inputText.text == "가위")
        {
            switch(computerNum)
            {
                case 1:
                    sysText.text = "비겼습니다 후훗 " + computerNum;
                    break;
                case 2:
                    sysText.text = "졌습니다. 크큭" + computerNum;
                    break;
                case 3:
                    sysText.text = "이겼습니다. 뽈롱" + computerNum;
                    break;
            }
        }

        else if (inputText.text == "바위")
        {
            switch (computerNum)
            {
                case 1:
                    sysText.text = "이겼습니다. 뽈롱" + computerNum;
                    break;
                case 2:
                    sysText.text = "비겼습니다 후훗" + computerNum;
                    break;
                case 3:
                    sysText.text = "졌습니다. 크큭" + computerNum;
                    break;
            }
        }
        else
        {
            switch (computerNum)
            {
                case 1:
                    sysText.text = "졌습니다. 크큭" + computerNum;
                    break;
                case 2:
                    sysText.text = "이겼습니다. 뽈롱" + computerNum;
                    break;
                case 3:
                    sysText.text = "비겼습니다 후훗" + computerNum;
                    break;
            }
        }
    }

}

