using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineBehavior : MonoBehaviour {

    /*
     * Standard
     * Shooter
     * Shield
     * Healer
     * Tank
     * 
     */
    
    public Transform targetObject;
    public float speed;
    public string type;


    // Use this for initialization
    void Awake () {

        targetObject = GameObject.FindGameObjectWithTag("Player").transform;
        //type = "standard";

    }
	
	// Update is called once per frame
	void Update () {

        transform.position = Vector2.MoveTowards(transform.position, targetObject.position, speed * Time.deltaTime);

    }
}
