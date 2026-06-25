using UnityEngine;

[System.Serializable]
public class Move
{
    public bool acceptingInput = true;
    public KeyCode leftKey = KeyCode.LeftArrow;
    public KeyCode rightKey = KeyCode.RightArrow;
    public KeyCode jumpKey = KeyCode.W;
    public int jumpHeight = 6;
}