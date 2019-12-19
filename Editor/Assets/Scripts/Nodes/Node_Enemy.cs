using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Node_Enemy : Node_Generic
{
    public enum ENEMYTYPE { SKELETON, ZOMBIE }

    public int health;
    public ENEMYTYPE enemyType;

    public override bool Refresh()
    {
        if (health <= 0)
        {
            ErrorLogger.ThrowErrorMessage("An enemy cannot have a health value of 0.");
            return false;
        }
        else return true;
    }

    public override object Evaluate()
    {
        if (Refresh()) return this;
        else return null;
    }

    public override string GetSaveData()
    {
        switch(enemyType)
        {
            case ENEMYTYPE.SKELETON:
                return "skel(" + health + ")";

            case ENEMYTYPE.ZOMBIE:
                return "zomb(" + health + ")";
        }
        return string.Empty;
    }
}