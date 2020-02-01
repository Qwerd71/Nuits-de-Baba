using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager: MonoBehaviour
{
    GameObject go;
    public int JumpSpeed;
    public int MoveSpeed;
    float Horizontal;
    public bool Jumping = false;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    public int life = 1;
    public float invincibilityTime = 3;
    public float currentinvincibilityTime;
    public bool isInvincible = false;
    RaycastHit2D hit;

    public GameObject Heart1;
    public GameObject Heart2;
    public GameObject Heart3;
    private GameObject Heart_b;
    private GameObject Heart_f;
    public Animator animator;

    private Quaternion zero = new Quaternion(0f,0f,0f,0f);
    SpriteRenderer sr;
    public float flickerTime = 3;
    public float currentflickerTime;
    private float temp = 0;

    static PlayerManager _instancepm;

    public static PlayerManager Instance
    {
        get
        {
            if (_instancepm == null)
            {
                _instancepm = FindObjectOfType<PlayerManager>();
                if (_instancepm == null)
                {
                    _instancepm = Instantiate(new GameObject()).AddComponent<PlayerManager>();
                }
            }
            return _instancepm;
        }
    }



    // Start is called before the first frame update



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetButtonDown("Jump")  || Input.GetKeyDown("space"))&& !Jumping) {
            rb.AddForce(new Vector2(0, JumpSpeed));
            Jumping = true;
        }

        Horizontal = Input.GetAxis("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(Horizontal));
        animator.SetBool("Jumping", Jumping);

        if (Horizontal < 0)
            spriteRenderer.flipX = true;
        else if (Horizontal > 0)
            spriteRenderer.flipX = false;
        rb.velocity = new Vector2(Horizontal * MoveSpeed,rb.velocity.y);
        
        if (Time.time >= currentinvincibilityTime)
            isInvincible = false;
        if (isInvincible && !GameManager.Instance.isShielded)
            currentflickerTime = flickerTime + Time.time;
        if (Time.time <= currentflickerTime)
        {
            if (temp < 0.25)
                temp += Time.deltaTime;
            else
            {
                sr.enabled = !sr.enabled;
                temp = 0;
            }
        }
        else
            sr.enabled = true;

        if (transform.position.y <= -8)
        { // en cas de trou, on perd un coeur en tombant et on est respawn en début de niveau
            life -= 1;
            isInvincible = true;
            currentinvincibilityTime = invincibilityTime + Time.time;
            switch (life)
            {
                case (2):
                    LoseLife(Heart3);
                    break;
                case (1):
                    LoseLife(Heart2);
                    break;
                case (0):
                    LoseLife(Heart1);
                    Death();
                    break;
            }
            transform.position = GameManager.Instance.lastCheckpoint;
        }            
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag.Equals("Floor") || collision.collider.tag.Equals("Destroyable"))
            Jumping = false;
        if (collision.collider.tag.Equals("Enemy") && isInvincible == false)
        {
            life -= 1;
            isInvincible = true;
            currentinvincibilityTime = invincibilityTime + Time.time;
            switch (life)
            {
                case (2):
                    LoseLife(Heart3);
                    break;
                case (1):
                    LoseLife(Heart2);
                    break;
                case (0):
                    LoseLife(Heart1);
                    Death();
                    break;
            }
            rb.AddForce((transform.position - collision.collider.transform.position) * 1000);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
        if (collision.tag.Equals("Floor") || collision.tag.Equals("Destroyable"))
            Jumping = false;
        if (collision.tag.Equals("Enemy") && isInvincible == false)
        {
            life -= 1;
            isInvincible = true;
            currentinvincibilityTime = invincibilityTime + Time.time;
            switch (life)
            {
                case (2):
                    LoseLife(Heart3);
                    break;
                case (1):
                    LoseLife(Heart2);
                    break;
                case (0):
                    LoseLife(Heart1);
                    Death();
                    break;
            }
        }
    }
    private void Death()
    {
        GameManager.Instance.Reset();
    }
    private void LoseLife(GameObject Heart)
    {
        Heart_b = Heart.transform.GetChild(1).gameObject;
        Heart_f = Heart.transform.GetChild(0).gameObject;
        Heart_b.SetActive(true);
        Heart_f.SetActive(false);
    }
}
