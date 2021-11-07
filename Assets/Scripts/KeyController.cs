using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    PlayerController player;
    SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision){

    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.GetComponent<PlayerController>() != null){
            SoundManager.SoundInstace.Play(Sounds.KeyPickUp);
            player = collider.gameObject.GetComponent<PlayerController>();
            player.addScore();
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeOut(){
        for(float i =1f; i>0; ){
            i -= 0.25f;
            Color color = sr.color;
            color.a = i;
            sr.color = color;
            yield return new WaitForSeconds(0.25f);
        }
        Destroy(gameObject);
    }
}
