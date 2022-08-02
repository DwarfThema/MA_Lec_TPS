using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    //사용자 입력에 따라 앞뒤 좌우로 이동하고 싶다.
    //필요속성 : 이동속도

    //CharacterController 를 이용해 이동시키고 싶다.
    // 필요속성 : Character Controller
public class playerMove : MonoBehaviour
{
    // 필요속성 : 이동속더
    public float speed = 5f;

    //필요속성 : Character Controller
    CharacterController cc;

    Vector3 dir;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = Vector3.right * h + Vector3.forward * v;
        dir.Normalize();

        transform.position += dir * speed * Time.deltaTime;
    }
}
