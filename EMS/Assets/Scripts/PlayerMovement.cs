using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 20f;
    public float speed_move = 10f;

    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;
    
    //Animator m_Animator;
    Rigidbody m_Rigidbody;
    //AudioSource m_AudioSource;


    void Start()
    {
        //m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        //m_AudioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate() // 초당 50번 호출
    {
        float keyH = Input.GetAxis("Horizontal");
        float keyV = Input.GetAxis("Vertical");
        keyH = keyH * speed_move * Time.deltaTime;
        keyV = keyV * speed_move * Time.deltaTime;
        transform.Translate(Vector3.right * keyH);
        transform.Translate(Vector3.forward * keyV);


        /*float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        m_Movement.Set(horizontal, 0f, vertical); // x, y, z
        m_Movement.Normalize(); // 정규화

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;*/

        //m_Animator.SetBool("IsWalking", isWalking); // 애니메이션 파라미터 연결

        /*if (isWalking)
        {
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
        }
        else
        {
            m_AudioSource.Stop();
        }*/

        /*Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);*/
    }

    /*private void OnAnimatorMove()
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * speed_move); // deltaPosition : 프레임당 포지션의 이동량
        m_Rigidbody.MoveRotation(m_Rotation);
    }*/
}
