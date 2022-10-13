using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    public void ShowDamage(float damage)
    {
        _text.enabled = true;
        _text.text = damage.ToString();
        _text.color = new Color(damage / 100, 0, 0, 1);
        Destroy(gameObject, 0.3f);
    }
}
