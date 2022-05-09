using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recuerdo : MonoBehaviour
{

    public GameObject FtoInteractInRecuerdo;

    public AudioSource playerSource;
    public AudioClip recuerdoSound;

    public GameObject recuerdoSubtitle;

    void OnTriggerStay (Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            FtoInteractInRecuerdo.SetActive(true);
        }

        if (other.gameObject.name == "Player" && Input.GetKeyDown(KeyCode.F))
        {
            recuerdoSubtitle.SetActive(true);
            RecuerdoSoundPlay(recuerdoSound);
            FtoInteractInRecuerdo.SetActive(false);
            Invoke ("DestroyInvoke", 5);
            Invoke("TextRecuerdoSubtitleUnable", 5.1f);
        }
    }

    void OnTriggerExit (Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            FtoInteractInRecuerdo.SetActive(false);
        }
    }

    void RecuerdoSoundPlay (AudioClip Voice)
    {
        if (!playerSource.isPlaying)
        {
            playerSource.clip = Voice;
            playerSource.Play();
        }
    }

    void DestroyInvoke()
    {
      Destroy(this.gameObject);
      FtoInteractInRecuerdo.SetActive(false);
      recuerdoSubtitle.SetActive(false);
    }

    void TextRecuerdoSubstitleUnable()
    {
        recuerdoSubtitle.SetActive(false);
    }
}
