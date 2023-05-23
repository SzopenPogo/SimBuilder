using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjects : MonoBehaviour
{
    public static PlayerObjects Instance;

    [field: SerializeField] public List<ObjectData> Objects { get; private set; } = new();

    private void Awake() => Instance = this;
}
