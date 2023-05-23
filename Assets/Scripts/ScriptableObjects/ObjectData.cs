using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Object", menuName = "Objects")]
public class ObjectData : ScriptableObject
{
    [SerializeField] private Sprite icon;
    [SerializeField] private string objectName;
    [SerializeField] private GameObject model;
    [SerializeField] private List<Material> materials;

    public Sprite Icon => icon;
    public string ObjectName => objectName;
    public GameObject Model => model;
    public List<Material> Materials => materials;
}
