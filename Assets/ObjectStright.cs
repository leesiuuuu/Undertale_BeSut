using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class ObjectStright : MonoBehaviour
{
    private float accumTimeAterUpdate;
    private float updateTime = 0.5f;
    private Vector3 myposition;
    // Use this for initialization
    void Start()
    {
        myposition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        accumTimeAterUpdate += Time.deltaTime;
        if (accumTimeAterUpdate >= updateTime)
        {
            accumTimeAterUpdate = 0;
            // Target위치에서 자신의 위치를 뺀다
            Vector3 vec3dir = new Vector3(0, 0, 0) - myposition;
            vec3dir.Normalize();
            transform.position = transform.position + vec3dir;
        }
    }
}
