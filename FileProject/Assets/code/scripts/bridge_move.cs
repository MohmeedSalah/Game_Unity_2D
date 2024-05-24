using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bridge_move : MonoBehaviour
{
    // Start is called before the first frame update
    public int speed = 10;
    public int jumpSpeed = 20; 
    //public bool is_jump = false;
    public float direction = -1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 
        transform.position += new Vector3(direction, 0, 0) * speed * Time.deltaTime;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        { 
            direction = direction * -1;
            transform.position += new Vector3(direction, 0, 0) * speed * Time.deltaTime;
            if (direction <= -1)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (direction >= 1)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
        if (collision.gameObject.tag == "Fire")
        {
            collision.gameObject.SetActive(false);
        }

        
    }
}
 