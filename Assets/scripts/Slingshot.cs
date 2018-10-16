using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{

    public static Slingshot Instance;
    [SerializeField]
    public GameObject launchPoint;
    [SerializeField]
    private GameObject prefabProjectile;

    private GameObject projectile;

    public Vector3 launchPos;
    private bool aimingMode;

    private float velocityMult = 10;

    private void Awake()
    {
        Instance = this;

        launchPoint.SetActive(false);
        launchPos = launchPoint.transform.position;
    }

    private void Update()
    {
        //如果弹弓处于瞄准模式，则跳过以下代码
        if (!aimingMode) return;
        Vector3 mousePos2D = Input.mousePosition;

        // 将鼠标光标位置转换为三维世界坐标
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);
        //计算 launch 到 mousePos3D 两点之间的坐标差
        Vector3 mouseDelta = mousePos3D - launchPos;

        //将 mouseDelta 坐标差限制在弹弓的球状碰撞器半径范围内
        float maxMagnitude = this.GetComponent<SphereCollider>().radius;

        if(mouseDelta.magnitude > maxMagnitude)
        {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }

        //将 projecttitle 移动到新位置
        Vector3 projPos = launchPos + mouseDelta;
        projectile.transform.position = projPos;
        if(Input.GetMouseButtonUp(0))
        {
            aimingMode = false;
            projectile.GetComponent<Rigidbody>().isKinematic = false;
            projectile.GetComponent<Rigidbody>().velocity = -mouseDelta * velocityMult;
            //设置 摄像机要跟随的物体引用
            FollowCam.Instance.poi = projectile;
            projectile = null;
        }
    }

    private void OnMouseEnter()
    {
        Debug.Log("OnMouseEnter");
        launchPoint.SetActive(true);
    }

    private void OnMouseExit()
    {
        Debug.Log("OnMouseExit");
        launchPoint.SetActive(false);
    }

    private void OnMouseDown()
    {
        aimingMode = true;
        projectile = Instantiate(prefabProjectile) as GameObject;
        projectile.transform.position = launchPos;
        projectile.GetComponent<Rigidbody>().isKinematic = true;
    }
}
