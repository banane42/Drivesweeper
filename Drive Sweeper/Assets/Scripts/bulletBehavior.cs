using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletBehavior : MonoBehaviour {

    public float maxSpeed;
    private float lifeTime = 0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        lifeTime += Time.deltaTime;

        if (lifeTime > 3) {
            Destroy(gameObject);
        }

        Vector3 pos = transform.position;

        Vector3 velcoity = new Vector3(0, maxSpeed * Time.deltaTime, 0);

        pos += transform.rotation * velcoity;

        transform.position = pos;

        

	}
}
