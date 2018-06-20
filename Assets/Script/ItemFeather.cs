using UnityEngine;


public class ItemFeather : MonoBehaviour {

    private const float rotatationSpped = 90f;

	void Update ()
    {
        transform.Rotate(new Vector3(0f, rotatationSpped * Time.deltaTime, 0f), Space.Self);
	}
}
