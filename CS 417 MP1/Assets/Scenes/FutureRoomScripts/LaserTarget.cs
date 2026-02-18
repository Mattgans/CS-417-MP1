using UnityEngine;

public class LaserTarget : MonoBehaviour
{
    public GameObject objectToActivate;

    private bool alreadyHit = false;

    public void OnLaserHit()
    {
        if (alreadyHit) return;

        alreadyHit = true;

        objectToActivate.SetActive(true);
    }
}