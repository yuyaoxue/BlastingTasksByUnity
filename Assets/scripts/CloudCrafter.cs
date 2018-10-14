using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudCrafter : MonoBehaviour
{
    public int numClouds = 40;//要创建云朵的数量
    public GameObject[] CloudPrefabs;//云朵预设的数组
    public Vector3 cloudPosMin;// 云朵位置的下限
    public Vector3 cloudPosMax;//云朵位置的上限
    public float cloudScaleMin = 1;//云朵最小的缩放比例
    public float cloudScaleMax = 2;//云朵最大的缩放比例
    public float cloudSpeedMult = 0.5f;//设置云朵速度

    [HideInInspector]
    public GameObject[] cloudInstances;
    private void Awake()
    {
        InitCloud();
    }
    private void InitCloud()
    {
        cloudInstances = new GameObject[numClouds];//创建一个数组，用于存储所有云朵的实例
        GameObject anchor = GameObject.Find("CloudAnchor");

        GameObject cloud;
        for (int i = 0; i < numClouds; i++)
        {
            int prefabNum = Random.Range(0, CloudPrefabs.Length);
            cloud = Instantiate(CloudPrefabs[prefabNum]) as GameObject;
            //设置云朵位置
            Vector3 cPos = Vector3.zero;
            cPos.x = Random.Range(cloudPosMin.x, cloudPosMax.x);
            cPos.y = Random.Range(cloudPosMin.y, cloudPosMax.y);

            //设置云朵缩放比例
            float scaleU = Random.value;
            float scaleVal = Mathf.Lerp(cloudScaleMin, cloudScaleMax, scaleU);
            //较小的云朵（即 scaleU 值较小）离地面较近
            cPos.y = Mathf.Lerp(cloudPosMin.y, cPos.y, scaleU);
            //较小的云朵距离较远
            cPos.z = 100 - 90 * scaleU;


            cloud.transform.position = cPos;
            cloud.transform.localScale = Vector3.one * scaleVal;
            cloud.transform.SetParent(anchor.transform);
            cloudInstances[i] = cloud;
        }
    }

    private void Update()
    {
        CloudMove();
    }

    private void CloudMove()
    {
        foreach (GameObject cloud in cloudInstances)
        {
            float scaleVal = cloud.transform.localScale.x;
            Vector3 cPos = cloud.transform.position;

            //云朵越大，移动速度越快
            cPos.x -= scaleVal * Time.deltaTime * cloudSpeedMult;

            //如果云朵已经位于画面左侧较远位置
            if (cPos.x <= cloudPosMin.x)
            {
                // 则将他放置到最右侧
                cPos.x = cloudPosMax.x;
            }

            cloud.transform.position = cPos;
        }
    }
}
