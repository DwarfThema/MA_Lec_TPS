using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 사용자 입력에 따라 앞뒤좌우로 이동하고 싶다.
// 필요속성 : 이동속도
// CharacterController 를 이용해 이동시키고 싶다.
// 필요속성 : Character Controller
// 중력을 적용시키고 싶다.
// 필요속성 : 중력값, 수직속도
// 사용자가 점프버튼을 누르면 점프하고 싶다.
// 필요속성 : 점프파워
public class PlayerMove : MonoBehaviour
{
    // 필요속성 : 이동속도
    public float speed = 5;
    // 필요속성 : Character Controller
    CharacterController cc;

    // 필요속성 : 중력값, 수직속도
    public float gravity = -20;
    float yVelocity = 0;
    // 필요속성 : 점프파워
    public float jumpPower = 5;

    // 점프중인지 여부
    bool isJumping = false;
    int isJumpingcount = 0;
    public int maxJumpCount = 2;
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // 사용자 입력에 따라 앞뒤좌우로 이동하고 싶다.
        // 1. 사용자의 입력에 따라
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        // 2. 방향이 필요
        Vector3 dir = new Vector3(h, 0, v);
        // -> 내가 바라보는 방향을 기준으로 가고싶다.
        dir = Camera.main.transform.TransformDirection(dir);
        // v = v0 + at
        // 수직속도 구하기
        yVelocity += gravity * Time.deltaTime;

        // 만약 바닥에 닿아있다면
        if (cc.collisionFlags == CollisionFlags.Below)
        {
            // -> 수직속도를 0으로 하고 싶다.
            yVelocity = 0;
            //isJumping = false;
            isJumpingcount = 0;
        }

        // 점프를 안하고 있을 때 그리고
        // 사용자가 점프버튼을 누르면 점프하고 싶다.
        if (Input.GetButtonDown("Jump") && isJumpingcount < maxJumpCount)
        {
            // -> 수직속도를 변경하고 싶다.
            yVelocity = jumpPower;
            //isJumping = true;
            isJumpingcount++;
        }

        dir.y = yVelocity;
        // 3. 이동하고 싶다.
        // P = P0 + vt
        cc.Move(dir * speed * Time.deltaTime);
    }
}
