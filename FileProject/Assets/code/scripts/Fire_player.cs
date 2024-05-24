using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_player : MonoBehaviour
{
    public int speed = 20;
    private float direction_fire = 1.0f;
    public float dir = 1.0f;
    public bool is_Fire = false;
    public float Fire_time = 0.3f;
    public float Fire_time_cur = 0; 
    public Animator animator;
    public Transform player; 
    public Transform Fire_bullet; 
    //public GameObject Fire_object;
    public GameObject Fire_inst;
    public int fire_num = 0; 

    Rigidbody2D rp; 
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>();
        direction_fire = 1.0f;
        //transform.position = new Vector3(player.position.x, player.position.y, -100); 
        //Fire_object[fire_num].SetActive(true);  
    }

    // Update is called once per frame
    void Update()
    {
        float mo = Input.GetAxis("Horizontal");
        if (mo > 0 && dir == -1 && !is_Fire)
        { 
            dir = 1;
        }
        if (mo < 0 && dir == 1 && !is_Fire) { 
            dir = -1;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && !is_Fire)
        { 
            //Fire_inst = Instantiate(Fire_object, Fire_bullet.position,Fire_bullet.rotation);
            is_Fire = true; 
            direction_fire = dir; 
            Fire_inst.SetActive(true);
            animator.SetBool("FireExtra", true);
            Fire_time_cur = 0; 
            rp = Fire_inst.GetComponent<Rigidbody2D>();
            rp.position = new Vector2(player.position.x + (direction_fire), player.position.y +0.3f); 
            //rp.transform.localScale *= 2.5f;
        } 
        if (is_Fire)
        {
            if (direction_fire > 0)
            { 
                rp.velocity = (Fire_bullet.right * direction_fire) * speed;                                                             
            }
            else if (direction_fire < 0)
            {
                rp.velocity = (Fire_bullet.right * direction_fire) * speed;
                //rp.rotation = 180f;
            }
            Fire_time_cur += Time.deltaTime;
            if (Fire_time_cur > Fire_time)
            {
                animator.SetBool("FireExtra", false);
                Fire_inst.SetActive(false); 
                is_Fire = false;
            }
        }
         
    } 
}
