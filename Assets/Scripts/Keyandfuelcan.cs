using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyandfuelcan : MonoBehaviour
{
    public AudioSource playerAudioSource;
    public AudioClip foundKeyVoice;

    public GameObject textFoundKey;

    public GameObject fToInteract;

    float Timer = 5f;
    void Start()
    {
        textFoundKey.SetActive(false);
        fToInteract.SetActive(false);
    }

   
    void Update()
    {
      
    }

    void OnTriggerStay (Collider other)
  {
    if (other.gameObject.name == "Player")
    {
        fToInteract.SetActive(true);
        if (other.gameObject.name == "Player" && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log (Timer);
            KeyPickUp();
            fToInteract.SetActive(false);
        }
    }
  }

    void OnTriggerExit (Collider other)
    {
      if (other.gameObject.name == "Player")
      {
          fToInteract.SetActive(false);
          Timer = 5f;
          TimerOut();
      }
    }

    void MyVoicePlay(AudioClip Voice)
    {
        playerAudioSource.clip = Voice;
        playerAudioSource.Play();
    }

    void KeyPickUp()
    {
          MyVoicePlay(foundKeyVoice);
          Destroy(this.gameObject);
          textFoundKey.SetActive(true);
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
