using UnityEngine;

public class Arrow : MonoBehaviour
{
    public TrailRenderer Trail;
    public LayerMask DamageableLayerMask;
    public LayerMask ObstacleLayerMask;
    public float LifeTime;
    public float Damage;
    public float Force;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private Vector3 _previousFramePosition;
    private bool _isShot;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        Trail.gameObject.SetActive(false);
    }

    private void Update()
    {
        Rotate();
    }

    private void FixedUpdate()
    {
        StateUpdate();
    }

    public void Shoot()
    {
        Trail.gameObject.SetActive(true);
        _rigidbody.simulated = true;
        _rigidbody.AddForce(transform.right * Force, ForceMode2D.Impulse);
        Destroy(gameObject, LifeTime);
        _isShot = true;
        _previousFramePosition = transform.position;
    }

    private void Rotate()
    {
        if (_isShot)
        {
            var direction = _rigidbody.velocity;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private void HitEntity(Collider2D entity)
    {
        entity.GetComponent<DamageableObject>().TakeDamage(Damage);
        Destroy(gameObject);
    }

    private void HitObstacle(Vector3 position)
    {
        transform.position = position;
        _rigidbody.simulated = false;

        _animator.SetTrigger("Hit");
    }

    private void StateUpdate()
    {
        var lineEntity = Physics2D.Linecast(_previousFramePosition, transform.position, DamageableLayerMask);
        if (lineEntity && !lineEntity.collider.isTrigger)
        {
            HitEntity(lineEntity.collider);
        }
        var lineObstacle = Physics2D.Linecast(_previousFramePosition, transform.position, ObstacleLayerMask);
        if (lineObstacle && !lineObstacle.collider.isTrigger)
        {
            HitObstacle(lineObstacle.point);
        }

        _previousFramePosition = transform.position;
    }
}
