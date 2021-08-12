using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Vector2 deathKick= new Vector2(5f , 10f);

    [SerializeField] public int currentLife = 5;
    [SerializeField] private int health = 100;
    [SerializeField] private float timeBetweenHits = 2f;

    public static bool isAlive = true;
    public static int maxLives;
    public bool playerGotHit = false;
    public float timer;


    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    Rigidbody2D myRigidBody;

    void Start()
    {
        timer = 0f;
        maxLives=currentLife;
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        
    }

    void Update()
    {
        Die();
    }

    private void Die(){
        if(playerGotHit && timer >0f){
            timer-=Time.deltaTime;
        }
        if(myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy","Hazards")) || myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Enemy","Hazards"))){


            if(timer<=0f){
                playerGotHit = false;
                timer = timeBetweenHits;
                StartCoroutine(hitPlayer());
            }

        }
    }


    IEnumerator hitPlayer(){
        myAnimator.SetBool("Hurt", true);
        GetComponent<Rigidbody2D>().velocity = deathKick;
        FindObjectOfType<GameSession>().ProcessPlayerLoseHealth(20);

        playerGotHit = true;
        yield return new WaitForSeconds(2);

        myAnimator.SetBool("Hurt", false);
    }


    public void DeductFromHealth(int value){
        health-=value;
    }

    public void setHealth(int value){
        health = value;
    }

    public int getHealth(){
        return health;
    }
}
