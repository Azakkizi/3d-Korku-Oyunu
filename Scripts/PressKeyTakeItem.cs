using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressKeyTakeItem : MonoBehaviour
{
    public GameObject Instruction;
    public GameObject ThisTrigger;
    public GameObject Model;
    public Item Item;
    public AudioSource GrabSound;
    public bool Action = false;

    void Start()
    {
        Instruction.SetActive(false);
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "Player")
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

    void PickUp()
    {
        InventoryManager.Instance.Add(Item);
        Model.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Action == true)
            {
                GrabSound.Play();
                PickUp();
                Instruction.SetActive(false);
                Action = false;
            }
        }
    }
}
