using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpFuel: MonoBehaviour
{
    public GameObject FtointeractFuel;

    public AudioSource playerSourceFuel;
    public AudioClip itemFound;
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
                FuelFound (itemFound);
                FtointeractFuel.SetActive(false);
                Destroy(this.gameObject);
                Debug.Log(other.GetComponent<Player>().Fuel);
                other.GetComponent<Player>().Fuel.Add(gameObject);
                other.GetComponent<Player>().fuelToDisplay ++;    
            }
    }
    
    void OnTriggerExit (Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            FtointeractFuel.SetActive(false);
        }
    }

    void FuelFound (AudioClip FuelFoundIt)
    {
        playerSourceFuel.clip = FuelFoundIt;
        playerSourceFuel.Play();
    }



}
