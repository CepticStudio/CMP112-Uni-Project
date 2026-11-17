using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider))]
public class End : MonoBehaviour
{
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    private void Reset()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Cursor.lockState = CursorLockMode.None; 
            Cursor.visible = true;
            SceneManager.LoadScene(mainMenuSceneName);
        }
    }
}