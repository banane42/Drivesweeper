using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneScript : MonoBehaviour
{

    SpriteRenderer sc;
    public Sprite stoneOne;
    public Sprite stoneTwo;

    void Awake()
    {
        sc = GetComponent<SpriteRenderer>();

        if (Random.Range(0 , 2) == 0) {
            sc.sprite = stoneOne;
        }
        else {
            sc.sprite = stoneTwo;
        }


    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.CompareTag("Player")) {

            Destroy(gameObject);
            PlayerInfo.stone += 1;

        }

    }
}
