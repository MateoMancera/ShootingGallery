using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimientoPersonaje : MonoBehaviour
{
   
    [SerializeField] private ajustesMovimiento _ajustes = null;

    private Vector3 _direccionMovimiento;
    private CharacterController _controlador;

    private void Awake()
    {
        _controlador = GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movimientoDefault();
        _controlador.Move(_direccionMovimiento * Time.deltaTime);
    }

    private void FixedUpdate()
    {
    }

    private void movimientoDefault()
    {
        if (_controlador.isGrounded)
        {
            Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            if (input.x != 0 && input.y != 0)
            {
                input *= 0.777f;
            }

            _direccionMovimiento.x = input.x * _ajustes.speed;
            _direccionMovimiento.z = input.y * _ajustes.speed;
            _direccionMovimiento.y = -_ajustes.antiBump;

            _direccionMovimiento = transform.TransformDirection(_direccionMovimiento);

            if (Input.GetKey(KeyCode.Space))
            {
                salto();
            }
        }
        else
        {
            _direccionMovimiento.y -= _ajustes.gravity * Time.deltaTime;
        }
    }

    private void salto()
    {
        _direccionMovimiento.y += _ajustes.jumpForce;
    }
}
