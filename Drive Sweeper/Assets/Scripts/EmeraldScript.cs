﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmeraldScript : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.CompareTag("Player")) {

            Destroy(gameObject);
            PlayerInfo.emeralds += 1;

        }

    }

}
