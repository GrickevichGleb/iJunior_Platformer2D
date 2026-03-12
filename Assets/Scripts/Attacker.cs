using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Visualizer))]
public class Attacker : MonoBehaviour
{
    [SerializeField] private int _damage  = 20;
    [SerializeField] private float _range = 2.5f;
    [SerializeField] private float _cooldown = 1f;

    public float Range() => _range;
    
    private Collider2D _collider;
    private Visualizer _visualizer;

    private bool _isAttacking = false;
    private bool _canAttack = true;
    
    private void Awake()
    {
        _visualizer = GetComponent<Visualizer>();
        _collider = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        if(_isAttacking)
            PerformAttack();
    }

    public void Attack()
    {
        if (_canAttack)
            _isAttacking = true;
    }

    private void PerformAttack()
    {
        Vector3 direction = _collider.bounds.center + (transform.right * _range);
        
        RaycastHit2D[] hits = Physics2D.RaycastAll(_collider.bounds.center, transform.right, _range);

        Debug.DrawLine(_collider.bounds.center, direction, Color.red, 1f);
        
        foreach (var hit in hits)
        {
            if(hit.collider.gameObject == this.gameObject)
                continue;

            if (hit.collider.TryGetComponent(out Health health))
            {
                health.TakeDamage(_damage);
                Debug.Log($"{hit.collider.name} got {_damage} damage, now has {health.Current()} health");
            }
        }
        
        _visualizer.OnAttack();
        _isAttacking = false;

        StartCoroutine(AttackCooldown(_cooldown));
    }

    private IEnumerator AttackCooldown(float seconds)
    {
        _canAttack = false;

        yield return new WaitForSeconds(seconds);

        _canAttack = true;
    }
}
