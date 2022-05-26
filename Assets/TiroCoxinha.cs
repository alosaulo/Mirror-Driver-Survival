using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TiroCoxinha : NetworkBehaviour
{
    public float tempoDestruicao;
    public float velocidade;
    Rigidbody rb;

    bool colidi = false;
    float contadorColisao;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(rb.transform.forward * velocidade);
        StartCoroutine("ContadorDestruidor");
    }

    // Update is called once per frame
    void Update()
    {
        if (colidi == true) {
            contadorColisao = Time.deltaTime;
            if (contadorColisao == 0.5) {
                NetworkServer.Destroy(gameObject);
            }
        }
    }

    [ServerCallback]
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Carro") 
        {
            CarControllerNetwork carControllerNetwork = collision.gameObject.GetComponent<CarControllerNetwork>();
            carControllerNetwork.DarDano(1);
            colidi = true;
        }
    }

    IEnumerator ContadorDestruidor() {
        yield return new WaitForSeconds(tempoDestruicao);
        NetworkServer.Destroy(gameObject);
    }

}
