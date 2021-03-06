﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int stage = 4;
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
    public bool End_fill = true;
    public GameObject player;
    public GameObject fireball;
    public GameObject PowBar;
    private Quaternion zero = new Quaternion(0f, 0f, 0f, 0f);
    public GameObject Text;
    TextMeshPro tm;
    public Vector2 lastCheckpoint;
    static GameManager _instancegm;
    public Material mat1;
    public Material mat2;
    public float textTimer;
    bool textIsOn = false;
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
    private void Awake()
    {
        lastCheckpoint = PlayerManager.Instance.transform.position;
    }
    void Start()
    {
        
        Allgos = GameObject.FindGameObjectsWithTag("Enemy");
        stage = SceneManager.GetActiveScene().buildIndex;
        if (stage == 5)
            tm = Text.GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        if (End_fill)
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
                                StartCoroutine(PowBar.GetComponent<Jauge_Power>().PowerJaugeCoroutine());
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
                        var Direction = flamehit - Fireball.transform.position;
                        Direction = Direction - new Vector3(0, 0, Direction.z);
                        Direction = Direction.normalized;
                        Fireball.transform.position += Direction * 5.2f;

                        Fireball.GetComponent<Rigidbody2D>().AddForce(Direction * 1000);
                        Destroy(Fireball, 2f);
                        StartCoroutine(PowBar.GetComponent<Jauge_Power>().PowerJaugeCoroutine());
                    }
                    break;
                case 3:
                    if (Input.GetMouseButtonDown(0))
                    {
                        Shader.SetGlobalFloat("IsWorldA", (Shader.GetGlobalFloat("IsWorldA") + 1) % 2);
                        foreach (GameObject go in Allgos)
                        {
                            go.GetComponent<BoxCollider2D>().isTrigger = !go.GetComponent<BoxCollider2D>().isTrigger;
                        }
                    StartCoroutine(PowBar.GetComponent<Jauge_Power>().PowerJaugeCoroutine());
                    }
                    break;
                case 4:
                    if (Input.GetMouseButtonDown(0) && !isShielded)
                    {
                        actualShield = Instantiate(shield, player.transform.position, zero);
                        PlayerManager.Instance.currentinvincibilityTime = Time.time + shieldtime;
                        PlayerManager.Instance.isInvincible = true;
                        isShielded = true;
                        StartCoroutine(PowBar.GetComponent<Jauge_Power>().PowerJaugeCoroutine());
                    }
                    break;
                case 5:
                    if (Input.GetMouseButtonDown(0) && Time.time > textTimer)
                    {
                        Text.SetActive(true);
                        textTimer += Time.time;
                        denyhit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                        if (denyhit.collider != null)
                            switch (denyhit.collider.name)
                            {
                                case "Docteur":
                                    tm.text = "I'm sorry. We made all we could but we couldn't save her. My apologies.";
                                    break;
                                case "Bystander1":
                                    tm.text = "Did you hear about that story ? Poor girl !";
                                    break;
                                case "Bystander2":
                                    tm.text = "This kind of driver should be jailed until they die !";
                                    break;
                                case "Parent1":
                                    tm.text = "Why did it happen to us ? She was so young.";
                                    break;
                                case "Parent2":
                                    tm.text = "You will always be welcome at home.";
                                    break;
                                case "Friend1":
                                    tm.text = "I can't believe it.";
                                    break;
                                case "Friend2":
                                    tm.text = "Things will never be the same.";
                                    break;
                                case "Priest":
                                    tm.text = "She lives in a better world now.";
                                    break;
                                case "Family1":
                                    tm.text = "...";
                                    break;
                                case "Family2":
                                    tm.text = "";
                                    break;
                                default:
                                    break;
                            }

                    }
                    break;
                default: break;
            }
        transform.position = GameManager.Instance.player.transform.position;
        if (isShielded && Time.time >= PlayerManager.Instance.currentinvincibilityTime)
        {
            Destroy(actualShield);
            isShielded = false;
        }
        if(Time.time > textTimer && stage == 5)
        {
            textIsOn = false;
            Text.SetActive(false);
        }

    }
    public void Reset()
    {
        SceneManager.LoadScene(7);
        //SceneManager.LoadScene(stage);
        Shader.SetGlobalFloat("IsWorldA", 1);
    }
}
