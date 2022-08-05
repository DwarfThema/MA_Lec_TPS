
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region 상태정의
    enum EnemyState
    {
        Idle,
        Move,
        Attack,
        Damage,
        Die
    };

    EnemyState m_state = EnemyState.Idle;
    #endregion

    #region Idle 속성
    // 필요속성 : 대기시간, 경과시간
    public float idleDelayTime = 2;
    float currentTime = 0;
    #endregion

    #region Move 속성
    // 필요속성 : 타겟, 이동속도, CharacterController
    public Transform target;
    public float speed = 5;
    CharacterController cc;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        print("state : " + m_state);
        switch(m_state)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Move:
                Move();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Damage:
                Damage();
                break;
            case EnemyState.Die:
                Die();
                break;
        }
    }

    // 일정시간이 지나면 상태를 이동으로 전환하고 싶다.

    private void Idle()
    {
        // 일정시간이 지나면 상태를 이동으로 전환하고 싶다.
        // 1. 시간이 흘렀으니까
        currentTime += Time.deltaTime;
        // 2. 시간이 됐으니까
        if (currentTime > idleDelayTime)
        {
            // 3. 상태를 이동으로 전환
            m_state = EnemyState.Move;
            currentTime = 0;
        }
    }

    // 타겟쪽으로 이동하고 싶다.
    // 타겟 방향으로 회전하고 싶다.
    // 타겟이 공격범위안에 들어오면 상태를 공격으로 전환하고 싶다.
    // 필요속성 : 공격범위
    public float attackRange = 2;
    private void Move()
    {
        // 타겟쪽으로 이동하고 싶다.
        // 1. 방향이필요(target - me)
        Vector3 dir = target.position - transform.position;
        float distance = dir.magnitude;
        dir.Normalize();
        dir.y = 0;
        // 2. 이동하고 싶다.
        cc.SimpleMove(dir * speed);
        // Enemy 의 방향을 dir 로 하자
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);

        // 타겟이 공격범위안에 들어오면 상태를 공격으로 전환하고 싶다.
        if(distance < attackRange)
        {
            m_state = EnemyState.Attack;
        }
    }

    // 일정시간에 한번씩 공격하고 싶다.
    // 필요속성 : 공격대기시간
    // 타겟이 공격범위를 벗어나면 상태를 이동으로 전환하고 싶다.
    public float attackDelayTime = 2;
    private void Attack()
    {
        currentTime += Time.deltaTime;
        if(currentTime > attackDelayTime)
        {
            currentTime = 0;
            print("attack!!!");
        }
        // 타겟이 공격범위를 벗어나면 상태를 이동으로 전환하고 싶다.
        float distance = Vector3.Distance(target.position, transform.position);
        if(distance > attackRange)
        {
            m_state = EnemyState.Move;
        }
    }

    // 피격 이벤트 받아서 처리 함수
    // 3대맞으면 죽도록 처리하자
    // 필요속성 : 체력
    // 체력이 남아있으면 상태를 피격으로 전환하고 싶다.
    // 그렇지않으면 상태를 죽음으로 전환하고 싶다.
    // 죽음 상태가 되면 충돌되지 않도록 처리
    public int hp = 3;
    public void OnDamageProcess()
    {
        // 3대맞으면 죽도록 처리하자
        hp--;
        // 체력이 없을 때 상태를 죽음으로 전환하고 싶다.
        if (hp <= 0)
        {
            m_state = EnemyState.Die;
            cc.enabled = false;
        }
        // 그렇지 않으면 상태를 피격으로 전환하고 싶다.
        else
        {
            m_state = EnemyState.Damage;
        }
    }

    // 일정시간 기다렸다가 상태를 대기로 전환하고 싶다.
    // 필요속성 : 피격대기시간
    public float damageDelayTime = 2;
    private void Damage()
    {
        currentTime += Time.deltaTime;
        // 2. 시간이 됐으니까
        if (currentTime > damageDelayTime)
        {
            // 3. 상태를 이동으로 전환
            m_state = EnemyState.Idle;
            currentTime = 0;
        }
    }

    // 아래로 사라지도록 하자.
    // 없어지면 제거하자. -2
    public float dieSpeed = 0.5f;
    private void Die()
    {
        transform.position += Vector3.down * dieSpeed * Time.deltaTime;
        if(transform.position.y < -3)
        {
            Destroy(gameObject);
        }
    }
}