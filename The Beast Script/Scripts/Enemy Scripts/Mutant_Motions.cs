using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mutant_Motions : StateMachineBehaviour
{
    string[] Attack = { "Attack 1", "Attack 2", "Attack 3" };
    [HideInInspector] public Mutant_Controller MutantControls;
    [HideInInspector] public Transform MutantTransform, PlayerTransform;
    float CoveredDist = 0;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (MutantControls.PlayerSpotted && MutantControls.plController.CurrentHealth > 0)
        {
            MutantTransform.LookAt(PlayerTransform);

            if (MutantControls.PlayerPos.z < MutantControls.AttackRange)
            {
                //Stop and hit
                MutantControls.Motion(0);
                if (!animator.IsInTransition(0))
                {
                    animator.SetTrigger(Attack[Random.Range(0, 3)]);
                } 
            }
            else
            {
                //Chase
                MutantControls.Motion(2);
            }
        }
        else
        {
            /*Checks Whether Path is Assigned and Valid if Valid look and move towards the Path Node Assigned
             *if not assigned doesnt move stays there*/
            if (MutantControls.IsPathValid())
            {
                MutantTransform.LookAt(MutantControls.NextNodePosition());
                MutantControls.Motion(1);
            }
            else
            {
                Roaming(animator);
            }
        }
    }

    void Roaming(Animator animator)
    {
        animator.SetFloat("Forward", 1);
        float speed = MutantControls.Speed * Time.deltaTime;
        MutantTransform.Translate(0, 0, speed);
        CoveredDist += speed;
        if (CoveredDist > MutantControls.RoamingDist)
        {            
            CoveredDist = 0;
            MutantTransform.Rotate(0, 180f, 0);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
