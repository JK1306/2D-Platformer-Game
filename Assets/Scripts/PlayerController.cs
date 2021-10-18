using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator playerAnimator;
    BoxCollider2D boxCollider;
    private float speed, jump;
    float offsetY, boxY;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        // Debug.Log(boxCollider.center);
        boxY = boxCollider.size.y;
        offsetY = boxCollider.offset.y;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerVector = transform.localScale;

        speed = Input.GetAxis("Horizontal");
        jump = Input.GetAxis("Jump");
        playerAnimator.SetFloat("speed",Mathf.Abs(speed));
        if(speed < 0){
            playerVector.x = -1f * Mathf.Abs(playerVector.x);
        }else if(speed > 0){
            playerVector.x = Mathf.Abs(playerVector.x);
        }
        transform.localScale = playerVector;

        if(0<jump){
            playerAnimator.SetBool("jump",true);
        }else{
            playerAnimator.SetBool("jump",false);
        }

        if(Input.GetKeyDown(KeyCode.LeftControl)){
            playerAnimator.SetBool("crouch",true);
            // Debug.Log("BoxY Value :"+boxY);
            // Debug.Log("BoxY Value :"+(boxY*0.5f));
            // Debug.Log("BoxY : "+(boxY * 0.5f)/2);
            boxCollider.offset = new Vector2(boxCollider.offset.x ,(boxY * 0.5f)/2);
            boxCollider.size = new Vector2(boxCollider.size.x, boxY * 0.5f);
        }else if(Input.GetKeyUp(KeyCode.LeftControl)){
            playerAnimator.SetBool("crouch",false);
            boxCollider.offset = new Vector2(boxCollider.offset.x,offsetY);
            boxCollider.size = new Vector2(boxCollider.size.x, boxY);
        }
    }
}
