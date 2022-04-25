using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : EnemyController
{
    [Header("Stats")]
    [SerializeField] private float _moveSpeed;

    [Header("Debuffs")]
    [SerializeField] [Range(0, 1f)] private float _speedReduction = 1;

    void Start()
    {
        
    }

    void Update()
    {
        if (!IsActive) return;
        DebuffCalculator();
    }

    private void FixedUpdate()
    {
        EnemyMovement();
    }

    #region Calculations
    private void DebuffCalculator()
    {
        _moveSpeed = BaseMoveSpeed * _speedReduction;
    }
    #endregion

    #region Movement
    /// <summary>
    /// Calculate the distance between the player and the enemy
    /// </summary>
    /// <returns>Returns the distance between the player and the enemy</returns>
    private Vector3 CheckDistance()
    {
        //if (Vector3.Distance(Target.position, transform.position) < AttackRadius) return;
        //transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, _moveSpeed * Time.deltaTime);

        var move = Vector3.MoveTowards(transform.position, Target.transform.position, _moveSpeed * Time.deltaTime);

        return move;
        
    }

    /// <summary>
    /// Moves the enemy towards the player
    /// </summary>
    private void EnemyMovement()
    {
        if (!CanMove) return;
        Rb.MovePosition(CheckDistance());
    }

    public override void Knockback()
    {
        base.Knockback();
    }
    #endregion

}
