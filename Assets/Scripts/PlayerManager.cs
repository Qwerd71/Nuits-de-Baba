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
    public int life = 3;
    public float invincibilityTime;
    public float currentinvincibilityTime;
    public bool isInvincible = false;

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
        rb.velocity = new Vector2(Horizontal * MoveSpeed,rb.velocity.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag.Equals("Floor") || collision.collider.tag.Equals("Destroyable"))
            Jumping = false;
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy" && isInvincible == false)
        {
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
        GameManager.Instance.Reset();
    }
}
