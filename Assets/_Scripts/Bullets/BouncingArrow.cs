using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingArrow : BulletController
{
    private enum BulletType { EnemyBounce, ScreenBounce };
    [SerializeField] private BulletType type;

    [Header("Stats")]
    [SerializeField] private float _bulletSpeed = 5;

    [Header("Vars")]
    private Vector3 _bulletDirection;
    [SerializeField] private List<GameObject> _latestEnemy = new List<GameObject>();

    [Header("Components")]
    [SerializeField] private GameObject _nearestEnemy;
    private Rigidbody2D _rb;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        _nearestEnemy = DetectNearestEnemy();
        
        if (_nearestEnemy != null)
        {
            _bulletDirection = _nearestEnemy.transform.position - transform.position;

        }
        else
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
        if (other.gameObject.CompareTag("Enemy") && !_latestEnemy.Contains(other.gameObject))
        {
            //Destroy(gameObject);
            _latestEnemy.Add(other.gameObject);
            _nearestEnemy = DetectClosestEnemy();

            if (_nearestEnemy == null) Destroy(gameObject);
        }
    }

    #region Movement
    private void MoveBullet()
    {
        switch (type)
        {
            case BulletType.EnemyBounce:
                _rb.MovePosition(BulletFollow());
                break;
            case BulletType.ScreenBounce:
                _rb.MovePosition(CameraBounce());
                break;
            default:
                break;
        }
    }

    private Vector2 BulletFollow()
    {
        return Vector3.MoveTowards(transform.position, _nearestEnemy.transform.position, _bulletSpeed * Time.deltaTime);
    }
    
    // Detect nearest enemy
    private GameObject DetectClosestEnemy()
    {
        // Create a circle between playe rposition with a radius of 20 to all the objects in layer 6 (enemy)
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 20, (1 << 6));
        if (enemies.Length > 0)
        {
            GameObject nearestEnemy = null;

            // Distance index
            float indexDistance = Mathf.Infinity;

            // Going through all enemies and calculating distance
            for (int i = 0; i < enemies.Length; i++)
            {
                bool isMatch = false;
                for (int j = 0; j < _latestEnemy.Count; j++)
                {
                    if (enemies[i].gameObject == _latestEnemy[j]) isMatch = true;
                }
                
                // Calculating distance between player and enemy
                float distance = Vector2.Distance(transform.position, enemies[i].transform.position);
                if (distance < indexDistance && !isMatch)
                {
                    nearestEnemy = enemies[i].gameObject;
                    indexDistance = distance;
                }
            }
            
            // print("El enemigo mas cercano a mi es: " + nearestEnemy.name + " con una distancia de: " + indexDistance);
            return nearestEnemy;
        }
        
        // If no enemies, destroy bullet
        Destroy(gameObject);
        return null;
    }
    #endregion

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    // Move the bullet to a random direction and bounce it when it reaches the camera border
    private Vector2 CameraBounce()
    {
        Vector2 newPosition = transform.position;
        newPosition.x += _bulletSpeed * Time.deltaTime;
        if (newPosition.x > Camera.main.orthographicSize * Camera.main.aspect)
        {
            newPosition.x = -Camera.main.orthographicSize * Camera.main.aspect;
            _bulletDirection.x *= -1;
        }
        else if (newPosition.x < -Camera.main.orthographicSize * Camera.main.aspect)
        {
            newPosition.x = Camera.main.orthographicSize * Camera.main.aspect;
            _bulletDirection.x *= -1;
        }
        newPosition.y += _bulletSpeed * Time.deltaTime;
        if (newPosition.y > Camera.main.orthographicSize)
        {
            newPosition.y = -Camera.main.orthographicSize;
            _bulletDirection.y *= -1;
        }
        else if (newPosition.y < -Camera.main.orthographicSize)
        {
            newPosition.y = Camera.main.orthographicSize;
            _bulletDirection.y *= -1;
        }
        return newPosition;
    }
}