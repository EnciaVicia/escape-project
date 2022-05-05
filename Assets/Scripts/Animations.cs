using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    Animator anim;
    public float velocidad = 0.0f;
    public float aceleración = 0.1f;
    public float desaceleración = 0.5f;
    public bool moverseoprimido;


    void Start()
    {
        
        anim = GetComponent<Animator>();
    }

    
    void Update()
    {
        bool moverseoprimido = Input.GetKey("w");

        if (moverseoprimido && velocidad < 1.0f)
        {
            velocidad += Time.deltaTime * aceleración;
        }

         if (!moverseoprimido && velocidad > 0.0)
        {
            velocidad -= Time.deltaTime * desaceleración;
        }
        if (!moverseoprimido && velocidad < 0.0f)
        {
            velocidad = 0.0f;
        }
        anim.SetFloat("Velocity", velocidad);
    }
}
