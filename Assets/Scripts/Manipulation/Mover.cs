using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    //Components
    [SerializeField] private Selectable selectable;
    [SerializeField] private Transform childObjectTransform;
    
    //Data
    Vector3 childObjectInitialLocalPosition;
    private bool isMoving;

    private void Start()
    {
        childObjectInitialLocalPosition = childObjectTransform.localPosition;
    }

    private void Update()
    {
        if (!IsMovable())
        {
            StopMove();
            return;
        }

        CameraController.Instance.SetCameraMove(false);
        Move();
    }

    private void OnDisable()
    {
        StopMove();
    }

    private bool IsMovable()
    {
        if (!selectable.IsSelected)
            return false;
        
        if (!InputReader.Instance.IsLeftMouseButtonClicked)
            return false;

        return true;
    }

    private void StopMove()
    {
        if (!isMoving)
            return;

        CameraController.Instance.SetCameraMove(true);
        isMoving = false;
    }

    private Vector3 GetMousePosition()
    {
        //Get Mouse Raycast
        RaycastHit mouseRaycastHit = MouseRaycast.Instance.CastRaycast(
            Selector.Instance.SelectableLayerMask);

        return MouseRaycast.Instance.GetMouseToWorldPosition(mouseRaycastHit);
    }

    private void Move()
    {
        //Set moving
        if (!isMoving)
        {
            isMoving = true;
            CameraController.Instance.SetCameraMove(false);
        }

        //Current child local position (except Y)
        Vector3 childLocalPosition = childObjectTransform.localPosition;
        childLocalPosition.y = 0f;

        //Calculate and update position
        Vector3 targetPosition = GetMousePosition() + childLocalPosition;
        transform.position = targetPosition;
        
        //Reset child transform to default because collision can change this value
        childObjectTransform.localPosition = childObjectInitialLocalPosition;
    }
}
