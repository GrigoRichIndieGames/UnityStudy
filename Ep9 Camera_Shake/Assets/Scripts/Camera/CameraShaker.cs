using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    public int QtOfAnimations = 5;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Shake()
    {
        int index = Random.Range(0, QtOfAnimations + 1);

        _animator.SetTrigger($"Shake{index}");
    }
}
