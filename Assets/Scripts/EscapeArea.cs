using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeArea : MonoBehaviour
{
    public GameObject fToInteractToEscape;
    public AudioSource carListener;
    public AudioClip carSound;

    void Update()
    {
        
    }

    void OnTriggerStay (Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            fToInteractToEscape.SetActive(true);
        }
        if (other.gameObject.name == "Player" && Input.GetKeyDown(KeyCode.F))
        {
            fToInteractToEscape.SetActive(false);
            Invoke("Load2Scene", 5);
            CarSoundPlay(carSound);
            
        }
    }

    void OnTriggerExit (Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            fToInteractToEscape.SetActive(false);
        }
    }

    void Load2Scene()
    {
        SceneManager.LoadScene(3);
    }

    void CarSoundPlay(AudioClip Voice)
    {
        if (!carListener.isPlaying)
        {
            carListener.clip = Voice;
            carListener.Play();
        }
    }
}
