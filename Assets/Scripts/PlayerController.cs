using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed, jump;
    public Animator playerAnimator;
    float offsetY, boxY;
    bool inAir = false;
    Rigidbody2D rb2d;
    BoxCollider2D boxCollider;
    private float horizontalForce, verticalForce;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        boxY = boxCollider.size.y;
        offsetY = boxCollider.offset.y;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalForce = Input.GetAxis("Horizontal");
        verticalForce = Input.GetAxis("Jump");

        PlayerAnimation();
        PlayerMovement();
    }

    public void PlayerMovement(){
        Vector3 playerPosition = transform.localPosition;
        if(horizontalForce != 0){
            playerPosition.x += speed * horizontalForce * Time.deltaTime;
            transform.localPosition = playerPosition;
        }
        if((verticalForce > 0) && !(inAir)){
            rb2d.AddForce(new Vector2(0,jump), ForceMode2D.Force);
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
            inAir = false;
        }
    }
}
