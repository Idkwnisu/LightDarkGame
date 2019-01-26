using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarpetUpDown : MonoBehaviour
{
    public float openingAmount;
    public bool open = false;

    private BoxCollider _collider;
    private Vector3 _initialScale;

    public float minTime;
    public float maxTime;


    private Vector3 posTarget;
    private Vector3 scaleTarget;

    [Range(0.01f,1.0f)]
    public float ratio;
    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<BoxCollider>();
        _initialScale = transform.localScale;
        Toggle();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       Vector3 newPosition = Vector3.Lerp(transform.position, posTarget, ratio);
        GetComponent<Rigidbody>().MovePosition(newPosition);
    }

    public void Toggle()
    {
        if (!open)
        {
            posTarget = transform.position + Vector3.up * openingAmount/2;
        }
        else
        {
            posTarget = transform.position - Vector3.up * openingAmount / 2;
            scaleTarget = _initialScale;
        }
        open = !open;

        float time = Random.Range(minTime, maxTime);
        Invoke("Toggle", time);
    }

}
