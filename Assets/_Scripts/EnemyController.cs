using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy")]
    public Rigidbody2D Rb;
    public Animator Animator;
    public Transform Target;


    [Header("Base Stats")]
    public float BaseHealth;
    public float BaseMoveSpeed;

    [Range(0, 2)] public float KnockbackRes = 1;
    public bool InmunityKnockBack = false;

    [Range(0, 2)] public float StunResistance = 1;

    [Header("Base Attack")]
    public float AttackRadius;


    [Header("Other")]
    public bool CanMove = true;
    public bool IsActive = true;


    private void Awake()
    {
        Target = FindObjectOfType<PlayerController>().transform;
        Rb = this.GetComponent<Rigidbody2D>();
        Animator = this.GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void LateUpdate()
    {
        FlipSprite();
    }

    public virtual void Knockback()
    {
        if (InmunityKnockBack || KnockbackRes == 0) return;
        CanMove = false;

        var diff = transform.position - Target.transform.position;
        Rb.AddForce(diff, ForceMode2D.Impulse);

        StartCoroutine(KnocBackTime(Rb));
    }

    public IEnumerator KnocBackTime(Rigidbody2D rigidbody2D)
    {
        yield return new WaitForSeconds(.1f * KnockbackRes); // Wait for time before stopping knockback
        rigidbody2D.velocity = Vector3.zero;
        CanMove = true;
        yield return null;
    }

    // Stun the enemy for 1 second and calculate stun resistance
    public virtual void Stun(float time)
    {
        StartCoroutine(StunTime(time));
    }

    // Stun time
    public IEnumerator StunTime(float time)
    {
        CanMove = false;
        yield return new WaitForSeconds(time * StunResistance);
        CanMove = true;
        yield return null;
    }

    // Take damage
    public virtual void TakeDamage(float damage)
    {
        BaseHealth -= damage;
        if (BaseHealth <= 0)
        {
            Die();
        }
    }

    // Die
    public virtual void Die()
    {
        Destroy(this.gameObject);
    }

    // Lerp number from 0 to 1 with the time specified
    public float LerpNumber(float time)
    {
        return Mathf.Lerp(0, 1, time);
    }

    // Flip sprite if looking at the other direction
    public void FlipSprite()
    {
        if (transform.position.x < Target.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
