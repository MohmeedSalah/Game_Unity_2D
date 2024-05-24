using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Winner : MonoBehaviour
{
    Text score;
    public float Win_time_cur = 0f;
    public float Win_time = 4f; 
    // Start is called before the first frame update
    void Start()
    {
        score = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    { 
        if (Move.Winner == 1)
        {        
            score.text = "Winner Winner CheckenDinner"; 
            Win_time_cur += Time.deltaTime;
            if (Win_time_cur > Win_time) 
            {
                Move.Winner = 0;
                Win_time_cur = 0; 
                score.text = "";
            } 
        }
    }
}
