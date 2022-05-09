using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeWithHelicopter : MonoBehaviour
{
    public GameObject fToInteractHelicopter;
    public AudioSource playerAudioSource;
    public AudioClip helicopterCortanding;

    void Start ()
    {
        fToInteractHelicopter.SetActive(false);
    }
    
    void OnTriggerStay (Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            fToInteractHelicopter.SetActive(true);
        }
        if (other.gameObject.name == "Player" && Input.GetKeyDown(KeyCode.F))
        {
            HelicopterSoundPlay(helicopterCortanding);
            fToInteractHelicopter.SetActive(false);
            Invoke ("LoadCreditsScene", 8);
        }
    }
    
    void OnTriggerExit (Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            fToInteractHelicopter.SetActive(false);
        }
    }

    void HelicopterSoundPlay(AudioClip Voice)
    {
        if (!playerAudioSource.isPlaying)
        {
            playerAudioSource.clip = Voice;
            playerAudioSource.Play();
        }
    }

    void LoadCreditsScene()
    {
        SceneManager.LoadScene(4);
    }
}
