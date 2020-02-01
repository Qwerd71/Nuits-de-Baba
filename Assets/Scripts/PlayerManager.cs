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
    private int life = 3;
    public int invicibilityFrames;
    


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

        Horizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(Horizontal * MoveSpeed,rb.velocity.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag.Equals("Floor") || collision.collider.tag.Equals("Destroyable"))
            Jumping = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag.Equals("Enemy"))
        {
            if ((life -= 1) == 0)
                Debug.Log("ici");

            
        }
            //Graphics : lost heart
    }
}
