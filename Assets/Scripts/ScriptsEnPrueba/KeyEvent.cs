using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KeyEvent : MonoBehaviour
{
    public AudioSource playerAudioSource;

    public AudioClip foundKeyVoice;

    public GameObject textFoundKey;

    public GameObject fToInteract;

    float Timer = 5f;

    public static event Action EventKey;
    void Start()
    {
        KeyEvent.EventKey += MyVoice;
        KeyEvent.EventKey += TextMyVoice;
        KeyEvent.EventKey += Ftext;
        KeyEvent.EventKey += TimerOut;




        fToInteract.SetActive(false);
        textFoundKey.SetActive(false);
    }

   
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player" && Input.GetKey(KeyCode.F))
        {
            if (EventKey != null)
            {
                EventKey();
            }
        }
    }

    void MyVoice ()
    {
        MyVoicePlay(foundKeyVoice);
    }

    void MyVoicePlay(AudioClip Voice)
    {
        playerAudioSource.clip = Voice;
        playerAudioSource.Play();
    }

    void TextMyVoice()
    {
        textFoundKey.SetActive(true);
    }

    void Ftext()
    {
        fToInteract.SetActive(true);
    }

    void TimerOut()
    {
       Timer -= Time.deltaTime;
       if (Timer <= 0)
       {
         textFoundKey.SetActive(false);
       }
    }


}
