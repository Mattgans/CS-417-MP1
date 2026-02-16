using UnityEngine;
using TMPro; // Required for TextMesh Pro
using UnityEngine.EventSystems;

public class VRCodeEntry : MonoBehaviour, IPointerClickHandler
{
    [Header("Settings")]
    public string correctCode = "1234";
    
    private TMP_InputField inputField;
    private TouchScreenKeyboard overlayKeyboard;

    void Start()
    {
        inputField = GetComponent<TMP_InputField>();
        
        // Disable the physical hardware keyboard simulation to force virtual keyboard
        inputField.shouldHideMobileInput = false; 
    }

    // This detects when you click the box with your VR laser
    public void OnPointerClick(PointerEventData eventData)
    {
        OpenSystemKeyboard();
    }

    public void OpenSystemKeyboard()
    {
        // Opens the Quest native keyboard overlay
        // The 'false' parameter prevents it from obscuring your view completely if using a secure field
        overlayKeyboard = TouchScreenKeyboard.Open(inputField.text, TouchScreenKeyboardType.NumberPad);
    }

    void Update()
    {
        // Sync the system keyboard text to the Unity UI
        if (overlayKeyboard != null && overlayKeyboard.status == TouchScreenKeyboard.Status.Visible)
        {
            inputField.text = overlayKeyboard.text;
        }

        // Check if user hit "Enter" or "Done" on the keyboard
        if (overlayKeyboard != null && overlayKeyboard.status == TouchScreenKeyboard.Status.Done)
        {
             CheckCode(inputField.text);
             overlayKeyboard = null;
        }
    }

    void CheckCode(string input)
    {
        if (input == correctCode)
        {
            Debug.Log("Access Granted!");
            // Add your game logic here (e.g., open door)
        }
        else
        {
            Debug.Log("Access Denied");
            inputField.text = ""; // Clear the text
        }
    }
}