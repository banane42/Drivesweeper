using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCollision : MonoBehaviour {
    
    TileScript ts;
    public GameObject player;
    public int health;

    private void Start() {
        ts = GetComponent<TileScript>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {

        /*if (collision.gameObject.CompareTag("Bullet")) {

            if (ts.isCleared && ts.isMine && ts.mineActive) {

                print("hit by bullet");

            }

        }*/

        if (collision.gameObject.CompareTag("Enemy") && ts.isCleared) {

            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>() , GetComponent<Collider2D>());

        }

        /*if (ts.isCleared) {

            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), this.GetComponent<Collider2D>());


        }*/

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Bullet")) {

            if (ts.isCleared && ts.isMine && ts.mineActive) {

                Destroy(collision.gameObject);
                StartCoroutine(FlashDamage());
                health--;
                if (health <= 0) {
                    ts.mineActive = false;
                    ts.bc.enabled = false;

                }

            }
            else if (!ts.isCleared) {
                Destroy(collision.gameObject);
            }

        }
        if (collision.gameObject.CompareTag("Bullet Mine") && !ts.isCleared) {
            Destroy(collision.gameObject);
        }
        

    }

    IEnumerator FlashDamage() {
        if (ts.mineActive) {
            ts.sr.sprite = ts.mineSpawnerActive;
            yield return new WaitForSeconds(0.03f);
            ts.updateSprite();
        }

    }
}
