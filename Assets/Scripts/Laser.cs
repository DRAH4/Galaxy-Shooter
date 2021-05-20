using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    public float speed = 10;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);       //having the laser speed and having it destroy enemies

        if (transform.position.y > 8 && transform.parent != null)
        {
            Destroy(transform.parent.gameObject);
        }

        else if (transform.position.y > 8)
        {
            Destroy(this.gameObject);
        }

    }
}