using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 사용자가 발사버튼을 누르면 총알을 발사하고 싶다.
// 필요속성 : 총알공장, 총구
public class PlayerFire : MonoBehaviour
{
    // 필요속성 : 총알공장, 총구
    public GameObject bulletFactory;
    public Transform firePosition;
    // 총알파편
    public Transform bulletImpact;
    ParticleSystem bulletPS;
    AudioSource bulletAudio;

    // Start is called before the first frame update
    void Start()
    {
        bulletPS = bulletImpact.GetComponent<ParticleSystem>();
        bulletAudio = bulletImpact.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // 사용자가 발사버튼을 누르면 총알을 발사하고 싶다.
        // 1. 사용자가 발사버튼을 눌렀으니까
        if (Input.GetButtonDown("Fire1"))
        {
            bulletAudio.Stop();
            bulletAudio.Play();
            //ShootBullet();
            ShootRay();
        }
    }

    void ShootRay()
    {
        // Ray 를 이용해서 총알을 발사하고 싶다.
        // Ray 필요
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // RaycastHit
        RaycastHit hitInfo;

        // 플레이어만 빼고 충돌하게 하고 싶다.
        int layer = 1 << gameObject.layer;

        // Ray 던진다.
        // -> 만약 충돌했다면
        if (Physics.Raycast(ray, out hitInfo, 1000, ~layer))
        {
            // -> 부딪힌 지점에 파편효과 표시
            // 1. 파편이 부딪힌 지점으로 위치
            bulletImpact.position = hitInfo.point;
            // 부딪힌 지점이 향하는 방향으로 파편이 튀도록
            bulletImpact.forward = hitInfo.normal;
            // 2. 효과 재생
            bulletPS.Stop();
            bulletPS.Play();

            // 만약 맞은 녀석이 Enemy 라면
            Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
            if (enemy)
            {
                // -> 피격 이벤트를 호출하자
                enemy.OnDamageProcess(ray.direction);

            }
        }
    }

    private void ShootBullet()
    {
        // 2. 총알이 필요
        GameObject bullet = Instantiate(bulletFactory);
        // 3. 총알을 발사하고 싶다.
        bullet.transform.position = firePosition.position;
        bullet.transform.forward = firePosition.forward;
    }
}
