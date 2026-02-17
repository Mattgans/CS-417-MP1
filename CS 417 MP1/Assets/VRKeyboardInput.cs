using UnityEngine;
using TMPro; // Needed for TextMeshPro
using UnityEngine.EventSystems; // Needed for detecting VR pointer clicks

public class VRKeyboardInput : MonoBehaviour, IPointerClickHandler
{
    private TMP_InputField inputField;

    void Start()
    {
        // Automatically find the TMP_InputField on this object
        inputField = GetComponent<TMP_InputField>();
    }

    // This function runs when you press the trigger on this object
    public void OnPointerClick(PointerEventData eventData)
    {
        // 1. Highlight the box so Unity knows it's active
        inputField.ActivateInputField();

        // 2. Open the native Quest system keyboard
        // The first parameter is existing text (optional), second is keyboard type
        TouchScreenKeyboard.Open(inputField.text, TouchScreenKeyboardType.Default);
    }
}