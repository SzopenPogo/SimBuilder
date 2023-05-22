using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Selectable : MonoBehaviour
{
    public bool IsSelected { get; protected set; }

    public virtual void Select()
    {
        IsSelected = true;
        Debug.Log("Selected");
    }

    public virtual void Unselect()
    {
        IsSelected = false;
        Debug.Log("Unselected");
    }
}
