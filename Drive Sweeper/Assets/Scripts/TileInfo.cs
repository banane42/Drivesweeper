using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfo {

    public bool isMine;
    public bool isCleared;
    public int xPos;
    public int yPos;
    public int neighborMineCount;

    public TileInfo(int _xPos, int _yPos, bool _isCleared) {

        xPos = _xPos;
        yPos = _yPos;
        isCleared = _isCleared;
        neighborMineCount = 0;

        if (Random.Range(1 , 5) > 3) {
            isMine = true;
        }
        else {
            isMine = false;
        }

    }

    public TileInfo(int _xPos, int _yPos , bool _isCleared, bool _isMine) {

        xPos = _xPos;
        yPos = _yPos;
        isCleared = _isCleared;
        isMine = _isMine;

    }

    public Vector2 getVector2D() {
        return new Vector2(xPos, yPos);
    }

    public void incrementMineCount() {
        neighborMineCount++;
    }
}
