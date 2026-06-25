using UnityEngine;

public class MoveControl : MonoBehaviour
{
    public PlayerData def;
    public HeroController controller;

    private void Update()
    {
        if (!def.MOVE_DATA.acceptingInput) return;

        if (Input.GetKeyDown(def.MOVE_DATA.leftKey))
        {
            controller.Move(-1, 0);
            def.FDIR = -1;
        }
        if (Input.GetKeyDown(def.MOVE_DATA.rightKey))
        {
            controller.Move(1, 0);
            def.FDIR = 1;
        }
        if (Input.GetKeyDown(def.MOVE_DATA.jumpKey))
        {
            if (controller.gameObject.GetComponent<Collider2D>().GetContacts(new Collider2D[1]) == 0) return;
            controller.Move(0, 1);
        }
    }
}