using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScratchNote : MonoBehaviour
{
    public int PowerAde;

    public int randomNum;
    public int inputNum;
    //숫자로 변환
    public Text inputText;
    //입력받은 텍스트

    public int maxNum;
    public int minNum;

    public Text gameText;

    // Start is called before the first frame update
    void Start()
    {


        PowerAde = 10;
        //StartCoroutine(GayJoyGo());

        randomNum = Random.Range(0, 101);
        maxNum = 101;
        minNum = 0;


    }


    IEnumerator GayJoyGo()
    {
        while (PowerAde > 0)
        {
            Debug.Log("벽에 파란 병 " + PowerAde + "개가 있네.");
            PowerAde--;
            Debug.Log("만약에 그 중 한 병이 갑자기 떨어지면, 이제는 벽에 파란 병 " + PowerAde + "개가 있네.");
            if (PowerAde == 1)
            {
                Debug.Log("1병의 파워에이드가 벽장에 있네, 1병의 파워에이드라네. 그것을 내려서 넘겼네, 더 이상 벽장에 파워에이드가 없네.");
            }
            if (PowerAde == 0)
            {
                Debug.Log("더 이상 벽장에 파워에이드가 없네, 파워에이드는 더 이상 없다네.");
                Debug.Log("더 이상 벽장에 파워에이드가 없네, 파워에이드는 더 이상 없다네.");
            }
        }

        yield return new WaitForSeconds(5f);
        PowerAde = 10;
        StartCoroutine(GayJoyGo());
    }



    public void RandomChecker()
    {
        inputNum = int.Parse(inputText.text);
        if (inputNum == randomNum)
        {
            gameText.text = "정답입니다! 새 게임을 시작합니다";
            Debug.Log(inputText.text);
            inputNum = 0;
            randomNum = Random.Range(0, 101);
            maxNum = 101;
            minNum = 0;

        }

        else if (inputNum <= minNum || inputNum >= maxNum)
        {
            gameText.text = "허용 범위를 벗어났습니다!";
        }

        else if (inputNum == minNum || inputNum == maxNum)
        {
            gameText.text = "최소/최대 값을 제외한 값을 입력해주세요!";
        }

        else if (inputNum < randomNum)
        {
            gameText.text = "업!";
            minNum = inputNum;
        }

        else if (inputNum > randomNum)
        {
            gameText.text = "다운!";
            maxNum = inputNum;
        }
    }

}
