using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectCreator : MonoBehaviour
{
    public static PlayerObjectCreator Instance;

    [field: SerializeField] private Transform objectContainer;

    private void Awake() => Instance = this;

    public void CreatePlayerObiejct(ObjectData data, Material material)
    {
        //Create new object
        GameObject newObject = data.Model;

        //Set Material
        if(newObject.TryGetComponent(out PlayerObject playerObject))
            playerObject.SetMaterial(material);

        Instantiate(newObject, objectContainer);
    }
}
