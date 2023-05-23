using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UiChoiceBarSelectable : MonoBehaviour, IPointerDownHandler
{
    [field: Header("Container Components")]
    [field: SerializeField] protected Image backgroundImage;
    [field: SerializeField] protected Color selectedBackgroundColor;

    //Data
    protected Color initialColor;
    protected UiObjectContainer container;
    protected bool isSelected;

    public virtual void Init(UiObjectContainer container)
    {
        initialColor = backgroundImage.color;
        this.container = container;
    }

    public virtual void Select()
    {
        if (isSelected)
            return;

        backgroundImage.color = selectedBackgroundColor;
        isSelected = true;
    }

    public void Unselect()
    {
        backgroundImage.color = initialColor;
        isSelected = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Select();
    }
}
