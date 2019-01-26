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

    //////temp///////
    public GameObject player;
    public Material lightMat;
    public Material nightMat;
    public Material dangerMat;
    public Image image;
    //////temp///////
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
            image.color = new Color(sanity / 100, sanity / 100, sanity / 100);
        }
    }

    public void StartCycle()
    {
        Invoke("Night", nightTime);
        player.GetComponent<SkinnedMeshRenderer>().material = lightMat;

    }

    public void Light()
    {
        _light = true;
        player.GetComponent<SkinnedMeshRenderer>().material = lightMat;
        if(_danger)
        {
            CancelInvoke();
        }
        Invoke("Night", lightTime);
        _danger = false;
    }

    public void Night()
    {
        _light = false;
        if (_safe)
        {
            player.GetComponent<SkinnedMeshRenderer>().material = nightMat;

            Invoke("Light", nightTime);
        }
        else
        {
            player.GetComponent<SkinnedMeshRenderer>().material = dangerMat;

            _danger = true;
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
            CancelInvoke();
            _danger = false;
            Invoke("Night", nightTime);
            player.GetComponent<SkinnedMeshRenderer>().material = nightMat;

        }
        if(!_light)
        {
            Invoke("Light", nightTime);
        }
    }

    public void Unsafe()
    {
        _safe = false;
        if(!_light)
        {
            _danger = true;
            player.GetComponent<SkinnedMeshRenderer>().material = dangerMat;

        }
    }


}
