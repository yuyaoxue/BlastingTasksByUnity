using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class ProjectileLine : MonoBehaviour
{
    public static ProjectileLine Instance;

    public float minDist = 0.1f;


    [SerializeField]
    private LineRenderer line;

    private GameObject _poi;

    public List<Vector3> points;


    void Awake()
    {
        Instance = this;

        line.enabled = false;
        points = new List<Vector3>();
    }


    private void FixedUpdate()
    {
        if(poi == null)
        {
            if(FollowCam.Instance.poi != null)
            {
                if(FollowCam.Instance.poi.tag == GameConst.Tag_Projectile)
                {
                    poi = FollowCam.Instance.poi;

                }else
                {
                    return;//如果未找到兴趣点，则返回
                } 
            }
        }

        if (poi == null)
        {
            return;
        }

        //如果存在兴趣点，则在 FixedUpdate 中在其位置上增加一个点
        AddPoint();
        if(poi.GetComponent<Rigidbody>().IsSleeping())
        {
            // 当兴趣点静止时，将其清空（设置为 null）
            poi = null;
            Debug.Log("当兴趣点静止时，将其清空（设置为 null）");
        }
    }

    public GameObject poi
    {
        get
        {
            return _poi;
        }
        set
        {
            _poi = value;
            if(_poi != null)
            {
                // 当把 _poi 设置为新对象时，将复位其所有内容

                line.enabled = false;
                points = new List<Vector3>();
                AddPoint();
            }
        }
    }
    public void AddPoint()
    {
       
        //用于在线条上添加一个点
        Vector3 pt = _poi.transform.position;
        if (points.Count > 0 && (pt - lastPoint).magnitude < minDist)
        {
            //如果该点与上一个点的位置不够远，则返回
            return;
        }

        if(points.Count == 0)
        {
            Vector3 launchPos = Slingshot.Instance.launchPoint.transform.position;
            Vector3 launchPosDiff = pt - launchPos;

            // ... 则添加一根线条，帮助之后瞄准
            points.Add(pt + launchPosDiff);
            points.Add(pt);
            line.positionCount = 2;

            //设置前两个点
            line.SetPosition(0,points[0]);
            line.SetPosition(1,points[1]);

            //启用线渲染器
            line.enabled = true;
        }
        else
        {
            //正常添加点的操作
            points.Add(pt);

            line.positionCount = points.Count;
            line.SetPosition(points.Count - 1, lastPoint);
            line.enabled = true;
        }
    }


    public Vector3 lastPoint
    {
        get
        {
            if(points == null)
            {
                // 如果当前还没有点，返回 Vector3.zero
                return Vector3.zero;
            }
            return points[points.Count - 1];
        }
    }
    public void Clear()
    {
        _poi = null;
        line.enabled = false;
        points = new List<Vector3>();
    }
}
