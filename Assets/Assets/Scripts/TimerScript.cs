using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour
{
    public float time;
    public TextMeshProUGUI timerText;

    [SerializeField] private float timeLimit = 60f;
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    private bool hasLoadedMenu = false;

    void Update()
    {
        if (hasLoadedMenu) return;

        time += Time.deltaTime;
        timerText.text = time.ToString("F2");

        if (time >= timeLimit)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            hasLoadedMenu = true;
            SceneManager.LoadScene(mainMenuSceneName);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(mainMenuSceneName);
        }
    }
}