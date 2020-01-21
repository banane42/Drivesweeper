using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour {

    public Sprite tileUncleared;
    public Sprite tileCleared;
    public Sprite spriteOne;
    public Sprite spriteTwo;
    public Sprite spriteThree;
    public Sprite spriteFour;
    public Sprite spriteFive;
    public Sprite spriteSix;
    public Sprite spriteSeven;
    public Sprite spriteEight;
    public Sprite mineSpawnerActive;
    public Sprite mineSpawnerDeactive;
    public Sprite mineSpawn;
    public Sprite bugged;
    public Sprite flag;

    [HideInInspector]
    public SpriteRenderer sr;
    [HideInInspector]
    public BoxCollider2D bc;

    public GameObject[] Bombs;
    public GameObject Drone;
    public GameObject Stone;

    public int arrayRowPos;
    public int arrayColPos;

    //[HideInInspector]
    public GameObject[] neighbors = new GameObject[8];
    /* ------------- Order in which the neighbors are stored visualized
     * | 0 | 7 | 6 | X is the position of the current tile
     * -------------
     * | 1 | X | 5 |
     * -------------
     * | 2 | 3 | 4 |
     * -------------
     */


    enum SpawnTypes {
        Burster, Consistant
    }

    public bool isMine;
    public bool mineActive;
    public bool isCleared;
    public bool hasClearedNeighbor;
    private bool targeted;
    private bool flagged;
    private int tileDifficulty;
    public int neighborMineCount;
    SpawnTypes type;

    private void Awake() {

        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
        transform.SetParent(GameController.gc.TileParent);
        tileDifficulty = GameController.gc.difficulty;

    }

    // Use this for initialization
    public void Initialize(bool forceClear, int i, int j) {

        mineActive = false;
        isCleared = false;
        targeted = false;
        flagged = false;

        arrayRowPos = i;
        arrayColPos = j;

        //now uses the current difficulty to determine if the tile is a mine
        if (Random.value < MineChance()) {
            isMine = true;
            mineActive = true;
        }
        else isMine = false;

        if (forceClear) {
            isMine = false;
            isCleared = true;
            mineActive = false;
            bc.enabled = false;
        }

        int rand = Random.Range(0,2);
        if (rand == 0) {
            type = SpawnTypes.Burster;
        }
        else if (rand == 1) {
            type = SpawnTypes.Consistant;
        }
        
        neighborMineCount = 0;

    }

    public void Initialize(bool forceClear, float mineChance, int i, int j) {

        mineActive = false;
        isCleared = false;
        targeted = false;
        flagged = false;

        arrayRowPos = i;
        arrayColPos = j;

        if (Random.Range(1f , 100f) < mineChance) {//mineChance must be between >= 1f and <= 100f
            isMine = true;
            mineActive = true;
        }
        else
            isMine = false;

        if (forceClear) {
            isMine = false;
            isCleared = true;
            mineActive = false;
            bc.enabled = false;
        }

        int rand = Random.Range(0 , 2);
        if (rand == 0) {
            type = SpawnTypes.Burster;
        }
        else if (rand == 1) {
            type = SpawnTypes.Consistant;
        }

        neighborMineCount = 0;

    }

    private void OnMouseOver() {
        //float distance = Vector2.Distance(transform.position , GameController.gc.player.transform.position); Might be used for drone range

        if (PlayerInfo.controlMode == PlayerInfo.ControlMode.Shooting && PlayerInfo.secondaryShootingMode == PlayerInfo.SecondaryShootingMode.Drone && Input.GetMouseButton(1) && !isCleared && !targeted && PlayerInfo.droneCount < PlayerInfo.maxDrones && hasClearedNeighbor) {
            targeted = true;
            PlayerInfo.droneCount += 1;
            GameObject tempDrone = Instantiate(Drone , new Vector2(GameController.gc.player.transform.position.x , GameController.gc.player.transform.position.y) , Quaternion.identity);
            tempDrone.GetComponent<DroneController>().Initialize(this);
        }
        else if (PlayerInfo.controlMode == PlayerInfo.ControlMode.Shooting && PlayerInfo.secondaryShootingMode == PlayerInfo.SecondaryShootingMode.Flag && Input.GetMouseButtonDown(1)) {

            if (flagged) {
                flagged = !flagged;
                updateSprite();
            }
            else if(!flagged && PlayerInfo.flags > 0) {
                flagged = !flagged;
                sr.sprite = flag;
                PlayerInfo.flags -= 1;
            }

        }
    }

    public void updateSprite() {

        if (!isCleared) {
            sr.sprite = tileUncleared;
        }
        else if (!isMine) {
            if (neighborMineCount == 0) {
                sr.sprite = tileCleared;
            }
            else if (neighborMineCount == 1) {
                sr.sprite = spriteOne;
            }
            else if (neighborMineCount == 2) {
                sr.sprite = spriteTwo;
            }
            else if (neighborMineCount == 3) {
                sr.sprite = spriteThree;
            }
            else if (neighborMineCount == 4) {
                sr.sprite = spriteFour;
            }
            else if (neighborMineCount == 5) {
                sr.sprite = spriteFive;
            }
            else if (neighborMineCount == 6) {
                sr.sprite = spriteSix;
            }
            else if (neighborMineCount == 7) {
                sr.sprite = spriteSeven;
            }
            else if (neighborMineCount == 8) {
                sr.sprite = spriteEight;
            }
        }
        else if (isMine) {
            if (mineActive) {
                sr.sprite = mineSpawn;
            }
            else {
                sr.sprite = mineSpawnerDeactive;
            }
        }

    }

    public void updateNeighbors() {

        int mineCount = 0;
        
        if (arrayRowPos > 0 && arrayColPos > 0) {
            neighbors[0] = GameController.tiles[arrayRowPos - 1 , arrayColPos - 1];//top left

            if (GameController.tiles[arrayRowPos - 1 , arrayColPos - 1].GetComponent<TileScript>().isMine) {
                mineCount++;
            }
        }
        if (arrayColPos > 0) {
            neighbors[1] = GameController.tiles[arrayRowPos , arrayColPos - 1];//middle left

            if (GameController.tiles[arrayRowPos , arrayColPos - 1].GetComponent<TileScript>().isMine) {
                mineCount++;
            }
        }
        if (arrayRowPos < GameController.rows - 1 && arrayColPos > 0) {
            neighbors[2] = GameController.tiles[arrayRowPos + 1 , arrayColPos - 1];//bottom left

            if (GameController.tiles[arrayRowPos + 1 , arrayColPos - 1].GetComponent<TileScript>().isMine) {
                mineCount++;
            }
        }
        if (arrayRowPos < GameController.rows - 1) {
            neighbors[3] = GameController.tiles[arrayRowPos + 1 , arrayColPos];//bottom middle

            if (GameController.tiles[arrayRowPos + 1 , arrayColPos].GetComponent<TileScript>().isMine) {
                mineCount++;
            }
        }
        if (arrayRowPos < GameController.rows - 1 && arrayColPos < GameController.cols - 1) {
            neighbors[4] = GameController.tiles[arrayRowPos + 1, arrayColPos + 1];//bottom right

            if (GameController.tiles[arrayRowPos + 1 , arrayColPos + 1].GetComponent<TileScript>().isMine) {
                mineCount++;
            }
        }
        if (arrayColPos < GameController.cols - 1) {
            neighbors[5] = GameController.tiles[arrayRowPos , arrayColPos + 1];//middle right

            if (GameController.tiles[arrayRowPos , arrayColPos + 1].GetComponent<TileScript>().isMine) {
                mineCount++;
            }
        }
        if (arrayRowPos > 0 && arrayColPos < GameController.cols - 1) {
            neighbors[6] = GameController.tiles[arrayRowPos - 1, arrayColPos + 1];//top right

            if (GameController.tiles[arrayRowPos - 1 , arrayColPos + 1].GetComponent<TileScript>().isMine) {
                mineCount++;
            }
        }
        if (arrayRowPos > 0) {
            neighbors[7] = GameController.tiles[arrayRowPos - 1, arrayColPos];//top middle

            if (GameController.tiles[arrayRowPos - 1 , arrayColPos].GetComponent<TileScript>().isMine) {
                mineCount++;
            }
        }

        neighborMineCount = mineCount;

        for (int i = 1; i < 8; i += 2) {

            if (neighbors[i] != null) {

                if (neighbors[i].GetComponent<TileScript>().isCleared) {
                    hasClearedNeighbor = true;
                }
            }

        }

        updateSprite();
    }

    public void setCleared() {

        isCleared = true;

        if (!isMine) {
            bc.enabled = false;
        }

        for (int i = 1; i < 8; i += 2) {

            if (neighbors[i] != null) {
                neighbors[i].GetComponent<TileScript>().hasClearedNeighbor = true;
            }

        }

        //spawning stone

        if (!isMine) {

            int stoneCount = Random.Range(1 , 4);

            for (int i = 0; i < stoneCount; i++) {

                float xVal = transform.position.x + Random.Range(-0.3f , 0.3f);
                float yVal = transform.position.y + Random.Range(-0.3f , 0.3f);

                Instantiate(Stone , new Vector2(xVal , yVal) , Quaternion.Euler(0f , 0f , Random.Range(0f , 360f)));

            }
        }
        else if (isMine) {

            StartCoroutine(SpawnMines());

        }

    }

    private IEnumerator SpawnMines() {

        if (type == SpawnTypes.Burster) {

            for (int i = 0; i < 15; i++) {

                if (mineActive) {

                    GameObject temp = Instantiate(Bombs[Random.Range(0, Bombs.Length)] , new Vector2(transform.position.x , transform.position.y) , Quaternion.identity);
                    temp.GetComponent<MineCollision>().SetHealth(tileDifficulty);
                    yield return new WaitForSeconds(0.25f);

                }

            }

            mineActive = false;
            bc.enabled = false;
            updateSprite();

        }
        else if (type == SpawnTypes.Consistant) {

            while (mineActive) {

                GameObject temp = Instantiate(Bombs[Random.Range(0 , Bombs.Length)] , new Vector2(transform.position.x , transform.position.y) , Quaternion.identity);
                temp.GetComponent<MineCollision>().SetHealth(tileDifficulty);
                yield return new WaitForSeconds(2f);
            }

        }

    }

    //Sigmoid function returning the chance that this tile is a mine based on 
    //the current difficulty at the time of the tiles instantiation
    float MineChance() {

        return 1 / (1 + (10 * Mathf.Exp(-1 * GameController.gc.difficulty)));

    }

}
