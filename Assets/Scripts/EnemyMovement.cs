using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    Rigidbody2D myRigidBody;
    BoxCollider2D enemyFeetCollider;
    CapsuleCollider2D enemyBodyCollider;

    public bool shouldMove;


    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        shouldMove = true;
    }

    // Update is called once per frame
    void Update()
    {
         Move();
    }
    private void Move(){
        if(shouldMove){
            if (IsFacingRight()){
                myRigidBody.velocity = new Vector2(moveSpeed, 0f);
            }
            else{
                myRigidBody.velocity = new Vector2(-moveSpeed, 0f);
            }
        }
        else{
            myRigidBody.velocity = new Vector2(0f, 0f);

        }
    }
    private void FlipSprite(){
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidBody.velocity.x)), 1f);

    }
    private void OnTriggerExit2D(Collider2D other) {
        FlipSprite();
    }

    bool IsFacingRight(){
        return transform.localScale.x > 0;
    }
}

