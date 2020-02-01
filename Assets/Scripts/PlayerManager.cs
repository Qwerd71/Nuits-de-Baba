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
    public float invincibilityTime;
    public float currentinvincibilityTime;
    public bool isInvincible = false;
    public GameObject Heart;
    private GameObject Heart_b;
    private GameObject Heart_f;

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
        Heart_f = Heart.transform.GetChild(0).gameObject;
        Heart_b = Heart.transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && !Jumping) {
            rb.AddForce(new Vector2(0, JumpSpeed));
            Jumping = true;
        }
        if (Time.time >= currentinvincibilityTime)
            isInvincible = false;
        

        Horizontal = Input.GetAxis("Horizontal");
        if (Horizontal < 0)
            spriteRenderer.flipX = true;
        else if (Horizontal > 0)
            spriteRenderer.flipX = false;
        rb.velocity = new Vector2(Horizontal * MoveSpeed,rb.velocity.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag.Equals("Floor") || collision.collider.tag.Equals("Destroyable"))
            Jumping = false;
        if (collision.collider.tag.Equals("Enemy") && isInvincible == false)
        {
            Debug.Log(life);
            if ((life -= 1) == 0)
                Death();
            else
            {
                isInvincible = true;
                currentinvincibilityTime = invincibilityTime + Time.time;
            }
        }
            //Graphics : lost heart
    }
    private void Death()
    {
        Heart_b.SetActive(true);
        Heart_f.SetActive(false);
        GameManager.Instance.Reset();
    }
}
