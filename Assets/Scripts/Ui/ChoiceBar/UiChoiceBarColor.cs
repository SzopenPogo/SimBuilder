using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UiChoiceBarColor : UiChoiceBarSelectable
{
    [field: Header("Components")]
    [field: SerializeField] private Image colorImage;

    //Data
    Material material;

    public void Init(UiObjectContainer container, Material material)
    {
        Init(container);

        this.material = material;
        colorImage.color = material.color;
    }

    public override void Select()
    {
        base.Select();
        container.SelectColor(this, material);
    }
}
