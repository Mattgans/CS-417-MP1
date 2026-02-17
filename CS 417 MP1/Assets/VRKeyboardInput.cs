using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections;

public class VRKeyboardInput : MonoBehaviour, IPointerDownHandler
{
    private TMP_InputField inputField;
    private TouchScreenKeyboard overlayKeyboard;

    void Start()
    {
        inputField = GetComponent<TMP_InputField>();
    }

    // Use OnPointerDown instead of OnClick for better VR responsiveness
    public void OnPointerDown(PointerEventData eventData)
    {
        // 1. Highlight the box visually
        inputField.ActivateInputField();

        // 2. Start a timer to open the keyboard (Fixes Quest 3 focus bug)
        StopAllCoroutines();
        StartCoroutine(OpenKeyboardWithDelay());
    }

    private IEnumerator OpenKeyboardWithDelay()
    {
        // Wait 0.15 seconds to let the physics/UI click event finish completely
        yield return new WaitForSeconds(0.15f);

        // 3. Open the keyboard
        // We check if it's already visible to prevent double-opening
        if (overlayKeyboard == null || overlayKeyboard.status != TouchScreenKeyboard.Status.Visible)
        {
            overlayKeyboard = TouchScreenKeyboard.Open(inputField.text, TouchScreenKeyboardType.Default);
        }
    }

    void Update()
    {
        // 4. Sync the typing back to the Unity box
        if (overlayKeyboard != null && overlayKeyboard.status == TouchScreenKeyboard.Status.Visible)
        {
            inputField.text = overlayKeyboard.text;
        }
    }
}