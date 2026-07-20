using UnityEngine;

public class ActivateGameObject : uFSMAction
{
    public GameObject gameObject;
    public bool activate;

    public override void OnEnter(uFSMContext ctx)
    {
        gameObject.SetActive(activate);
    }
}