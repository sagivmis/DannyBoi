using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] public float speed =20f;
    [SerializeField] public int damage = 40;
    public Rigidbody2D rb;
    public GameObject impactEffect;

    void Start()
    {
        rb.velocity = transform.right*speed;
    }


    private void OnTriggerEnter2D(Collider2D other) {
        // Debug.Log("Collided");
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
        if(enemyHealth!=null){
            enemyHealth.TakeDamage(damage);
        }
        GameObject effectClone = Instantiate(impactEffect, transform.position, transform.rotation);

        Destroy(effectClone, 0.6f);
        // impactEffect.SetActive(true);
        // gameObject.SetActive(false);
        Destroy(gameObject, 0.6f);
    }
    // public void 
    private void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
