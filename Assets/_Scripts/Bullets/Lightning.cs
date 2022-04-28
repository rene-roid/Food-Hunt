using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : BulletController
{
    public GameObject lightning;

    void Start()
    {
        Fire();
    }

    void Update()
    {
        
    }

    // Intantiate 4 lightning bolts 3 units away from this gameobject with a 90 angle difference
    public void Fire()
    {
        GameObject lightning1 = Instantiate(lightning, transform.position + transform.up * 3, Quaternion.identity);
        GameObject lightning2 = Instantiate(lightning, transform.position + transform.right * 3, Quaternion.identity);
        GameObject lightning3 = Instantiate(lightning, transform.position + -transform.up * 3 , Quaternion.identity);
        GameObject lightning4 = Instantiate(lightning, transform.position + -transform.right * 3, Quaternion.identity);
    }

}
