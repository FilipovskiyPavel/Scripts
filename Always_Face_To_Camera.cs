using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Always_Face_To_Camera : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    // Start is called before the first frame update
    private void LateUpdate()
    {
        Vector3 cameraPos = _camera.transform.position;
        cameraPos.y = transform.position.y;
        transform.LookAt(cameraPos);
        transform.Rotate(0f,180f,0f);
    }
}
