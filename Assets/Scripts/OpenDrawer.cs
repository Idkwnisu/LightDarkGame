using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDrawer : MonoBehaviour
{
    public float openingAmount;
    public bool open = false;

    private BoxCollider _collider;
    private Vector3 _initialScale;

    public float minTimeOpen;
    public float maxTimeOpen;

    public float minTimeClosed;
    public float maxTimeClosed;

    private Vector3 posTarget;
    private Vector3 scaleTarget;
    private Vector3 safeSpot;

    private bool arrived = true;

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
        Vector3 Position = Vector3.Lerp(transform.position, posTarget, ratio);
        GetComponent<Rigidbody>().MovePosition(Position);
        if ((transform.position - posTarget).magnitude < 0.1f)
        {
            arrived = true;
        }
        else
        {
            arrived = false;
        }
        transform.localScale = Vector3.Lerp(transform.localScale, scaleTarget, ratio);
    }

    public void Toggle()
    {
        float time;
        if (!open)
        {
            posTarget = transform.position + Vector3.left * openingAmount/2;
            safeSpot = posTarget;
            float scaleX = (_collider.bounds.size.x + openingAmount) / _collider.bounds.size.x * _initialScale.x;
            scaleTarget = new Vector3(scaleX, _initialScale.y, _initialScale.z);
            time = Random.Range(minTimeOpen, maxTimeOpen);
        }
        else
        {
            posTarget = transform.position - Vector3.left * openingAmount / 2;
            scaleTarget = _initialScale;
            time = Random.Range(minTimeClosed, maxTimeClosed);
        }
        open = !open;

        
        Invoke("Toggle", time);
    }
}
