using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using Cinemachine;

public class CarControllerNetwork : NetworkBehaviour
{

    int maxHealth;

    [SyncVar(hook = "MudarCopo")]
    public int Health;

    public Image HPImg;

    [SyncVar(hook = "ColorMe")] public Color32 color;

    public GameObject myCamera;

    public float torque;

    public float direcao;

    public Vector3 centroDeGravidade;

    Rigidbody myBody;

    public WheelCollider[] Rodas;

    public float cooldownTiro;

    float contaTiro;

    //0 tiro no lado esquerdo
    //1 tiro no lado direito
    byte lado = 0;

    public Transform origemTiro1;
    public Transform origemTiro2;

    public GameObject tiroPrefab;

    public Renderer myRender;


    public override void OnStartServer()
    {
        base.OnStartServer();
        color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = Health;
        myBody = GetComponent<Rigidbody>();
        myBody.centerOfMass = centroDeGravidade;
        if (isLocalPlayer) {
            myCamera.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer) { 
            Acelerar();
            Virar();
            if (Input.GetButton("Fire1") && contaTiro >= cooldownTiro) 
            {
                CmdSpawnTiro();
                contaTiro = 0;
            }
            if(contaTiro < cooldownTiro) 
            { 
                contaTiro += Time.deltaTime;
            }
        }
        //Debug.Log(myBody.velocity.magnitude * 3.6);
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

    private void ColorMe(Color32 oldColor, Color32 newColor)
    {
        /*if (myRender == null)
            myRender = GetComponent<Renderer>();*/
        myRender.material.color = newColor;
    }

    private void MudarCopo(int oldHealth, int newHealth)
    {
        RpcMudarImage();
    }

    [ClientRpc]
    private void RpcMudarImage() {
        HPImg.fillAmount = (float)Health / (float)maxHealth;
    }

    [Command]
    void CmdSpawnTiro() {
        GameObject coxinha;
        if (lado == 0)
        {
            coxinha = Instantiate(tiroPrefab, origemTiro1.position, origemTiro1.rotation);
            lado = 1;
        }
        else 
        {
            coxinha = Instantiate(tiroPrefab, origemTiro2.position, origemTiro2.rotation);
            lado = 0;
        }
        NetworkServer.Spawn(coxinha);
    }

    [ClientRpc]
    void RpcMudarVida(int dano) {
        Health -= dano;
    }

    public void DarDano(int dano) {
        RpcMudarVida(dano);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    void OnDestroy()
    {
        Destroy(myRender);
    }

}
