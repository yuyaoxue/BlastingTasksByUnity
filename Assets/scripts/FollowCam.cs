using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public static FollowCam Instance;

    [HideInInspector]
    public GameObject poi;//摄像机要跟随的物体

    public float easing = 0.05f;
    public Vector2 minXY;

    private float _camz;//摄像机 z 坐标
    private Camera _mainCamera;
    private void Awake()
    {
        Instance = this;
        _camz = this.transform.position.z;
        _mainCamera = GetComponent<Camera>();
    }
    private void FixedUpdate()
    {
        Vector3 destination;

        if (poi == null)
        {
            destination = Vector3.zero;
        }
        else
        {
            destination = poi.transform.position;

            if(poi.tag == "Projectile")
            {
                // 如果它处于 sleeping状态（即未移动）
                if(poi.GetComponent<Rigidbody>().IsSleeping())
                {
                    //返回默认视图
                    poi = null;
                    return;
                }
            }
            //限定 x 和 y 的最小值，避免当弹丸落地时摄像机拍摄到地面以下的画面
            destination.x = Mathf.Max(minXY.x, destination.x);
            destination.y = Mathf.Max(minXY.y, destination.y);
            destination.z = _camz;
            //在摄像机当前位置和目标位置之间增添插值
            destination = Vector3.Lerp(transform.position, destination, easing);
            //将跟随物体的坐标 赋值给 摄像机
            transform.position = destination;
            //设置 摄像机的 orthograohicSize, 使地面始终处于画面之中
            _mainCamera.orthographicSize = destination.y + 10;
        }
    }
}
