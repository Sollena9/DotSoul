using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{

    public GameObject attackPos;
    public GameObject leftHands;
    public GameObject rightHands;



    private void Awake()
    {
        /*leftHands;
         */
    }

    void Start()
    {
        
    }

    // Update is called once per frame

    private void Update()
    {
        GetAttackAngle();

    }


    public void GetAttackAngle()
    {
        Vector2 len = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        float z = Mathf.Atan2(len.y, len.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, z);

    }

}



