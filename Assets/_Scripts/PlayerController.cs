using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private Rigidbody2D _rb;
    private float health = 100;


    [Header("Movement")]
    [SerializeField] private float _activeMoveSpeed;
    public float WalkSpeed = 5.0f;

    [SerializeField] private bool _isMoving => _movement != Vector3.zero;
    [SerializeField] private Vector3 _movement;
    public bool CanMove = true;


    [Header("Animations")]
    [SerializeField] private Animator _animator;


    [Header("Other")]
    public bool ActiveScript = true;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!ActiveScript) return;
        MyInput();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    #region Input
    /// <summary>
    /// Recolecting all user input
    /// </summary>
    private void MyInput()
    {
        MovementInput();
    }

    /// <summary>
    /// Getting _movement input
    /// </summary>
    private void MovementInput()
    {
        _movement = Vector3.zero;
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");

        _movement.Normalize();
    }
    #endregion

    #region Movement
    /// <summary>
    /// Player _movement
    /// </summary>
    private void Movement()
    {
        if (!CanMove) return;
        if (_movement == Vector3.zero) return;

        _activeMoveSpeed = WalkSpeed;

        _rb.MovePosition(transform.position + _movement * _activeMoveSpeed * Time.deltaTime);
    }
    #endregion
}
