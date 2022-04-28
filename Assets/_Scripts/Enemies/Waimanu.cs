using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waimanu : EnemyController
{
    [Header("Stats")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;

    [Header("Debuffs")]
    [SerializeField][Range(0, 1f)] private float _speedReduction = 1;

    void Start()
    {
        
    }

    void Update()
    {
        DebuffCalculator();
    }


    private void FixedUpdate()
    {
        Movement();
    }
    #region Calculations
    private void DebuffCalculator()
    {
        _moveSpeed = BaseMoveSpeed * _speedReduction;
    }
    #endregion

    #region Movement
    private void Movement()
    {
        var dir = Target.position - transform.position;
        transform.up = Vector3.MoveTowards(transform.up, dir, _rotationSpeed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.up, _moveSpeed * Time.deltaTime);

    }
    #endregion
}
