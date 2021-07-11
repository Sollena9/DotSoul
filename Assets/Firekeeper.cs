using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Firekeeper : MonoBehaviour
{
    public GameObject txtBox;
    public GameObject commKey;
    public GameObject soulTxt;

    public bool hitPlayer;

    public List<string> words;
    public float sayWaitTime;
    public int crapSelNum;

    public float levNeedSoul;
    public float levIncreaseNum;
    private bool leveling;


    private Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        //헛소리 코루틴 시작
        StartCoroutine(FireKeeperSayCrap());
    }

    private void Update()
    {
        // 레벨업 키 반응 코드
        if(hitPlayer && Input.GetKeyDown(KeyCode.E) && !leveling)
        {
            FirekeeperLevelUp();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        // 플레이어 닿았을 때 로직
        if (col.gameObject.CompareTag("Player"))
        {
            commKey.SetActive(true);
            hitPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        // 플레이어 닿지 않았을 때 로직
        if(col.gameObject.CompareTag("Player"))
        {
            commKey.SetActive(false);
            hitPlayer = false;

        }
    }
    //화방녀 레벨업 코드 소울비교하여 레벨업
    private void FirekeeperLevelUp()
    {
        var playerinfo = FindObjectOfType<PlayerInfo>();
        if(levNeedSoul < playerinfo.Soul)
        {
            leveling = true;
            anim.SetBool("LevelUp", true);
            playerinfo.PlayerLevelUp(levNeedSoul);
            playerinfo.Soul -= (int)levNeedSoul;
            IncreaseLevelUpSoul();
        }

    }
    //레벨업 소울 계수만큼 증가 코드
    private void IncreaseLevelUpSoul()
    {
        levNeedSoul *= levIncreaseNum;
    }

    private void levAnim()
    {
        anim.SetBool("LevelUp", false);
        leveling = false;
    }


    //헛소리 코루틴
    IEnumerator FireKeeperSayCrap()
    {

        crapSelNum = Random.Range(0, 4);
        if (crapSelNum < 4)
        {
            txtBox.SetActive(true);
            txtBox.GetComponentInChildren<Text>().text = words[crapSelNum];
        }

        yield return new WaitForSeconds(sayWaitTime);


        StartCoroutine(FireKeeperSayCrap());


    }
}
