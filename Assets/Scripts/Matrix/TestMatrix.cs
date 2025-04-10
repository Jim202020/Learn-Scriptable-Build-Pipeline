using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class TestMatrix : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        print("Translate:" + Matrix4x4.Translate(new Vector3(10, 20, 30)));

        var go1 = AddGo1();
        AddGo2(go1);
    }

    GameObject AddGo1()
    {
        var go = new GameObject("go1");
        var transform = go.transform;
        transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        transform.rotation = Quaternion.Euler(30, 30, 30);
        transform.position = new Vector3(20, 30, 40);
        return go;
    }

    void AddGo2(GameObject go1)
    {
        print("=======addGo2=======");
        var go = new GameObject("go2");
        var transform = go.transform;
        transform.parent = go1.transform;
        transform.localRotation = Quaternion.Euler(20, 20, 20);
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        transform.localPosition = new Vector3(10, 20, 30);
        print("localScale: " + transform.localScale);
        print("lossyScale: " + transform.lossyScale);

        var p_mr = Matrix4x4.Rotate(go1.transform.localRotation);
        var p_ms = Matrix4x4.Scale(go1.transform.localScale);
        var p_mp = Matrix4x4.Translate(go1.transform.localPosition);

        var p_mr_inverse = p_mr.inverse;
        var p_ms_inverse = p_ms.inverse;
        var p_mp_inverse = p_mp.inverse;

        var mr = Matrix4x4.Rotate(transform.localRotation);
        var ms = Matrix4x4.Scale(transform.localScale);
        var mp = Matrix4x4.Translate(transform.localPosition);

        var mr_inverse = mr.inverse;
        var ms_inverse = ms.inverse;
        var mp_inverse = mp.inverse;

        print("localToWorldMatrix: \n" + transform.localToWorldMatrix);
        print("======custom localToWorldMatrix:\n" + p_mp * p_mr * p_ms * mp * mr * ms);

        print("worldToLocalMatrix: \n" + transform.worldToLocalMatrix);
        print("======inverse worldToLocalMatrix:\n" + (p_mp * p_mr * p_ms * mp * mr * ms).inverse);
        print("======custom inverse worldToLocalMatrix:\n" + ms_inverse * mr_inverse * mp_inverse * p_ms_inverse * p_mr_inverse * p_mp_inverse);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
