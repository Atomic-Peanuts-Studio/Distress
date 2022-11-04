using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpreadAttack : EnemyStateWithWaiting
{
    [SerializeField] private float _amountOfBullets;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _bulletForce = 10f;
    private void OnEnable()
    {
        Type = StateType.RangedAttack;
    }
    internal override void PerformMainBehavior()
    {
        var angle = 180 / _amountOfBullets;
        for (int i = 0; i < _amountOfBullets; i++)
        {
            var dirX = _owner.enemyRoot.position.x + Mathf.Sin((((angle * i) + 180f * i) * Mathf.PI) / 180f);
            var dirY = _owner.enemyRoot.position.y + Mathf.Cos((((angle * i) + 180f * i) * Mathf.PI) / 180f);
            var bulMoveVector = new Vector3(dirX, dirY, 0f);
            var bulMoveDirection = (bulMoveVector - _owner.enemyRoot.position).normalized;

            var bullet = Instantiate(_bulletPrefab, _owner.enemyRoot.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = bulMoveDirection * _bulletForce;
            Destroy(bullet, 4);
        }
        OnMainBehaviorDone();
    }
}
