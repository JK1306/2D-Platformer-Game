using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public LayerMask floorLayer;
    public bool isFacingRight = true;
    public float targetDistance;
    PlayerController player;
    RaycastHit2D enemyRayCast;
    //  enemyCircle;
    GameObject rayCastObj;
    // Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        rayCastObj = gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
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

    void EnemyMovement(){
        Vector2 enemyPosition = transform.localPosition;

        if(gameObject.transform.eulerAngles.x == 0){
            enemyPosition.x += speed * Time.deltaTime;
        }else{
            enemyPosition.x -= speed * Time.deltaTime;
        }

        transform.localPosition = enemyPosition;
    }

    void OnCollisionEnter2D(Collision2D collision){
        // Debug.Log(collision.gameObject.tag);
        if(collision.gameObject.GetComponent<PlayerController>() != null){
            player = collision.gameObject.GetComponent<PlayerController>();
            // player.KillPlayer();
        }
    }
}
