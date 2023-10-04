using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 45.0f;

    private Vector3 initialPos;

    void Start()
    {
        initialPos = transform.position;
    }

    void Update()
    {
        Vector3 currentRotation = transform.eulerAngles;
        currentRotation.y += rotationSpeed * Time.deltaTime;
        transform.eulerAngles = currentRotation;
        transform.position += new Vector3(0, 0.01f*Mathf.Sin(Time.time) , 0);
    }
}
