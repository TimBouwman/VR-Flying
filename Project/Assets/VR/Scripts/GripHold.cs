using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GripHold : HandHold
{
    [SerializeField] private Transform attachTransform = null;
    [SerializeField] private Vector3 rotationOffset = Vector3.zero;

    protected override void BeginAction(XRBaseInteractor interactor)
    {
        base.BeginAction(interactor);
    }

    protected override void EndAction(XRBaseInteractor interactor)
    {
        base.BeginAction(interactor);
    }

    protected override void Grab(XRBaseInteractor interactor)
    {
        base.Grab(interactor);
        handle.SetGripHand(interactor, attachTransform, rotationOffset);
    }

    protected override void Drop(XRBaseInteractor interactor)
    {
        base.Drop(interactor);
        handle.ClearGripHand(interactor);
    }
}