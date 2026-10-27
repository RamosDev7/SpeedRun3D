using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// Interface durante a corrida: tempo, velocidade, progresso, pontuação e menu de pausa.
/// Salvar em: Assets/Scripts/RaceHUD.cs
/// Anexar em: o Canvas do HUD ("HUDCanvas") na RaceScene.
/// NÃO anexe no PausePanel: ele é desativado e o script pararia junto.
/// </summary>
public class RaceHUD : MonoBehaviour
{
    [Header("Referências do jogo")]
    public RaceManager raceManager;
    public CarController car;

    [Header("Textos e barra de progresso")]
    public TMP_Text timeText;
    public TMP_Text speedText;
    public TMP_Text scoreText;
    public TMP_Text progressText;
    public Slider progressBar;

    [Header("Pausa")]
    public Button pauseButton;
    public GameObject pausePanel;
    public Button resumeButton;
    public Button restartButton;
    public Button menuButton;

    void Start()
    {
        if (raceManager == null || car == null)
        {
            Debug.LogError("RaceHUD: arraste RaceManager e Car para os campos do Inspector!");
            enabled = false;
            return;
        }

        if (pausePanel != null) pausePanel.SetActive(false);

        AddClick(pauseButton, raceManager.TogglePause);
        AddClick(resumeButton, () => raceManager.SetPaused(false));
        AddClick(restartButton, raceManager.RestartRace);
        AddClick(menuButton, raceManager.GoToMenu);
    }

    void Update()
    {
        SetText(timeText, GameData.FormatTime(raceManager.ElapsedTime));
        SetText(speedText, Mathf.RoundToInt(car.SpeedKmh).ToString("000") + " km/h");
        SetText(scoreText, "PONTOS: " + raceManager.CurrentScore);

        int percent = Mathf.RoundToInt(raceManager.Progress * 100f);
        SetText(progressText, percent + "%");
        if (progressBar != null) progressBar.value = raceManager.Progress;

        if (pausePanel != null && pausePanel.activeSelf != raceManager.IsPaused)
        {
            pausePanel.SetActive(raceManager.IsPaused);
        }
    }

    // Pequenos ajudantes: evitam erro se algum campo ficar vazio no Inspector.
    private void SetText(TMP_Text label, string value)
    {
        if (label != null) label.text = value;
    }

    private void AddClick(Button button, UnityAction action)
    {
        if (button != null) button.onClick.AddListener(action);
    }
}
