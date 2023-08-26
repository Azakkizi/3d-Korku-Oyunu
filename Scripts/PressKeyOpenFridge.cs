using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class PressKeyOpenFridge : MonoBehaviour
{
    public GameObject Instruction;
    public GameObject AnimeObject;
    public GameObject ThisTrigger;
    public GameObject Objective_Fridge;
    public GameObject Crucifix;
    public GameObject HolyWater;
   // public AudioSource DoorOpenSound;
    public bool Action = false;

    void Start()
    {
        Instruction.SetActive(false);
        Objective_Fridge.SetActive(false);
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "Player" && InventoryManager.Instance.Items.Exists(item => item.id == 3))
        {
            Instruction.SetActive(true);
            Action = true;
        }
    }

    void OnTriggerExit(Collider collision)
    {
        Instruction.SetActive(false);
        Action = false;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Action == true)
            {
                Instruction.SetActive(false);
                AnimeObject.GetComponent<Animator>().Play("fridge-open");
                ThisTrigger.SetActive(false);
               // DoorOpenSound.Play();
                Action = false;
                Objective_Fridge.SetActive(true);
                if(!InventoryManager.Instance.Items.Exists(item => item.id == 2)) {
                    Crucifix.SetActive(false);
                }
                if(!InventoryManager.Instance.Items.Exists(item => item.id == 3)) {
                    HolyWater.SetActive(false);
                }
                GameObject.FindGameObjectWithTag("Player").transform.GetComponent<FirstPersonController>().enabled = false;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
}
