using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickupSFX;
    [SerializeField] int pointsForCoinPickup = 10;

    bool ProcessCollision = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(ProcessCollision)
        {
            print("Collider Coin");
            ProcessCollision = false;
            FindObjectOfType<GameSession>().AddToScore(pointsForCoinPickup);
            if(FindObjectOfType<ControlPanel>().isSoundOff)
            {
                AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);
            }
            Destroy(gameObject);
        }
    }
}
