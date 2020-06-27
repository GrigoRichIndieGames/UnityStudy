using UnityEngine;

public class EnemyMoveController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private Vector2 _horizontalVelocity;
    private Vector3 _leftFlip = new Vector3(0, 180, 0);
    private float _signPreviosFrame;
    private float _signCurrentFrame;

    private void Start()
    {
        GetReferences();
    }

    private void Update()
    {
        Flip();
    }

    public void Move(float speed)
    {
        _horizontalVelocity.Set(speed, _rigidbody.velocity.y);
        _rigidbody.velocity = _horizontalVelocity;
    }

    private void Flip()
    {
        _signCurrentFrame = _rigidbody.velocity.x == 0 ? _signPreviosFrame : Mathf.Sign(_rigidbody.velocity.x);

        if (_signCurrentFrame != _signPreviosFrame)
        {
            transform.rotation = Quaternion.Euler(_rigidbody.velocity.x < 0 ? _leftFlip : Vector3.zero);
        }
        _signPreviosFrame = _signCurrentFrame;
    }

    private void GetReferences()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }
}
