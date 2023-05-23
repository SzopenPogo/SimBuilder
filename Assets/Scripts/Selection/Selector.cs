using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
    public static Selector Instance { get; private set; }

    //Data
    [field: SerializeField] public LayerMask SelectableLayerMask { get; private set; }

    public GameObject SelectedGameObject { get; private set; }
    public Selectable SelectedScript{ get; private set; }


    public bool IsActive { get; private set; } = true;

    private void Awake() => Instance = this;

    private void Start()
    {
        InputReader.Instance.LeftClickEvent += Select; 
    }

    private void OnDestroy()
    {
        InputReader.Instance.LeftClickEvent -= Select;
    }

    public void SetSelector(bool isActive) => IsActive = isActive;

    private bool IsSelectableLayerMask(GameObject gameObject)
    {
        return SelectableLayerMask == (SelectableLayerMask | (1 << gameObject.layer));
    }

    private bool TryGetGameObjectSelectable(GameObject gameObject, out Selectable selectableScript)
    {
        return gameObject.transform.parent.TryGetComponent(out selectableScript);
    }

    public void ResetSelectedGameObject()
    {
        if (SelectedGameObject == null)
            return;

        if(TryGetGameObjectSelectable(SelectedGameObject, out Selectable selectableScript))
            selectableScript.Unselect();

        SelectedGameObject = null;
    }

    public bool TryGetHittedSelectableGameObject(out GameObject hittedObject)
    {
        hittedObject = MouseRaycast.Instance.GetHittedGameObject();

        if (hittedObject == null)
            return false;

        if (!IsSelectableLayerMask(hittedObject))
            return false;

        return true;
    }

    private void Select()
    {
        if (!IsActive)
            return;

        if (!TryGetHittedSelectableGameObject(out GameObject hittedObject))
        {
            ResetSelectedGameObject();
            return;
        }

        if (hittedObject == SelectedGameObject)
            return;

        if (SelectedScript != null)
            SelectedScript.Unselect();


        //Check if hitted object parent have Selectable script
        if (!TryGetGameObjectSelectable(hittedObject, out Selectable selectableScript))
        {
            ResetSelectedGameObject();
            return;
        }

        //Select hitted object
        selectableScript.Select();
        SelectedGameObject = hittedObject;
        SelectedScript = selectableScript;
    }
}
