using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [Header ("Texts")]
    [SerializeField] Text liveText; 
    [SerializeField] Text scoreText; 
    [SerializeField] Text healthText; 

    [Space]

    [Header ("Player")]
    PlayerHealth playerHealth;
    PlayerMovement playerMovement;
    PlayerShoot playerShoot;
    GameObject player;
    
    [Space]
    [Header ("Buttons")]
    public Button jumpButton;
    public Button shootButton;
    public Joystick joystick;

    [Space]

    [Header ("Config")]
    int score = 0;
    public const int maxHealth = 100;

    public bool updatedButtons = false;

    private void Awake() {
        int numGameSession = FindObjectsOfType<GameSession>().Length;
        if(numGameSession>1) {
            gameObject.SetActive(false);
            Destroy(gameObject);
            }
        else {
            DontDestroyOnLoad(gameObject);
            }
    }

    public void refreshButtons(){
        updatedButtons= false;
    }

    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        playerShoot = FindObjectOfType<PlayerShoot>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        // player = GameObject.Find("Player");


        // shootButton.onClick.AddListener(check);

        liveText.text = playerHealth.currentLife.ToString();
        scoreText.text = score.ToString();
        healthText.text = playerHealth.getHealth().ToString();

    }

    private void Update() {        
        player = GameObject.FindWithTag("Player");
        if(player!=null){updateButtons();}
        
        
    }

    public void Jump(){
        playerMovement = player.GetComponent<PlayerMovement>();
        playerMovement.JumpMobile();
    }
    
    public void Up(){
        playerMovement = player.GetComponent<PlayerMovement>();
        // if(jumpButton.GetComponent<PressDetector>().buttonPressed){
        playerMovement.ClimbLadder();
        // }
        // Vector2 climbingVelocity = new Vector2(playerMovement.myRigidBody.velocity.x,playerMovement.climbForce);
        // playerMovement.myRigidBody.velocity=climbingVelocity;
    }
    public void Down(){
        playerMovement = player.GetComponent<PlayerMovement>();
        // playerMovement.ClimbLadder(-0.003f);
        // Vector2 climbingVelocity = new Vector2(playerMovement.myRigidBody.velocity.x,-1*playerMovement.climbForce);
        // playerMovement.myRigidBody.velocity=climbingVelocity;
    }

    public void Shoot(){
        playerShoot=player.GetComponent<PlayerShoot>();
        playerShoot.Shoot();
    }
    public void updateButtons(){
        if(!updatedButtons){
            // jumpButton = 
            Button[] buttonArray  = FindObjectOfType<GameSession>().GetComponentsInChildren<Button>();
            for(int i=0; i<buttonArray.Length; i++){
                if(buttonArray[i].name == "JumpButton"){
                    jumpButton = buttonArray[i];
                    Debug.Log($"added button ---{buttonArray[i].name} ");
                }
                else if( buttonArray[i].name == "ShootButton"){
                    shootButton = buttonArray[i];
                    Debug.Log($"added button ---{buttonArray[i].name} ");
                }
            }
            jumpButton.GetComponent<Button>().onClick.AddListener(Jump);
            shootButton.GetComponent<Button>().onClick.AddListener(Shoot);
            updatedButtons=true;
        }
    }
    public void check(){
        Debug.Log(player);
        Debug.Log(playerShoot);
        Debug.Log(playerMovement);
    }
    public void AddToScore(int value){
        score += value;
        scoreText.text = score.ToString();
    }

    public void ProcessPlayerDeath(){
        if(playerHealth.currentLife>1){
            TakeLife();
            // Revive(); // enable when finish putting player in gamesession
        }
        else{
            ResetGameSession();
        }
    }

    public void ProcessPlayerLoseHealth(int value){
        if(playerHealth.getHealth()>value){
            playerHealth.DeductFromHealth(value);
            healthText.text = playerHealth.getHealth().ToString();
        }
        else{
            ProcessPlayerDeath();
            Revive();
        }
    }
    public void Revive(){
            playerHealth.setHealth(maxHealth);
            healthText.text = playerHealth.getHealth().ToString(); 
    }

    private void ResetGameSession(){
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    private void TakeLife(){
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        playerHealth.currentLife--;
        liveText.text = playerHealth.currentLife.ToString();
        SceneManager.LoadScene(currentSceneIndex);
    }
}
