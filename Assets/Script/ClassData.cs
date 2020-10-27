using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassData 
{
   

}

[System.Serializable]
public class PlayerClass
{
    public float PlayerLives;
    public float PlayerSpeed;
}
[System.Serializable]
public class PlayerWeaponClass
{
    public int bulletClip;
    public int bulletDamage;
    public float bulletDelay;
    public float bulletReloadTime;
}

[System.Serializable]
public class NormalZombieClass
{
    public int normalZombieHP;
    public int normalZombieAttackRange;
    public float normalZombieSpeed;
}
[System.Serializable]
public class ClownZombieClass
{
    public int clownZombieHP;
    public int clownZombieAttackRange;
    public float clownZombieSpeed;
}


