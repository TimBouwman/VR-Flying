using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TwoHandGrabInteractable : XRGrabInteractable
{
    [SerializeField] private Vector3 rotationOffset = Vector3.zero;
    private GripHold[] gripHolds = null;
    private XRBaseInteractor firstInteractor = null;
    private XRBaseInteractor secondInteractor = null;

    #region Override Methods
    protected override void Awake()
    {
        base.Awake();
        SetupHolds();
    }
    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        if (firstInteractor && secondInteractor)
        {
            //selectingInteractor.attachTransform.rotation = Quaternion.LookRotation(secondInteractor.attachTransform.position - selectingInteractor.attachTransform.position);
            //Vector3 difference = secondInteractor.attachTransform.position - selectingInteractor.attachTransform.position;
            //float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            //selectingInteractor.attachTransform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
            selectingInteractor.attachTransform.LookAt(secondInteractor.attachTransform.position);
            selectingInteractor.attachTransform.Rotate(rotationOffset, Space.Self);
        }
        base.ProcessInteractable(updatePhase);
    }
    #endregion

    #region Custom Methods
    private void SetupHolds()
    {
        gripHolds = new GripHold[this.transform.childCount];
        for (int i = 0; i < gripHolds.Length; i++)
        {
            gripHolds[i] = this.transform.GetChild(i).GetComponent<GripHold>();
            gripHolds[i].Setup(this);
        }
    }
    public void SetGripHand(XRBaseInteractor interactor, Transform attachTransform)
    {
        if (firstInteractor == null)
        {
            firstInteractor = interactor;
            this.attachTransform = attachTransform;
            OnSelectEnter(firstInteractor);
        }
        else if (secondInteractor == null)
        {
            secondInteractor = interactor;
        }
    }
    public void ClearGripHand(XRBaseInteractor interactor)
    {
        if (interactor.Equals(firstInteractor))
        {
            firstInteractor = null;
            OnSelectExit(interactor);
        }
        else if (interactor.Equals(secondInteractor))
        {
            secondInteractor = null;
        }
    }
    #endregion
}