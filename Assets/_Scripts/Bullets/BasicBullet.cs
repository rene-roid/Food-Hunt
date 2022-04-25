using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : BulletController
{
    [Header("Stats")]
    [SerializeField] private float _bulletSpeed = 5;

    [Header("Vars")]
    private Vector3 _bulletDirection;

    [Header("Components")]
    private GameObject _nearestEnemy;
    private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        _nearestEnemy = DetectNearestEnemy();
        if (_nearestEnemy != null)
        {
            _bulletDirection = _nearestEnemy.transform.position - transform.position;
            
        } else
        {
            // Destroy bullet
            Destroy(gameObject);
        }

    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        MoveBullet();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If bullet hits enemy destroy the bullet, deal damage to the enemy and knocback the enemy
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyController>().TakeDamage(Damage);
            other.gameObject.GetComponent<EnemyController>().Knockback();
            Destroy(gameObject);
        }
    }

    private void MoveBullet()
    {
        _rb.MovePosition(StraightBullet());
    }

    private Vector2 BulletFollow()
    {
        return Vector3.MoveTowards(transform.position, _nearestEnemy.transform.position, _bulletSpeed * Time.deltaTime);
    }

    private Vector2 StraightBullet()
    {
        transform.up = _bulletDirection;
        return Vector3.MoveTowards(transform.position, transform.position + transform.up, _bulletSpeed * Time.deltaTime);
    }
}
