using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jauge_Power : MonoBehaviour
{

    private RectTransform scale;
    public GameObject GameManage;

    // Start is called before the first frame update
    void Awake()
    {
        scale = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator PowerJaugeCoroutine()
    {
        GameManage.GetComponent<GameManager>().End_fill = false;
        scale.localScale = new Vector3(0,scale.localScale.y,scale.localScale.z);
        for (float t = 5; t < 7; t += Time.deltaTime)
        {
            yield return new WaitForEndOfFrame();
            scale.localScale = new Vector3(Mathf.InverseLerp(5, 7, t), scale.localScale.y, scale.localScale.z);
        }
        scale.localScale = new Vector3(1, scale.localScale.y, scale.localScale.z);
        GameManage.GetComponent<GameManager>().End_fill = true;
    }
}
