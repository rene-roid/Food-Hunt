using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockWand : BulletController
{
    [Header("Stats")]
    [SerializeField] private float _bulletSpeed = 5;

    [Header("Components")]
    [SerializeField] private Transform _playerRb;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    // Get a random direction from _playerRb with explication
    private Vector3 GetRandomDirection()
    {
        Vector3 direction = _playerRb.GetComponent<Rigidbody2D>().velocity.normalized;
        float angle = Random.Range(-45, 45);
        direction = Quaternion.Euler(0, 0, angle) * direction;
        return direction;
    }
}
