using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class HeroController : DataControl
{
    [NonSerialized]
    public HeroController instance;

    new public Rigidbody2D rigidbody;

    public bool canJump;

    public bool isGrounded;

    [NonSerialized]

    float speed = 0;
    float jumpForce = 0;

    public override void Awake()
    {
        base.Awake();

        instance = this;

        Debug.Log("A");

        speed = (float)this.GetData("MoveVarSpeed").GetValue();
        jumpForce = (float)this.GetData("JumpVarForce").GetValue();

        RegisterAllHandles();
    }

    public override void Update()
    {
        ControlHandleRegistry.ExecuteHandle("MoveHandle");
        ControlHandleRegistry.ExecuteHandle("AbilityHandle");
    }

    void FixedUpdate()
    {
    }
    KeyCode GetCurrentKeyDown()
    {
        foreach (KeyCode kc in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(kc))
            {
                Debug.Log(kc);
                return kc;
            }
        }
        return KeyCode.None;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Terrain")) return;

        foreach(ContactPoint2D contactPoint in collision.contacts)
        {
            if (Vector2.Dot(contactPoint.normal, Vector2.up) > 0.5f)
            {
                isGrounded = true;
                canJump = true;
                return;
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Terrain"))
            isGrounded = false;
    }

    void MoveHandle(float speed, float jumpSpeed)
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rigidbody.AddForce(new Vector2(-speed, 0));
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rigidbody.AddForce(new Vector2(speed, 0));
        }
        if (Input.GetKey(KeyCode.W) && canJump)
        {
            if (!isGrounded)
            {
                canJump = false;
                return;
            }
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpSpeed);
            StartCoroutine(DisableJump());
        }
    }

    void AbilityHandle(float jumpForce)
    {
        if (Input.GetKeyDown(KeyCode.S) && isGrounded)
        {
            StartCoroutine(UprightDive(jumpForce));
        }
    }

    IEnumerator DisableJump()
    {
        yield return new WaitForSeconds(0.2f);
        canJump = false;
    }

    IEnumerator UprightDive(float jumpForce)
    {
        var timer = 0f;
        while (timer < 1f)
        {
            timer += Time.deltaTime;
            yield return null; 
        }

        yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.S));
        rigidbody.AddForce(new Vector2(rigidbody.velocity.x, jumpForce * 4), ForceMode2D.Impulse);
    }

    void RegisterAllHandles()
    {
        new ControlHandle<float, float>("MoveHandle", speed, jumpForce, MoveHandle);
        new ControlHandle<float>("AbilityHandle", jumpForce, AbilityHandle);
    }
}

