using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class NameEntryUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private Button submitButton;

    private Action<string> _onSubmit;
    void Awake()
    {
        submitButton.onClick.AddListener(OnSubmitClicked);
    }

    public void Show(Action<string> onSubmit)
    {
        _onSubmit = onSubmit;
        nameInput.text = "";
        gameObject.SetActive(true);
    }

    private void OnSubmitClicked()
    {
        string name = nameInput.text.Trim();
        if (string.IsNullOrEmpty(name))
        {
            // you could flash a warning here
            return;
        }
        gameObject.SetActive(false);
        _onSubmit?.Invoke(name);
    }
}
