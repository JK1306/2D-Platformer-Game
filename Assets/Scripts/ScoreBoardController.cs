using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreBoardController : MonoBehaviour
{
    TextMeshProUGUI scoreBord;
    // Start is called before the first frame update
    void Start()
    {
        scoreBord = gameObject.GetComponent<TextMeshProUGUI>();
        scoreBord.text = "Score : 0";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void dispScore(int score)
    {
        scoreBord.text = "Score : "+score;
    }
}
