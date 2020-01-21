using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIController : MonoBehaviour {

    public static UIController uic;

    public GameObject ShootGUI;
    public GameObject BuildGUI;
    public GameObject ShopMenu;
    public GameObject WinText;

    public GameObject Shop;
    public GameObject Miner;
    public static GameObject currentBuilding;

    public Text DroneText;
    public Text AmmoText;
    public Text HealthText;
    public Text StoneText;
    public Text MinePowderText;
    public Text RubiesText;
    public Text EmeraldsText;
    public Text DroneGUIText;
    public Text FlagGUIText;
    public Text FlagCountText;
    public Text ShopGUIText;
    public Text UnselectedGUIText;
    public Text MinerGUIText;
    public Text MouseMessageText;
    

    public Texture2D aimCursor;
    public Texture2D xCursor;

    public CursorMode cursorMode = CursorMode.ForceSoftware;

    private void Awake() {

        if (uic != null) {
            Destroy(uic);
        }
        uic = this;

        Cursor.SetCursor(aimCursor , Vector2.zero , cursorMode);
        FlagCountText.enabled = false;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Vector3 mousePos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
        FlagCountText.text = PlayerInfo.flags.ToString();
        FlagCountText.transform.position = Input.mousePosition + new Vector3(20f, -20f, 0f);

        if (PlayerInfo.controlMode == PlayerInfo.ControlMode.Shooting) {

            ShootGUI.SetActive(true);
            BuildGUI.SetActive(false);

            if (PlayerInfo.secondaryShootingMode == PlayerInfo.SecondaryShootingMode.Drone) {

                DroneGUIText.color = Color.red;
                FlagGUIText.color = Color.black;
                FlagCountText.enabled = false;

            }
            else if (PlayerInfo.secondaryShootingMode == PlayerInfo.SecondaryShootingMode.Flag) {

                DroneGUIText.color = Color.black;
                FlagGUIText.color = Color.red;
                FlagCountText.enabled = true;

            }
            else {

                DroneGUIText.color = Color.black;
                FlagGUIText.color = Color.black;
                FlagCountText.enabled = false;

            }


        }
        else if (PlayerInfo.controlMode == PlayerInfo.ControlMode.Build) {

            ShootGUI.SetActive(false);
            BuildGUI.SetActive(true);

        }

        /*if (Input.GetKeyDown(KeyCode.Q)) {

            if (modeToggle) {//going into build mode
                modeToggle = !modeToggle;
                ShootGUI.SetActive(false);
                BuildGUI.SetActive(true);
                PlayerInfo.controlMode = PlayerInfo.ControlMode.Build;
                PlayerInfo.buildingMode = PlayerInfo.BuildingMode.Unselected;
                FlagCountText.enabled = false;
                ShopGUIText.color = Color.red;
                MinerGUIText.color = Color.red;
                UnselectedGUIText.color = Color.black;
                currentBuilding = null;
                //Destroy(currentBuilding);
                Cursor.visible = true;
            }
            else {//goint into shoot mode
                modeToggle = !modeToggle;
                ShootGUI.SetActive(true);
                BuildGUI.SetActive(false);
                PlayerInfo.controlMode = PlayerInfo.ControlMode.Shooting;
                PlayerInfo.secondaryShootingMode = PlayerInfo.SecondaryShootingMode.Drone;
                DroneGUIText.color = Color.black;
                FlagGUIText.color = Color.red;
                FlagCountText.enabled = false;
                currentBuilding = null;
                //Destroy(currentBuilding);

                Cursor.visible = true;
            }

        }

        if (Input.GetKeyDown(KeyCode.Alpha0)) {

            if (PlayerInfo.controlMode == PlayerInfo.ControlMode.Shooting) {

            }
            else if (PlayerInfo.controlMode == PlayerInfo.ControlMode.Build) {

                PlayerInfo.buildingMode = PlayerInfo.BuildingMode.Unselected;
                ShopGUIText.color = Color.red;
                MinerGUIText.color = Color.red;
                UnselectedGUIText.color = Color.black;
                currentBuilding = null;
                //Destroy(currentBuilding);
                Cursor.visible = true;

            }

        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) {

            if (PlayerInfo.controlMode == PlayerInfo.ControlMode.Shooting) {
                PlayerInfo.secondaryShootingMode = PlayerInfo.SecondaryShootingMode.Drone;
                DroneGUIText.color = Color.black;
                FlagGUIText.color = Color.red;
                FlagCountText.enabled = false;
            }
            else if (PlayerInfo.controlMode == PlayerInfo.ControlMode.Build) {
                PlayerInfo.buildingMode = PlayerInfo.BuildingMode.Shop;
                ShopGUIText.color = Color.black;
                MinerGUIText.color = Color.red;
                UnselectedGUIText.color = Color.red;

                currentBuilding = null;
                //Destroy(currentBuilding);

                currentBuilding = Instantiate(Shop , mousePos , Quaternion.identity);

                Cursor.visible = false;
            }

        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) {

            if (PlayerInfo.controlMode == PlayerInfo.ControlMode.Shooting) {
                PlayerInfo.secondaryShootingMode = PlayerInfo.SecondaryShootingMode.Flag;
                DroneGUIText.color = Color.red;
                FlagGUIText.color = Color.black;
                FlagCountText.enabled = true;
            }
            else if (PlayerInfo.controlMode == PlayerInfo.ControlMode.Build) {

                PlayerInfo.buildingMode = PlayerInfo.BuildingMode.Miner;
                ShopGUIText.color = Color.red;
                MinerGUIText.color = Color.black;
                UnselectedGUIText.color = Color.red;

                currentBuilding = null;
                //Destroy(currentBuilding);

                currentBuilding = Instantiate(Miner, mousePos, Quaternion.identity);

                Cursor.visible = false;

            }

        }*/

        DroneText.text = PlayerInfo.droneCount + " / " + PlayerInfo.maxDrones;
        AmmoText.text = PlayerInfo.ammo.ToString();
        HealthText.text = PlayerInfo.health.ToString();
        StoneText.text = PlayerInfo.stone + " / " + PlayerInfo.stoneGoal;
        MinePowderText.text = PlayerInfo.blackMinePowder + " / " + PlayerInfo.blackPowederGoal;
        RubiesText.text = PlayerInfo.rubies + " / " + PlayerInfo.rubyGoal;
        EmeraldsText.text = PlayerInfo.emeralds + " / " + PlayerInfo.emeraldGoal;

        if (PlayerInfo.stone >= PlayerInfo.stoneGoal && PlayerInfo.blackMinePowder >= PlayerInfo.blackPowederGoal && PlayerInfo.rubies >= PlayerInfo.rubyGoal 
            && PlayerInfo.emeralds>= PlayerInfo.emeraldGoal) {

            WinText.SetActive(true);

        }

    }

    public static void MouseMessage(string message, Color color, float duration) {

        uic.MouseMessageText.text = message;
        uic.MouseMessageText.color = color;
        uic.MouseMessageText.transform.position = Input.mousePosition + new Vector3(20f , -20f , 0f);
        uic.MouseMessageText.canvasRenderer.SetAlpha(1f);

        uic.MouseMessageText.CrossFadeAlpha(0f, duration, false);

    }

}
