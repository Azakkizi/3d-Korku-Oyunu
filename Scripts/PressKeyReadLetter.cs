using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class PressKeyReadLetter : MonoBehaviour
{
    public GameObject Instruction;
    public GameObject LetterText;
    public GameObject ThisTrigger;
    //public GameObject LetterObject;
    public AudioSource PaperSound;
    public bool Action = false;

    void Start()
    {
        Instruction.SetActive(false);
        LetterText.SetActive(false);
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
                PaperSound.Play();
                LetterText.SetActive(true);
                //LetterObject.SetActive(false);
                Instruction.SetActive(false);
                Action = false;
                GameObject.FindGameObjectWithTag("Player").transform.GetComponent<FirstPersonController>().enabled = false;
            }
            else
            {
                LetterText.SetActive(false);
                //ThisTrigger.SetActive(false);
                GameObject.FindGameObjectWithTag("Player").transform.GetComponent<FirstPersonController>().enabled = true;
            }
        }
    }
}
