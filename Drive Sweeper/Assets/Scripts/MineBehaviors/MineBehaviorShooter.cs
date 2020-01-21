using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineBehaviorShooter : MineBehavior
{
    /*
     * Behavior for the shooting mine type
     * Inherits from MineBehavior
     */

    public GameObject bullet;
    public float range;
    public float fireRate;
    float nextFire = 0f;

    void Awake()
    {

        targetObject = GameObject.FindGameObjectWithTag("Player").transform;
        //type = "shooter";

    }

    void Update()
    {
        float targetDist = Vector2.Distance(transform.position , targetObject.position);

        if (targetDist < range) {

            //shoot at player
            Vector2 fireVector = GameController.gc.player.transform.position - transform.position;
            fireVector.Normalize();
            float zRotation = Mathf.Atan2(fireVector.y , fireVector.x) * Mathf.Rad2Deg - 90f;

            if (Time.time > nextFire) {
                Instantiate(bullet , transform.position , Quaternion.Euler(0f , 0f , zRotation));
                nextFire = Time.time + fireRate;
            }
            

        }
        else {

            //move towards player
            transform.position = Vector2.MoveTowards(transform.position , targetObject.position , speed * Time.deltaTime);

        }

    }
}
