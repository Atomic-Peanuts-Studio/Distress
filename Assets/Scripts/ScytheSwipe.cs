using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScytheSwipe : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private float _chargeTimer = 0.5f;
    [SerializeField] public float _damage = 1f;
    [Header("Referenced Objects")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private float _currentChargeTimer = 0;
    private Health _playerInSwipe;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) _playerInSwipe = collision.gameObject.GetComponent<Health>();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Health>() == _playerInSwipe) _playerInSwipe = null;
    }
    private void Update()
    {
        _currentChargeTimer += Time.deltaTime;
        var chargeProgress = _currentChargeTimer / _chargeTimer;
        _spriteRenderer.color = new Color(chargeProgress, 0, 0, 1);
        if (_currentChargeTimer > _chargeTimer)
        {
            if (_playerInSwipe != null) _playerInSwipe.GetHit((int)_damage, gameObject);
            Destroy(gameObject);
        }
    }
}
