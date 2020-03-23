using UnityEngine;

public class EnemyAI : DamageableObject
{
    public float Speed;

    private EnemyMoveController _moveController;


    private void Start()
    {
        GetReferences();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        _moveController.Move(Speed);
    }

    private void GetReferences()
    {
        _moveController = GetComponent<EnemyMoveController>();
    }
}
