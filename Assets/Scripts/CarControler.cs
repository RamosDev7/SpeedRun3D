using UnityEngine;
using UnityEngine.InputSystem;

public class ControladorCarro : MonoBehaviour
{
    [SerializeField] private WheelCollider rodaFrenteEsquerdaCollider;
    [SerializeField] private WheelCollider rodaFrenteDireitaCollider;
    [SerializeField] private WheelCollider rodaTraseiraEsquerdaCollider;
    [SerializeField] private WheelCollider rodaTraseiraDireitaCollider;

    [SerializeField] private Transform rodaFrenteEsquerdaVisual;
    [SerializeField] private Transform rodaFrenteDireitaVisual;
    [SerializeField] private Transform rodaTraseiraEsquerdaVisual;
    [SerializeField] private Transform rodaTraseiraDireitaVisual;

    [SerializeField] private float forcaDoMotor = 1500f;
    [SerializeField] private float forcaDoFreio = 3000f;
    [SerializeField] private float resistenciaDeRolagem = 1000f;
    [SerializeField] private float anguloMaximoDirecao = 30f;

    [SerializeField] private InputAction acaoMover;
    [SerializeField] private InputAction acaoFrear;

    private float entradaVertical;
    private float entradaHorizontal;
    private bool estaFreando;

    void Start()
    {
        acaoMover.Enable();
        acaoFrear.Enable();
    }

    void FixedUpdate()
    {
        LerEntradas();
        ControlarMotorEFreio();
        ControlarDirecao();
        AtualizarRodaVisual(rodaFrenteDireitaCollider, rodaFrenteDireitaVisual);
        AtualizarRodaVisual(rodaFrenteEsquerdaCollider, rodaFrenteEsquerdaVisual);
        AtualizarRodaVisual(rodaTraseiraDireitaCollider, rodaTraseiraDireitaVisual);
        AtualizarRodaVisual(rodaTraseiraEsquerdaCollider, rodaTraseiraEsquerdaVisual);
    }

    private void LerEntradas()
    {
        Vector2 mover = acaoMover.ReadValue<Vector2>();
        entradaHorizontal = mover.x;
        entradaVertical = mover.y;
        estaFreando = acaoFrear.IsPressed();
    }

    private void ControlarMotorEFreio()
    {
        float forcaAtualDoMotor = entradaVertical * forcaDoMotor;
        rodaTraseiraDireitaCollider.motorTorque = forcaAtualDoMotor;
        rodaTraseiraEsquerdaCollider.motorTorque = forcaAtualDoMotor;

        float freioAtual = 0f;

        if (estaFreando)
        {
            freioAtual = forcaDoFreio;
        }
        else if (Mathf.Abs(entradaVertical) < 0.1f)
        {
            freioAtual = resistenciaDeRolagem;
        }
        else if (entradaVertical * rodaTraseiraEsquerdaCollider.rpm < 0f)
        {
            freioAtual = resistenciaDeRolagem;
        }

        rodaFrenteDireitaCollider.brakeTorque = freioAtual;
        rodaFrenteEsquerdaCollider.brakeTorque = freioAtual;
        rodaTraseiraDireitaCollider.brakeTorque = freioAtual;
        rodaTraseiraEsquerdaCollider.brakeTorque = freioAtual;
    }

    private void ControlarDirecao()
    {
        float anguloAtualDirecao = entradaHorizontal * anguloMaximoDirecao;
        rodaFrenteDireitaCollider.steerAngle = anguloAtualDirecao;
        rodaFrenteEsquerdaCollider.steerAngle = anguloAtualDirecao;
    }

    private void AtualizarRodaVisual(WheelCollider rodaCollider, Transform rodaVisual)
    {
        rodaCollider.GetWorldPose(out Vector3 posicao, out Quaternion rotacao);
        rodaVisual.position = posicao;
        rodaVisual.rotation = rotacao;
    }
}