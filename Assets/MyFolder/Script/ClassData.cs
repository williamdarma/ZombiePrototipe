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
    public float bulletDamage;
    public float bulletDelay;
    public float bulletReloadTime;
}

[System.Serializable]
public class NormalZombieClass
{
    public float normalZombieHP;
    public float normalZombieAttackRange;
    public float normalZombieSpeed;
}
[System.Serializable]
public class ClownZombieClass
{
    public float clownZombieHP;
    public float clownZombieAttackRange;
    public float clownZombieSpeed;
}


