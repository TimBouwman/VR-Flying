using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TwoHandGrabInteractable : XRGrabInteractable
{
    #region Variables
    [SerializeField] private Vector3 rotationOffset = Vector3.zero;
    private enum TwoHandRotationType { None, First, Second };
    [SerializeField] private TwoHandRotationType twoHandRotationType = TwoHandRotationType.None;

    [SerializeField] private bool retainOrigin = false;
    private Vector3 oldPos = Vector3.zero;
    private Quaternion oldRot = Quaternion.identity;

    private GripHold[] gripHolds = null;
    private Quaternion attachTransformRot = Quaternion.identity;
    private XRBaseInteractor secondInteractor = null;
    private Transform secondInteractableAttachTransform = null;
    private Vector3 secondInteractableRotationOffset = Vector3.zero;
    #endregion

    #region Override Methods
    protected override void Awake()
    {
        base.Awake();
        SetupHolds();
    }
    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        if (selectingInteractor && secondInteractor)
        {
            selectingInteractor.attachTransform.rotation = GetTwoHandRotation();
            selectingInteractor.attachTransform.Rotate(rotationOffset, Space.Self);
        }
        base.ProcessInteractable(updatePhase);
    }
    #endregion

    #region Custom Methods
    private Quaternion GetTwoHandRotation()
    {
        Quaternion newRotation = Quaternion.identity;

        switch (twoHandRotationType)
        {
            case TwoHandRotationType.None:
                newRotation = Quaternion.LookRotation(selectingInteractor.transform.position - secondInteractor.attachTransform.position);
                break;
            case TwoHandRotationType.First:
                newRotation = Quaternion.LookRotation(selectingInteractor.transform.position - secondInteractor.attachTransform.position, selectingInteractor.attachTransform.up);
                break;
            case TwoHandRotationType.Second:
                newRotation = Quaternion.LookRotation(selectingInteractor.transform.position - secondInteractor.attachTransform.position, secondInteractor.attachTransform.up);
                break;
        }
        return newRotation;
    }
    private void SetupHolds()
    {
        gripHolds = new GripHold[this.transform.childCount];
        for (int i = 0; i < gripHolds.Length; i++)
        {
            gripHolds[i] = this.transform.GetChild(i).GetComponent<GripHold>();
            gripHolds[i].Setup(this);
        }
        if (retainOrigin)
        {
            oldPos = this.transform.localPosition;
            oldRot = this.transform.localRotation;
        }
    }
    public void SetGripHand(XRBaseInteractor interactor, Transform attachTransform, Vector3 rotationOffset)
    {
        if (selectingInteractor == null)
        {
            attachTransformRot = interactor.attachTransform.rotation;
            this.attachTransform = attachTransform;
            this.rotationOffset = rotationOffset;
            OnSelectEnter(interactor);
        }
        else if (secondInteractor == null)
        {
            secondInteractor = interactor;
            secondInteractableAttachTransform = attachTransform;
            secondInteractableRotationOffset = rotationOffset;
        }
    }
    public void ClearGripHand(XRBaseInteractor interactor)
    {
        if (selectingInteractor.Equals(interactor))
        {
            selectingInteractor.attachTransform.rotation = attachTransformRot;
            OnSelectExit(interactor);

            if(secondInteractor)
            {
                SetGripHand(secondInteractor, secondInteractableAttachTransform, secondInteractableRotationOffset);
                secondInteractor = null;
            }
            else if (retainOrigin)
            {
                this.transform.localPosition = oldPos;
                this.transform.localRotation = oldRot;
            }
        }
        else if (interactor.Equals(secondInteractor))
        {
            secondInteractor = null;
        }
    }
    #endregion
}