using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
  public GameObject cam2;
  public bool hasKey = false;
  private float timeLeft = 2f;
  private float gravity = 3f;
  public AudioSource playerAudio;
  public AudioClip keyFound;
  public Light Flashlight;
  public bool ActivLight = false;
  public List<GameObject> Fuel = new List<GameObject>();
  private Vector3 moveDirection;
  public float movementSpeed;
  public float distance = 3f;
  public int health = 100;
  public GameObject EscapeArea;
  public Text IntHealth;
  public int fuelToDisplay = 0;
  public Text intFuel;
  public Text backToCar;
  

  void Awake()
  {   
      EscapeArea.SetActive(false);
  }
  void Update()
  {
    if (health <= 0) {
      Time.timeScale = 0f;
      Invoke("LoadSceneOnDeath", 7f);
      FlashlightOffOn();
    }
    IntHealth.text = "" + health;
    intFuel.text = "" + fuelToDisplay;

    FuelList();
  }
  void StartAudioClip(AudioClip clip)
  {
    playerAudio.clip = clip;
    playerAudio.Play();
  }
  public void TakeDamage(int amount)
  {
      health -= amount;
  }
  void LoadSceneOnDeath()
  {
    SceneManager.LoadScene("War Project Escape");
  }

 void FlashlightOffOn()
  {
    if (Input.GetKeyDown(KeyCode.T))
    {
      if(ActivLight == false)
    {
        Flashlight.enabled = true;
        ActivLight = !ActivLight;
    }

      if(ActivLight == true)
    {
        Flashlight.enabled = false;
        ActivLight = !ActivLight;
    }

    }

  }

  void FuelList()
  {
      if (Fuel.Count >= 3)
      {
          backToCar.text = "- Regresa al auto y escapa";
          backToCar.color = Color.green;
          intFuel.enabled = false;
          EscapeArea.SetActive(true);
      }
  }
}
