using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : StateMachineBehaviour
{
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<EnemyAI>().Chase();
    }
}
