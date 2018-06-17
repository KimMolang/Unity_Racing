using UnityEngine;


public class ItemFeather : MonoBehaviour {

    private const float m_fRotatationSpped = 90f;

	void Update ()
    {
        transform.Rotate(new Vector3(0f, m_fRotatationSpped * Time.deltaTime, 0f), Space.Self);
	}
}
