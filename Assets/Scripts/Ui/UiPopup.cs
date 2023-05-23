using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiPopup : MonoBehaviour
{
    [field: Header("Components")]
    [field: SerializeField] private CanvasGroup windowCanvasGroup;
    [field: SerializeField] private CanvasGroup backdropCanvasGroup;
    [field: SerializeField] private Button backdropButton;
    [field: SerializeField] private Button closeButton;

    [field: Header("Values")]
    [field: SerializeField] private float animationTime = .5f;
    [field: SerializeField] private float windowOpacityAnimationTime = .35f;
    [field: SerializeField] private float animationCloseTime = .25f;

    //Non serializable Values
    private readonly float inactiveOpacity = 0f;
    private readonly float activeBackdropOpacity = .6f;
    private readonly float activeWindowOpacity = 1f;
    private readonly float closeWindowScale = .5f;

    //Actions
    public Action OnCloseButtonClicked;

    private void OnEnable()
    {
        //Buttons
        backdropButton.onClick.AddListener(OnPopupCloseButton);
        closeButton.onClick.AddListener(OnPopupCloseButton);

        //Backdrop
        backdropCanvasGroup.alpha = inactiveOpacity;
        backdropCanvasGroup.LeanAlpha(activeBackdropOpacity, animationTime);

        //Window
        windowCanvasGroup.alpha = inactiveOpacity;
        windowCanvasGroup.LeanAlpha(activeWindowOpacity, windowOpacityAnimationTime);
        windowCanvasGroup.transform.LeanScale(Vector2.one, animationTime).setEaseOutBack();

        //Refresh window
        LayoutRebuilder.ForceRebuildLayoutImmediate(windowCanvasGroup.GetComponent<RectTransform>());
    }

    private void OnDisable()
    {
        backdropButton.onClick.RemoveListener(OnPopupCloseButton);
        closeButton.onClick.RemoveListener(OnPopupCloseButton);
    }

    public void ClosePopup()
    {
        //Backdrop
        backdropCanvasGroup.LeanAlpha(0, animationCloseTime);

        //Window
        windowCanvasGroup.LeanAlpha(0, animationCloseTime);
        windowCanvasGroup.transform.LeanScale(new Vector2(closeWindowScale, closeWindowScale), animationCloseTime);

        //Object
        Invoke(nameof(DisbalePopup), animationTime);
    }

    private void DisbalePopup() => gameObject.SetActive(false);

    private void OnPopupCloseButton() => OnCloseButtonClicked?.Invoke();
}
