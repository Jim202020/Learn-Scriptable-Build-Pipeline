using UnityEngine;

public class TestCamera : MonoBehaviour
{
    public Camera camera;
    public GameObject go1;
    // Start is called before the first frame update
    void Start()
    {
        TestWorldToViewportPoint();
        TestWorldToCameraMatrix();
    }

    void TestWorldToViewportPoint()
    {
        print("WorldToViewportPoint:" + camera.WorldToViewportPoint(go1.transform.position));
    }

    void TestWorldToCameraMatrix()
    {
        print("worldToCameraMatrix: \n" + camera.worldToCameraMatrix);

        var pos = camera.transform.position;
        var eulerAngles = camera.transform.eulerAngles;

        print("eulerAngles: \n" + eulerAngles);

        //var matrix = Matrix4x4.TRS(-pos,Quaternion.Euler(-eulerAngles.x,-eulerAngles.y,-eulerAngles.z),new Vector3(1f,1f,1f));
        //print("worldToCameraMatrix 2: \n" + matrix);
        var matrix = Matrix4x4.Scale(new Vector3(1f,1f,-1f)) * Matrix4x4.Rotate(Quaternion.Euler(eulerAngles.x,eulerAngles.y,eulerAngles.z)).inverse * Matrix4x4.Translate(-pos);
        print("worldToCameraMatrix 2: \n" + matrix);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
