using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int stage = 4;
    GameObject inthevoid = null;
    GameObject[] Allgos;
    bool onMap1 = true;
    BoxCollider2D bc;
    RaycastHit2D hit;
    public float shieldtime;
    public float currentshieldtime;
    public GameObject shield;
    private bool isShielded = false;
    public GameObject player;
    private Quaternion zero = new Quaternion(0f, 0f, 0f, 0f);
    TextMesh tm;
    

    static GameManager _instancegm;

    public static GameManager Instance

    {
        get
        {
            if (_instancegm == null)
            {
                _instancegm = FindObjectOfType<GameManager>();
                if (_instancegm == null)
                {
                    _instancegm = Instantiate(new GameObject()).AddComponent<GameManager>();
                }
            }
            return _instancegm;
        }

    }

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
                if (Input.GetMouseButtonDown(0))
                {// ce bloc pour le pouvoir du stage 1
                    hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                    if (inthevoid == null && hit.collider.gameObject.tag == "Destroyable")
                    {
                        inthevoid = hit.collider.gameObject;
                        hit.collider.gameObject.SetActive(false);
                    }
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
                if (Input.GetMouseButtonDown(0) && !isShielded) 
                {
                    Instantiate(shield,player.transform.position,zero,player.transform) ; // (?)
                    PlayerManager.Instance.currentinvincibilityTime = Time.time + PlayerManager.Instance.invincibilityTime ;
                    PlayerManager.Instance.isInvincible = true;
                    isShielded = true;
                }
                break;
            default:
                if (Input.GetMouseButtonDown(0))
                {
                    hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                    switch (hit.collider.name)
                    {
                        case "NPC1":
                            tm.text = "";
                            tm.font = new Font("Arial");
                            break;
                        case "NPC2":
                            tm.text = "";
                            tm.font = new Font("Arial");
                            break;
                        case "NPC3":
                            tm.text = "";
                            tm.font = new Font("Arial");
                            break;
                        case "NPC4":
                            tm.text = "";
                            tm.font = new Font("Arial");
                            break;
                    }

                        
                }
                
                break;
        }
        if (isShielded && Time.time >= PlayerManager.Instance.currentinvincibilityTime)
        {
            Destroy(shield);
        }
            
    }
    public void Reset()
    {
        
    }
}
