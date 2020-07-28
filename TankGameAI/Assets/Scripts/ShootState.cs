using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootState : StateMachineBehaviour
{
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<EnemyAI>().SetLookRotation();
        animator.GetComponent<EnemyAI>().Shoot();
    }
}
