using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float climbForce;

    [SerializeField] private float runSpeed;
    [SerializeField] private Vector2 deathKick= new Vector2(-3f , 25f);
    [SerializeField] private float jumpForce;

    float gravityScaleAtStart;
    bool isMovingRight = true;
    bool isFacingRight = true;
    bool jump =false;

    public Rigidbody2D myRigidBody;
    Animator myAnimator;
    Animation anim;
    public CapsuleCollider2D myBodyCollider;
    public BoxCollider2D myFeetCollider;
    GameSession gameSession;
    
    bool rotated = false;

    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animation>();
        gravityScaleAtStart = myRigidBody.gravityScale;
    }

    void Update()
    {
        if(!PlayerHealth.isAlive)
        {
            return;
        }
        
        Run();
        // Jump();
        ClimbLadder();
        // Underwater();
        FlipSprite();

        // if(jump){
        JumpMobile();
        // }
        
    }


    private void Run(){
        // VARS CONTROL
        if(runSpeed>20f){
            runSpeed=20f;
        }

        // MOVEMENT
        float control = gameSession.joystick.Horizontal;
        
        // float control = Input.GetAxis("Horizontal");  // -1 ~ +1
        Vector2 playerVelocity= Vector2.zero;
        if((control >=0.15f  && control <= 0.65f )|| (control<= -0.15f && control>=-0.65f)){
        playerVelocity = new Vector2(control*runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;
        }
        else if (Mathf.Abs(control)>=0.65f){
            control = 0.65f*Mathf.Sign(control);        
            playerVelocity = new Vector2(control*runSpeed, myRigidBody.velocity.y);
            myRigidBody.velocity = playerVelocity;
        }
        else if(Mathf.Abs(control)>=0.01f){
            control = 0.15f*Mathf.Sign(control);        
            playerVelocity = new Vector2(control*runSpeed, myRigidBody.velocity.y);
            myRigidBody.velocity = playerVelocity;
        }
        // ANIMATIONS CONTROL
        if(Mathf.Abs(playerVelocity.x) >= 0.3f) {
            myAnimator.SetBool("Running", true);
            myAnimator.speed=1;
        }
        else {myAnimator.SetBool("Running", false);}
    }

    public void ClimbLadder(){
        if(myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climb"))){
            // float control = c;
            // if(!rotated) {gameSession.joystick.transform.Rotate(0f,0f,90f); rotated=true;}
            float control = gameSession.joystick.Vertical;
            if(control>=0.2f){
            Vector2 climbingVelocity = new Vector2(myRigidBody.velocity.x/2, climbForce);
            myRigidBody.velocity=climbingVelocity;
            }
            else if(control<= -0.2f){
            Vector2 climbingVelocity = new Vector2(myRigidBody.velocity.x/2, -climbForce);
            myRigidBody.velocity=climbingVelocity;
            }
            else{
            Vector2 climbingVelocity = new Vector2(myRigidBody.velocity.x/2, 0f);
            myRigidBody.velocity=climbingVelocity;
            }
            bool playerHasVerticalSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;

            if(!playerHasVerticalSpeed) myAnimator.speed = 0;
            else {myAnimator.speed=1;}

            myRigidBody.gravityScale = 0f;
        }
        else{
            myRigidBody.gravityScale = gravityScaleAtStart;
            // gameSession.joystick.transform.Rotate(0f,0f,0f);
            // rotated = false;
        }
        // bool playerHasVerticalSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon && myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Climb"));
        bool playerTouchingLadder = myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climb"));
        myAnimator.SetBool("Climbing", playerTouchingLadder);
    }


    public void JumpMobile(){
        // if(myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground") ) || myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Objects"))){
        //     Vector2 jumpVelocityToAdd = new Vector2(0f, jumpForce);
        //     myRigidBody.velocity +=jumpVelocityToAdd;   
        //  }
        if(myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground") ) || myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Objects"))){

            float control = gameSession.joystick.Vertical;
            if(control>=0.2f){
            Vector2 jumpVelocityToAdd = new Vector2(myRigidBody.velocity.x, jumpForce);
            myRigidBody.velocity =jumpVelocityToAdd;  
            }
        }
    }
    public void setJumpDown(){
        jump = true;
    }
    public void setJumpUp(){
        jump = false;
    }
    private void Jump(){
        // if(myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))){
        //     if(Input.GetButtonDown("Jump")){
                
        //         Vector2 jumpVelocityToAdd = new Vector2(0f, jumpForce);
        //         myRigidBody.velocity +=jumpVelocityToAdd;
        //     }
        // }
        // if(myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Objects"))){
        //     if(Input.GetButtonDown("Jump")){
                
        //         Vector2 jumpVelocityToAdd = new Vector2(0f, jumpForce);
        //         myRigidBody.velocity +=jumpVelocityToAdd;
        //     }
        // }
    }


    public void Win(){
        myAnimator.SetBool("Win", true);
    }


    private void FlipSprite(){
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if(playerHasHorizontalSpeed){
            // transform.localScale= new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
            // myRigidBody.rotation = 0f;
            if(myRigidBody.velocity.x >=0){
                 isMovingRight = true;

            }
            else {isMovingRight=false;}

            if(isMovingRight && isFacingRight){return;}
            if(!isMovingRight && !isFacingRight){return;}

            transform.Rotate(0f,180f,0f);
            isFacingRight=!isFacingRight;
        }
    }
}
