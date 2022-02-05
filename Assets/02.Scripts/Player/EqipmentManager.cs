using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EqipmentManager : MonoBehaviour
{
    // Start is called before the first frame update
    public WeaponData weapondata;

    public GameObject[] weponlist;
    public Player_Movement thePlayer;
    public PlayerTag state;

    private bool comboPossible;
    private int comboStack;
    public Animator anim;


    private void Start()
    {
        thePlayer = GetComponent<Player_Movement>();
        state = GetComponent<PlayerTag>();

        if (GetComponent<PlayerInfo>().eqipWeapon)
            EqipWeapon(0);

    }



    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && (state.HasFlag(State._Idle) || state.HasFlag(State._Move)))
        {
            Attack();
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
        thePlayer.StateChanger(true, State._Combat);
        thePlayer.StateChanger(true, State._Attack);

        if (comboStack == 0)
        {
            anim.Play("Attack");
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
            anim.Play("Attack_Combo1");
        }

        if (comboStack == 3)
        {
            anim.Play("Attack_Combo2");
        }
    }

    public void ComboReset()
    {
        comboPossible = false;
        comboStack = 0;
        thePlayer.StateChanger(false, State._Combat);
        thePlayer.StateChanger(false, State._Attack);
    }
}
