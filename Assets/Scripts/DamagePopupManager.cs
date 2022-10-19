using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePopupManager : MonoBehaviour
{
    [SerializeField] private Health _healthToTrack;
    [SerializeField] private GameObject _damagePopupPrefab;
    [SerializeField] private float _maxDistance = 0.01f;
    private void Start()
    {
        _healthToTrack.takeDamage.AddListener(CreateDamagePopup);
    }
    private void CreateDamagePopup(float damageAmount)
    {
        GameObject damagePopupGameObject = Instantiate(_damagePopupPrefab, transform.position, Quaternion.identity, transform);
        damagePopupGameObject.GetComponent<RectTransform>().localPosition = new Vector2(Random.Range(-1 * _maxDistance, _maxDistance), Random.Range(-1 * _maxDistance, _maxDistance));
        DamagePopup damagePopup = damagePopupGameObject.GetComponent<DamagePopup>();
        if (damagePopup) damagePopup.ShowDamage(damageAmount);
    }
}
