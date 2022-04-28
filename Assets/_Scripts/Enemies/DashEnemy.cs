using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashEnemy : EnemyController
{
    [Header("Stats")]
    [SerializeField] private float _moveSpeed;

    [SerializeField] private float _dashRange = 3f;
    [SerializeField] private LineRenderer _dashEffect;

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
            if (Vector3.Distance(transform.position, Target.position) > _dashRange / 2 && !_inRange)
            {
                // Reset lookat
                transform.rotation = Quaternion.Euler(0, 0, 0);
                Animator.Play("move");

                Rb.MovePosition(Vector3.MoveTowards(transform.position, Target.position, _moveSpeed * Time.deltaTime));
            } else
            {
                // When in range start dash

                // Getting dash once and only once
                if (!_inRange)
                {
                    GetDashDirection();
                    
                    _dashEffect.SetPosition(0, transform.position);
                    _dashEffect.SetPosition(1, transform.position);

                    _inRange = true;
                } else
                {
                    _dashEffect.SetPosition(1, Vector3.Lerp(_dashEffect.GetPosition(1), _dashDirection, LerpTwoNumbersInSpecifiedTime(0, 1, 0.1f)));
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
                
                Rb.isKinematic = false;
                Rb.bodyType = RigidbodyType2D.Dynamic;

                Rb.velocity = Vector3.zero;

                _dashEffect.SetPosition(0, transform.position);
                _dashEffect.SetPosition(1, transform.position);
            } // Check if the gameobject position is near _dashDirection
            else if (Vector3.Distance(transform.position, _dashDirection) < 1f)
            {
                
                _inRange = false;
                _inDash = false;
                
                Rb.isKinematic = false;
                Rb.bodyType = RigidbodyType2D.Dynamic;
                
                Rb.velocity = Vector3.zero;

                _dashEffect.SetPosition(0, transform.position);
                _dashEffect.SetPosition(1, transform.position);
            }
            else
            {
                // Move gameobject to _dashDirection
                Rb.MovePosition(Vector3.MoveTowards(transform.position, _dashDirection, _moveSpeed * 10 * Time.deltaTime));

                _dashEffect.SetPosition(0, transform.position);
            }
        }
    }

    private IEnumerator DashStop()
    {
        // Wait
        yield return new WaitForSeconds(.5f);


        // Activate dash mode
        Animator.Play("attack");

        // Look towards the _dashDirection in the z rotation
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(_dashDirection.y - transform.position.y, _dashDirection.x - transform.position.x) * Mathf.Rad2Deg);

        _inDash = true;
        Rb.bodyType = RigidbodyType2D.Kinematic;
        Rb.isKinematic = true;
        // Move gameobject to _dashDirection
    }

    private void GetDashDirection()
    {
        var dir = Target.position - transform.position;
        var dist = Vector3.Distance(transform.position, Target.position);
        _dashDirection = dir.normalized * (dist + _dashRange) + transform.position;
    }

    #endregion


    private float LerpTwoNumbersInSpecifiedTime(float start, float end, float time)
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / time;
            return Mathf.Lerp(start, end, t);
        }
        return 1;
    }

}
