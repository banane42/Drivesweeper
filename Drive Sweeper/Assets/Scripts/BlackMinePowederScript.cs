using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackMinePowederScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {

            Destroy(gameObject);
            PlayerInfo.blackMinePowder += 1;

        }
        
    }
}
