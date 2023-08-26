using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class SkillManager : MonoBehaviour
{
    public GameObject skillTree;
    public bool skillTreeIsClosed;

    void Start()
    {
        skillTreeIsClosed = true;
        skillTree.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(skillTreeIsClosed == true)
            {
                skillTree.SetActive(true);
                skillTreeIsClosed= false;
                GameObject.FindGameObjectWithTag("Player").transform.GetComponent<FirstPersonController>().enabled = false;
            }
            else
            {
                skillTree.SetActive(false);
                skillTreeIsClosed= true;
                GameObject.FindGameObjectWithTag("Player").transform.GetComponent<FirstPersonController>().enabled = true;
            }
        }
    }
}