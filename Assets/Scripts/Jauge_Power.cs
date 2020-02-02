using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jauge_Power : MonoBehaviour
{

    Vector3 scale;

    // Start is called before the first frame update
    void Start()
    {
        scale = GetComponent<RectTransform>().localScale;
        StartCoroutine(PowerJaugeCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator PowerJaugeCoroutine()
    {
        scale = new Vector3(0,scale.y,scale.z);
        for(float t = 5; t < 10; t += Time.deltaTime)
        {
            yield return new WaitForEndOfFrame();
            scale = new Vector3(Mathf.InverseLerp(5, 10, t), scale.y, scale.z);
        }
        scale = new Vector3(1, scale.y, scale.z);
    }
}
