using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTransform : MonoBehaviour
{
    public GameObject rotateGo;
    public GameObject rotateRoundGo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(rotateGo != null)
        {
            rotateGo.transform.Rotate(new Vector3(1, 1, 1), Time.deltaTime * 30);
        }

        if(rotateRoundGo != null)
        {
            rotateRoundGo.transform.RotateAround(new Vector3(1,0,0), new Vector3(1, 1, 1), Time.deltaTime * 30);
        }
    }
}
