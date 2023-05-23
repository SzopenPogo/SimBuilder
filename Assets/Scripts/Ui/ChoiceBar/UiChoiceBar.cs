using UnityEngine;
using UnityEngine.UI;

public class UiChoiceBar : MonoBehaviour
{
    [field: Header("Components")]
    [field: SerializeField] private Button manageButton;
    [field: SerializeField] private UiPopup choicePopup;

    private bool isContainerVisible = false;

    private void Awake()
    {
        manageButton.onClick.AddListener(ToggleChoicebar);
    }

    private void Start()
    {
        choicePopup.OnCloseButtonClicked += ToggleChoicebar;
    }

    private void OnDestroy()
    {
        manageButton.onClick.RemoveAllListeners();
        choicePopup.OnCloseButtonClicked -= ToggleChoicebar;
    }

    public void ToggleChoicebar()
    {
        //Set camera controller and selector
        CameraController.Instance.SetCameraMove(isContainerVisible);
        Selector.Instance.SetSelector(isContainerVisible);

        if (!isContainerVisible)
        {
            Selector.Instance.ResetSelectedGameObject();
            choicePopup.gameObject.SetActive(true);
        }
        else
        {
            choicePopup.ClosePopup();
        }


        isContainerVisible = !isContainerVisible;
    }
}
