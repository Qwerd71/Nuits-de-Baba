using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    private int stage = 1;
    GameObject inthevoid = null;
    GameObject[] Allgos;
    bool onMap1 = true;
    BoxCollider2D bc;
    
    // Start is called before the first frame update
    void Start()
    {
        Allgos = GameObject.FindGameObjectsWithTag("Destroyable");
    }

    // Update is called once per frame
    void Update()
    {
        switch (stage)
        {
            case 1:
                if (Input.GetMouseButtonDown(0) && Physics2D.Raycast // ce bloc pour le pouvoir du stage 1
                (Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero)
                && inthevoid == null && hit1.collider.gameObject.tag == "Destroyable"))
                {
                    Debug.Log("yoga");
                    inthevoid = hit1.collider.gameObject;
                    hit1.collider.gameObject.SetActive(false);
                }
                else if (Input.GetMouseButtonDown(0) && inthevoid != null)
                {
                    inthevoid.SetActive(true);
                    inthevoid = null;
                }
                break;

            case 2:
                if (Input.GetMouseButtonDown(0) && Physics.Raycast // ce bloc pour le pouvoir du stage 2
                (Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit2)
                && hit2.collider.gameObject.tag == "Destroyable")
                {
                    hit2.collider.gameObject.SetActive(false);
                }
                break;
            case 3:
                if (Input.GetMouseButtonDown(0))
                {
                    foreach (GameObject go in Allgos)
                    {
                        go.GetComponent<Collider>().isTrigger = !go.GetComponent<Collider>().isTrigger; //à changer pour boxcollider2D
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
}
