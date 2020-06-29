using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacdots : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other) {
        if(other.name == "pacman")
        {
            GameManager.score += 10;
            GameObject[] pacdots = GameObject.FindGameObjectsWithTag("pacdot");
            Destroy(gameObject);

            if (pacdots.Length == 1)
            {
                GameObject.FindObjectOfType<GameUINavigation>().LoadLevel();
            }
        }
    }
}
