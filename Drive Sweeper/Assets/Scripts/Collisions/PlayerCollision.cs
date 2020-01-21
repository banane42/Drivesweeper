using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject.CompareTag("Enemy")) {
            PlayerInfo.health--;

            if (PlayerInfo.health <= 0) {
                Destroy(this.gameObject);
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.CompareTag("Bullet Mine")) {
            PlayerInfo.health--;
            Destroy(collision.gameObject);

            if (PlayerInfo.health <= 0) {
                Destroy(this.gameObject);
            }
        }

    }
}
