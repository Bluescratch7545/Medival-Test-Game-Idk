using UnityEngine;

[System.Serializable]
public class Dash
{
    public bool canDash;
    public bool hasDash;
    public double dashCooldown = 0.75;
    public double dashForce = 20;
}