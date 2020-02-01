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
    public int life = 3;
    public float invincibilityTime;
    public float currentinvincibilityTime;
    public bool isInvincible = false;
    public GameObject Heart1;
    public GameObject Heart2;
    public GameObject Heart3;
    public GameObject Brokenheart;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && !Jumping) {
            rb.AddForce(new Vector2(0, JumpSpeed));
            Jumping = true;
        }

        Horizontal = Input.GetAxis("Horizontal");
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
            if (temp < 0.5)
                temp += Time.deltaTime;
            else
            {
                sr.enabled = !sr.enabled;
                temp = 0;
            }
        }
        else
            sr.enabled = true;

        
            
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag.Equals("Floor") || collision.collider.tag.Equals("Destroyable"))
            Jumping = false;
    
        if (collision.collider.tag.Equals("Enemy") && isInvincible == false) 
        {
            if ((life -= 1) == 0)
                Death();
            else
            {
                isInvincible = true;
                currentinvincibilityTime = invincibilityTime + Time.time;
                switch (life)
                {
                    case (2):
                        Instantiate(Brokenheart, Camera.main.transform.position, zero) ;
                        Destroy(Heart3);
                        break;
                    case (1):
                        Vector3 posHeart2 = Heart2.transform.position;
                        Instantiate(Brokenheart, posHeart2, zero);
                        break;
                    case (0):
                        Vector3 posHeart1 = Heart1.transform.position;
                        Instantiate(Brokenheart, posHeart1, zero);
                        break;
                }
            }
        }
    }
    private void Death()
    {
        GameManager.Instance.Reset();
    }
}
