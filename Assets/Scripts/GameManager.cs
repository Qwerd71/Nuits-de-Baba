using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{


    private int stage;
    GameObject inthevoid = null;
    GameObject[] Allgos;
    bool onMap1 = true;
    BoxCollider2D bc;
    RaycastHit2D hit;
    
    // Start is called before the first frame update
    void Start()
    {
        Allgos = GameObject.FindGameObjectsWithTag("Destroyable");
        stage = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        switch (stage)
        {
            case 1:
                if (Input.GetMouseButtonDown(0))// ce bloc pour le pouvoir du stage 1
                    hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (inthevoid == null && hit.collider.gameObject.tag == "Destroyable")
                {
                    inthevoid = hit.collider.gameObject;
                    hit.collider.gameObject.SetActive(false);
                }
                else if (Input.GetMouseButtonDown(0) && inthevoid != null)
                {
                    inthevoid.SetActive(true);
                    inthevoid = null;
                }
                break;

            case 2:
                if (Input.GetMouseButtonDown(0)) // ce bloc pour le pouvoir du stage 2
                    hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider.gameObject.tag == "Destroyable" || hit.collider.gameObject.tag == "Ennemy")
                {
                    hit.collider.gameObject.SetActive(false);
                }
                break;
            case 3:
                if (Input.GetMouseButtonDown(0))
                {
                    foreach (GameObject go in Allgos)
                    {
                        go.GetComponent<Collider2D>().isTrigger = !go.GetComponent<Collider2D>().isTrigger; //à changer pour boxcollider2D
                        // Changer les couleurs pour passer au négatif.
                    }
                }
                break;
            case 4:
                if (Input.GetMouseButtonDown(0))
                    ;
                    break;
        }
    }

    private void Restart()
    {
        
    }
}
