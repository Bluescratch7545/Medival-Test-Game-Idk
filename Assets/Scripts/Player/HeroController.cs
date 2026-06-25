using UnityEditor.Experimental.RestService;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    public static HeroController instance;

    [Header("PLAY_DATA_INSTANCE")]
    public PlayerData PLAY_DATA;

    public void Dash(int dirFacing)
    {
        var rb = gameObject.GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.velocity = new Vector2((float)PLAY_DATA.DASH_DATA.dashForce * dirFacing, 0f);
    }

    public void Move(int xDir, int yDir)
    {
        var rb = gameObject.GetComponent<Rigidbody2D>();
        if (xDir != 0)
        {
            rb.velocity = new Vector2(5 * xDir, rb.velocity.y);
        }
        if (yDir != 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, PLAY_DATA.MOVE_DATA.jumpHeight * yDir);
        }
    }
}