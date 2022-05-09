using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip soundButon;
    public void ButonPlay()
    {
        ScriptAudio.inst.StopMusic();
        StartAudioClip(soundButon);
        Invoke ("LoadScenePlay", 0.5f);
    }

    public void ButonOptions()
    {
        Invoke ("LoadSceneOptions", 0.5f);
        StartAudioClip(soundButon);
    }

    public void ButonBack()
    {
        Invoke ("LoadSceneBack", 0.5f);
        StartAudioClip(soundButon);
    }

    public void ButonMainMenu()
    {
        ScriptAudio.inst.StartMusic();
        Time.timeScale = 1f;
        Invoke ("LoadSceneBack", 0.5f);
        StartAudioClip(soundButon);
    }

    public void ButonExit()
    {
        StartAudioClip(soundButon);
        Application.Quit();
    }

    public void PauseButonExit()
    {
        StartAudioClip(soundButon);
        Application.Quit();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        StartAudioClip(soundButon);
    }

    void LoadScenePlay()
    {
        SceneManager.LoadScene(1);
    }

    void LoadSceneOptions()
    {
        SceneManager.LoadScene(2);
    }

    void LoadSceneBack()
    {
        SceneManager.LoadScene(0);
    }
    void StartAudioClip(AudioClip clip)
    {
        audioSource.clip = soundButon;
        audioSource.Play();
    }
  
}
