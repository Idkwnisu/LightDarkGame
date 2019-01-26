using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightObj : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Day()
    {
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(0).gameObject.SetActive(true);
    }
    public void Night()
    {
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
