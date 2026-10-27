using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Menu inicial: Iniciar corrida, Reiniciar e Sair.
/// Salvar em: Assets/Scripts/MainMenu.cs
/// Anexar em: GameObject vazio "MenuManager" na cena MainMenu.
/// </summary>
public class MainMenu : MonoBehaviour
{
    [Header("Botões (arraste do Hierarchy)")]
    public Button startButton;
    public Button restartButton;
    public Button quitButton;

    void Start()
    {
        Time.timeScale = 1f;
        GameData.ResetResult();

        if (startButton != null) startButton.onClick.AddListener(StartRace);
        if (restartButton != null) restartButton.onClick.AddListener(RestartRace);
        if (quitButton != null) quitButton.onClick.AddListener(QuitGame);
    }

    public void StartRace()
    {
        GameData.ResetResult();
        SceneManager.LoadScene(GameData.RaceScene);
    }

    // "Reiniciar": começa uma corrida nova do zero (recarrega a Race Scene).
    public void RestartRace()
    {
        StartRace();
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;   // no Editor, apenas para o Play
#else
        Application.Quit();                                // no jogo compilado, fecha o programa
#endif
    }
}
