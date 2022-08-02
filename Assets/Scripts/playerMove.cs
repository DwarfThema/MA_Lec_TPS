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
    public float gravity = -9.81f;
    public float jumpPower = 10;
    float yVelocity;

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

        if (Input.GetButtonDown("Jump"))
        {
            yVelocity = jumpPower;
        }

        Vector3 dir = Vector3.right * h + Vector3.forward * v;
        dir.Normalize();

        //캐릭터 컨트롤러 사용하면 하늘로 그냥 날아가버린다. 중력을 적용하자.
        dir = Camera.main.transform.TransformDirection(dir);
        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;

        //수직항력 구현
        if(cc.isGrounded){
            yVelocity = 0;
        }

        cc.Move(dir * speed * Time.deltaTime);
        //transform.position += dir * speed * Time.deltaTime;
    }
}
