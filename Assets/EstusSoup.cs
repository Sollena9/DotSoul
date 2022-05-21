using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EstusSoup : MonoBehaviour
{
    public GameObject estusPopUpUi;
    public bool popUpState;

    private Animator anim;

    // Start is called before the first frame update

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if(!popUpState)
                estusPopUpUi.gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && Input.GetKey(KeyCode.E))
        {
            anim.SetInteger("Drink", +1);
            var player = col.gameObject.GetComponent<PlayerInfo>();
            //player.HP += player.estusHPCurruntCount;
            //Debug.Log(player.HP);
            popUpState = true;
            estusPopUpUi.gameObject.SetActive(false);
        }
    }


    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if(!popUpState)
                estusPopUpUi.gameObject.SetActive(false);
        }

    }
}
