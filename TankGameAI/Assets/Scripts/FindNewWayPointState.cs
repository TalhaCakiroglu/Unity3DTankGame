using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindNewWayPointState : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<EnemyAI>().SetNewWayPoint();
    }
}
