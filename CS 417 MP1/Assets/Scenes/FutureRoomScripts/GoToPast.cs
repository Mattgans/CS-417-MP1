using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToPast : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("Past Room");
        }
    }
}