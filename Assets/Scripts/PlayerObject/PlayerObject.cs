using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    [field: SerializeField] private GameObject objectVisual;

    public void SetMaterial(Material newMaterial)
    {
        if (!objectVisual.TryGetComponent(out Renderer renderer))
            return;

        renderer.material = newMaterial;
    }
}
