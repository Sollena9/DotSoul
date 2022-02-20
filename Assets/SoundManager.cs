using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    public new AudioSource audio;

    public List<AudioClip> sound = new List<AudioClip>();

    public static SoundManager instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
    private void OnEnable()
    {
        this.audio = this.gameObject.AddComponent<AudioSource>();
        audio.playOnAwake = false;
    }




    public void PlaySound(string soundname)
    {
        if(soundname == "Jump")
        {
            audio.PlayOneShot(sound[0]);
        }

        else if(soundname == "SJump")
        {
            audio.PlayOneShot(sound[1]);
        }
        else if (soundname == "Bump")
        {
            audio.PlayOneShot(sound[2]);
        }
    }

}
