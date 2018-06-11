using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flyer : MonoBehaviour
{

    private enum EState
    {
        NONE,

        IDLE,
        FALLING,
        DEAD,
    }

    private EState      m_eCurState = EState.NONE;

    private Rigidbody   m_comRigidBody;
    private Animator    m_comAnimaotr;

    void Awake()
    {
        m_comRigidBody = GetComponent<Rigidbody>();
        m_comAnimaotr = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // (수정) 상태바꾸는 걸 다른 곳에서 하자.
    // https://m.blog.naver.com/PostView.nhn?blogId=lidoukens&logNo=220685106163&scrapedType=1&scrapedLog=221296803883&scrapedOpenType=2
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Floor":
                m_eCurState = EState.DEAD;
                break;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        switch (other.collider.tag)
        {
            case "Floor":
                m_eCurState = EState.DEAD;
                UpdateAnimation();  // 그럼 이걸 불러줄 타이밍은..?
                // 애니메이션 바뀌고 상태를 업뎃하면
                // 상태에 따라 애니메이션을 업뎃 못 함 ㅋㅋㅋㅋ
                break;
        }
    }

    private void UpdateAnimation()
    {
        switch (m_eCurState)
        {
            case EState.NONE:
                break;

            case EState.FALLING:
                m_comRigidBody.useGravity = true;
                break;

            case EState.DEAD:
                m_comAnimaotr.Play("FA_Dead");
                m_comRigidBody.useGravity = false;
                // 그 이후로 상태 어떻게 다시 업뎃 시키지?
                // 데스에서 다시 살아날 때 무적 시간도 줘야함
                break;
        }
    }

    private void EndAnimation()
    {
        switch (m_eCurState)
        {
            case EState.NONE:
                break;

            case EState.FALLING:
                break;

            case EState.DEAD:
                Debug.Log("!!");
                break;
        }
    }
}
