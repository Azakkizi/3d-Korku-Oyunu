using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FlashlightAdvanced : MonoBehaviour
{
    public Light light;
    public TMP_Text text;


    public float lifetime = 100;

    public float batteries = 1;

    public AudioSource flashON;
    public AudioSource flashOFF;

    private bool on;
    private bool off;


    void Start()
    {
        light = GetComponent<Light>();

        off = true;
        light.enabled = false;

    }



    void Update()
    {
        text.text = "Flashlight Battery: " +lifetime.ToString("0") + "%" + "\nPress R to reload.";

        if(Input.GetKeyDown(KeyCode.F) && off)
        {
            flashON.Play();
            light.enabled = true;
            on = true;
            off = false;
        }

        else if (Input.GetKeyDown(KeyCode.F) && on)
        {
            flashOFF.Play();
            light.enabled = false;
            on = false;
            off = true;
        }

        if (on)
        {
            lifetime -= 1 * Time.deltaTime;
        }

        if(lifetime <= 0)
        {
            light.enabled = false;
            on = false;
            off = true;
            lifetime = 0;
        }

        if (lifetime >= 100)
        {
            lifetime = 100;
        }

        if (Input.GetKeyDown(KeyCode.R) && batteries >= 1)
        {
            batteries -= 1;
            lifetime += 50;
        }

        if (Input.GetKeyDown(KeyCode.E) && batteries == 0)
        {
            return;
        }

        if(batteries <= 0)
        {
            batteries = 0;
        }



    }
}
