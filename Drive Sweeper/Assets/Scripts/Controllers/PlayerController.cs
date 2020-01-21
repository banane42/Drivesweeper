using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    public float rotationSpeed;

    bool modeToggle;

    Rigidbody2D rb;

    // Use this for initialization
    private void Awake() {

        rb = GetComponent<Rigidbody2D>();

        modeToggle = false;
        PlayerInfo.controlMode = PlayerInfo.ControlMode.Shooting;
        PlayerInfo.secondaryShootingMode = PlayerInfo.SecondaryShootingMode.Drone;
        PlayerInfo.buildingMode = PlayerInfo.BuildingMode.Null;

    }

    private void Update() {

        if (Input.GetKeyDown(KeyCode.Q)) {

            modeToggle = !modeToggle;

            if (modeToggle) {//go into build mode

                PlayerInfo.controlMode = PlayerInfo.ControlMode.Build;
                PlayerInfo.buildingMode = PlayerInfo.BuildingMode.Unselected;
                PlayerInfo.secondaryShootingMode = PlayerInfo.SecondaryShootingMode.Null;

            }
            else {// go into shoot mode

                PlayerInfo.controlMode = PlayerInfo.ControlMode.Shooting;
                PlayerInfo.secondaryShootingMode = PlayerInfo.SecondaryShootingMode.Drone;
                PlayerInfo.buildingMode = PlayerInfo.BuildingMode.Null;

            }

        }

        if (Input.GetKeyDown(KeyCode.Alpha0)) {

            if (PlayerInfo.controlMode == PlayerInfo.ControlMode.Shooting) {



            }
            else if (PlayerInfo.controlMode == PlayerInfo.ControlMode.Build) {



            }

        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) {

            if (PlayerInfo.controlMode == PlayerInfo.ControlMode.Shooting) {

                PlayerInfo.secondaryShootingMode = PlayerInfo.SecondaryShootingMode.Drone;

            }
            else if (PlayerInfo.controlMode == PlayerInfo.ControlMode.Build) {



            }

        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) {

            if (PlayerInfo.controlMode == PlayerInfo.ControlMode.Shooting) {

                PlayerInfo.secondaryShootingMode = PlayerInfo.SecondaryShootingMode.Flag;

            }
            else if (PlayerInfo.controlMode == PlayerInfo.ControlMode.Build) {



            }

        }

    }

    private void FixedUpdate() {

        float moveHorizontal = -1 * Input.GetAxis("Horizontal");//A and D key
        float moveVertical = -1 * Input.GetAxis("Vertical");//W and S key

        rb.angularVelocity += rotationSpeed * moveHorizontal;
        rb.velocity += (Vector2) transform.up * (moveVertical * moveSpeed);
    }
}
