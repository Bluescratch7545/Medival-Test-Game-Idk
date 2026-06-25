using UnityEngine;

public class DashControl : MonoBehaviour
{
    public PlayerData def;
    public HeroController controller;

    private void Update()
    {
        if (Input.GetKeyDown(def.DASH_DATA.dashKey))
        {
            controller.Dash(def.FDIR);
        }
    }
}