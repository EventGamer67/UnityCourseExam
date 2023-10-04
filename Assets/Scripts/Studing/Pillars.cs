using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillars : MonoBehaviour
{
    [SerializeField] private float _speed = 1;
    [SerializeField] private float _amplitude = 2;
    public float Speed => _speed;
    public float Amplitude => _amplitude;

    [SerializeField] private float offset;

    private Transform _ourTransform;
    private float initX;
    private float initY;
    private float initZ;
    void Start()
    {
        _ourTransform = GetComponent<Transform>();
        initY = _ourTransform.position.y;
        initX = _ourTransform.position.x;
        initZ = _ourTransform.position.z;
        offset = Random.Range(1,1000);
    }

    void Update()
    {
        _ourTransform.position = new Vector3(initX,initY+Mathf.Sin(offset+(Time.time*_speed))*_amplitude,initZ);
    }
}
