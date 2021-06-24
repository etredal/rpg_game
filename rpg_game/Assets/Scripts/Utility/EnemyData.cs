using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class EnemyData
{
    public string EnemyType;
    public string EnemyName;
    public float HP;
    public AttackData[] Attacks;
}
