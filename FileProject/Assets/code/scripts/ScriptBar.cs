using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptBar : MonoBehaviour
{
    // Start is called before the first frame update
    public static int Bar_live = 100; 
    Text Health;
    // Start is called before the first frame update
    void Start()
    {
        Health = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Health.text = "Health:" + Bar_live + '%';
    }
}
