using UnityEngine;
using UnityEngine.UI;

public class UiObjectContainer : MonoBehaviour
{
    [field: Header("Components")]
    [field: SerializeField] private RectTransform contentRectTransform;
    [field: SerializeField] private Button createButton;
    [field: SerializeField] private UiChoiceBar choiceBarController;

    [field: Header("Object Components")]
    [field: SerializeField] private Transform objectContainerTransform;
    [field: SerializeField] private GameObject objectPrefab;

    [field: Header("Color Components")]
    [field: SerializeField] private Transform colorContainerTransform;
    [field: SerializeField] private GameObject colorPrefab;

    //Values
    private const float RefreshUiDelay = .1f;

    //ObjectData
    private ObjectData selectedObjectData;
    private UiChoiceBarObject selectedObjectUi;
    private UiChoiceBarColor selectedObjectColorUi;
    private Material selectedObjectMaterial;
   
    private void OnEnable()
    {
        RenderObjects();
        createButton.onClick.AddListener(CreateObject);
        Invoke(nameof(RefreshContentUi), RefreshUiDelay);
    }

    private void OnDisable()
    {
        createButton.onClick.RemoveAllListeners();
        ClearChildren(colorContainerTransform);
        DisableCreateButton();
    }

    private void EnableCreateButton()
    {
        createButton.gameObject.SetActive(true);
    }

    private void DisableCreateButton()
    {
        createButton.gameObject.SetActive(false);
    }


    private void ClearChildren(Transform container)
    {
        foreach (Transform child in container)
            Destroy(child.gameObject);
    }

    private void RenderObjects()
    {
        ClearChildren(objectContainerTransform);

        //Render Objects
        for (int i = 0; i < PlayerObjects.Instance.Objects.Count; i++)
        {
            GameObject objectUi = Instantiate(objectPrefab, objectContainerTransform);

            //Init object bar data
            if (objectUi.TryGetComponent(out UiChoiceBarObject objectScript))
                objectScript.Init(this, PlayerObjects.Instance.Objects[i]);
        }
    }

    public void RefreshContentUi()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(contentRectTransform);
    }

    public void SelectObject(UiChoiceBarObject objectUi ,ObjectData objectData)
    {
        //If some object was selected before
        if (selectedObjectUi != null)
        {
            selectedObjectUi.Unselect();
            DisableCreateButton();
        }

        //Select new object
        selectedObjectUi = objectUi;
        selectedObjectData = objectData;

        RenderSelectedObjectColors();
    }

    private void RenderSelectedObjectColors()
    {
        ClearChildren(colorContainerTransform);

        if (selectedObjectData == null)
            return;

        for (int i = 0; i < selectedObjectData.Materials.Count; i++)
        {
            GameObject color = Instantiate(colorPrefab, colorContainerTransform);

            if (color.TryGetComponent(out UiChoiceBarColor colorScript))
                colorScript.Init(this, selectedObjectData.Materials[i]);
        }

        Invoke(nameof(RefreshContentUi), RefreshUiDelay);
    }

    public void SelectColor(UiChoiceBarColor colorUi, Material selectedMaterial)
    {
        if(selectedObjectColorUi  != null)
            selectedObjectColorUi.Unselect();

        selectedObjectColorUi = colorUi;
        selectedObjectMaterial = selectedMaterial;

        EnableCreateButton();

        RefreshContentUi();
    }

    public void CreateObject()
    {
        choiceBarController.ToggleChoicebar();
        PlayerObjectCreator.Instance.CreatePlayerObiejct(selectedObjectData, selectedObjectMaterial);
    }
}
