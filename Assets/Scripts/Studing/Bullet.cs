using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] public float speed = 10f; 
    public float lifetime = 2f; 
    
    private float startTime;

    [SerializeField] public bool destroyable = true;

    private void Start()
    {
        startTime = Time.time;

    }

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        if (destroyable)
        {
            if (Time.time - startTime > lifetime)
            {
                Destroy(gameObject);
            }
        }
    }
}
