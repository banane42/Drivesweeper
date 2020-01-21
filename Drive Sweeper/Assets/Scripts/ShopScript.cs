using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    BoxCollider2D bc;
    SpriteRenderer sr;

    public bool placed;
    public bool isValid;

    int currMouseXPos;
    int currMouseYPos;
    int prevMouseXPos;
    int prevMouseYPos;

    void Awake(){

        bc = GetComponent<BoxCollider2D>();
        sr = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        bc.enabled = false;

        UIController.currentBuilding = gameObject;
        placed = false;
        isValid = false;

    }

    void Update(){

        if (!placed) {

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currMouseXPos = (int) mousePos.x;
            currMouseYPos = (int) mousePos.y;

            isValid = CheckValid(currMouseXPos, currMouseYPos);

            transform.position = new Vector3Int(currMouseXPos , currMouseYPos , 0);

        }

        if (Input.GetMouseButtonDown(0) && isValid && PlayerInfo.controlMode == PlayerInfo.ControlMode.Build && PlayerInfo.buildingMode == PlayerInfo.BuildingMode.Shop) {

            if (PlayerInfo.stone >= 100 && PlayerInfo.blackMinePowder >= 25) {

                PlayerInfo.stone -= 100;
                PlayerInfo.blackMinePowder -= 25;

                placed = true;
                bc.enabled = true;
                UIController.currentBuilding = null;
 
                UIController.MouseMessage("Shop Built!", Color.green, 2f);

                PlayerInfo.buildingMode = PlayerInfo.BuildingMode.Unselected;
                UIController.uic.ShopGUIText.color = Color.red;
                UIController.uic.UnselectedGUIText.color = Color.black;
                UIController.uic.MinerGUIText.color = Color.red;
                //Destroy(UIController.currentBuilding);
                Cursor.visible = true;

            }
            else {

                if (PlayerInfo.stone <= 100 && PlayerInfo.blackMinePowder <= 25) {

                    UIController.MouseMessage("Insufficient resouces!\nStone: " + PlayerInfo.stone + "/100\nBlack Mine Powder: " + PlayerInfo.blackMinePowder + "/25" , Color.red , 5f);

                }
                else if (PlayerInfo.stone <= 100) {

                    UIController.MouseMessage("Insufficient resouces!\nStone: " + PlayerInfo.stone + "/100" , Color.red , 5f);

                }
                else if (PlayerInfo.blackMinePowder <= 25) {

                    UIController.MouseMessage("Insufficient resouces!\nBlack Mine Powder: " + PlayerInfo.blackMinePowder + "/25" , Color.red , 5f);

                }

            }
            
        }

    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.CompareTag("Bullet") && placed) {
            Destroy(collision.gameObject);
        }

    }

    private void OnTriggerStay2D(Collider2D collision) {

        if (collision.gameObject.CompareTag("Resource")) {
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject.CompareTag("Player") && placed) {

            UIController.uic.ShopMenu.SetActive(true);

        }

    }

    private void OnCollisionExit2D(Collision2D collision) {

        if (collision.gameObject.CompareTag("Player") && placed) {

            UIController.uic.ShopMenu.SetActive(false);
            Cursor.SetCursor(UIController.uic.aimCursor , Vector2.zero , CursorMode.ForceSoftware);
            PlayerInfo.canFire = true;

        }

    }

    private bool CheckValid(int mouseXPos, int mouseYPos) {

        int arrayRow = GameController.middleYTileRow - mouseYPos;
        int arrayCol = GameController.middleXTileCol + mouseXPos;

        TileScript currentTile = GameController.tiles[arrayRow , arrayCol].GetComponent<TileScript>();

        TileScript[] currTiles = {

            currentTile,
            currentTile.neighbors[3].GetComponent<TileScript>(),
            currentTile.neighbors[4].GetComponent<TileScript>(),
            currentTile.neighbors[5].GetComponent<TileScript>()

        };


        if (currTiles[0].isCleared && !currTiles[0].mineActive && 
            currTiles[1].isCleared && !currTiles[1].mineActive && 
            currTiles[2].isCleared && !currTiles[2].mineActive && 
            currTiles[3].isCleared && !currTiles[3].mineActive) {

            sr.color = Color.white;

            return true;

        }

        sr.color = Color.red;
        return false;
    }
}
