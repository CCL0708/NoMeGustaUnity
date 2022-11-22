using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretscript : MonoBehaviour
{
    public enum ENEMY_STATE
    {

        Attack,
        Nothing
       
    }

    public float Range;

    public Transform Target;

    bool Detected = false;

    Vector2 Direction;

    public GameObject AlarmLight;

    public GameObject Bullet;

    public float FireRate;

    float NextTimeToFire = 0;

    public Transform ShootPoint;

    public float Force;

    public GameObject scaryeye;

    public GameObject closedeye;

    public ENEMY_STATE enemyState;

    




    void Start()
    {
        enemyState = ENEMY_STATE.Nothing;
    }

    // Update is called once per frame
    void Update()
    {
        switch (enemyState)
        {
            

            case ENEMY_STATE.Nothing:

                scaryeye.SetActive(false);
                closedeye.SetActive(true);

                Vector2 targetPos = Target.position;

                Direction = targetPos - (Vector2)transform.position;
                
                RaycastHit2D rayInfo = Physics2D.Raycast(transform.position, Direction, Range);
                if (rayInfo)
                {
                    if (rayInfo.collider.gameObject.tag == "Player")
                    {   
                        enemyState = ENEMY_STATE.Attack;
                        Detected = true;
       
                    }
                    else
                    {
                        Detected = false;
                        AlarmLight.GetComponent<SpriteRenderer>().color = Color.green;
                        
                    }
                }
                break;

            case ENEMY_STATE.Attack:

                if (Detected == true)
                {
                    scaryeye.SetActive(true);
                    closedeye.SetActive(false);
                    AlarmLight.GetComponent<SpriteRenderer>().color = Color.red;
                    
                    
                    if(Time.time > NextTimeToFire)
                    {
                        NextTimeToFire = Time.time + 1 / FireRate;
                        shoot();
                        Detected = false;
                        break; 
                    }
                    




                }
               else 
                {
                    
                        enemyState = ENEMY_STATE.Nothing;
                       
                    
                }
                break;
        }
    }
                   void shoot()
                    {
                         GameObject BulletIns = Instantiate(Bullet, ShootPoint.position, Quaternion.identity);
                         BulletIns.GetComponent<Rigidbody2D>().AddForce(Direction * Force);
                         
                    }
               






                void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}

