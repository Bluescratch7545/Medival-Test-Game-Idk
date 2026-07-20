using UnityEngine;

public class Wait : uFSMAction
{
    public float time;
    public bool realTime;
    public string finishedEvent;

    public override void OnUpdate(uFSMContext ctx)
    {
        float timer = 0;

        timer += Time.deltaTime;

        if (timer >= time)
        {
            ctx.owner.GetComponent<uFSM>().SendEvent(finishedEvent);
        }
    }
}