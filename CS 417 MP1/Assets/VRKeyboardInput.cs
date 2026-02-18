using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class VRKeyboardInput : MonoBehaviour, IPointerDownHandler
{
    [Header("Code Settings")]
    [Tooltip("Drag the disabled object from your Hierarchy here")]
    public GameObject objectToEnable; 
    
    [Header("UI Settings")]
    public Image backgroundBox; 

    private TMP_InputField inputField;
    private TouchScreenKeyboard overlayKeyboard;
    private bool hasSolved = false;

    void Start()
    {
        inputField = GetComponent<TMP_InputField>();

        if (backgroundBox == null)
        {
            backgroundBox = GetComponent<Image>();
        }
        
        // Safety check: Make sure the object starts disabled if you haven't already
        if (objectToEnable != null && objectToEnable.activeSelf)
        {
            objectToEnable.SetActive(false);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (hasSolved) return;

        inputField.ActivateInputField();
        StopAllCoroutines();
        StartCoroutine(OpenKeyboardWithDelay());
    }

    private IEnumerator OpenKeyboardWithDelay()
    {
        yield return new WaitForSeconds(0.15f);

        if (overlayKeyboard == null || overlayKeyboard.status != TouchScreenKeyboard.Status.Visible)
        {
            overlayKeyboard = TouchScreenKeyboard.Open(inputField.text, TouchScreenKeyboardType.Default);
        }
    }

    void Update()
    {
        if (overlayKeyboard != null && overlayKeyboard.status == TouchScreenKeyboard.Status.Visible)
        {
            inputField.text = overlayKeyboard.text;
        }

        // Check for the code "4357"
        if (!hasSolved && inputField.text == "4357")
        {
            Success();
        }
    }

    void Success()
    {
        hasSolved = true;

        // 1. Turn the background Green
        if (backgroundBox != null)
        {
            backgroundBox.color = Color.green;
        }

        // 2. ENABLE the object instead of spawning
        if (objectToEnable != null)
        {
            objectToEnable.SetActive(true);
        }

        // 3. Close the keyboard
        if (overlayKeyboard != null)
        {
            overlayKeyboard.active = false;
            overlayKeyboard = null;
        }

        // 4. Lock the input field
        inputField.interactable = false;
    }
}