using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReleaseSkill : MonoBehaviour
{
    public GameObject doubleHand;
    public float pullspeed = 0;
    private float maxPull = 10;
    public Vector3 handPos;
    public bool pullStar = false;
    bool holdGO = false;
    private RaycastHit hit;
    public LayerMask hitMask;
    public Vector3 hitPos;

    void Update()
    {
        if (pullStar == false && holdGO == false) //如果技能没有释放，也没拿着东西
        {
            if (Input.GetMouseButtonDown(0)) //可以点击鼠标左键释放技能
            {
                Release(); //释放技能

            }
        }
        else if (holdGO == true) //如果手里拿着东西
        {
            if (Input.GetMouseButtonDown(1)) //按下鼠标右键
            {
                hit.transform.SetParent(null); //放下Cube（解除Cube和双手掌的父子关系）
                holdGO = false; //空手状态   
                
            }
        }
        pull();
        KeepPos(hit.transform); //拿稳Cube
    }

    void HandTrail(float trailTime){//trail of hand
        Transform[]  myHand = doubleHand.GetComponentsInChildren<Transform>();
        for(int i = 1;i < myHand.Length; i++){
            if(myHand[i].GetComponent<TrailRenderer>() != null) myHand[i].GetComponent<TrailRenderer>().time = trailTime;
        }

    }

    void Release(){
        //HandTrail(1);
        handPos = doubleHand.transform.position;
        pullspeed = 50;
        pullStar = true;
        Ray ray = new Ray(handPos, doubleHand.transform.forward);
        
        if(Physics.Raycast(ray, out hit,maxPull, hitMask, QueryTriggerInteraction.Collide)){
            hitPos = hit.point;
        }
    }

    void pull(){
         if (pullStar == true) //当技能处于释放状态
        {
            //双手在自身坐标z轴移动，速度方向待定
            doubleHand.transform.Translate(0, 0, Time.deltaTime * pullspeed);
            //如果射线没照到东西，双手到达最长距离后返回
            if (hit.transform == null && (doubleHand.transform.position - handPos).magnitude > maxPull)
            {
                HandTrail(0); //轨迹显示时间为0（不显示）
                pullspeed = -50; //双手往后移动
            }
            // 如果照到东西,双手接近该东西,抓住它返回
            else if (hit.transform != null && (doubleHand.transform.position - hitPos).magnitude < 0.5f)
            {
                //抓住射线打到的Cube(设为双手的子物体，跟随双手移动)
                hit.transform.parent = doubleHand.transform;       
                holdGO = true; //表示正拿着Cube           
                HandTrail(0); //隐轨迹显示时间为0（不显示）
                pullspeed = -50; //双手往后移动
            }
            //如果双手返回时接近出发时位置，则双手位置复位，停止移动
            if (pullspeed < 0 && (doubleHand.transform.position - handPos).magnitude < 0.5f)
            {
                pullspeed = 0; //移动停止
                doubleHand.transform.position = handPos; //位置复位
                pullStar = false; //技能处于未放出状态
            }
        }
    }

     void KeepPos(Transform Cube) //保持位置
    {
        if (Cube != null && Cube.parent != null)//如果Cube不为空，且有父物体（被抓住了）
        {
            Cube.position = doubleHand.transform.position; //位置固定
            Cube.rotation = doubleHand.transform.rotation; //旋转固定       
        }
    }

}
