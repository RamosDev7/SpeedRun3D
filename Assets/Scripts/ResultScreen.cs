using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Tela de resultado: mostra tempo final, pontuação e medalha, e oferece jogar de novo.
/// Salvar em: Assets/Scripts/ResultScreen.cs
/// Anexar em: GameObject vazio "ResultManager" na cena ResultScene.
/// </summary>
public class ResultScreen : MonoBehaviour
{
    [Header("Textos (arraste do Hierarchy)")]
    public TMP_Text timeText;
    public TMP_Text scoreText;
    public TMP_Text medalText;

    [Header("Botões (arraste do Hierarchy)")]
    public Button playAgainButton;
    public Button menuButton;

    void Start()
    {
        Time.timeScale = 1f;

        if (timeText != null) timeText.text = "Tempo final: " + GameData.FormatTime(GameData.FinalTime);
        if (scoreText != null) scoreText.text = "Pontos: " + GameData.FinalScore;

        if (medalText != null)
        {
            medalText.text = "Medalha: " + GameData.FinalMedal;
            medalText.color = GetMedalColor(GameData.FinalMedal);
        }

        if (playAgainButton != null) playAgainButton.onClick.AddListener(PlayAgain);
        if (menuButton != null) menuButton.onClick.AddListener(GoToMenu);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(GameData.RaceScene);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(GameData.MainMenuScene);
    }

    private Color GetMedalColor(string medal)
    {
        if (medal == GameData.MedalGold) return new Color(1f, 0.84f, 0.04f);
        if (medal == GameData.MedalSilver) return new Color(0.78f, 0.80f, 0.85f);
        if (medal == GameData.MedalBronze) return new Color(0.80f, 0.50f, 0.20f);
        return Color.white;
    }
}
