using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class message : MonoBehaviour
{

    public GameObject messageUi;
    public GameObject directorMessage;


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
                messageUi.gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {

        if (col.gameObject.CompareTag("Player") && Input.GetKey(KeyCode.E))
        {
            messageUi.gameObject.SetActive(false);
            directorMessage.gameObject.SetActive(true);

        }
    }


    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            messageUi.gameObject.SetActive(false);
            directorMessage.gameObject.SetActive(false);

        }

    }
}

