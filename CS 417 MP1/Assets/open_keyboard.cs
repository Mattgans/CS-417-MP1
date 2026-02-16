using UnityEngine;
using TMPro;

public class OpenXRKeyboardHandler : MonoBehaviour
{
    private TMP_InputField inputField;
    private TouchScreenKeyboard overlayKeyboard;

    void Start()
    {
        inputField = GetComponent<TMP_InputField>();
        // Open the keyboard when the user clicks the field
        inputField.onSelect.AddListener(delegate { OpenKeyboard(); });
    }

    public void OpenKeyboard()
    {
        // TouchScreenKeyboardType.Default is the only supported type for the overlay
        overlayKeyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
    }

    void Update()
    {
        // Update the InputField text as the user types on the Quest overlay
        if (overlayKeyboard != null)
        {
            inputField.text = overlayKeyboard.text;
        }
    }
}