using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed, jump;
    public Animator playerAnimator;
    public ScoreBoardController scoreBoard;
    public GameOverController gameOver;
    public bool isAlive = true;
    public Image[] playerHealthImage;
    int playerHealthLength, 
        jumpCount=0,
        jumpThreshold=2;
    static bool sceneLoaded = false;
    static int playerHealth;
    float offsetY, boxY;
    int score=0;
    bool inAir = false;
    Rigidbody2D rb2d;
    BoxCollider2D boxCollider;
    private float horizontalForce, verticalForce;

    void Start()
    {
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        boxY = boxCollider.size.y;
        offsetY = boxCollider.offset.y;
        playerHealthLength = playerHealthImage.Length;
        if(!sceneLoaded){
            sceneLoaded = true;
            playerHealth = playerHealthLength;
        }else{
            for(int i=0; i<(playerHealthLength-playerHealth); i++){
                Destroy(playerHealthImage[(playerHealthLength-1) - i]);
            }
        }
    }

    IEnumerator PlayerDeathAnimation(){
        playerAnimator.SetBool("dead",true);
        yield return new WaitForSeconds(3);
        gameOver.EnableGameObject();
    }

    IEnumerator PlayerHurtAnimation(){
        playerAnimator.SetBool("hurt", true);
        yield return new WaitForSeconds(0.5f);
        playerAnimator.SetBool("hurt", false);
    }

    public void ReducePlayerHealth(bool fellDown = false)
    {
        if(playerHealth != 0){
            playerHealth--;
            Destroy(playerHealthImage[playerHealth]);
        }
        if(playerHealth == 0){
            SoundManager.SoundInstace.Play(Sounds.PlayerCriticalHit);
            this.enabled = false;
            isAlive = false;
            StartCoroutine(PlayerDeathAnimation());
            sceneLoaded = false;
        }else{
            SoundManager.SoundInstace.Play(Sounds.PlayerEnemyHit);
            Debug.Log("Play Hurt Animation");
            StartCoroutine(PlayerHurtAnimation());
            if(fellDown)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void addScore(int score=10)
    {
        this.score += score;
        scoreBoard.dispScore(this.score);
    }

    // Update is called once per frame
    void Update()
    {
        horizontalForce = Input.GetAxis("Horizontal");
        if(Input.GetKeyDown(KeyCode.Space)){
            verticalForce = Input.GetAxis("Jump");
        }else if(Input.GetKeyUp(KeyCode.Space)){
            verticalForce = 0;
        }

        PlayerAnimation();
    }

    void FixedUpdate(){
        PlayerMovement();
    }

    public void PlayerMovement(){
        Vector3 playerPosition = transform.localPosition;
        if(horizontalForce != 0){
            // SoundManager.SoundInstace.Play(Sounds.PlayerRun);
            playerPosition.x += speed * horizontalForce * Time.deltaTime;
            transform.localPosition = playerPosition;
        }
        // if((verticalForce > 0) && (!(inAir) || jumpCount < jumpThreshold)){
        if(verticalForce > 0 && jumpCount < jumpThreshold){
            Debug.Log("In Air : "+inAir);
            Debug.Log("Jump Count : "+jumpCount);
            SoundManager.SoundInstace.Play(Sounds.PlayerJumpUp);
            rb2d.AddForce(new Vector2(0,jump), ForceMode2D.Force);
            jumpCount++;
            // verticalForce = 0;
            // rb2d.velocity = Vector2.up * jump;
            inAir = true;
        }

    }

    private void PlayerAnimation()
    {
        Vector3 playerVector = transform.localScale;

        playerAnimator.SetFloat("speed", Mathf.Abs(horizontalForce));
        if (horizontalForce < 0)
        {
            playerVector.x = -1f * Mathf.Abs(playerVector.x);
        }
        else if (horizontalForce > 0)
        {
            playerVector.x = Mathf.Abs(playerVector.x);
        }
        transform.localScale = playerVector;

        // jump
        if (0 < verticalForce)
        {
            if(!inAir)
                playerAnimator.SetBool("jump", true);
        }
        else if(!inAir)
        {
            playerAnimator.SetBool("jump", false);
        }

        // crouch
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            playerAnimator.SetBool("crouch", true);
            boxCollider.offset = new Vector2(boxCollider.offset.x, (boxY * 0.5f) / 2);
            boxCollider.size = new Vector2(boxCollider.size.x, boxY * 0.5f);
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            playerAnimator.SetBool("crouch", false);
            boxCollider.offset = new Vector2(boxCollider.offset.x, offsetY);
            boxCollider.size = new Vector2(boxCollider.size.x, boxY);
        }
    }

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.tag == "FloorTile"){
            SoundManager.SoundInstace.Play(Sounds.PlayerLandDown);
            inAir = false;
            jumpCount = 0;
        }
    }

    // void OnTriggerEnter2D(Collider2D colObj){
    //     // Scene currentScene = SceneManager.GetActiveScene();
        
    //     // if(colObj.gameObject.tag == "SceneEnd")
    //     //     SceneManager.LoadScene(currentScene.buildIndex+1);
    // }
}
