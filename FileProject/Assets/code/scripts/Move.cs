using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public static int count = 0;
    public int speed = 10;
    public float jumpSpeed = 50;
    public float enemySpeed = 3;
    public bool is_jump = false;
    public bool is_fire = false;
    public bool is_climb = false;
    public bool is_stairs = false;
    public bool ground = false;
    public bool box = false;
    public bool AreaOn = false;
    public bool can_jump = true;
    public bool is_sliding = false;
    public float direction;
    public Animator animator;
    private float Fire_time_cur = 0;
    public float jump_time_cur = 0.0f;
    private float Fire_time = 1f;
    private float Fire_animation_time = 0.4f;
    public Rigidbody2D rp;
    public GameObject enemy; 
    public HealthBar bar;
    private bool enemy_kill = false;
    [SerializeField] AudioSource jumb;
    [SerializeField] AudioSource fire;
    [SerializeField] AudioSource collect;
    [SerializeField] AudioSource win;
    [SerializeField] AudioSource win2;
    [SerializeField] AudioSource full;
    [SerializeField] public AudioSource walk;
    [SerializeField] public AudioSource start;
    [SerializeField] AudioSource death;
    private float fallTime = 0f;
    private bool isFalling = false;
    private int right = 0;
    private int angle = 0;
    private int wall_right = 1;
    private Vector3 startPosition;
    private bool isWallJumping = false;
    private bool isStickingToWall = false;
    private int wallDirection = 0; 
    public float Damage_time = 1;
    public float Damage_time_cur = 1;
    public float length_bar = 10;
    public static int Winner = 0;
    void Start()
    { 
        bar.SetMaxHealth(100);
        startPosition = transform.position;
        animator = GetComponent<Animator>();
        walk.Play();
        start.Play();
        full.Stop();
    }

    void Update()
    {
        direction = Input.GetAxis("Horizontal");
        if (enemy_kill)
        { 
            Damage_time_cur += Time.deltaTime;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(-wallDirection *1 , 2));   
            if (Damage_time_cur * 2 > Damage_time)
            { 
                ScriptBar.Bar_live -= 30;
                bar.SetHealth(ScriptBar.Bar_live);
                length_bar -= 2;
                //Bar.transform.localScale = new Vector3(length_bar,+ 1f, 1f);
                Damage_time_cur = 0;
            }
            if (ScriptBar.Bar_live <= 0)
            {
                bar.SetHealth(100);
                death.Play();
                ScriptBar.Bar_live = 100;
                length_bar = 10;
                //Bar.transform.localScale = new Vector3(length_bar, 1f, 1f);
                transform.position = new Vector3(-80, -15, 0);
                scriptHealth.score_live -= 1; 
                if (scriptHealth.score_live <= 0)
                {
                    transform.gameObject.SetActive(false); ;
                }
            }
        }
        if (direction == 0)
        {
            animator.SetBool("run", false);
        }
        else if (direction > 0.0f)
        {
            animator.SetBool("run", true);
            if (right != 1)
            {
                Vector3 mo = new Vector3(transform.position.x, transform.position.y, transform.position.z); //0.62
                if (right == 2)
                {
                    transform.RotateAround(mo, Vector3.up, 180f);
                }
                right = 1;
            }
        }
        else if (direction < 0 && right != 2)
        {
            animator.SetBool("run", true); 
            Vector3 mo = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            if (right != 2) transform.RotateAround(mo, Vector3.up, 180f);
            right = 2;
        }
        if(direction != 0) animator.SetBool("run", true);
        transform.position += new Vector3(direction, 0, 0) * speed * Time.deltaTime;
        HandleFire();
        HandleJump();
        HandleStairs();
        HandleEnemyMovement(); 

        if (isStickingToWall)
        {
            direction = 0;
            transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
        }
        if (isStickingToWall && Input.GetKeyDown(KeyCode.Space))
        {
            isStickingToWall = false;
            can_jump = false;

            float jumpForceX = jumpSpeed * 1.2f;
            float jumpForceY = jumpSpeed;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(-wallDirection * jumpForceX, jumpForceY));
            Flip();
            animator.SetBool("jumping", true);
            jumb.Play();
        }
    }
    private void Flip()
    {
        right = (right == 1) ? 1 : 2; // Toggle between right (1) and left (2)
        Vector3 scale = transform.localScale;
        //scale.x *= -1; // Flip the player's scale horizontally
        transform.localScale = scale;
    }


    private void HandleFire()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !is_fire)
        {
            is_fire = true;
            can_jump = false;
            animator.SetBool("FireExtra", true);
            fire.Play();
            Fire_time_cur += Time.deltaTime;
        }

        if (is_fire)
        {
            Fire_time_cur += Time.deltaTime;
            if (Fire_time_cur > Fire_time)
            {
                is_fire = false;
            }
            if (Fire_time_cur > Fire_animation_time)
            {
                animator.SetBool("FireExtra", false);
            }
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !is_jump && can_jump)
        {
            jump_time_cur = 0;
            can_jump = false;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1) * jumpSpeed);
            animator.SetBool("jumping", true);
            jumb.Play();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && !is_jump && can_jump)
        {
            // Add more force for a long jump
            jump_time_cur = 0;
            can_jump = false;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1) * (jumpSpeed * 1.5f)); // Increase jump force
            animator.SetBool("jumping", true);
            jumb.Play();
        }

        if ((is_jump && !is_stairs) || jump_time_cur > 0.0f)
        {
            jump_time_cur += Time.deltaTime;
            if (jump_time_cur > Fire_time)
            {
                can_jump = true;
                jump_time_cur = 0;
            }
        }

        if (jump_time_cur == 0)
        {
            can_jump = true;
        }
    }


    private void HandleStairs()
    {
        if (is_stairs)
        {
            float dir_y = Input.GetAxis("Vertical");
            rp.gravityScale = 0f;
            if (Mathf.Abs(dir_y) > 0.0f)
            {
                is_climb = true;
                animator.SetBool("climb", true);
                transform.position += new Vector3(0, dir_y, 0) * 5 * Time.deltaTime;
            }
        }
        else
        {
            rp.gravityScale = 1f;
        }
    }

    private void HandleEnemyMovement()
    {
        if (AreaOn)
        {
            if (Bird_Target_player.direction == 1)
            {
                if (enemy.transform.position.x <= transform.position.x)
                {
                    enemy.transform.rotation = Quaternion.Euler(0, 180, 45);
                    float distance = Vector2.Distance(enemy.transform.position, transform.position);
                    Vector2 direction = (transform.position - enemy.transform.position).normalized;
                    enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, transform.position, 8 * Time.deltaTime);
                }
                else
                {
                    enemy.transform.rotation = Quaternion.Euler(0, 0, 45);
                    float distance = Vector2.Distance(enemy.transform.position, transform.position);
                    Vector2 direction = (transform.position - enemy.transform.position).normalized;
                    enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, transform.position, 8 * Time.deltaTime);
                }
            }
            else
            {
                if (enemy.transform.position.x <= transform.position.x)
                {
                    enemy.transform.rotation = Quaternion.Euler(0, 180, 45);
                    float distance = Vector2.Distance(enemy.transform.position, transform.position);
                    Vector2 direction = (transform.position - enemy.transform.position).normalized;
                    enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, transform.position, 8 * Time.deltaTime);
                }
                else
                {
                    enemy.transform.rotation = Quaternion.Euler(0, 0, 45);
                    float distance = Vector2.Distance(enemy.transform.position, transform.position);
                    Vector2 direction = (transform.position - enemy.transform.position).normalized;
                    enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, transform.position, 8 * Time.deltaTime);
                }
            }
        } 
    }
     


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            wall_right = 1;
            animator.SetBool("jumping", false);
            ground = true;
        }
        if (collision.gameObject.tag == "Box")
        {
            box = true;
            animator.SetBool("jumping", false);
        }
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Box")
        {
            is_jump = false;
            animator.SetBool("jumping", false);
        }
        if (collision.gameObject.tag == "cherry")
        {
            DestroyObject(collision.gameObject);
            scriptText.score_value += 10;
            collect.Play();
        }
        //comment this
        if (collision.gameObject.tag == "house")
        {
            walk.Stop();
            win.Play();
            win2.Play();
            transform.position = new Vector3(113f, -15.2f, 0f);
            Winner=1;
            count++;
        }
        if (collision.gameObject.tag == "house" && scriptText.score_value >= 50)
        { 
            transform.position = new Vector3(120, -15, 0);
            scriptText.score_value = 0;
            count++;
            walk.Stop();
            win.Play();
            //new WaitForSeconds(0.25f);
            win2.Play();
            Winner=1;
        }
        if (collision.gameObject.tag == "Die")
        {
            full.Play();
            // Reset player position
            transform.position = startPosition;
            scriptHealth.score_live--;
        }
        if (collision.gameObject.tag == "Wall")
        {
            isStickingToWall = true;
            animator.SetBool("jumping", false);

            if (collision.contacts[0].normal.x > 0)
            {
                wallDirection = -1;
            }
            else
            {
                wallDirection = 1;
            }
        }
        if (collision.gameObject.tag == "enemy")
        {
            enemy_kill = true;
            Damage_time_cur = 1;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground") ground = false;
        if (collision.gameObject.tag == "Box") box = false;
        if (!ground && !box)
        {
            is_jump = true;
        }
        if (collision.gameObject.tag == "Wall")
        {
            isWallJumping = false;
        }
        if (collision.gameObject.tag == "enemy")
        {
            enemy_kill = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Area_Eagle")
        {
            AreaOn = true;
        }
        if (collision.gameObject.tag == "stairs")
        {
            is_stairs = true;
            is_jump = true;
            animator.SetBool("jumping", false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Area_Eagle")
        {
            AreaOn = false;
            while (enemy.transform.position.y < 13)
            {

                enemy.transform.position += new Vector3(0, 1, 0) * Time.deltaTime;
            }
            if (Bird_Target_player.direction <= -1)
                enemy.transform.rotation = Quaternion.Euler(0, 0, 0);
            else
                enemy.transform.rotation = Quaternion.Euler(0, 180, 0);
            //enemy.transform.position = new vector3(enemy.transform.position.x, 13, 0);
        }
        if (collision.gameObject.tag == "stairs")
        {
            is_stairs = false;
            is_climb = false;
        }
        animator.SetBool("climb", false);
    }
}