using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float LifeTime;
    public float Damage;
    public float Force;

    private Rigidbody2D _rigidbody;
    private bool _isShot;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Rotate();
    }

    public void Shoot()
    {
        _rigidbody.simulated = true;
        _rigidbody.AddForce(transform.right * Force, ForceMode2D.Impulse);
        Destroy(gameObject, LifeTime);
        _isShot = true;
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
}
