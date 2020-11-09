using System.Diagnostics;
using UnityEngine.XR.Interaction.Toolkit;

public class HandHold : XRBaseInteractable
{
    protected TwoHandGrabInteractable handle = null;

    public void Setup(TwoHandGrabInteractable handle)
    {
        this.handle = handle;
    }

    protected override void Awake()
    {
        base.Awake();
        onSelectEnter.AddListener(Grab);
        onSelectExit.AddListener(Drop);
    }

    private void OnDestroy()
    {
        onSelectEnter.RemoveListener(Grab);
        onSelectExit.RemoveListener(Drop);
    }

    protected virtual void BeginAction(XRBaseInteractor interactor)
    {
        
    }

    protected virtual void EndAction(XRBaseInteractor interactor)
    {
        
    }

    protected virtual void Grab(XRBaseInteractor interactor)
    {

    }

    protected virtual void Drop(XRBaseInteractor interactor)
    {

    }

    private void TryToHideHand(XRBaseInteractor interactor, bool hide)
    {

    }

    public void BreakHold(XRBaseInteractor interactor)
    {

    }
}