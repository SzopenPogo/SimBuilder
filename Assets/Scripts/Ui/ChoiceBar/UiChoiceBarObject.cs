using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UiChoiceBarObject : UiChoiceBarSelectable
{
    [field: Header("Object Components")]
    [field: SerializeField] private Image icon;
    [field: SerializeField] private TextMeshProUGUI title;


    //Data
    private ObjectData objectData;

    private void OnDestroy()
    {
        Unselect();
    }

    public void Init(UiObjectContainer container, ObjectData objectData)
    {
        Init(container);
        this.objectData = objectData;

        icon.sprite = objectData.Icon;
        title.text = objectData.ObjectName;
    }
    public override void Select()
    {
        base.Select();
        container.SelectObject(this, objectData);
    }
}
