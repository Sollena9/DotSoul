using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class PlayerCooltimeManager : MonoBehaviour
{
    // Start is called before the first frame update


    public Image[] skillFilter = new Image[2];
    public TextMeshProUGUI[] coolTimeCounter = new TextMeshProUGUI[2];
    public float[] coolTime = new float[2];
    [SerializeField]
    private float[] currentCoolTime = new float[2];
    [SerializeField]
    public bool[] canUseSkill = new bool[2];




    private void Start()
    {

        SetSkill();



    }
    public void SetSkill()
    {
        skillFilter[0].fillAmount = 0; //처음에 스킬 버튼을 가리지 않음
        canUseSkill[0] = true;
        var time = GetComponent<Player_Movement>().data.coolTime_Roll;
        coolTime[0] = time;
        currentCoolTime[0] = time;
    }


    public void UseSkill(int num)
    {
        if (canUseSkill[num])
        {
            skillFilter[num].fillAmount = 1; //스킬 버튼을 가림
            coolTimeCounter[num].gameObject.SetActive(true);
            StartCoroutine("Cooltime",num);

            currentCoolTime[num] = coolTime[num];
            coolTimeCounter[num].text = "" + currentCoolTime[num];

            StartCoroutine("CoolTimeCounter", num);

            canUseSkill[num] = false; //스킬을 사용하면 사용할 수 없는 상태로 바꿈
        }
        else
        {
            Debug.Log("아직 스킬을 사용할 수 없습니다.");
        }


    }

    private IEnumerator Cooltime(int num)
    {

        while (skillFilter[num].fillAmount > 0)
        {
            skillFilter[num].fillAmount -= 1 * Time.smoothDeltaTime / coolTime[num];

            yield return null;
        }

        canUseSkill[num] = true; //스킬 쿨타임이 끝나면 스킬을 사용할 수 있는 상태로 바꿈
        coolTimeCounter[num].gameObject.SetActive(false);
        yield break;
    }

    //남은 쿨타임을 계산할 코르틴을 만들어줍니다.
    private IEnumerator CoolTimeCounter(int num)
    {

        while (currentCoolTime[num] > 0)
        {
            yield return new WaitForSeconds(1.0f);

            currentCoolTime[num] -= 1.0f;
            coolTimeCounter[num].text = "" + currentCoolTime[num];

        }

        yield break;
    }
}

