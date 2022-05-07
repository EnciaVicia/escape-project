using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public int ammoToReload = 20;
    void OnTriggerStay(Collider other)
    {
      if (other.GetComponent<Collider>().tag == "Player") {
        Weapon pistol = other.gameObject.GetComponentInChildren<Weapon>();
        if (pistol != null) {
          pistol.RechargeAmmo(ammoToReload);
          Destroy(this.gameObject);
        }
      }
    }
}
