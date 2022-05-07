using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keyandfuelcan : MonoBehaviour
{
    public AudioSource playerAudioSource;
    public AudioClip foundKeyVoice;

    public GameObject textFoundKey;

    public GameObject fToInteract;

    public GameObject intFuelActive;

    public Text Objetivos;
    public Text intFuel;
    float Timer = 5f;
    void Start()
    {
        textFoundKey.SetActive(false);
        fToInteract.SetActive(false);
        intFuelActive.SetActive(false);
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
            intFuelActive.SetActive(true);
            Objetivos.text = Objetivos.GetComponent<Text>().text = "- Recoge 3 combustibles = ";
            Debug.Log (Timer);
            KeyPickUp();
            fToInteract.SetActive(false);
            Invoke("TextFoundKeyUnable", 5f);
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
        if (!playerAudioSource.isPlaying)
        {
          playerAudioSource.clip = Voice;
          playerAudioSource.Play();
        }
    }

    void KeyPickUp()
    {
          MyVoicePlay(foundKeyVoice);
          Invoke ("DestroyInvoke", 5);
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

    void TextFoundKeyUnable()
    {
        Destroy(textFoundKey);
    }

    void DestroyInvoke()
    {
      fToInteract.SetActive(false);
      Destroy(this.gameObject);
    }
}
