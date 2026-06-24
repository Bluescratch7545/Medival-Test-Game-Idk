using UnityEngine;

[System.Serializable]
public class PlayerData : MonoBehaviour
{
    public static PlayerData instance;

    public Attack ATTACK_DATA;
    public Dash DASH_DATA;

    void Awake()
    {
        instance = this;
    }
}