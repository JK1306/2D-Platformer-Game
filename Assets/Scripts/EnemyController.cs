using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public LayerMask floorLayer;
    public bool isFacingRight = true;
    public float targetDistance;
    public float health;
    PlayerController player;
    bool upperHit = false;
    RaycastHit2D enemyRayCast;
    Animator enemyAnimator;
    // bool playedAttack = false;
    GameObject rayCastObj;
    // Transform playerTransform;
    void Start()
    {
        rayCastObj = gameObject.transform.GetChild(0).gameObject;
        enemyAnimator = gameObject.GetComponent<Animator>();
        enemyAnimator.SetBool("walk",true);
    }

    void Update()
    {
        EnemyAnimation();
        EnemyMovement();
    }

    void flipGameObject(){
        if(!isFacingRight){
            gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
            isFacingRight = true;
        }else{
            gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
            isFacingRight = false;
        }
    }

    bool isHitWall(){
        Vector2 targetDist = rayCastObj.transform.position;
        targetDist.x += targetDistance;

        Debug.DrawRay(rayCastObj.transform.position, Vector3.forward, Color.black);
        enemyRayCast = Physics2D.Linecast(rayCastObj.transform.position, targetDist, floorLayer);

        if(Physics2D.Linecast(rayCastObj.transform.position, targetDist, 1 << LayerMask.NameToLayer("TerrainGround"))){
            return true;
        }
        return false;
    }

    bool CametoEnd(){
        enemyRayCast = Physics2D.Raycast(rayCastObj.transform.position, Vector2.down, 1f);
        if(!enemyRayCast.collider){
            return true;
        }
        return false;
    }

    void EnemyAnimation(){
        if(isHitWall() || CametoEnd()){
            flipGameObject();
        }
    }

    public void reduceHealth(PlayerController playerControl){
        if(health > 0)
            health -= 10;

        if(health <= 0){
            this.enabled = false;
            speed = 0f;
            playerControl.addScore(20);
            SoundManager.SoundInstace.Play(Sounds.ChomperDie);
            StartCoroutine(PlayDeathAnimation());
        }
    }

    void EnemyMovement(){
        Vector2 enemyPosition = transform.localPosition;
        // SoundManager.SoundInstace.Play(Sounds.ChomperWalk);

        if(gameObject.transform.eulerAngles.x == 0){
            enemyPosition.x += speed * Time.deltaTime;
        }else{
            enemyPosition.x -= speed * Time.deltaTime;
        }
        transform.localPosition = enemyPosition;
    }

    IEnumerator PlayAttackAnimation(){
        enemyAnimator.SetBool("attack",true);
        yield return new WaitForSeconds(0.5f);
        enemyAnimator.SetBool("attack",false);
        enemyAnimator.SetBool("walk",true);
    }

    IEnumerator PlayDeathAnimation(){
        enemyAnimator.SetBool("death",true);
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.GetComponent<PlayerController>() != null && !upperHit){
            Debug.Log("Collider Hit");
            player = collision.gameObject.GetComponent<PlayerController>();
            if(player.isAlive){
                SoundManager.SoundInstace.Play(Sounds.ChomperAttack);
                enemyAnimator.SetBool("walk",false);
                StartCoroutine(PlayAttackAnimation());
                player.ReducePlayerHealth();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.GetComponent<PlayerController>() != null){
            upperHit = true;
            player = collider.gameObject.GetComponent<PlayerController>();
            reduceHealth(player);
        }
    }

    void OnTriggerExit2D(Collider2D collider){
        if(collider.gameObject.GetComponent<PlayerController>() != null){
            Debug.Log("player Exited");
            upperHit = false;
        }
    }
}
