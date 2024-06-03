using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPropulsion : MonoBehaviour
{
    [SerializeField] float ThrustRate;
    [SerializeField] float ThrustDecay;
    private Rigidbody2D RB;
    private float ForwardThrust;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Thrust"))
        {
            ForwardThrust += ThrustRate;
        }
        else if (ForwardThrust > 0)
        {
            ForwardThrust -= ThrustDecay;
            if (ForwardThrust <= 0)
            {
                ForwardThrust = 0;
            }
        }

    }
}
