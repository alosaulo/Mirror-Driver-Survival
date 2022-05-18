using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TiroCoxinha : NetworkBehaviour
{
    public float velocidade;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = rb.transform.forward * velocidade;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        NetworkServer.Destroy(gameObject);
        Destroy(gameObject);
    }

}
