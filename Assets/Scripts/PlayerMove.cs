using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float XMove;
    [SerializeField] private float YMove;
    [SerializeField] private float ZMove;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > 10 || transform.position.x < -10)
        {
            XMove *= -1;
        }

        if (transform.position.z > 10 || transform.position.z < -10)
        {
            ZMove *= -1;
        }

        if (transform.position.y > 10 || transform.position.y < -10)
        {
            YMove *= -1;
        }
        transform.Translate(XMove, YMove, ZMove);
    }
}
