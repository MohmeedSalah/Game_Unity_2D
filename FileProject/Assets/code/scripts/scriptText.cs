using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scriptText : MonoBehaviour
{
    public static int score_value = 0;
    Text score; 
    // Start is called before the first frame update
    void Start()
    {
        score = GetComponent<Text>();   
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "Score: = " + score_value;
    }
}
