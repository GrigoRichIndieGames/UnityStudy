using UnityEngine;

public class RangeAttackController : MonoBehaviour
{
    public GameObject ArrowPrefab;
    public GameObject PointPrefab;
    public Transform RangeWeaponPosition;
    public Transform ArrowSpawnPosition;
    public Transform TargetPoint;
    public float TimeBtwShot;
    public float DistanceBtwPoint;
    public int NumberOfPoints;

    private GameObject[] _trajectoryPoints;
    private Arrow _currentArrow;
    private Vector3 _direction;
    private float _rotationZ;
    private float _timer;
    private bool _hasArrow;


    private void Start()
    {
        CreateArrow();
        CreateTrajectoryPoints();
    }

    private void Update()
    {
        Aim();
        CreateArrow();
        CalculateTrajectory();
        Shoot();
    }

    public void Shoot()
    {
        if (Input.GetButtonDown("Fire1") && _hasArrow)
        {
            _currentArrow.Shoot();
            _currentArrow.transform.parent = null;
            _hasArrow = false;
        }
    }

    private void Aim()
    {
        _direction = TargetPoint.position - RangeWeaponPosition.position;
        if (Input.GetButton("Fire2"))
        {
            _rotationZ = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
            RangeWeaponPosition.rotation = Quaternion.Euler(0, 0, _rotationZ);
        }
    }

    private void CreateArrow()
    {
        if (!_hasArrow && _timer <= 0)
        {
            _currentArrow = Instantiate(ArrowPrefab, ArrowSpawnPosition).GetComponent<Arrow>();
            _hasArrow = true;
            _timer = TimeBtwShot;
        }
        else if (!_hasArrow)
        {
            _timer -= Time.deltaTime;
        }
    }

    private void CalculateTrajectory()
    {
        for (int i = 0; i < _trajectoryPoints.Length; i++)
        {
            _trajectoryPoints[i].transform.position = CalculatePointPosition(i * DistanceBtwPoint);
        }
    }

    private Vector2 CalculatePointPosition(float distance)
    {
        Vector2 position = (Vector2)ArrowSpawnPosition.position +
            (Vector2)(_direction.normalized * _currentArrow.Force * distance) +
            (Physics2D.gravity * (distance * distance) * 0.5f);
        return position;
    }

    private void CreateTrajectoryPoints()
    {
        _trajectoryPoints = new GameObject[NumberOfPoints];

        for (int i = 0; i < NumberOfPoints; i++)
        {
            _trajectoryPoints[i] = Instantiate(PointPrefab, ArrowSpawnPosition);
        }
    }
}
