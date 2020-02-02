using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScene : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        StartCoroutine(Death());
    }
    public IEnumerator Death()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(GameManager.stage);
    }
}
