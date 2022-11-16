/***
 * Author: Betzaida
 * Created: 11-14-2022
 * Modified:
 * Description: controller for navmeshagent
 ***/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]

public class AnimationController : MonoBehaviour
{
    private NavMeshAgent thisNavMeshAgent;
    private Animator thisAnimator;

    public float runVelocity = 0.1f;
    public string animationRunParam;
    public string animationSpeedParam;

    private float maxSpeed;

    private void Awake()
    {
        thisNavMeshAgent = GetComponent<NavMeshAgent>();
        thisAnimator = GetComponent<Animator>();
        maxSpeed = thisNavMeshAgent.speed;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //thisAnimator.SetBool(animationRunParam, thisNavMeshAgent.velocity.magnitude > runVelocity);
        thisAnimator.SetFloat(animationSpeedParam, thisNavMeshAgent.velocity.magnitude/maxSpeed);
    }
}
