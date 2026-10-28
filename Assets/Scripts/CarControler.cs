using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    [SerializeField] private WheelCollider rodaFrenteDireitaCollider;
    [SerializeField] private WheelCollider rodaFrenteEsquerdaCollider;
    [SerializeField] private WheelCollider rodaTraseiraDireitaCollider;
    [SerializeField] private WheelCollider rodaTraseiraEsquerdaCollider;


    [SerializeField] private float velocidadeMaxima = 1500f;
    [SerializeField] private float forcaFreio = 3000f;
    [SerializeField] private float resistenciaDeRolagem = 1000f;
    [SerializeField] private float anguloMaximoDeDirecao = 30f;


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
    }

    private void LerEntradas()
    {
        Vector2 mover = acaoMover.ReadValue<Vector2>();
        
        entradaVertical = mover.y;
        entradaHorizontal = mover.x;
        estaFreando = acaoFrear.IsPressed();
    }

    private void ControlarMotorEFreio()
    {
        float forcaAtualDoMotor = entradaVertical * velocidadeMaxima;

        rodaTraseiraDireitaCollider.motorTorque = forcaAtualDoMotor;
        rodaTraseiraEsquerdaCollider.motorTorque = forcaAtualDoMotor;

        
        float freioAtual = 0f;
        
        
        if (estaFreando)
        {
            freioAtual = forcaFreio;
        }
        else if (Mathf.Abs(entradaVertical) < 0.1f)
        {
            freioAtual = resistenciaDeRolagem;
        }
        else if (entradaVertical * rodaTraseiraDireitaCollider.rpm < 0f || entradaVertical * rodaTraseiraEsquerdaCollider.rpm < 0f)
        {
            freioAtual = resistenciaDeRolagem;
        }

        rodaTraseiraDireitaCollider.brakeTorque = freioAtual;
        rodaTraseiraEsquerdaCollider.brakeTorque = freioAtual;
        rodaFrenteDireitaCollider.brakeTorque = freioAtual;
        rodaFrenteEsquerdaCollider.brakeTorque = freioAtual;
    }


    private void ControlarDirecao()
    {
        float anguloAtualDeDirecao = entradaHorizontal * anguloMaximoDeDirecao;
        rodaFrenteDireitaCollider.steerAngle = anguloAtualDeDirecao;
        rodaFrenteEsquerdaCollider.steerAngle = anguloAtualDeDirecao;   
    }
    
}
