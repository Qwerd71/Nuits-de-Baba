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
    RaycastHit2D denyhit;
    Vector3 flamehit;
    public float shieldtime;
    public float currentshieldtime = 3;
    public GameObject shield;
    private GameObject actualShield;
    public bool isShielded = false;
    public GameObject player;
    public GameObject fireball;
    private GameObject PowBar;
    private Quaternion zero = new Quaternion(0f, 0f, 0f, 0f);
    TextMesh tm;
    public Vector2 lastCheckpoint = new Vector3(-8,0,0);
    static GameManager _instancegm;
    public Material mat1;
    public Material mat2;
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
        PowBar = Camera.main.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        switch (stage)
        {
            case 1:

                if (Input.GetMouseButtonDown(0) && inthevoid != null)
                {
                    inthevoid.SetActive(true);
                    inthevoid = null;
                }
                else if (Input.GetMouseButtonDown(0))
                {// ce bloc pour le pouvoir du stage 1
                    denyhit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                    if (denyhit.collider != null)
                        if (inthevoid == null && (denyhit.collider.gameObject.tag.Equals("Wall")) || (denyhit.collider.gameObject.tag.Equals("Enemy")))
                        {
                            inthevoid = denyhit.collider.gameObject;
                            denyhit.collider.gameObject.SetActive(false);
                            PowBar.GetComponent<Animator>().Play("Pouv_empty");
                        }
                }
                break;

            case 2:
                if (Input.GetMouseButtonDown(0)) // ce bloc pour le pouvoir du stage 2
                {
                    flamehit = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                        GameObject Fireball = Instantiate(fireball, player.transform.position, Quaternion.identity);

                    Fireball.transform.LookAt(new Vector2(flamehit.x, flamehit.y));
                    if (flamehit.x - player.transform.position.x < 0)
                        Fireball.GetComponent<SpriteRenderer>().flipX = true;

                    Fireball.transform.rotation = new Quaternion(0, 0, Fireball.transform.rotation.z, Fireball.transform.rotation.w);
                    var Direction = (flamehit - Fireball.transform.position);
                    Direction = Direction - new Vector3(0, 0, Direction.z);
                    Direction = Direction.normalized;
                    Fireball.transform.position += Direction * 3.3f;

                    Fireball.GetComponent<Rigidbody2D>().AddForce( Direction*1000);
                    Destroy(Fireball, 2f);
      
                        PowBar.gameObject.GetComponent<Animator>().Play("Pouv_full");
                }
                //gameObject.SetActive(false);
                break;
            case 3:
                if (Input.GetMouseButtonDown(0))
                {
                    foreach (GameObject go in Allgos)
                    {
                        go.GetComponent<Collider2D>().isTrigger = !go.GetComponent<Collider2D>().isTrigger; //à changer pour boxcollider2D
                        if (go.GetComponent<Collider2D>().isTrigger)
                            go.GetComponent<SpriteRenderer>().material = mat2;
                        else
                            go.GetComponent<SpriteRenderer>().material = mat1;
                    }
                }
                break;
            case 4:
                if (Input.GetMouseButtonDown(0) && !isShielded) 
                {
                    actualShield = Instantiate(shield,player.transform.position,zero,player.transform) ; // (?)
                    PlayerManager.Instance.currentinvincibilityTime = Time.time + shieldtime ;
                    PlayerManager.Instance.isInvincible = true;
                    isShielded = true;
                }
                break;
            default:
                if (Input.GetMouseButtonDown(0))
                {
                    denyhit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                    if (denyhit.collider != null)
                    switch (denyhit.collider.name)
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
                            tm.text = "...";
                            tm.font = new Font("Arial");
                            break;
                        case "Family2":
                            tm.text = "";
                            tm.font = new Font("Arial");
                            break;
                        default:
                            Debug.Log("Rien");
                            break;
                    }
                }
                break;
        }
        transform.position = GameManager.Instance.player.transform.position;
        if (isShielded && Time.time >= PlayerManager.Instance.currentinvincibilityTime)
        {
            Destroy(actualShield);
            isShielded = false;
        }
    }
    public void Reset()
    {
        SceneManager.LoadScene(stage);
    }
}
