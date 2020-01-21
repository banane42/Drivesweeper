using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenuController : MonoBehaviour
{
    public Text AddButtonDroneTxt;
    int addDroneStonePrice;
    int addDroneBlackPowederPrice;

    PlayerInfo.ControlMode prevMode;

    public void MouseEnter() {

        Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
        PlayerInfo.canFire = false;

    }

    public void MouseExit() {

        Cursor.SetCursor(UIController.uic.aimCursor, Vector2.zero , CursorMode.ForceSoftware);
        PlayerInfo.canFire = true;

    }

    public void AmmoBtn() {

        if (PlayerInfo.stone >= 10 && PlayerInfo.blackMinePowder >= 3) {

            PlayerInfo.stone -= 10;
            PlayerInfo.blackMinePowder -= 3;

            PlayerInfo.ammo += 5;

            UIController.MouseMessage("Ammo Bought!" , Color.green , 1.5f);

        }
        else {

            UIController.MouseMessage("Insufficiant resources!", Color.red, 1.5f);

        }

    }

    public void FlagBtn() {

        if (PlayerInfo.stone >= 25) {

            PlayerInfo.stone -= 25;

            PlayerInfo.flags += 3;

            UIController.MouseMessage("Flags Bought! " + PlayerInfo.flags , Color.green , 1.5f);

        }
        else {

            UIController.MouseMessage("Insufficiant resources!" , Color.red , 1.5f);

        }

    }

    //Growth function Stone 100 * 1.15 ^ PlayerInfo.maxDrones rounded to the nearest 25
    //Growth function blackMinePoweder 50 * 1.15 ^ PlayerInfo.maxDrones rounded to the nearest 25
    public void AddDroneBtn() {

        if (PlayerInfo.maxDrones <= 3) {

            if (PlayerInfo.stone >= 100 && PlayerInfo.blackMinePowder >= 50) {

                PlayerInfo.stone -= 100;
                PlayerInfo.blackMinePowder -= 50;

                PlayerInfo.maxDrones += 1;

                addDroneStonePrice = Mathf.RoundToInt((100f * Mathf.Pow(1.15f , PlayerInfo.maxDrones)) / 25) * 25;
                addDroneBlackPowederPrice = Mathf.RoundToInt((50f * Mathf.Pow(1.15f , PlayerInfo.maxDrones)) / 25) * 25;

                AddButtonDroneTxt.text = "Stone: " + addDroneStonePrice + "\nMine\nPowder: " + addDroneBlackPowederPrice + "\n1 x 1 Drone";

                UIController.MouseMessage("Drone Added!" , Color.green , 1.5f);

            }
            else {

                UIController.MouseMessage("Insufficiant resources!" , Color.red , 1.5f);

            }

        }
        else if (PlayerInfo.stone >= addDroneStonePrice && PlayerInfo.blackMinePowder >= addDroneBlackPowederPrice) {

            PlayerInfo.stone -= addDroneStonePrice;
            PlayerInfo.blackMinePowder -= addDroneBlackPowederPrice;

            PlayerInfo.maxDrones += 1;

            addDroneStonePrice = Mathf.RoundToInt((100f * Mathf.Pow(1.15f , PlayerInfo.maxDrones)) / 25) * 25;
            addDroneBlackPowederPrice = Mathf.RoundToInt((50f * Mathf.Pow(1.15f , PlayerInfo.maxDrones)) / 25) * 25;

            AddButtonDroneTxt.text = "Stone: " + addDroneStonePrice + "\nMine\nPowder: " + addDroneBlackPowederPrice + "\n1 x 1 Drone";

        }
        else {

            UIController.MouseMessage("Insufficiant resources!" , Color.red , 1.5f);

        }





    }
    
}
