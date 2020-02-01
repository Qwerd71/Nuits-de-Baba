using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int stage = 2;
    GameObject inthevoid = null;
    GameObject[] Allgos;
    BoxCollider2D bc;
    Vector3 hit3;
    RaycastHit2D hit;
    public float shieldtime;
    public float currentshieldtime;
    public GameObject shield;
    public bool isShielded = false;
    public GameObject player;
    public GameObject fireball;
    private Quaternion zero = new Quaternion(0f, 0f, 0f, 0f);
    TextMesh tm;
    public Vector3 lastCheckpoint = new Vector3(-8,0,0);
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
        //stage = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        switch (stage)
        {
            case 1:
                if (Input.GetMouseButtonDown(0))
                {// ce bloc pour le pouvoir du stage 1
                    //Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
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
                {
                    hit3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        GameObject Fireball = Instantiate(fireball, player.transform.position, fireball.transform.rotation);

                        Fireball.transform.LookAt(new Vector2(hit3.x, hit3.y));
                        Fireball.transform.rotation = new Quaternion(0, 0, Fireball.transform.rotation.z, Fireball.transform.rotation.w);
                        Fireball.GetComponent<Rigidbody2D>().AddForce(new Vector2((hit3 - Fireball.transform.position).x, (hit3 - Fireball.transform.position).y) * 50);
                        Destroy(Fireball, 2f);
                }
                //gameObject.SetActive(false);
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
                        case "Docteur":
                            tm.text = "I'm sorry. We made all we could but we couldn't save her. My apologies.";
                            tm.font = new Font("Arial");
                            break;
                        case "Bystander1":
                            tm.text = "Did you hear about that story ? Poor girl !";
                            tm.font = new Font("Arial");
                            break;
                        case "Bystander2":
                            tm.text = "This kind of driver should be jailed until they die !";
                            tm.font = new Font("Arial");
                            break;
                        case "Parent1":
                            tm.text = "Why did it happen to us ? She was so young.";
                            tm.font = new Font("Arial");
                            break;
                        case "Parent2":
                            tm.text = "You will always be welcome at home.";
                            tm.font = new Font("Arial");
                            break;
                        case "Friend1":
                            tm.text = "I can't believe it.";
                            tm.font = new Font("Arial");
                            break;
                        case "Friend2":
                            tm.text = "Things will never be the same.";
                            tm.font = new Font("Arial");
                            break;
                        case "Priest":
                            tm.text = "She lives in a better world now.";
                            tm.font = new Font("Arial");
                            break;
                        case "Family1":
                            tm.text = "";
                            tm.font = new Font("Arial");
                            break;
                        case "Family2":
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
        SceneManager.LoadScene(stage);
    }
}
