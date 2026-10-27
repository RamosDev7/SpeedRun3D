using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Cérebro da Race Scene: cronômetro, progresso, pausa, chegada e troca para a Result Scene.
/// Salvar em: Assets/Scripts/RaceManager.cs
/// Anexar em: GameObject vazio "RaceManager" (RaceScene).
/// </summary>
public class RaceManager : MonoBehaviour
{
    [Header("Referências (arraste do Hierarchy)")]
    public CarController car;
    public Transform startPoint;   // objeto vazio na largada (z = 0)
    public Transform finishLine;   // cubo amarelo da chegada (z = 400)

    [Header("Configuração")]
    public float delayBeforeResult = 2f;   // segundos entre cruzar a linha e abrir o resultado

    // Lidas pelo RaceHUD
    public float ElapsedTime { get; private set; }
    public float Progress { get; private set; }     // 0 a 1
    public bool IsRacing { get; private set; }
    public bool IsPaused { get; private set; }
    public int CurrentScore { get { return GameData.CalculateScore(ElapsedTime); } }

    void Start()
    {
        if (car == null || startPoint == null || finishLine == null)
        {
            Debug.LogError("RaceManager: arraste Car, Start Point e Finish Line para os campos do Inspector!");
            enabled = false;   // desliga este script para não gerar uma chuva de erros
            return;
        }

        Time.timeScale = 1f;    // garante que o jogo não começa pausado
        GameData.ResetResult();
        ElapsedTime = 0f;
        Progress = 0f;
        IsPaused = false;
        IsRacing = true;
    }

    void Update()
    {
        if (!IsRacing) return;

        // Time.deltaTime vale 0 com o jogo pausado, então o cronômetro para sozinho.
        ElapsedTime += Time.deltaTime;

        // Progresso: onde o carro está (eixo Z) entre a largada e a chegada.
        // Funciona porque nossa pista é uma reta ao longo do eixo Z.
        Progress = Mathf.Clamp01(Mathf.InverseLerp(startPoint.position.z, finishLine.position.z, car.transform.position.z));

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

    // ---------- Pausa ----------

    public void SetPaused(bool paused)
    {
        if (!IsRacing) return;
        IsPaused = paused;
        Time.timeScale = paused ? 0f : 1f;
    }

    public void TogglePause()
    {
        SetPaused(!IsPaused);
    }

    // ---------- Chegada ----------

    // Chamado pelo FinishTrigger quando o carro cruza a linha.
    public void FinishRace()
    {
        if (!IsRacing) return;

        IsRacing = false;
        Progress = 1f;
        car.Stop();

        GameData.FinalTime = ElapsedTime;
        GameData.FinalScore = GameData.CalculateScore(ElapsedTime);
        GameData.FinalMedal = GameData.GetMedal(ElapsedTime);

        Debug.Log("Corrida finalizada! Tempo: " + GameData.FormatTime(GameData.FinalTime)
                  + " | Pontos: " + GameData.FinalScore
                  + " | Medalha: " + GameData.FinalMedal);

        StartCoroutine(LoadResultAfterDelay());
    }

    private IEnumerator LoadResultAfterDelay()
    {
        yield return new WaitForSecondsRealtime(delayBeforeResult);
        SceneManager.LoadScene(GameData.ResultScene);
    }

    // ---------- Botões do menu de pausa ----------

    public void RestartRace()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(GameData.RaceScene);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(GameData.MainMenuScene);
    }
}
