using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion_Motions : StateMachineBehaviour
{
    string[] Attacks = { "Attack 1", "Attack 2" };
    [HideInInspector] public Transform PLTransform, MiniTransform;
    [HideInInspector] public Minion_Controller minionController;
    float FireShot = 1;
    double NextShot;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(minionController.PlayerSpotted && minionController.plController.CurrentHealth > 0)
        {
            //When in Range look at Player and Move toward Player
            MiniTransform.LookAt(PLTransform);

            if(minionController.PlayerPos.z < minionController.ShootDist && NextShot < Time.time)
            {
                //When in The above Range Shoot Projectile form Enemy  and after shootinh wait for next shot
                //The enemy after a  shot will move towards the player
                minionController.Motion(0);
                animator.SetTrigger("FireballShot");
                NextShot = Time.time + FireShot;  
            }
            if (minionController.PlayerPos.z < minionController.CAttackR)
            {
                //if Player is too close the enemy enemy fights with his hands
                minionController.Motion(0);
                if (!animator.IsInTransition(0))
                {
                    animator.SetTrigger(Attacks[Random.Range(0, 2)]);
                }
            }
            else
            {
                minionController.Motion(2);
            }
        }
        else
        {
            /*Checks Whether Path is Assigned and Valid if Valid look and move towards the Path Node Assigned
             if not assigned doesnt move stays there if Player Not in Range*/
            if (minionController.IsPathValid())
            {
                MiniTransform.LookAt(minionController.NextNodePosition());
                minionController.Motion(1);
            }
            else
            {
                minionController.Motion(0);
            }
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
