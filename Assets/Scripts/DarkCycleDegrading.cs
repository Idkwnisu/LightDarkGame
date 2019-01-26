using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DarkCycleDegrading : MonoBehaviour
{
    public static DarkCycleDegrading instance = null;

    private bool _safe = false;
    private bool _light = true;
    private bool _danger = false;

    private float sanity = 100;
    public float degradingRate = 2;

    public float lightTime;
    public float nightTime;
    public float dieTime;

    public PlayerController player;

    public GameObject[] firstStep;
    public GameObject[] secondStep;
    public GameObject[] thirdStep;
    public GameObject[] fourthStep;

    public Image image;



    private void Awake()
    {

        if (instance == null)
            instance = this;
        else if (instance != this)
             Destroy(gameObject);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_danger)
        {
            sanity -= degradingRate * Time.deltaTime;
            if (sanity < 80)
            {
                for (int i = 0; i < firstStep.Length; i++)
                {
                    firstStep[i].GetComponent<DayNightObj>().Night();
                }
            }
            if (sanity < 60)
            {
                for (int i = 0; i < secondStep.Length; i++)
                {
                    secondStep[i].GetComponent<DayNightObj>().Night();
                }
            }
            if (sanity < 40)
            {
                for (int i = 0; i < thirdStep.Length; i++)
                {
                    thirdStep[i].GetComponent<DayNightObj>().Night();
                }
            }
            if (sanity < 20)
            {
                for (int i = 0; i < fourthStep.Length; i++)
                {
                    fourthStep[i].GetComponent<DayNightObj>().Night();
                }
            }
            image.color = new Color(sanity / 100, sanity / 100, sanity / 100);
        }
    }

    public void StartCycle()
    {
        Invoke("Night", nightTime);
    }

    public void Light()
    {
        player.controlsEnabled = true;
        _light = true;
        if(_danger)
        {
            CancelInvoke();
        }
        _danger = false;
        for(int i = 0; i < firstStep.Length; i++)
        {
            firstStep[i].GetComponent<DayNightObj>().Day();
        }
        for (int i = 0; i < secondStep.Length; i++)
        {
            secondStep[i].GetComponent<DayNightObj>().Day();
        }
        for (int i = 0; i < thirdStep.Length; i++)
        {
            thirdStep[i].GetComponent<DayNightObj>().Day();
        }
        for (int i = 0; i < fourthStep.Length; i++)
        {
            fourthStep[i].GetComponent<DayNightObj>().Day();
        }
    }

    public void Night()
    {
        _light = false;
        if(sanity < 80)
        {
            for(int i = 0; i < firstStep.Length; i++)
            {
                firstStep[i].GetComponent<DayNightObj>().Night();
            }
        }
        if(sanity < 60)
        {
            for (int i = 0; i < secondStep.Length; i++)
            {
                secondStep[i].GetComponent<DayNightObj>().Night();
            }
        }
        if(sanity < 40)
        {
            for (int i = 0; i < thirdStep.Length; i++)
            {
                thirdStep[i].GetComponent<DayNightObj>().Night();
            }
        }
        if(sanity < 20)
        {
            for (int i = 0; i < fourthStep.Length; i++)
            {
                fourthStep[i].GetComponent<DayNightObj>().Night();
            }
        }
        if(!_safe)
        {
            _danger = true;
        }
        else
        {
            player.Stop();
            player.controlsEnabled = false;
        }
    }

    public void ToggleLight()
    {
        _light = !_light;
    }

    public void Safe()
    {
        _safe = true;

        if(_danger)
        {
            player.Stop();
            player.controlsEnabled = false;
            _danger = false;
        }
    }

    public void Unsafe()
    {
        _safe = false;
        if(!_light)
        {
            _danger = true;
        }
    }

    public bool isLight()
    {
        return _light;
    }

    public bool isSafe()
    {
        return _safe;
    }
}
