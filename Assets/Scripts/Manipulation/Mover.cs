using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private Selectable selectable;
    [SerializeField] private Transform visualObjectTransform;

    private bool isMoving;

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

        Vector3 targetPosition = GetMousePosition();

        transform.position = targetPosition;
    }

}
