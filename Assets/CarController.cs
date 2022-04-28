using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float torque;

    public float direcao;

    public Vector3 centroDeGravidade;

    Rigidbody myBody;

    public WheelCollider[] Rodas;


    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody>();
        myBody.centerOfMass = centroDeGravidade;
    }

    // Update is called once per frame
    void Update()
    {
        Acelerar();
        Virar();
        Debug.Log(myBody.velocity.magnitude * 3.6);

    }

    void Acelerar() {
        float vAxis = Input.GetAxis("Vertical");
        Rodas[0].motorTorque = vAxis * torque;
        Rodas[1].motorTorque = vAxis * torque;
        Rodas[2].motorTorque = vAxis * torque;
        Rodas[3].motorTorque = vAxis * torque;
    }

    void Virar() {
        float hAxis = Input.GetAxis("Horizontal");
        Rodas[0].steerAngle = hAxis * direcao;
        Rodas[1].steerAngle = hAxis * direcao;
    }

}
