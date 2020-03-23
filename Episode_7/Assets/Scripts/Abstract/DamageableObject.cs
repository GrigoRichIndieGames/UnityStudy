using UnityEngine;

public class DamageableObject : MonoBehaviour
{
    [SerializeField] private float _healthPoints;

    public void TakeDamage(float damage)
    {
        _healthPoints -= damage;

        if (_healthPoints <= 0)
        {
            Die();
        }

        print("Hit!");
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
