using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    private Vector3 _difference;
    // Start is called before the first frame update
    void Start()
    {
        _difference = transform.position - player.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + _difference;
    }
}
