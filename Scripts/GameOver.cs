using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{   //Gameover canvasimizi cagirdik
    public GameObject canvas;
    //isDead field'imiz animatorun icindeki AttackState StateMachineBehaviour Scriptinde oldugu icin, animatorumuzu cagirmamiz lazim.
    public Animator animator;
    //Olum cigligi
    public AudioSource scream;
    
    public void Awake()
    {   //script Awake oldugunda animatoru al
        animator = GetComponent<Animator>();     
    }
    public void Update(){
        // Eger olduysek gameover canvas objesini enable et, sadece olunce gozukmesi icin ife sardik.
        if (animator.GetBehaviour<AttackState>().isDead == true){
            canvas.SetActive(true);
            Cursor.visible =true;
            Cursor.lockState = CursorLockMode.None;
            }
        if (!scream.isPlaying && animator.GetBehaviour<AttackState>().isScream == true){
            scream.Play();           
            }
        
    }


}
