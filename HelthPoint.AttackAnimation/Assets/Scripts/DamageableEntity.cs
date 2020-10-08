using UnityEngine;

public class DamageableEntity : MonoBehaviour
{
    public GameObject HPbarPrefab;
    public Transform CanvasTransform;
    public Transform HPBarAnchor;
    public float MaxHealthPoints;
    [HideInInspector] public float HealthPoints;

    private HPBar _hpBar;

    private void Awake()
    {
        HealthPoints = MaxHealthPoints;

        var bar = Instantiate(HPbarPrefab, CanvasTransform);

        _hpBar = bar.GetComponent<HPBar>();

        _hpBar.Initialize(HPBarAnchor, this);
    }

    public void TakeDamage(float damage)
    {
        HealthPoints -= damage;

        if (HealthPoints <= 0)
        {
            HealthPoints = 0;
            Die();
        }

        _hpBar.UpdateBar();
    }

    public void Die()
    {
        Destroy(_hpBar.gameObject);
        Destroy(gameObject);
    }
}
