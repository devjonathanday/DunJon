﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Container : Node_Generic
{
    [SerializeField] NodeInput[] roomObjectInputs;
    public NodeInput previousRoomInput;
    public NodeOutput nextRoomOutput;

    public override bool Refresh()
    {
        if(nextRoomOutput != null && nextRoomOutput.lineReferences.Count > 1)
        {
            ErrorLogger.ThrowErrorMessage("A room cannot have multiple next rooms.");
            return false;
        }
        return true;
    }
    public override object Evaluate()
    {
        if (Refresh())
        {
            if (inputs[0].lineReference == null || inputs[1].lineReference == null)
            {
                ErrorLogger.ThrowErrorMessage("A room node has missing size parameters.");
                return null;
            }
            else if ((int)inputs[0].lineReference.start.attachedNode.Evaluate() == 0 ||
                     (int)inputs[1].lineReference.start.attachedNode.Evaluate() == 0)
            {
                ErrorLogger.ThrowErrorMessage("A room cannot have a size parameter of 0.");
                return null;
            }

            string returnString = "room(";
            returnString += inputs[0].lineReference.start.attachedNode.Evaluate().ToString() + ",";
            returnString += inputs[1].lineReference.start.attachedNode.Evaluate().ToString() + ")\n";

            for (int i = 0; i < roomObjectInputs.Length; i++)
            {
                if (roomObjectInputs[i].lineReference != null)
                {
                    Node_Enemy enemy = (Node_Enemy)roomObjectInputs[i].lineReference.start.attachedNode.Evaluate();
                    if (enemy != null)
                    {
                        switch (enemy.enemyType)
                        {
                            case Node_Enemy.ENEMYTYPE.SKELETON:
                                returnString += "skel(" + enemy.health + ")\n";
                                break;
                            case Node_Enemy.ENEMYTYPE.ZOMBIE:
                                returnString += "zomb(" + enemy.health + ")\n";
                                break;
                        }
                    }
                }
            }

            returnString += "endroom\n";
            return returnString;
        }
        else return null;
    }
}