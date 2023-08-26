using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : StateMachineBehaviour
{   float timer;
    //Patrolling time
    public float huntDuration;
    //Agent karakterin dolasacagi harita noktalari listesi.
    List<Transform> wayPoints = new List<Transform>();
    //AGent karakterimiz
    UnityEngine.AI.NavMeshAgent agent;
    //Player karakterimiz
    Transform player;
    //ChaseState'e girmek icin gerekli olan maksimum disance.
    public float chaseRange;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   
        //Player init
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //Agent init
        agent = animator.GetComponent<UnityEngine.AI.NavMeshAgent>();
        //Patrol hizimiz
        agent.speed = 1.5f;
        //hunduration init
        huntDuration = 40;
        //chaserange init
        chaseRange = 5;
        //timer init
        timer = 0;
        //Random bir sekilde bu waypointlerde roam atmasini sagladigimiz kod parcasi
        GameObject go = GameObject.FindGameObjectWithTag("WayPoints");
        foreach (Transform t in go.transform)
        {
            wayPoints.Add(t);
            agent.SetDestination(wayPoints[Random.Range(0,wayPoints.Count)].position);
        }
    }

     override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   
        if(agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.SetDestination(wayPoints[Random.Range(0,wayPoints.Count)].position);
        }
        //timer setup
        timer += Time.deltaTime;
        //Timer huntDuration'a kadar akar ve daha sonrasinda PatrolState'den IdleState'e doner.
        if (timer > huntDuration)
            animator.SetBool("isPatrolling",false);
        //player ve agent arasindaki distance i alalim.
        float distance = Vector3.Distance(player.position, animator.transform.position);
        //bu distance chaseRange'den kucukce ChaseState'e gecer.
        if (distance < chaseRange)
            animator.SetBool("isChasing", true);
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   //State'den cikarken konum bilgisnini kaybetmesin diye 
        agent.SetDestination(agent.transform.position);
    }
}
