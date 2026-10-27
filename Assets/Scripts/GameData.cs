using UnityEngine;

/// <summary>
/// Classe ESTÁTICA: não vai em nenhum GameObject.
/// Guarda as regras de pontuação/medalhas e o resultado da corrida,
/// para que a Result Scene consiga ler o que aconteceu na Race Scene.
/// Salvar em: Assets/Scripts/GameData.cs
/// </summary>
public static class GameData
{
    // ---------- Nomes das cenas (precisam ser IGUAIS aos nomes dos arquivos de cena) ----------
    public const string MainMenuScene = "MainMenu";
    public const string RaceScene = "RaceScene";
    public const string ResultScene = "ResultScene";

    // ---------- Nomes das medalhas ----------
    public const string MedalGold = "Ouro";
    public const string MedalSilver = "Prata";
    public const string MedalBronze = "Bronze";
    public const string MedalNone = "Sem medalha";

    // ---------- Regras de pontuação (ajuste à vontade depois de testar) ----------
    // Pontuação = pontos por concluir + pontos por cada segundo que "sobrou" do tempo limite.
    public const int CompletionPoints = 1000;
    public const float TimeLimit = 90f;
    public const int PointsPerSecondLeft = 20;

    // ---------- Tempos (em segundos) para ganhar cada medalha ----------
    public const float GoldTime = 30f;
    public const float SilverTime = 40f;
    public const float BronzeTime = 55f;

    // ---------- Resultado da última corrida (lido pela Result Scene) ----------
    public static float FinalTime;
    public static int FinalScore;
    public static string FinalMedal = MedalNone;

    public static void ResetResult()
    {
        FinalTime = 0f;
        FinalScore = 0;
        FinalMedal = MedalNone;
    }

    /// <summary>Pontuação que o jogador receberia se terminasse a corrida neste tempo.</summary>
    public static int CalculateScore(float time)
    {
        float secondsLeft = Mathf.Max(0f, TimeLimit - time);
        return CompletionPoints + Mathf.RoundToInt(secondsLeft * PointsPerSecondLeft);
    }

    public static string GetMedal(float time)
    {
        if (time <= GoldTime) return MedalGold;
        if (time <= SilverTime) return MedalSilver;
        if (time <= BronzeTime) return MedalBronze;
        return MedalNone;
    }

    /// <summary>Converte segundos para o formato 01:24.58 (minutos:segundos.centésimos).</summary>
    public static string FormatTime(float time)
    {
        int totalHundredths = Mathf.FloorToInt(Mathf.Max(0f, time) * 100f);
        int minutes = totalHundredths / 6000;
        int seconds = (totalHundredths / 100) % 60;
        int hundredths = totalHundredths % 100;
        return minutes.ToString("00") + ":" + seconds.ToString("00") + "." + hundredths.ToString("00");
    }
}
