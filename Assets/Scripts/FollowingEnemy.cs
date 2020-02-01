using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingEnemy : MonoBehaviour
{
    public float MoveSpeed;
    private RaycastHit2D ray;
    bool isVisible = false;
    Vector3 move;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (isVisible) 
        {
            move = new Vector3(Time.deltaTime * MoveSpeed,0,0);
            if (transform.position.x <= PlayerManager.Instance.transform.position.x)
            {
                transform.position += move;
            }
            else
                transform.position -= move;
                ;
        }
    }
    private void OnBecameVisible()
    {
        isVisible = true;
    }
    private void OnBecameInvisible()
    {
        isVisible = false;
    }
}
