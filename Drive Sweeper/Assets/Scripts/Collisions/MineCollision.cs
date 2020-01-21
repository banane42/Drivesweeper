using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineCollision : MonoBehaviour {

    public GameObject powder;
    public GameObject ruby;
    public GameObject emerald;
    private MineBehavior mb;
    private Transform HealthBar;
    private SpriteRenderer hbSr;
    private Color[] colors = {Color.red, new Color(255f, 50f, 0f), Color.yellow, Color.green};

    public int maxHealth;
    private int health;
    private float scaleRemoveAmount;

    private void Awake() {

        mb = GetComponent<MineBehavior>();
        HealthBar = transform.GetChild(0);
        hbSr = HealthBar.GetComponent<SpriteRenderer>();
        hbSr.color = Color.green;

    }

    private void OnCollisionEnter2D(Collision2D collision) {


        if (collision.gameObject.CompareTag("Player")) {
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.CompareTag("Bullet")) {
            health--;

            float hpPercent = (float) health / maxHealth;
            
            hbSr.color = colors[Mathf.Max(Mathf.RoundToInt(hpPercent * 3), 0)];
            HealthBar.localScale = new Vector3(HealthBar.localScale.x - scaleRemoveAmount, 7f, 0f);

            Destroy(collision.gameObject);

            if (health <= 0) {
                Destroy(this.gameObject);

                if (mb.type == "standard") {
                    Instantiate(powder , new Vector2(transform.position.x , transform.position.y) , Quaternion.Euler(0f , 0f , Random.Range(0 , 360f)));
                }
                else if (mb.type == "shooter") {
                    Instantiate(ruby , new Vector2(transform.position.x , transform.position.y) , Quaternion.Euler(0f , 0f , Random.Range(0 , 360f)));
                }
                else if (mb.type == "tank") {
                    Instantiate(emerald , new Vector2(transform.position.x , transform.position.y) , Quaternion.Euler(0f , 0f , Random.Range(0 , 360f)));
                }

            }
        }

    }

    public void SetHealth(int diff) {

        if (mb.type == "shooter") {

            if (diff <= 2) {
                health = maxHealth = 1;
            }
            else if (diff == 3) {
                health = maxHealth = 3;
            }
            else if (diff == 4) {
                health = maxHealth = 5;
            }
            else if (diff == 5) {
                health = maxHealth = 7;
            }

        }
        else if (mb.type == "standard") {

            if (diff == 1) {
                health = maxHealth = 1;
            }
            else if (diff == 2) {
                health = maxHealth = 2;
            }
            else if (diff == 3) {
                health = maxHealth = 4;
            }
            else if (diff == 4) {
                health = maxHealth = 8;
            }
            else if (diff == 5) {
                health = maxHealth = 16;
            }

        }
        else if (mb.type == "tank") {

            if (diff == 1) {
                health = maxHealth = 5;
            }
            else if (diff == 2) {
                health = maxHealth = 10;
            }
            else if (diff == 3) {
                health = maxHealth = 15;
            }
            else if (diff == 4) {
                health = maxHealth = 20;
            }
            else if (diff == 5) {
                health = maxHealth = 25;
            }

        }


        scaleRemoveAmount = ((float) 1 / maxHealth) * HealthBar.localScale.x;
    }
}
