using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightCycle : MonoBehaviour
{
    public static NightCycle instance = null;

    public float speedDarkness;
    public float speedLight;
    private float darkness = 0;

    public GameObject[] lights;
    private float[] intensities;

    private bool Started = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        intensities = new float[lights.Length];
        for (int i = 0; i < lights.Length; i++)
        {
            intensities[i] = lights[i].GetComponent<Light>().intensity;
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Started)
        {
            if (DarkCycleDegrading.instance.isLight())
            {
                darkness += speedDarkness * Time.deltaTime;
                UpdateLights();
                if (darkness >= 100)
                {
                    darkness = 100;
                    DarkCycleDegrading.instance.Night();
                }
            }
            if (!DarkCycleDegrading.instance.isLight() && DarkCycleDegrading.instance.isSafe())
            {
                darkness -= speedLight * Time.deltaTime;
                UpdateLights();
                if (darkness <= 0)
                {
                    darkness = 0;
                    DarkCycleDegrading.instance.Light();
                }
            }
            else if(DarkCycleDegrading.instance.isSafe())
            {
                darkness = 0;
                UpdateLights();
            }
        }
    }

    public void UpdateLights()
    {
        for(int i = 0; i < lights.Length; i++)
        {
            lights[i].GetComponent<Light>().intensity = intensities[i] * 95 / 100 * (100-darkness) / 100 + intensities[i] * 5 / 100;
        }
    }

    public void StartCycle()
    {
        Started = true;
    }
}
