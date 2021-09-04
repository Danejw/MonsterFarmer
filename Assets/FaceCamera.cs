using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    public Transform trans;
    private Transform body;

    private void Start()
    {
        body = GetComponent<Transform>();
    }

    void Update()
    {
        if (trans)
            body.LookAt(trans);
    }
}
