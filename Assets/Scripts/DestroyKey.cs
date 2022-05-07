using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyKey : MonoBehaviour
{
   
    void Update()
    {
        
    }

    void OnTriggerStay (Collider other)
    {
        if (other.gameObject.name == "Player" && Input.GetKeyDown(KeyCode.F))
        {
            Destroy(this.gameObject);
        }
    }
}
