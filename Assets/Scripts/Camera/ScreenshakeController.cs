using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshakeController : MonoBehaviour
{

    private float shakeTimeLeft, shakePower;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            ShakeStart(.5f, 1f);
        }    
    }

    private void LateUpdate()
    {
        if (shakeTimeLeft > 0)
        {
            shakeTimeLeft -= Time.deltaTime;

            float xAmount = Random.Range(-1f, 1f) * shakePower;
            float yAmount = Random.Range(-1f, 1f) * shakePower;

            transform.position += new Vector3(xAmount, yAmount, 0f);
        }
    }

    public void ShakeStart(float length, float power)
    {
        shakeTimeLeft = length;
        shakePower = power;
    }
}
