using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootingScript : MonoBehaviour {

    public float offsetRotation = 180;
    //private float fireRate;
    float nextFire = 0f;

    public GameObject bullet;

    Transform firePoint;

    private void Awake() {
        firePoint = transform.Find("firePosition");
        
    }

    // Use this for initialization
    void Start () {
        //fireRate = PlayerInfo.fireRate;
    }
	
	// Update is called once per frame
	void Update () {

        if (PlayerInfo.controlMode == PlayerInfo.ControlMode.Shooting) {

            Vector2 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            difference.Normalize();

            float rotationZ = Mathf.Atan2(difference.y , difference.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0f , 0f , rotationZ + offsetRotation);

            if (PlayerInfo.controlMode == PlayerInfo.ControlMode.Shooting && Input.GetButton("Fire1") && Time.time > nextFire && PlayerInfo.ammo > 0 && PlayerInfo.canFire) {
                nextFire = Time.time + PlayerInfo.fireRate;
                Shoot();
            }

        }
        
	}

    void Shoot() {
        PlayerInfo.ammo -= 1;
        Instantiate(bullet, firePoint.position, firePoint.rotation);
    }
}
