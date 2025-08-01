using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class towerAnimControl : MonoBehaviour
{
    public Action towerBuild;
    public Action towerAttack;
    public Action towerAttackEnd;
    public void buildEnd()
    {
        towerBuild?.Invoke();
    }
    public void towerAttackKey()
    {
        towerAttack?.Invoke();
    }
    public void attackEnd()
    {
        towerAttackEnd?.Invoke();
    }
}
