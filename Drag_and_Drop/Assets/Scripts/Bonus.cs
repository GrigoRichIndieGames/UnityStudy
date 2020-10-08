using UnityEngine;


namespace GrigoRichIndieGames
{
    public sealed class Bonus : MonoBehaviour
    {
        public Rigidbody2D _rigidbody;
        public Animator _animator;
        public SpriteRenderer _renderer;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _renderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            _rigidbody.AddForce(Vector2.up, ForceMode2D.Force);
        }
    }
}