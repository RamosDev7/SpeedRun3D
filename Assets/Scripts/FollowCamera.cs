using UnityEngine;

/// <summary>
/// Câmera em terceira pessoa: fica atrás do carro e acompanha suavemente.
/// Salvar em: Assets/Scripts/FollowCamera.cs
/// Anexar em: Main Camera (RaceScene). Arraste o objeto "Car" para o campo Target.
/// </summary>
public class FollowCamera : MonoBehaviour
{
    public Transform target;

    [Header("Posição em relação ao carro (x, altura, distância)")]
    public Vector3 offset = new Vector3(0f, 4f, -8f);

    [Header("Para onde a câmera olha")]
    public float lookAheadDistance = 6f;   // olha um pouco à frente do carro
    public float lookHeight = 1f;

    [Header("Suavização (maior = mais rígido)")]
    public float positionSmooth = 6f;
    public float rotationSmooth = 8f;

    void Start()
    {
        SnapToTarget();
    }

    // LateUpdate roda depois de todos os Update: o carro já se moveu neste frame.
    void LateUpdate()
    {
        if (target == null) return;

        // Posição desejada: atrás do carro, girando junto com ele
        Vector3 desiredPosition = target.position + target.rotation * offset;
        float posT = 1f - Mathf.Exp(-positionSmooth * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, posT);

        // Rotação desejada: olhar para um ponto à frente do carro
        Vector3 lookPoint = target.position + target.forward * lookAheadDistance + Vector3.up * lookHeight;
        Quaternion desiredRotation = Quaternion.LookRotation(lookPoint - transform.position);
        float rotT = 1f - Mathf.Exp(-rotationSmooth * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotT);
    }

    // Coloca a câmera já na posição certa no início (sem "voar" até o carro).
    private void SnapToTarget()
    {
        if (target == null) return;

        transform.position = target.position + target.rotation * offset;
        Vector3 lookPoint = target.position + target.forward * lookAheadDistance + Vector3.up * lookHeight;
        transform.rotation = Quaternion.LookRotation(lookPoint - transform.position);
    }
}
