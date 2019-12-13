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

    public override void Refresh()
    {
        outputs[0].value = this;
    }
}