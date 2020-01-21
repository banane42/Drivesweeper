using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public static GameController gc;

    public static int rows = 7;//115 to cover the whole screen initially, 5 good for small testing
    public static int cols = 7;

    private int northExtent = rows / 2;
    private int southExtent = -1 * (rows / 2);
    private int eastExtent = cols / 2;
    private int westExtent = -1 * (cols / 2);

    public int difficulty = 1;

    public static int middleXTileCol = cols / 2;
    public static int middleYTileRow = rows / 2;

    private int triggerDistance = 57;//57 is the value in which you dont see the world generating. 7 is good for small visual testing

    public static GameObject[,] tiles = new GameObject[rows , cols];

    public GameObject TileInst;
    public GameObject player;
    public Transform TileParent;

    bool devMode = true;
    bool expand = true;

    private void Awake() {

        if (gc != null) {
            Destroy(gc);
        }
        gc = this;

        PlayerInfo.SetDefault();

        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < cols; j++) {

                tiles[i , j] = Instantiate(TileInst , new Vector2(j - (cols / 2), -i + (rows / 2)) , Quaternion.identity);

                if (i > (rows / 2) - 2 && i < (rows / 2) + 2 && j > (cols / 2) - 2 && j < (cols / 2) + 2) {
                    tiles[i , j].GetComponent<TileScript>().Initialize(true, i, j);
                    

                }
                else {
                    tiles[i , j].GetComponent<TileScript>().Initialize(false, i, j);
                }
            }
        }

    }

    // Use this for initialization
    void Start() {

        for (int i = 0; i < rows; i++) {

            for (int j = 0; j < cols; j++) {

                TileScript tile = tiles[i , j].GetComponent<TileScript>();

                tile.updateNeighbors();
                tile.updateSprite();
                
            }
        }

    }

    // Update is called once per frame
    void Update() {

        //dev keys to help test things, remove if this game is ever done
        if (Input.GetKeyDown(KeyCode.P)) {
            PlayerInfo.stone += 10000;
            PlayerInfo.blackMinePowder += 10000;
            PlayerInfo.flags += 100;
            print("Adding resources");
        }
        if (Input.GetKeyDown(KeyCode.O)) {
            if (devMode) {
                devMode = !devMode;
                PlayerInfo.SetDev();
                print("Dev mode");
            }
            else {
                devMode = !devMode;
                PlayerInfo.SetDefault();
                print("Default mode");
            }
        }
        if (Input.GetKeyDown(KeyCode.E)) {

            if (expand) {
                expand = false;
                print("Expansion disabled");
            }
            else {
                expand = true;
                print("Expansion enabled");
            }

        }

        int playerX = (int) player.transform.position.x;
        int playerY = (int) player.transform.position.y;

        //Expand East
        if (playerX > eastExtent - triggerDistance) {

            eastExtent++;
            if (expand) {
                ExpandEast();
            }

        }

        //Expand West
        if (playerX < westExtent + triggerDistance) {

            westExtent--;
            if (expand) {
                ExpandWest();
            }

        }

        //Expand North
        if (playerY > northExtent - triggerDistance) {

            northExtent++;
            if (expand) {
                ExpandNorth();
            }

        }

        //Expand South
        if (playerY < southExtent + triggerDistance) {

            southExtent--;
            if (expand) {
                ExpandSouth();
            }

        }

    }

    void ExpandEast() {

        GameObject[,] newTiles = new GameObject[rows , cols + 1];

        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < cols; j++) {

                newTiles[i , j] = tiles[i , j];

            }
        }

        for (int i = 0; i < rows; i++) {

            newTiles[i , cols] = Instantiate(TileInst, new Vector2(eastExtent, northExtent - i), Quaternion.identity);
            newTiles[i , cols].GetComponent<TileScript>().Initialize(false, i, cols);

        }

        tiles = newTiles;
        cols++;

        for (int i = 0; i < rows; i++) {

            tiles[i , cols - 1].GetComponent<TileScript>().updateNeighbors();
            tiles[i , cols - 2].GetComponent<TileScript>().updateNeighbors();

        }

        UpdateDifficulty();
    }

    void ExpandWest() {

        GameObject[,] newTiles = new GameObject[rows , cols + 1];

        middleXTileCol++;

        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < cols; j++) {

                newTiles[i , j + 1] = tiles[i , j];
                newTiles[i , j + 1].GetComponent<TileScript>().arrayColPos += 1;

            }
        }

        for (int i = 0; i < rows; i++) {

            newTiles[i , 0] = Instantiate(TileInst , new Vector2(westExtent, northExtent - i) , Quaternion.identity);
            newTiles[i , 0].GetComponent<TileScript>().Initialize(false , i , 0);

        }

        tiles = newTiles;
        cols++;

        for (int i = 0; i < rows; i++) {

            tiles[i , 0].GetComponent<TileScript>().updateNeighbors();
            tiles[i , 1].GetComponent<TileScript>().updateNeighbors();

        }

        UpdateDifficulty();
    }

    void ExpandNorth() {

        GameObject[,] newTiles = new GameObject[rows + 1, cols];

        middleYTileRow++;

        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < cols; j++) {

                newTiles[i + 1 , j] = tiles[i , j];
                newTiles[i + 1 , j].GetComponent<TileScript>().arrayRowPos += 1;

            }
        }

        for (int i = 0; i < cols; i++) {

            newTiles[0 , i] = Instantiate(TileInst, new Vector2(westExtent + i, northExtent), Quaternion.identity);
            newTiles[0 , i].GetComponent<TileScript>().Initialize(false, 0, i);

        }

        tiles = newTiles;
        rows++;

        for (int i = 0; i < cols; i++) {

            tiles[0 , i].GetComponent<TileScript>().updateNeighbors();
            tiles[1 , i].GetComponent<TileScript>().updateNeighbors();

        }

        UpdateDifficulty();
    }

    void ExpandSouth() {

        GameObject[,] newTiles = new GameObject[rows + 1 , cols];

        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < cols; j++) {

                newTiles[i , j] = tiles[i , j];

            }
        }

        for (int i = 0; i < cols; i++) {

            newTiles[rows , i] = Instantiate(TileInst, new Vector2(westExtent + i, southExtent), Quaternion.identity);
            newTiles[rows , i].GetComponent<TileScript>().Initialize(false, rows, i);

        }

        tiles = newTiles;
        rows++;

        for (int i = 0; i < cols; i++) {

            tiles[rows - 1 , i].GetComponent<TileScript>().updateNeighbors();
            tiles[rows - 2 , i].GetComponent<TileScript>().updateNeighbors();

        }

        UpdateDifficulty();
    }

    /*
     * Calculates the difficulty tier of the game based on how many tiles
     * are present in the world
     * There are currently 5 difficulty tiers.
     * 
     * Current difficulty function is (1.00008 ^ totalTiles) rounded down
     * 
     * Difficulty level  Total tiles 
     *      1                0
     *      2              8666
     *      3              13734
     *      4              17330
     *      5              20120
     */

    void UpdateDifficulty() {

        int calcDifficulty = Mathf.FloorToInt(Mathf.Pow(1.00008f, TileParent.childCount));

        if (calcDifficulty <= 5 && calcDifficulty != difficulty) {
            difficulty = calcDifficulty;
        }

    }
}
