using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private float _maxVelocity;
    [SerializeField] private Rigidbody2D _rb;
    private void Awake()
    {
        _text.outlineColor = Color.white;
        _text.outlineWidth = 0.2f;
    }
    public void ShowDamage(float damage)
    {
        _text.enabled = true;
        _text.text = damage.ToString();
        _text.color = new Color(damage / 100, 0, 0, 1);
        float speed = Random.Range(0, _maxVelocity);
        Vector2 direction = Random.insideUnitCircle;
        Vector2 velocity = direction * speed;
        _rb.velocity = velocity;
        Destroy(gameObject, 0.6f);
    }
}
