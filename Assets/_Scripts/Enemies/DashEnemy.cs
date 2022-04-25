using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashEnemy : EnemyController
{
    [Header("Stats")]
    [SerializeField] private float _moveSpeed;

    [SerializeField] private float _dashRange = 3f;

    [Header("Debuffs")]
    [SerializeField] [Range(0, 1f)] private float _speedReduction = 1;

    [Header("References")]
    [SerializeField] private Vector3 _dashDirection = Vector3.zero;
    [SerializeField] private bool _inRange = false;
    [SerializeField] private bool _inDash = false;

    void Start()
    { 
        
    }

    void Update()
    {
        if (!IsActive) return;
        DebuffCalculator();
        //Move();
        
    }

    private void FixedUpdate()
    {
        if (!IsActive) return;
        StopDash();
    }

    private void DebuffCalculator()
    {
        _moveSpeed = BaseMoveSpeed * _speedReduction;
    }

    #region Movement
    // Follow the target and when in range dash towards it
    private void StopDash()
    {
        if (!CanMove) return;
        
        // If it is not in dash mode follow the player
        if (!_inDash)
        {
            // Follow the player until it reaches the range
            if (Vector3.Distance(transform.position, Target.position) > _dashRange && !_inRange)
            {
                Rb.MovePosition(Vector3.MoveTowards(transform.position, Target.position, _moveSpeed * Time.deltaTime));
            } else
            {
                // When in range start dash

                // Getting dash once and only once
                if (!_inRange)
                {
                    GetDashDirection();
                    _inRange = true;
                }

                // Start dash
                StartCoroutine(DashStop());
            }
        } else
        {
            _inRange = false;
            
            // If in dash mode and has reached the target stop dash
            if (transform.position == _dashDirection)
            {
                _inRange = false;
                _inDash = false;
                
            } // Check if the gameobject position is near _dashDirection
            else if (Vector3.Distance(transform.position, _dashDirection) < 0.2f)
            {
                
                _inRange = false;
                _inDash = false;
            }
            else
            {
                // Move gameobject to _dashDirection
                Rb.MovePosition(Vector3.MoveTowards(transform.position, _dashDirection, _moveSpeed * 10 * Time.deltaTime));
            }
        }
    }

    private IEnumerator DashStop()
    {
        // Wait
        yield return new WaitForSeconds(.5f);

        // Activate dash mode
        _inDash = true;
        // Move gameobject to _dashDirection
    }

    private void GetDashDirection()
    {
        var dir = Target.position - transform.position;
        var dist = Vector3.Distance(transform.position, Target.position);
        _dashDirection = dir.normalized * (dist * 2) + transform.position;
    }

    #endregion
}
