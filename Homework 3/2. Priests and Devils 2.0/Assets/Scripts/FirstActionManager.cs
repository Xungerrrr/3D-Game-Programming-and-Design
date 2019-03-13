using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstActionManager : SSActionManager, ISSActionCallback {
    // 移动船
    public void MoveBoat(BoatController boatController)
    {
        SSMoveToAction action = SSMoveToAction.GetSSMoveToAction(boatController.getDestination(), 20);
        RunAction(boatController.boat, action, this);
    }
    // 移动角色
    public void MoveCharacter(MyCharacterController myCharacterController, Vector3 destination)
    {
        Vector3 current = myCharacterController.character.transform.position;
        Vector3 middle = destination; // 利用 middle 实现折线运动
        if (destination.y < current.y)
        {
            middle.y = current.y;
        }
        else
        {
            middle.x = current.x;
        }
        SSAction firstMove = SSMoveToAction.GetSSMoveToAction(middle, 20);
        SSAction secondMove = SSMoveToAction.GetSSMoveToAction(destination, 20);
        SSAction sequenceAction = SSSequenceAction.GetSSSequenceAction(1, 0, new List<SSAction> { firstMove, secondMove });
        RunAction(myCharacterController.character, sequenceAction, this);
    }
    public void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Completed, int intParam = 0, string strParam = null, object objectParam = null)
    {

    }
}
