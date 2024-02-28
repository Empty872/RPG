using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    [FormerlySerializedAs("playerPosition")]
    public Transform playerTransform;

    private Vector3 offset = new(3, 5f, -3f);
    private Vector3 rotation = new(45, -45, 0);

    void Start()
    {
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = playerTransform.position + offset;
        transform.rotation = Quaternion.Euler(rotation);
    }
}