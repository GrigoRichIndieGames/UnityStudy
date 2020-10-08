using UnityEngine;

public sealed class MeleeAttack : MonoBehaviour
{
    public Transform AttackPoint;
    public LayerMask DamageableLayerMask;
    public float Damage;
    public float AttackRadius;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            _animator.SetTrigger("Attack");
        }
    }

    public void Attack()
    {
        var enemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRadius, DamageableLayerMask);
        if (enemies.Length > 0)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<DamageableEntity>().TakeDamage(Damage);
            }
        }
    }
}
