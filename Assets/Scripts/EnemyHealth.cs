using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 100;
    // public GameObject deathEffect;
    public Animator myAnimator;

    private void Start() {
        myAnimator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage){
        StartCoroutine(Animate("Hit", 0.1f));
        health-=damage;
        if(health<=0){
            StartCoroutine(Die(0.7f));
        }
    }

    // public void Die(){
    //     StartCoroutine(Animate("Dead", 0.7f));
    //     EnemyMovement enemyMovement=GetComponent<EnemyMovement>();
    //     enemyMovement.shouldMove = false;

    //     Destroy(gameObject, 0.7f);
    // }

    IEnumerator Die(float timeToDie){
        StartCoroutine(Animate("Dead", timeToDie));
        EnemyMovement enemyMovement=GetComponent<EnemyMovement>();
        enemyMovement.shouldMove = false;

        yield return new WaitForSeconds(timeToDie);
        Destroy(gameObject);

        
    }

    IEnumerator Animate(string nameOfAnimation, float timeToAnimate){
        myAnimator.SetBool(nameOfAnimation, true);

        yield return new WaitForSeconds(timeToAnimate);
        
        myAnimator.SetBool(nameOfAnimation, false);
    }


}
