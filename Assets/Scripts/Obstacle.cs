using UnityEngine;

/// <summary>
/// Marca um objeto como OBSTÁCULO. O CarController procura este componente ao colidir.
/// Salvar em: Assets/Scripts/Obstacle.cs
/// Anexar em: cada obstáculo da pista (o objeto precisa ter um Collider).
/// </summary>
public class Obstacle : MonoBehaviour
{
    [Tooltip("0.4 = depois da batida o carro fica com 40% da velocidade que tinha")]
    [Range(0f, 1f)]
    public float speedMultiplier = 0.4f;
}
