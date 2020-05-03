using UnityEngine;

public class MeleeAttackController : MonoBehaviour
{
    public Transform AttackPoint;
    public LayerMask DamageableLayerMask;
    public float Damage;
    public float AttackRange;
    public float TimeBtwAttack;

    private Animator _animator;
    private float _timer;

    private void Start()
    {
        GetReferences();
    }

    private void Update()
    {
        Attack();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    }

    private void Attack()
    {
        if (_timer <= 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                _animator.SetTrigger("Attack");

                Collider2D[] enemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, DamageableLayerMask);

                if (enemies.Length != 0)
                {
                    for (int i = 0; i < enemies.Length; i++)
                    {
                        enemies[i].GetComponent<DamageableObject>().TakeDamage(Damage);
                    }
                }

                _timer = TimeBtwAttack;
            }
        }
        else
        {
            _timer -= Time.deltaTime;
        }
    }

    private void GetReferences()
    {
        _animator = GetComponent<Animator>();
    }
}
