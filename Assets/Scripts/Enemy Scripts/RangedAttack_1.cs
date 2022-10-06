using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack_1 : EnemyState
{

     //SpriteRenderer sprite;
    public GameObject bulletPrefab;
    [SerializeField] private Transform fPlayer;
    public Transform bulletPos;
    public float bulletForce = 10f;
    public float rateOfFire = 1.0f;

    private void OnEnable(){
        Type = StateType.RangedAttack;
    }
    public override void DoUpdate() {
        RangedAttackOne();
    }


    public void RangedAttackOne() {
        if (_owner.meleeAttackCooldown < 0.1f)
        {
            GameObject newBullet = Instantiate(bulletPrefab,
                    transform.position, transform.rotation);

            Rigidbody2D newRigidBodyBullet = newBullet.GetComponent<Rigidbody2D>();

            newRigidBodyBullet.velocity = (fPlayer.position -
                    bulletPos.position).normalized * bulletForce;

            Destroy(newBullet, 2.0f);
            _owner.meleeAttackCooldown = rateOfFire;
            _owner.ChangeState(nextState);
        }
    }

}
