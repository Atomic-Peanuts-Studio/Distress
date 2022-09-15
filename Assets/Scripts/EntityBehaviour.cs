using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EntityState {
    Idle, 
    Chase,
    Attack,
    /**
    * The plan is to have the ranged enemy dodge as the player attempts to 
    * get in range. Taking a bit of inspiration from The Forest, & Gunfire 
    * with this one. We'll see How it feels. 
    */
    Dodge
}

public class EntityBehaviour : MonoBehaviour
{


    public EntityState State {get; private set;}
    [SerializeField] private Transform fPlayer;
    [SerializeField] private float fEntityMoveSpeed = 3f;
    public float chaseDistanceThreshold = 5f;
    public float attackDistanceThreshold = 2f;

    //SpriteRenderer sprite;
    public GameObject bulletPrefab;
    public Transform bulletPos;
    public float bulletForce = 10f;
    public float rateOfFire = 1.0f;
    private float rofTimer;


    // Start is called before the first frame update
    void OnEnable()
    {
        State = EntityState.Chase;
    }

    void Start () {
        //sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    { 
        switch (State){
            case EntityState.Idle:
                SpotPlayer();
                break;
            case EntityState.Chase:
                ChasePlayer();
                break;
            case EntityState.Attack:
                EnemyAttack();
                break;
            case EntityState.Dodge:
                EnemyDodge();
                break;
        }
    }


    /**
     *  The idea was to have this bit check to see if the player was in range, 
     *  however that ended up being handled within the ChasePlayer function. 
     *  Maybe I'll have this be a patrol state via Waypoints
     */
    void SpotPlayer() {
       
    }

    // Entity chases player when player is in range. 
    void ChasePlayer() {

        float distance = Vector2.Distance(transform.position, fPlayer.position);
        if (distance < chaseDistanceThreshold) {
            transform.position = Vector2.MoveTowards(transform.position, 
                    fPlayer.position, fEntityMoveSpeed * Time.deltaTime);
            
            if (distance <= attackDistanceThreshold)
                State = EntityState.Attack;
        }
    }

    // When in Range enemy will start attacking the player. 
    void EnemyAttack() {
      rofTimer -= Time.deltaTime;

      if(rofTimer > 0) return;

      rofTimer = rateOfFire;
    
      //sprite.color = new Color (255, 255, 0, 1);
      GameObject newBullet = Instantiate(bulletPrefab, transform.position, transform.rotation); 
      Rigidbody2D newRigidBodyBullet = newBullet.GetComponent<Rigidbody2D>();
      newRigidBodyBullet.velocity = (fPlayer.position - bulletPos.position).normalized * bulletForce;             

              
      Destroy(newBullet, 2.0f);
    }

    /**
     * Plan for this bad boy is to have the Entity reposition 'quickly' based
     * on the player actions. Basically entity will react to the player, but is 
     * unable to access this state more than once. Not sure how I'll do that 
     * but that's the idea
     */
    void EnemyDodge() {
       
    }
}
