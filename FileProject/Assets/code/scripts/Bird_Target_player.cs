using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird_Target_player : MonoBehaviour
{
    public int speed = 10; 
    public Transform Player;
    public static float direction = -1;
    [SerializeField] AudioSource death;
    [SerializeField] AudioSource enemydeath;
    void Update()
    {
        transform.position += new Vector3(direction, 0, 0) * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Box")
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
            enemydeath.Play();
            collision.gameObject.SetActive(false);
            DestroyObject(gameObject);
        }
    }
}
