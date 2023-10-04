using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float Speed => _speed;
    [SerializeField] private float _speed;

    public float Coins => _coins;
    [SerializeField] private float _coins;

    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject _healthPack;

    public float Health => _health;
    [SerializeField] private float _health = 5;

    [SerializeField] private GameObject _bulletPrefab;
    private GameObject _bulletClone;


    public float moveSpeed = 5f;
    public float sensitivity = 2f;

    private Rigidbody rb;

    public float jumpForce = 1f;
    public float jumpCooldown = 1.0f;
    private float lastJumpTime;

    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI healthText;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (healthText != null)
        {
            healthText.text = "Health: " + _health;
        }
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        transform.Rotate(Vector3.up * mouseX);

        float moveZ = Input.GetAxis("Vertical");
        float moveX = Input.GetAxis("Horizontal");

        float rotationYRadians = transform.eulerAngles.y * Mathf.Deg2Rad;

        Vector3 moveDirection = new Vector3(Mathf.Sin(rotationYRadians), 0, Mathf.Cos(rotationYRadians));

        Vector3 move = moveDirection * moveZ * moveSpeed;

        move += transform.right * moveX * moveSpeed;

        rb.velocity = new Vector3(-move.x, rb.velocity.y, -move.z);

        if (Input.GetKeyDown(KeyCode.Space) && CanJump())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            lastJumpTime = Time.time;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            SpawnBullet();
        }
    }
    private bool CanJump()
    {
        return Time.time - lastJumpTime >= jumpCooldown;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            Instantiate(other.gameObject, new Vector3(Mathf.Floor(Random.Range(-20 / 2, 20)), 2, Mathf.Floor(Random.Range(-20 / 2, 20))), Quaternion.identity);
            Destroy(other.gameObject);
            _coins++;
            if (coinText != null)
            {
                if (_coins == 32)
                {
                    coinText.text = "YOU WIN";

                }
                else
                {
                    coinText.text = "Diamonds: " + _coins;
                }
            }
            for(int i = 0; i < _coins; i++)
            {
                Instantiate(_enemy, new Vector3(Mathf.Floor(Random.Range(-20 / 2, 20)), 2, Mathf.Floor(Random.Range(-20 / 2, 20))), Quaternion.identity);
            }
            if(Random.value < 0.2f)
            {
                Instantiate(_healthPack, new Vector3(Mathf.Floor(Random.Range(-20 / 2, 20)), 2, Mathf.Floor(Random.Range(-20 / 2, 20))), Quaternion.identity);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            _health--;
            if (healthText != null)
            {
                healthText.text = "Health: " + _health;
            }
            if (_health <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        if (collision.gameObject.CompareTag("HealPack"))
        {
            _health++;
            Destroy(collision.gameObject);
            if (healthText != null)
            {
                healthText.text = "Health: " + _health;
            }
        }
    }

    private void SpawnBullet()
    {
        Vector3 spawnPosition = transform.position - transform.forward * 1.0f;

        GameObject bullet = Instantiate(_bulletPrefab, spawnPosition, Quaternion.identity);
        float playerRotationY = transform.eulerAngles.y;
        bullet.transform.rotation = Quaternion.Euler(0f, playerRotationY + 180, 0f);
        bullet.GetComponent<Bullet>().destroyable = true;
        bullet.GetComponent<Bullet>().speed = 5;
        _bulletClone = bullet;
    }
}
