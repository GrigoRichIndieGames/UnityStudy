using UnityEngine;

public class MoveController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Animator _animator;//Интерфейс для контроля анимационной системы Mecanim.
    private Vector3 _leftFlip = new Vector3(0, 180, 0);
    private Vector2 _horizontalVelocity;
    private float _horizontalSpeed;
    private float _verticalSpeed;
    private float _signPreviosFrame;
    private float _signCurrentFrame;
    private bool _isSit;

    public float MoveSpeed;


    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        _horizontalSpeed = Input.GetAxis("Horizontal");
        _verticalSpeed = Input.GetAxis("Vertical");

        Flip();
        Sit();
        Animate();
    }


    private void Move()
    {
        if (!_isSit)
        {
            _horizontalVelocity.Set(_horizontalSpeed * MoveSpeed, _rigidbody.velocity.y);
            _rigidbody.velocity = _horizontalVelocity;
        }
    }

    private void Flip()
    {
        _signCurrentFrame = _horizontalSpeed == 0 ? _signPreviosFrame : Mathf.Sign(_horizontalSpeed);

        if (_signCurrentFrame != _signPreviosFrame)
        {
            transform.rotation = Quaternion.Euler(_horizontalSpeed < 0 ? _leftFlip : Vector3.zero);
        }
        _signPreviosFrame = _signCurrentFrame;
    }

    private void Sit()
    {
        _isSit = _verticalSpeed < 0 ? true : false;
    }

    private void Animate()
    {
        _animator.SetBool("IsRun", _horizontalSpeed != 0 ? true : false);
        _animator.SetBool("IsSit", _isSit ? true : false);
    }
}
