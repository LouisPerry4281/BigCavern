using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField] float RotationRate;
    private Vector3 RotationVector;
    private float HoriInput;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotatePlayer();
    }
    private void RotatePlayer()
    {
        HoriInput = Input.GetAxisRaw("Horizontal");
        RotationVector = new Vector3(0, 0, -HoriInput * RotationRate);
        gameObject.transform.Rotate(RotationVector);
    }
}
