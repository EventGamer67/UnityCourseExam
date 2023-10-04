using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target; 
    [SerializeField] private float smoothSpeed = 0.925f;
    [SerializeField] private int test = -6;

    private void FixedUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("Camera target is not assigned.");
            return;
        }

        float rotationYRadians = target.transform.rotation.eulerAngles.y * Mathf.Deg2Rad;

        Vector3 rotatedDesiredPosition = new Vector3(target.position.x+(test * Mathf.Sin(rotationYRadians)), target.position.y+3, target.position.z + (test * Mathf.Cos(rotationYRadians)));

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, rotatedDesiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        transform.LookAt(target.position);
    }
}
