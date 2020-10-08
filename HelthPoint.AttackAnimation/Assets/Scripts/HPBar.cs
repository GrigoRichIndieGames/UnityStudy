using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public Image BarImage;
    public Text BarText;
    public Gradient Color;

    private Transform _anchor;
    private Camera _cameraMain;
    private DamageableEntity _entity;

    private void Awake()
    {
        _cameraMain = Camera.main;
    }

    public void Initialize(Transform anchor, DamageableEntity entity)
    {
        _anchor = anchor;
        transform.position = _cameraMain.WorldToScreenPoint(_anchor.position);

        _entity = entity;

        BarText.text = $"{_entity.HealthPoints} / {_entity.MaxHealthPoints}";
        BarImage.color = Color.Evaluate(1);
    }

    public void UpdateBar()
    {
        BarText.text = $"{_entity.HealthPoints} / {_entity.MaxHealthPoints}";

        var value = ((_entity.HealthPoints * 100) / _entity.MaxHealthPoints) / 100;

        BarImage.fillAmount = value;
        BarImage.color = Color.Evaluate(value);
    }
}
