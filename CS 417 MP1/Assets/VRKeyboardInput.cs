using UnityEngine;
using TMPro;
using UnityEngine.UI; // Needed to change the background color
using UnityEngine.EventSystems;
using System.Collections;

public class VRKeyboardInput : MonoBehaviour, IPointerDownHandler
{
    [Header("Code Settings")]
    public GameObject keyPrefab;
    public Transform spawnPoint;
    
    [Header("UI Settings")]
    public Image backgroundBox; // Drag the background image of the textbox here

    private TMP_InputField inputField;
    private TouchScreenKeyboard overlayKeyboard;
    private bool hasSpawned = false;

    void Start()
    {
        inputField = GetComponent<TMP_InputField>();

        // Auto-find the background image if you forgot to drag it in
        if (backgroundBox == null)
        {
            backgroundBox = GetComponent<Image>();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Don't open keyboard if we already solved the puzzle
        if (hasSpawned) return;

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
        if (!hasSpawned && inputField.text == "4357")
        {
            Success();
        }
    }

    void Success()
    {
        hasSpawned = true;

        // 1. Turn the background Green
        if (backgroundBox != null)
        {
            backgroundBox.color = Color.green;
        }

        // 2. Spawn the Key
        if (keyPrefab != null && spawnPoint != null)
        {
            Instantiate(keyPrefab, spawnPoint.position, spawnPoint.rotation);
        }

        // 3. Close the keyboard immediately
        if (overlayKeyboard != null)
        {
            overlayKeyboard.active = false;
            overlayKeyboard = null;
        }

        // 4. Lock the input field so they can't change the answer
        inputField.interactable = false;
    }
}