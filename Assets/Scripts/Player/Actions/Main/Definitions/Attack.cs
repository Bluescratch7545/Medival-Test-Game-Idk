using UnityEngine;

[System.Serializable]
public class Attack
{
    public bool canAttack;
    public int doDamage = 5;
    public KeyCode attackKey = KeyCode.X;
}