using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkCycleManager : MonoBehaviour
{
    public static DarkCycleManager instance = null;

    private bool _safe = false;
    private bool _light = true;
    private bool _danger = false;
    public float lightTime;
    public float nightTime;
    public float dieTime;

    //////temp///////
    public GameObject player;
    public Material lightMat;
    public Material nightMat;
    public Material dangerMat;
    public Material deadMat;
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

    }

    public void StartCycle()
    {
        Invoke("Night", nightTime);
        player.GetComponent<MeshRenderer>().material = lightMat;

    }

    public void Light()
    {
        _light = true;
        player.GetComponent<MeshRenderer>().material = lightMat;
        Invoke("Night", lightTime);
        _danger = false;
    }

    public void Die()
    {
        if(!_safe)
        {
            Debug.Log("Game over here");
            player.GetComponent<MeshRenderer>().material = deadMat;

        }
    }

    public void Night()
    {
        _light = false;
        if (_safe)
        {
            player.GetComponent<MeshRenderer>().material = nightMat;

            Invoke("Light", nightTime);
        }
        else
        {
            player.GetComponent<MeshRenderer>().material = dangerMat;

            Invoke("Die", dieTime);
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
            Invoke("Night", nightTime);
            player.GetComponent<MeshRenderer>().material = nightMat;

        }
    }

    public void Unsafe()
    {
        _safe = false;
        if(!_light)
        {
            Invoke("Die", dieTime);
            _danger = true;
            player.GetComponent<MeshRenderer>().material = dangerMat;

        }
    }


}
