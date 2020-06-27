using UnityEngine;

public class DamageableObject : MonoBehaviour
{
    //*****
    public GameObject HitPrefab;
    public Transform VFXPosition;
    //****

    [SerializeField] private float _healthPoints;

    public void TakeDamage(float damage)
    {
        _healthPoints -= damage;

        if (_healthPoints <= 0)
        {
            Die();
        }

        var clone = Instantiate(HitPrefab, VFXPosition);
        Destroy(clone, 2);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
