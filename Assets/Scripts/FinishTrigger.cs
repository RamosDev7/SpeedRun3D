using UnityEngine;

/// <summary>
/// Área invisível da linha de chegada. Quando o carro entra nela, a corrida termina.
/// Salvar em: Assets/Scripts/FinishTrigger.cs
/// Anexar em: GameObject vazio "FinishTrigger" posicionado na chegada.
/// Ao adicionar o script, o Unity cria o Box Collider e já marca "Is Trigger".
/// </summary>
[RequireComponent(typeof(BoxCollider))]
public class FinishTrigger : MonoBehaviour
{
    public RaceManager raceManager;

    // Reset() roda UMA vez, quando o componente é adicionado no Editor.
    void Reset()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (raceManager == null) return;

        // O collider do carro está no objeto que tem o CarController.
        if (other.GetComponentInParent<CarController>() != null)
        {
            raceManager.FinishRace();
        }
    }
}
