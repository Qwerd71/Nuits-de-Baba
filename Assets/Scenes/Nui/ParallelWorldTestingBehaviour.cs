using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallelWorldTestingBehaviour : MonoBehaviour
{
    public void ChangeWorld()
    {
        Shader.SetGlobalFloat("IsWorldA", (Shader.GetGlobalFloat("IsWorldA") + 1) % 2);
    }
}