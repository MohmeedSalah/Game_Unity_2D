using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap_land_move : MonoBehaviour
{
    private int speed = 2; 
    public Transform Player;
    public Transform up_box;
    public HealthBar bar;
    //public bool is_jump = false;
    public float direction = -1;
    public Rigidbody2D rp;
    float time_cur = 0;
    float time = 2f;
    bool test = false;
    float x = 0;
    float y = 0;
    //public Animator animator; 
    // Start is called before the first frame update
    void Start()
    {
        x = up_box.position.x;
        y = up_box.position.y;
    }
    // Update is called once per frame
    void Update()
    {
        //transform.position += new Vector3(0, direction, 0) * speed * Time.deltaTime;
        time_cur += Time.deltaTime;
        if (time_cur > time)
        {
            time_cur = 0;
            test = false;
            up_box.gameObject.SetActive(false);

        }

        if (!test) rp.velocity = new Vector2(0, speed * direction);
        else
        {
            rp.velocity = new Vector2(0, 0);
            //up_box.position = new Vector3(x, y, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Box")
        { 
            direction = direction * -1;
            test = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Box" || collision.gameObject.tag == "Ground")
        {
            up_box.gameObject.SetActive(true);
            direction = direction * -1;
            test = true; 
        }
        if (collision.gameObject.tag == "Player")
        {
            Player.position = new Vector3(-80, -15, 0); 
            bar.SetHealth(100);
            ScriptBar.Bar_live = 100;
            scriptHealth.score_live -= 1; 
        }
    }
}
