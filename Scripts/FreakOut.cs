using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class FreakOut : MonoBehaviour
{
    public SanityManager sanitySlider;
    public GameObject Objective_Fridge;

    public void TaskOnClick()
    {
        StartCoroutine(sanitySlider.LoseSanityFromAnother(25000));
        GameObject.FindGameObjectWithTag("Player").transform.GetComponent<FirstPersonController>().enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Objective_Fridge.SetActive(false);
    }
}