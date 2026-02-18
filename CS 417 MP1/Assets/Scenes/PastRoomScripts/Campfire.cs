using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Campfire : MonoBehaviour
{
    public int requiredBranches = 4;
    private int currentBranches = 0;

    public GameObject fireEffect;
    private bool isLit = false;

    private void Start()
    {
        fireEffect.SetActive(false);
    }

    public void RegisterBranch(SelectEnterEventArgs args)
    {
        currentBranches++;
        Debug.Log("Branches placed: " + currentBranches);
    }

    public void UnregisterBranch(SelectExitEventArgs args)
    {
        currentBranches--;
    }

    public void TryLightFire()
    {
        if (!isLit && currentBranches >= requiredBranches)
        {
            isLit = true;
            fireEffect.SetActive(true);
            Debug.Log("Campfire Lit!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Lighter"))
        {
            TryLightFire();
        }
    }
}