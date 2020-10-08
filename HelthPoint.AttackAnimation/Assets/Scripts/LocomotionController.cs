using UnityEngine;

public class LocomotionController : MonoBehaviour
{
    public LayerMask GroundLayerMask;
    public float Speed;
    public float JumpForce;
    public float CheckGroundRadius;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private Vector3 _leftFlip = new Vector3(0, 180, 0);
    private Vector2 _horizontalVelocity;
    private float _signCurrentFrame;
    private float _signPreviousFrame;
    private bool _isGround;


    private void Awake()
    {
        GetReferences();
    }

    private void Update()
    {
        StateUpdate();
        Flip();
        Animate();
        Jump();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void StateUpdate()
    {
        _isGround = Physics2D.OverlapCircle(transform.position, CheckGroundRadius, GroundLayerMask);
    }

    private void Move()
    {
        _horizontalVelocity.Set(Input.GetAxis("Horizontal") * Speed, _rigidbody.velocity.y);
        _rigidbody.velocity = _horizontalVelocity;
    }

    private void Flip()
    {
        _signCurrentFrame = Input.GetAxis("Horizontal") == 0 ? _signPreviousFrame : Mathf.Sign(Input.GetAxis("Horizontal"));
        if (_signCurrentFrame != _signPreviousFrame)
        {
            transform.rotation = Quaternion.Euler(Input.GetAxis("Horizontal") < 0 ? _leftFlip : Vector3.zero);
        }
        _signPreviousFrame = _signCurrentFrame;
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && _isGround)
        {
            _rigidbody.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        }
    }

    private void Animate()
    {
        _animator.SetFloat("HorizontalVelocity", _isGround ? Mathf.Abs(Input.GetAxis("Horizontal")) : 0);
        _animator.SetFloat("VerticalVelocity", _rigidbody.velocity.y);
        _animator.SetBool("IsJump", !_isGround);
    }

    private void GetReferences()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }
}
