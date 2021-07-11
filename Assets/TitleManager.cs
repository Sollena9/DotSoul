using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    [SerializeField]
    private Image blackImage;
    private bool startGame;


    // Update is called once per frame

    private void Start()
    {
        StartCoroutine(Alpha());

    }
    void FixedUpdate()
    {
        if(Input.anyKey)
        {
            blackImage.color = new Color(0, 0, 0, 0);
            SceneManager.LoadScene("CemeteryOfAsh");
            
        }

    }

    IEnumerator Alpha()
    {
        yield return new WaitForSeconds(0.1f);

            blackImage.color -= new Color(0, 0, 0, 0.1f);
            StartCoroutine(Alpha());
    }
}
