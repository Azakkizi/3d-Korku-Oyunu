using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;  // karakterin hareketini durdurmak icin karakterin scriptini cagirdik.


public class AttackState : StateMachineBehaviour
{   //Dusman karakterin oyuncuya suratini donmesi icin playeri init ettik.
    Transform player;
    //animasyonu bekleme suremiz
    public float deathAnimationDuration = 3f;
    //GameOver scriptinin calismasi ve canvasin active olmasi icin kontrol fieldi.
    public bool isDead;
    //animasyonu beklemek icin
    private float timer;
    //Oldugumuzda bagirma efekti kontrol boolu
    public bool isScream;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        isDead = false;
        isScream = false;
        timer = 0f;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   
        //Saldiri animasyonu gerceklestirken oyuncunun suratina donuk oldugundan emin olalim.
        animator.transform.LookAt(player);
        isScream = true;
        //Saldiri alinca karakterin hareket etmesini onleyelim
        player.GetComponent<FirstPersonController>().enabled =false; 
        //3 saniye attack animasyonunun izlenmesini istedigimiz, daha sonrasinda oldugumuzu ilan etmemiz icin if'e sarilmis kod parcasi
        timer += Time.deltaTime;
        if (!isDead && timer >= deathAnimationDuration)
        {   
            isDead = true;
        }
    }
}
