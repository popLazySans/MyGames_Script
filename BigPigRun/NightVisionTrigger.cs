using UnityEngine;

public class NightVisionTrigger : MonoBehaviour
{
    public NightVisionController nightVisionController;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            nightVisionController.enabled = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            nightVisionController.enabled = false;
        }
    }
}
