using UnityEngine;

public class MoveController : MonoBehaviour
{
    public enum JumpType
    {
        None = 0,
        One = 1,
        Multiple = 2
    }

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private RaycastHit2D _checkGroundRay;
    private RaycastHit2D _checkBorderHeadRay;
    private RaycastHit2D _checkBorderBodyRay;
    private Vector3 _leftFlip = new Vector3(0, 180, 0);
    private Vector2 _horizontalVelocity;
    private float _horizontalSpeed;
    private float _verticalSpeed;
    private float _signPreviosFrame;
    private float _signCurrentFrame;
    private int _currentCountOfJump;
    private bool _isSit;
    private bool _isGround;
    private bool _isBorder;


    public Transform CheckBorderHeadRayTransform;
    public Transform CheckBorderBodyRayTransform;
    public LayerMask BorderLayerMask;
    public LayerMask GroundLayerMask;
    public JumpType TypeOfJump;
    public float MoveSpeed;
    public float JumpForce;
    public float RayDistance;
    public int MaxCountOfJump;


    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        _currentCountOfJump = MaxCountOfJump;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        _horizontalSpeed = Input.GetAxis("Horizontal");
        _verticalSpeed = Input.GetAxis("Vertical");

        StateUpdate();
        Jump();
        Flip();
        Sit();
        Animate();
    }

    private void StateUpdate()
    {
        _checkGroundRay = Physics2D.Raycast
            (
                transform.position,
                -Vector2.up,
                RayDistance,
                GroundLayerMask
            );
        _isGround = _checkGroundRay;

        _checkBorderHeadRay = Physics2D.Raycast
            (
                CheckBorderHeadRayTransform.position,
                CheckBorderHeadRayTransform.right,
                RayDistance,
                BorderLayerMask
            );
        _checkBorderBodyRay = Physics2D.Raycast
            (
                CheckBorderBodyRayTransform.position,
                CheckBorderBodyRayTransform.right,
                RayDistance,
                BorderLayerMask
            );
        _isBorder = _checkBorderHeadRay || _checkBorderBodyRay;

        Debug.DrawRay
            (
                transform.position,
                -Vector2.up * RayDistance,
                Color.red
            );
        Debug.DrawRay
           (
               CheckBorderHeadRayTransform.position,
                CheckBorderHeadRayTransform.right * RayDistance,
               Color.red
           );
        Debug.DrawRay
           (
               CheckBorderBodyRayTransform.position,
                CheckBorderBodyRayTransform.right * RayDistance,
               Color.red
           );
    }

    private void Move()
    {
        if (!_isSit && !_isBorder)
        {
            _horizontalVelocity.Set(_horizontalSpeed * MoveSpeed, _rigidbody.velocity.y);
            _rigidbody.velocity = _horizontalVelocity;
        }
    }

    private void Jump()
    {
        if (_isGround) _currentCountOfJump = MaxCountOfJump;

        if (Input.GetButtonDown("Jump"))
            switch (TypeOfJump)
            {
                case JumpType.None:
                    break;
                case JumpType.One:
                    if (_isGround)
                        _rigidbody.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
                    break;
                case JumpType.Multiple:
                    if (_currentCountOfJump > 0)
                    {
                        _rigidbody.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
                        _currentCountOfJump--;
                    }
                    break;
                default:
                    break;
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
        _animator.SetBool("IsJump", !_isGround ? true : false);
    }
}
