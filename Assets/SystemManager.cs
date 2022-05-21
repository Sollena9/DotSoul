using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemManager : MonoBehaviour
{

    public float getSouls;

    [SerializeField]
    private Vector3 startPosition;

    public GameObject youDiedUI;

    public Vector2[] playerFallPos = new Vector2[2];
    public float fallValue;
    private Player_Movement thePlayer;
    public int dieNum;
    private IEnumerator fallCoroutine;

    [SerializeField]
    private Slider hp;

    [SerializeField]
    private Slider sp;

    [SerializeField]
    private Slider fp;

    [SerializeField]
    private Text addSouls;

    [SerializeField]
    private Text saveSouls;


    private void Awake()
    {
        //LoadGame();

    }


    void Start()
    {
        thePlayer = FindObjectOfType<Player_Movement>();
        startPosition = thePlayer.transform.position;
        fallCoroutine = FalldownChekcer();

        saveSouls.text = thePlayer.data.soul.ToString();
    }

    public virtual void RestartGame()
    {
        thePlayer.transform.position = startPosition;
        FullRecovery();
        StartCoroutine(fallCoroutine);


    }

    IEnumerator FalldownChekcer()
    {
        Debug.Log("sadfasdf");
        playerFallPos[0] = thePlayer.transform.position;

        yield return new WaitForSecondsRealtime(2f);

        playerFallPos[1] = thePlayer.transform.position;

        if (!thePlayer.isGrounded && (playerFallPos[0].y - playerFallPos[1].y) > fallValue)
        {
            StartCoroutine(PrintYouDied());
            StopCoroutine(fallCoroutine);
        }
        else
            StartCoroutine(FalldownChekcer());
    }


    public IEnumerator PrintYouDied()
    {
        youDiedUI.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);

        if (dieNum < 10)
        {
            Debug.Log("11");
            dieNum++;
            youDiedUI.transform.localScale += new Vector3(0.01f, 0.01f, 0f);
            StartCoroutine(PrintYouDied());
        }

        else if (dieNum >= 10)
        {
            youDiedUI.transform.localScale = new Vector3(0, 0, 0);
            youDiedUI.gameObject.SetActive(false);
            RestartGame();
            StopCoroutine(PrintYouDied());


        }

    }



    public void SaveCoubul()
    //커불. 위치 세이브
    {
        startPosition = thePlayer.transform.position;
        FullRecovery();
    }


    public virtual void ChangeH_S_FP(int kind, float value)
    //Hp(0), Fp(1), Sp(2) 증감하는 함수
    {
        switch (kind)
        {
            case 0:
                hp.value += value;
                //thePlayer.HP = hp.value;
                break;

            case 1:
                fp.value += value;
                //thePlayer.FP = fp.value;
                break;

            case 2:
                sp.value += value;
                thePlayer.data.SP = sp.value;
                break;
        }

    }


    private void StartGame()
    {
        FullRecovery();
        thePlayer.transform.position = startPosition;


    }

    public virtual void FullRecovery()
    //hp, fp, sp 풀 회복 -> 정보 Load 후에 실행 해야함.
    {
        //수정 필요 
        hp.value = hp.maxValue;
        fp.value = fp.maxValue;
        sp.value = sp.maxValue;

        //에스트 개수를 풀로 회복
        //내가 인벤에 소지하고 있는 아이템의 개수를 풀로 채워줌 -> 창고의 아이템은 차감.
    }



   

    public void IndecreaseSoul(float souls)
    {
        getSouls = souls;
        //getSoul에 인수로 받은 souls를 대입 -> getSoul은 증감하는 soul 값을 임시로 저장하는 변수
        addSouls.gameObject.SetActive(true);
        //addSouls는 증감하는 소울의 양을 출력하는 텍스트 UI, 증감했기 때문에 Active상태로 변경
        addSouls.text = getSouls.ToString();
        //Active True로 한 addSouls의 게임 오브젝트의 텍스트 값을 Tostring으로 Float -> Stirng 변환한 뒤 getSoul로 대입

        StartCoroutine(SoulText(1));
        //addSoul의 크기 변환 // +1 은 크게
        StartCoroutine(SoulCalculate(thePlayer.data.soul + getSouls, thePlayer.data.soul));
        //증감하는 소울의 양을 계산
    }

    private IEnumerator SoulText(int state)
    //증감하는 소울 텍스트 게임오브젝트의 크기를 바꾸는 함수
    {

        for (int i = 0; i <= 10; i++) //10번 반복함
        {
            if (state > 0) //증감이 되는 상태일때 크기를 0.01씩 10번 키움
            {
                saveSouls.transform.localScale += new Vector3(0.01f, 0.01f, 0);
            }
            else if (state < 0) //증감 계산이 끝나면 크기를 0.01씩 10번 줄임
            {
                saveSouls.transform.localScale -= new Vector3(0.01f, 0.01f, 0);
            }

            yield return new WaitForSecondsRealtime(0.01f);

        }



    }


    IEnumerator SoulCalculate(float target, float current)
    //target은 soul + getsoul
    //current는 soul
    {

        float duration = 0; // 카운팅에 걸리는 시간 설정. 

        if (getSouls < 10000) //증감되는 소울이 10000보다 작으면 카운팅에 1초가 걸리고 더 크면 2.5초가 걸림
        {
            duration = 1f;
        }
        else
            duration = 2.5f;

        float offset = (target - current) / duration; //증감된 소울에서 카운팅에 걸리는 시간을 나눠서 일정한 시간이 지날 때마다 얼마나 올라갈지 평균값을 구함



        while (current < target) //내가 현재 가지고 있는 소울이 최종적으로 얻을 소울보다 클 때 까지 계속 반복

        {

            current += offset * Time.deltaTime; //현재 가지고 있는 소울에 전에 구한 평균값을 더함

            saveSouls.text = ((int)current).ToString(); //내가 가진 소울을 정수로 바꾸고 그걸 문자열로 바꾸고 그걸 소울 UI에 대입함

            yield return null; //지연시키지 않음

        }


        StartCoroutine(SoulText(-1)); //addSouls의 크기를 줄임
        current = target; //현재 소울을 원래 지정된 값으로 맞춤
        thePlayer.data.soul = (int)current; //현재 소울을 실제 내가 가진 소울 변수에 대입함
        getSouls = 0; //증감할 소울 0으로 초기화함
        addSouls.gameObject.SetActive(false); //증감이 끝났기 때문에 Active를 false로 변경함
        saveSouls.text = ((int)current).ToString("0"); //지정된 값으로 맞춰진 현재 소울을 정수로 바꾸고 그걸 문자열로 바꾸고 그걸 소울 UI에 대입함


    }


}

