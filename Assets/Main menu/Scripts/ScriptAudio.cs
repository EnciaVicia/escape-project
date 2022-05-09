using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptAudio : MonoBehaviour
{
  
    AudioSource _audioSource;

    public static ScriptAudio inst;

    void Awake()
    {
        if(ScriptAudio.inst == null)
        {
            ScriptAudio.inst = this;
            DontDestroyOnLoad(gameObject);
            _audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }   
  
    public void StopMusic()
    {
        _audioSource.Stop();
    }

      public void StartMusic()
    {
        _audioSource.Play();
    }
}
