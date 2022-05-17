using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [Header("Bullet")]
    public float Damage;

    public float Thrust = 5;

    [Header("Components")]
    [SerializeField] private Transform _playerTransform;


    private void Awake()
    {
        _playerTransform = FindObjectOfType<PlayerController>().transform;
        
    }

    void Start()
    {
    }

    void Update()
    {
        
    }

    public void DetectIfEnemy(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            // Kockback enemy
            var enemyRB = other.GetComponent<Rigidbody2D>(); // Get the enemy rigidbody
            enemyRB.GetComponent<EnemyController>().Knockback(); // Knockback the enemy
        }
    }

    public GameObject DetectNearestEnemy()
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
                // Calculating distance between player and enemy
                float distance = Vector2.Distance(transform.position, enemies[i].transform.position);

                if (distance < indexDistance)
                {
                    nearestEnemy = enemies[i].gameObject;
                    indexDistance = distance;
                }
            }

            // print("El enemigo mas cercano a mi es: " + nearestEnemy.name + " con una distancia de: " + indexDistance);
            return nearestEnemy;
        }
        return null;
    }

    // Deal damage to the enemy
    public void DealDamage(GameObject enemy)
    {
        enemy.GetComponent<EnemyController>().TakeDamage(Damage);
    }
}
