using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("StartScene", 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartScene()
    {
        SceneManager.LoadScene("SampleScene");

    }
}
