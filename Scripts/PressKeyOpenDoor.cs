using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PressKeyOpenDoor : MonoBehaviour
{
    public GameObject Instruction;
    public GameObject AnimeObject;
    public GameObject ThisTrigger;
    public SanityManager sanitySlider; // SanitySlider scriptine erişim için referans
    public AudioSource DoorOpensSound;
    public GameObject canvas;
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


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Action == true)
            {   
                DoorOpensSound.Play();
                Instruction.SetActive(false);
                AnimeObject.GetComponent<Animator>().Play("chest-lid-open");
                canvas.SetActive(true);
                StartCoroutine(sanitySlider.LoseSanityFromAnother(25000));
                Action = false;
            }
        }

    }
}