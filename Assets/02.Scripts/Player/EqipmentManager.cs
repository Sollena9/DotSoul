using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EqipmentManager : MonoBehaviour
{
    // Start is called before the first frame update
    public WeaponData weapondata;

    public WeaponData weaponRight;
    public WeaponData weaponLeft;

    public GameObject[] weponlist;
    public Player_Movement thePlayer;

    private bool comboPossible;
    private int comboStack;
    [SerializeField]
    private Animator anim;


    private void Start()
    {
        thePlayer = GetComponent<Player_Movement>();

        /*if (GetComponent<PlayerInfo>().data.weaponState == PlayerData.playerWeaponState.Fist)
            EqipWeapon(0);
            */
    }



    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (thePlayer.data.playerState == PlayerData.playerCombatState.Idle ||
                thePlayer.data.playerState == PlayerData.playerCombatState.Walk)
                Attack();
            else
                return;
        }
    }

    //검모드: 아래 베기 후 위 베기 
    //무기별 특성 만들어야됨.
    //
    // 무기 아이템 습득 방식: 드롭 / 구매(재료 사용)
    //인벤토리 만들어야됨
        

    void EqipWeapon(int weaponNum)
    {

        GameObject normalWeapon = Instantiate(weponlist[weaponNum], transform.localPosition, Quaternion.identity);
        //normalWeapon.transform.position = normalWeapon.GetComponent<weaponInfo>().spawnPosition;
    }


    public void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log(col.gameObject.name);

    }



    public void Attack()
    {
        thePlayer.data.playerState = PlayerData.playerCombatState.Attack_Normal;

        if (comboStack == 0)
        {
            anim.Play("RegularAttack");
            comboStack = 1;
            return;
        }

        if (comboStack != 0)
        {
            if (comboPossible)
            {
                comboPossible = false;
                comboStack += 1;
            }
        }
    }

    public void ComboPossible()
    {
        comboPossible = true;
    }

    public void Combo()
    {
        if (comboStack == 2)
        {
            anim.Play("RegularAttack_Combo1");
        }

        if (comboStack == 3)
        {
            anim.Play("RegularAttack_Combo2");
        }
    }

    public void ComboReset()
    {
        comboPossible = false;
        comboStack = 0;
        thePlayer.data.playerState = PlayerData.playerCombatState.Idle;
    }


    public void ChangeWeapon()
    {
        //inven에서 정보 로드(아이템 정보, 왼손/오른손)
        //var isRightWeapon = GetComponent<>
        

    }
}
