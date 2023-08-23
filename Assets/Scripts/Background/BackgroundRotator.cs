using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundRotator : MonoBehaviour
{
    public float rotationSpeed = 30f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
    }
}
