using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{

    [SerializeField] AudioClip coinPickupSFX;
    [SerializeField] float soundVol = 0.25f;
    [SerializeField] int pointsForCoin = 100;

    // private void OnTriggerEnter2D(Collider2D other) {
    //     AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);
        
    //     Destroy(gameObject);
    // }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            ProcessCoinPickup();
        }
    }
    private void ProcessCoinPickup()
    {
        FindObjectOfType<GameSession>().AddToScore(pointsForCoin);
        GameObject audioListener = GameObject.FindWithTag("AudioListener");
        AudioSource.PlayClipAtPoint(coinPickupSFX, audioListener.transform.position, soundVol);
        Destroy(gameObject);
    }
}
