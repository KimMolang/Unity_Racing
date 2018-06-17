using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    /*
    // (수정)
    // enum value 카메라 상태
    //public Vector3 CamRotation = new Vector3(45.0f, -45.0f, 0.0f);

    // (수정) 이건 나중에 메이플 스토리 카메라 처럼, 길이 판정 할 것임.
    public Vector3 CamPosition = new Vector3(0, 5, -5);

    // (수정) 크리티컬 맞으면 카메라 흔들리는 효과

    private Transform targetTranstorm = null;

    public void SetTargetTransfrom( GameObject _target )
    {
        targetTranstorm = _target.transform;
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void LateUpdate()
    {
        // (수정) 이 리턴문은 null 객체를 만들어서 없앨 것이다.
        if (targetTranstorm == null)
            return;

        targetTranstorm.position = targetTranstorm.position + CamPosition;
        //targetTranstorm.localRotation = Quaternion.Euler(CamRotation);
    }
    */
}
