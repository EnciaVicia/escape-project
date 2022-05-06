using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpFuel: MonoBehaviour
{
    public GameObject FtointeractFuel;
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }

    void OnTriggerStay (Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            FtointeractFuel.SetActive(true);
        }

            if (other.gameObject.name == "Player" && Input.GetKeyDown(KeyCode.F))
            {
                FtointeractFuel.SetActive(false);
                Destroy(this.gameObject);
                Debug.Log(other.GetComponent<Player>().Fuel);
                other.GetComponent<Player>().Fuel.Add(gameObject);    
            }
    }
    
    void OnTriggerExit (Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            FtointeractFuel.SetActive(false);
        }
    }



}
