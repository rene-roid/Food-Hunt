using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {
        
    }


    // Get a random position in a box collider from a list of box colliders
    public Vector3 GetRandomPosition(List<BoxCollider2D> colliders)
    {
        var randomCollider = colliders[Random.Range(0, colliders.Count)];
        var randomPosition = new Vector3(Random.Range(randomCollider.bounds.min.x, randomCollider.bounds.max.x), Random.Range(randomCollider.bounds.min.y, randomCollider.bounds.max.y), 0);
        return randomPosition;
    }
}
