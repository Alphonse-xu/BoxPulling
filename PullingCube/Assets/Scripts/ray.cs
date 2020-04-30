using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ray : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //获取player的position信息
        Vector3 pos = player.transform.position;
        //射线碰撞物信息接收
        RaycastHit hit;

        //第一种写法（完全按照参数的写法）
        //Bool isHit =Physics.Raycast(pos, player.transform.forward,
        //   out hit, 5f, 1 << 0, QueryTriggerInteraction.Collide);
        
        //第二种写法
        //我们可以先创建出一条射线
        Ray ray = new Ray(pos, player.transform.forward);
        //然后用这条射线去做射线检测
        //射线，输出碰撞体信息，射线长度，射线在那一层检测，射线不忽略开启触发器(istrigger)的碰撞体
        bool isHit = Physics.Raycast(ray, out hit, 5f, 1 << 0, QueryTriggerInteraction.Collide);
        //在unity窗口画出射线        
        Debug.DrawLine(pos,new Vector3(pos.x,pos.y,pos.z + 5) ,Color.red);
        
        if(isHit)//如果射线检测到物体
        {
            //打印出被检测物体的名字
            print(hit.transform.name);
        }
    }
}
