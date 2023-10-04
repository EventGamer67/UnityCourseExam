using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHealth = 8f;
    private float currentHealth;
    [SerializeField] private GameObject _enemy;
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed;

    private void Start()
    {
        currentHealth = maxHealth; 
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (_target != null)
        {
            Vector3 direction = _target.position - transform.position;
            direction = direction.normalized;
            direction.y = 0;
            transform.Translate(direction * _speed*Time.deltaTime);
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 4);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if (bullet != null && bullet.destroyable)
            {
                TakeDamage(1);
                Destroy(collision.gameObject);
            }
        }
    }
}
