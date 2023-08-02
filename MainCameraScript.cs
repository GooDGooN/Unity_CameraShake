using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraScript : MonoBehaviour
{
    private Vector3 shakeDeltaPos;
    private Vector3 cameraPos;
    private float cameraMoveSpeed = 5.0f;
    private Coroutine shakeCoroutine;
    
    private void Start()
    {
        cameraPos = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position = cameraPos + shakeDeltaPos;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShakeCamera(Random.Range(5.0f, 30.0f), 0.05f);
        }
        MoveCamera();
    }

    private void MoveCamera()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            cameraPos += Vector3.left * cameraMoveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            cameraPos += Vector3.right * cameraMoveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            cameraPos += Vector3.forward * cameraMoveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            cameraPos += Vector3.back * cameraMoveSpeed * Time.deltaTime;
        }
    }

    /// <summary>
    /// Shake!
    /// </summary>
    /// <param name="shakePower"> > 0.0f </param>
    /// <param name="duration"> 0 ~ 1.0f </param>
    private void ShakeCamera(float shakePower, float duration)
    {
        if (shakeCoroutine != null)
        {
            StopCoroutine(shakeCoroutine);
        }
        shakeCoroutine = StartCoroutine(ShakingCamera(shakePower, duration));
    }

    private IEnumerator ShakingCamera(float shakePower, float duration)
    {
        var nowShake = shakePower;
        while (true) 
        {
            if (nowShake > 0.01f)
            {
                var rx = Random.Range(-nowShake, nowShake);
                var ry = Random.Range(-nowShake, nowShake);
                var rz = Random.Range(-nowShake, nowShake);
                shakeDeltaPos = new Vector3(rx, ry, rz);
                nowShake = Mathf.Lerp(nowShake, 0.0f, duration);
                yield return null;
            }
            else
            {
                shakeDeltaPos = Vector3.zero;
                break;
            }
        }
    }
}
