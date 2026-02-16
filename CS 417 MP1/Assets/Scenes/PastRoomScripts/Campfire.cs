using System.Collections.Generic;
using UnityEngine;

public class Campfire : MonoBehaviour
{
    public int requiredBranches = 4;
    private int branchCount = 0;
    private HashSet<GameObject> branchesInside = new HashSet<GameObject>();

    public GameObject fireEffect; 
    private bool isLit = false;
    private void Start()
    {
        fireEffect.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Branch"))
        {
            if (!branchesInside.Contains(other.gameObject))
            {
                branchesInside.Add(other.gameObject);
                branchCount++;
                Debug.Log("Branches in fire: " + branchCount);
            }
        }

        if (other.CompareTag("Lighter"))
        {
            TryLightFire();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Branch"))
        {
            if (branchesInside.Contains(other.gameObject))
            {
                branchesInside.Remove(other.gameObject);
                branchCount--;
            }
        }
    }

    private void TryLightFire()
    {
        if (!isLit && branchCount > requiredBranches)
        {
            isLit = true;
            fireEffect.SetActive(true);
            Debug.Log("Campfire Lit!");
        }
    }
}
