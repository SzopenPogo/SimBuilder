using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectSelectable : Selectable
{
    [field: SerializeField] private Mover mover;

    public override void Select()
    {
        base.Select();
        mover.enabled = true;
    }

    public override void Unselect()
    {
        base.Unselect();
        mover.enabled = false;
    }
}
