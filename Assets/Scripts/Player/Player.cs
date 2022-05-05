using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
  public GameObject cam2;
  public bool hasKey = false;
  private float timeLeft = 2f;
  private float gravity = 3f;
  public AudioSource playerAudio;
  public AudioClip keyFound;
  private Vector3 moveDirection;
  public float movementSpeed;
  public GameObject WoodenCarSign;
  public float distance = 3f;
  public int health = 100;

  void Awake()
  {
    WoodenCarSign.SetActive(false);
  }
  void Update()
  {
    if (health <= 0) {
      Time.timeScale = 0f;
      Invoke("LoadSceneOnDeath", 7f);
    }
  }
  void OnTriggerStay(Collider other)
  {
    if (other.gameObject.name == "GetKeyArea")
    {
      if (timeLeft <= 0)
      {
        hasKey = true;
        timeLeft = 3f;
        WoodenCarSign.SetActive(true);
        StartAudioClip(keyFound);
        other.gameObject.SetActive(false);
      } else
      {
        timeLeft -= Time.deltaTime;
      }
    }
    if (other.gameObject.name == "EscapeArea")
    {
      if (timeLeft <= 0)
      {
        Time.timeScale = 0f;
        timeLeft = 3f;
      } else
      {
        timeLeft -= Time.deltaTime;
      }
    }
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
}
