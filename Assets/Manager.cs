using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public float fadingRatio;
    public float startTime;

    public float buttonDelay;

    private bool started = false;
    private bool buttonStarted = false;
    private bool buttonEnabled = false;

    public Image logo;
    public Image button;
    public Button buttonAction;


    private float fadingLogo = 0;
    private float fadingButton = 0;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Fade", startTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (started)
        {
            if (!buttonStarted)
            {
                fadingLogo += fadingRatio * Time.deltaTime;
                if (fadingLogo > 1)
                {
                    fadingLogo = 1;
                    Invoke("FadeButton", buttonDelay);
                }
                logo.color = new Color(logo.color.r, logo.color.g, logo.color.b, fadingLogo);
            }
            else
            {
                fadingButton += fadingRatio * Time.deltaTime;
                if (fadingButton > 1)
                {
                    fadingButton = 1;
                    buttonEnabled = true;
                }
                button.color = new Color(button.color.r, button.color.g, button.color.b, fadingButton);
            }
        }
    }

    public void Fade()
    {
        started = true;
    }

    public void FadeButton()
    {
        buttonStarted = true;
    }

    public void ChangeScene()
    {
        if(buttonEnabled)
        {
            SceneManager.LoadScene("SampleScene");   
        }
    }
}
