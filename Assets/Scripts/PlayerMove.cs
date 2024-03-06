using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float HorizontalMoveSpeed;
    [SerializeField] private float VerticalMoveSpeed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        float XMove = Input.GetAxis("Horizontal") * HorizontalMoveSpeed * Time.deltaTime;
        float ZMove = Input.GetAxis("Vertical") * VerticalMoveSpeed * Time.deltaTime;
        transform.Translate(XMove, 0, ZMove);
    }
}
