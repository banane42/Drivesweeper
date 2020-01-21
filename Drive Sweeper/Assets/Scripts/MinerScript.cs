using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerScript : MonoBehaviour
{
    BoxCollider2D bc;
    SpriteRenderer sr;
    ParticleSystem ps;

    bool placed;
    bool isValid;

    int resourceType;

    private void Awake() {

        bc = GetComponent<BoxCollider2D>();
        sr = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        ps = transform.Find("Particle System").GetComponent<ParticleSystem>();

        ps.Stop();
        bc.enabled = false;

        UIController.currentBuilding = gameObject;

        placed = false;
        isValid = false;

    }

    void Update()
    {

        if (!placed) {

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int currMouseXPos = (int) mousePos.x;
            int currMouseYPos = (int) mousePos.y;

            isValid = CheckValid(currMouseXPos , currMouseYPos);

            transform.position = new Vector3Int(currMouseXPos , currMouseYPos , 0);

        }

        if (Input.GetMouseButtonDown(0) && isValid && PlayerInfo.controlMode == PlayerInfo.ControlMode.Build && PlayerInfo.buildingMode == PlayerInfo.BuildingMode.Miner) {

            if (PlayerInfo.stone >= 75 && PlayerInfo.blackMinePowder >= 50) {

                PlayerInfo.stone -= 75;
                PlayerInfo.blackMinePowder -= 50;

                placed = true;
                bc.enabled = true;
                ps.Play();

                UIController.currentBuilding = null;

                UIController.MouseMessage("Miner Built!" , Color.green , 2f);

                PlayerInfo.buildingMode = PlayerInfo.BuildingMode.Unselected;
                UIController.uic.ShopGUIText.color = Color.red;
                UIController.uic.UnselectedGUIText.color = Color.black;
                UIController.uic.MinerGUIText.color = Color.red;
                Destroy(UIController.currentBuilding);
                Cursor.visible = true;

            }
            else {

                if (PlayerInfo.stone <= 75 && PlayerInfo.blackMinePowder <= 50) {

                    UIController.MouseMessage("Insufficient resouces!\nStone: " + PlayerInfo.stone + "/75\nBlack Mine Powder: " + PlayerInfo.blackMinePowder + "/50" , Color.red , 5f);

                }
                else if (PlayerInfo.stone <= 100) {

                    UIController.MouseMessage("Insufficient resouces!\nStone: " + PlayerInfo.stone + "/75" , Color.red , 5f);

                }
                else if (PlayerInfo.blackMinePowder <= 25) {

                    UIController.MouseMessage("Insufficient resouces!\nBlack Mine Powder: " + PlayerInfo.blackMinePowder + "/50" , Color.red , 5f);

                }

            }

        }

    }

    bool CheckValid(int mouseXPos, int mouseYPos) {

        int arrayRow = GameController.middleYTileRow - mouseYPos;
        int arrayCol = GameController.middleXTileCol + mouseXPos;

        TileScript currentTile = GameController.tiles[arrayRow , arrayCol].GetComponent<TileScript>();

        if (currentTile.isCleared && !currentTile.mineActive) {

            sr.color = Color.white;
            resourceType = currentTile.neighborMineCount;

            return true;

        }

        sr.color = Color.red;
        return false;
    }
}
