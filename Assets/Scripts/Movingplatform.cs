using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movingplatform : MonoBehaviour
{
    public bool movingLeft;
    public float platformSpeed;
    public float minposx;
    public float maxposx;
    Vector3 move;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position.x > maxposx)
        {
            movingLeft = true;
        }
        else if (transform.position.x < minposx)
        {
            movingLeft = false;
        }
        if (movingLeft)
        {
            move = new Vector3(-Time.deltaTime * platformSpeed, 0, 0);
        }
        else
        {
            move = new Vector3(Time.deltaTime * platformSpeed, 0, 0);
        }
        transform.position += move;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
            PlayerManager.Instance.transform.position += move;
    }
}
