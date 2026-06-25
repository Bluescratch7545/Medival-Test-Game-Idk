using UnityEngine;

[System.Serializable]
public class PlayerData : MonoBehaviour
{
    public static PlayerData instance;

    public Move MOVE_DATA;
    public Attack ATTACK_DATA;
    public Dash DASH_DATA;

    public int FDIR = 1;
    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}