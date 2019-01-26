using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDrawer : MonoBehaviour
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
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, posTarget, ratio);
        transform.localScale = Vector3.Lerp(transform.localScale, scaleTarget, ratio);
    }

    public void Toggle()
    {
        if (!open)
        {
            posTarget = transform.position + Vector3.left * openingAmount/2;
            float scaleX = (_collider.bounds.size.x + openingAmount) / _collider.bounds.size.x * _initialScale.x;
            Debug.Log(scaleX);
            scaleTarget = new Vector3(scaleX, _initialScale.y, _initialScale.z);
        }
        else
        {
            posTarget = transform.position - Vector3.left * openingAmount / 2;
            scaleTarget = _initialScale;
        }
        open = !open;

        float time = Random.Range(minTime, maxTime);
        Invoke("Toggle", time);
    }

}
