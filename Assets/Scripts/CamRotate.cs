using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotate : MonoBehaviour
{
    //필요속성 : 회전속도
    public float rotSpeed = 205f;

    // 우리가 직접 각도를 관리하자
    public float mx;
    public float my;

    // Start is called before the first frame update
    void Start()
    {
        // 시작할 때 사용자가 정해준 각도 값으로 세팅하기
        mx = transform.eulerAngles.y;
        my = -transform.eulerAngles.x;
    }

    // Update is called once per frame
    void Update()
    {
        // 사용자의 마우스 입력에 따라 물체를 상하좌우로 회전시키고 싶다.
        // 3. 사용자의 입력에 따라
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");

        mx += h * rotSpeed * Time.deltaTime;
        my += v * rotSpeed * Time.deltaTime;

        // -60 ~ 60 으로 각도 제한 걸기
        // x축 -> pitch, y -> Yaw, z -> Roll
        my = Mathf.Clamp(my, -60, 60);

        transform.eulerAngles = new Vector3(-my,mx,0);
        
        // 2. 방향이 필요
        //Vector3 dir = new Vector3(-v,h,0);
        
        // 1. 회전하고 싶다.
        //transform.eulerAngles += dir * rotSpeed * Time.deltaTime;

    }
}
