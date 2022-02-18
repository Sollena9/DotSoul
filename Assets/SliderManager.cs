using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SliderManager : MonoBehaviour
{
    public Slider hp;
    public Slider mp;
    public Slider sp;
    [SerializeField]
    private float[] curruntValue = new float[3];
    private float pointSpeed;


    private void Start()
    {
        curruntValue[0] = hp.maxValue;
        curruntValue[1] = mp.maxValue;
        curruntValue[2] = sp.maxValue;
    }

    public void ResourceManager(int value, float point)
    {

        switch (value)
        {
            case 0:
                curruntValue[value] = hp.value + point;

                if (curruntValue[value] > 0)
                {
                    StartCoroutine(SliderAniamtion(value, point));
                    
                }
                else
                    hp.value = 0;
                break;

            case 1:
                curruntValue[value] = mp.value + point;

                if (curruntValue[value] > 0)
                {
                    StartCoroutine(SliderAniamtion(value, point));

                }
                else
                    mp.value = 0;
                break;

            case 2:
                curruntValue[value] = sp.value + point;

                if (curruntValue[value] > 0)
                {
                    Debug.Log("이프실행");
                    StartCoroutine(SliderAniamtion(value, point));

                }
                else
                    sp.value = 0;
                break;

            default:
                Debug.Log("실패");
                break;
        }
    }

   
    public IEnumerator SliderAniamtion(int value, float point)
    {
        switch(value)
        {
            case 0:
                while (curruntValue[value] > hp.value)
                {
                    if (hp.value >= hp.maxValue)
                        break;

                    hp.value += 100 * Time.smoothDeltaTime;
                    Debug.Log(hp.value);
                    yield return null;
                    //InfiniteLoopDetector.Run();

                }

                hp.value = curruntValue[value];
                break;


            case 1:
                while (curruntValue[value] > mp.value)
                {
                    if (mp.value >= mp.maxValue)
                        break;

                    mp.value += 100 * Time.smoothDeltaTime * 50;
                    Debug.Log(mp.value);
                    yield return null;
                    //InfiniteLoopDetector.Run();

                }

                mp.value = curruntValue[value];
                break;

            case 2:
                while (curruntValue[value] > sp.value)
                {
                    if (sp.value >= sp.maxValue)
                        break;

                    sp.value += 100 * Time.smoothDeltaTime * 50;
                    Debug.Log(sp.value);
                    yield return null;
                    //InfiniteLoopDetector.Run();

                }

                sp.value = curruntValue[value];
                break;

            default:
                Debug.Log("애니 실패");
                break;
        }

        Debug.Log("종료");
        yield break;
    }

}
