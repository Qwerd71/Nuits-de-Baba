﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject Player; // importation du player qu'il faut tracker
    Vector3 offset; // vecteur permettant de garder un angle de vue de la caméra constant
    void Start()
    {
        offset = transform.position - Player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player != null)
        {
            var Target = Player.transform.position + offset;
            transform.position = Target;

        }
    }
}
