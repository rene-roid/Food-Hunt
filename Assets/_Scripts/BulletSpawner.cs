using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [Header("Basic bullet")]
    [SerializeField] private GameObject _basicBullet;
    [SerializeField] private GameObject _meteor;

    private float _spawnerClock = 0;
    public float SpawnEvery = 2;
    

    void Start()
    {
        
    }

    void Update()
    {
        _spawnerClock += Time.deltaTime;

        if (_spawnerClock > SpawnEvery)
        {
            SpawnBasicBullet();
            SpawnMeteor();
            _spawnerClock = 0;
        }
    }

    private void SpawnBasicBullet()
    {
        GameObject newBasicBullet = Instantiate(_basicBullet, transform.position, Quaternion.identity);
        print("Spawned");
    }
    
    private void SpawnMeteor()
    {
        GameObject newMeteor = Instantiate(_meteor, transform.position, Quaternion.identity);
        print("Spawned");
    }
}
