using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 staringPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0,1)] float movementFactor;

    // Start is called before the first frame update
    void Start()
    {
        staringPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 offset = movementVector * movementFactor;
        transform.position = staringPosition + offset;
    }
}
