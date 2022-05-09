using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAidBox : MonoBehaviour
{
  public int amountToHeal = 50;
  void OnTriggerStay(Collider other)
  {
    if (other.GetComponent<Collider>().tag == "Player")
    {
      if (other.transform.GetComponent<Player>().health <= other.transform.GetComponent<Player>().maxHealth)
      other.transform.GetComponent<Player>().Heal(amountToHeal);
      Destroy(this.gameObject);
    }
  }
}
