using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour {

    public TileScript targetTile;

    public Sprite droneCompact;
    public Sprite droneMining;

    SpriteRenderer sr;

    private float speed;
    bool done;
    bool once;

    public void Initialize(TileScript _targetTile) {

        targetTile = _targetTile;
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = droneCompact;
        done = false;
        once = true;
        speed = PlayerInfo.droneSpeed;

    }

    private void Update() {

        if (!done) {

            transform.position = Vector2.MoveTowards(transform.position , targetTile.transform.position , Time.deltaTime * speed);

            if (Vector2.Distance(transform.position , targetTile.transform.position) <= 0 && once) {

                StartCoroutine(mineTile());
                
            }

        }
        else {

            transform.position = Vector2.MoveTowards(transform.position, GameController.gc.player.transform.position, Time.deltaTime * speed);

            if (Vector2.Distance(transform.position , GameController.gc.player.transform.position) <= 0) {
                PlayerInfo.droneCount -= 1;
                Destroy(gameObject);
            }

        }

    }

    IEnumerator mineTile() {
        sr.sprite = droneMining;
        once = false;

        yield return new WaitForSeconds(PlayerInfo.droneMineTime);
        done = true;
        sr.sprite = droneCompact;
        targetTile.setCleared();
        targetTile.updateSprite();

    }
}
