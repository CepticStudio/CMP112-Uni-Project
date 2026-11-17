using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ReturnToMainMenu : MonoBehaviour
{
    [Header("Scene")]
    [Tooltip("Exact scene name as listed in File > Build Settings.")]
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    [Header("Options")]
    [Tooltip("Delay (seconds) before loading, so the lever animation / sound can play.")]
    [SerializeField] private float delay = 0.75f;

    [Tooltip("Unlock and show the cursor so the player can use the menu.")]
    [SerializeField] private bool unlockCursor = true;

    private bool isLoading;

    public void LoadMainMenu()
    {
        if (isLoading) return;
        isLoading = true;
        StartCoroutine(LoadRoutine());
    }

    private IEnumerator LoadRoutine()
    {
        yield return new WaitForSeconds(delay);

        Time.timeScale = 1f;

        if (unlockCursor)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        SceneManager.LoadScene(mainMenuSceneName);
    }
}
