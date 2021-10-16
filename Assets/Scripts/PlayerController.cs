using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator playerAnimator;
    private float speed, jump;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerVector = transform.localScale;

        speed = Input.GetAxis("Horizontal");
        jump = Input.GetAxis("Vertical");

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
    }
}
