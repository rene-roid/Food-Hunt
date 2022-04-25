using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : BulletController
{
    [Header("Stats")]
    [SerializeField] private float _meteorSpeed = 5;

    [Header("Components")]
    private int hoa;

    [Header("Gameojects")]
    [SerializeField] private GameObject _explosion;
    private GameObject _explosionGameObject;
    private Vector3 _explosionPosition;

    void Start()
    {
        // Set gameoject at random position following on top of the camera
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Screen.height, Camera.main.nearClipPlane)) + new Vector3(0, 2, 0);

        // Set explosion prefab at GetRandomPosition
        _explosionPosition = GetRandomPosition();
        //_explosionGameObject = Instantiate(_explosion, _explosionPosition, Quaternion.identity);
    }

    void Update()
    {
        DestroyAfterReachingPos();
    }

    private void FixedUpdate()
    {
        // Move meteor
        MeteorMovement();
    }

    #region Movement
    // Move game object to _explosion
    private void MeteorMovement()
    {
        transform.position = Vector3.MoveTowards(transform.position, _explosionPosition, Time.deltaTime * _meteorSpeed);
    }

    // Get random position inside the camera view following the camera position
    private Vector3 GetRandomPosition()
    {
        var randomPosition = new Vector3(Random.Range(Camera.main.transform.position.x - Camera.main.orthographicSize * Camera.main.aspect, Camera.main.transform.position.x + Camera.main.orthographicSize * Camera.main.aspect),
                                         Random.Range(Camera.main.transform.position.y - Camera.main.orthographicSize, Camera.main.transform.position.y + Camera.main.orthographicSize),
                                         0);
        return randomPosition;
    }
    #endregion


    #region Damage
    // Deal damage to the enemy if it touches meteor when it is in the explosion position
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" && transform.position == _explosionPosition)
        {
            other.gameObject.GetComponent<EnemyController>().TakeDamage(1);

            // Stun the enemy
            other.gameObject.GetComponent<EnemyController>().Stun(2);
        }
    }
    #endregion


    #region Destroy
    //// Destroy game object when it is out of the camera view
    //private void OnBecameInvisible()
    //{
    //    Destroy(gameObject);
    //}

    // Destroy game object 5 seconds after it reaches the _explosionPosition
    private void DestroyAfterReachingPos()
    {
        if (transform.position == _explosionPosition)
        {
            Destroy(gameObject, 5);
        }
    }
    #endregion

}
