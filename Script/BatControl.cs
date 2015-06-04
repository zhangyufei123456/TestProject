using UnityEngine;
using System.Collections;

public class BatControl : MonoBehaviour
{

    public float minSpeed = 50;
    public float maxSpeed = 65;
    private float damping = 0.8f;  //旋转弧度大小

    private Vector3 wayPoint;   //旋转目标

    private float speed;   //飞行速度

    private int counter = 0;

    private int randLeftX;  //盘旋随机点坐标
    private int randLeftY;
    private int randRightX;
    private int randRightY;

    public float distantToFace;    //当前点到人脸距离
    public int lastAttack;  //判断上次是否攻击过
    public Transform mark;  //标示盘旋点

    Vector2 vec; //攻击位置，到时用人脸位置替换

    void Start()
    {
        speed = Random.Range(minSpeed + 10, maxSpeed + 10);
        BatInit();  //初始化蝙蝠位置

        Wander();   //判断并新建盘旋点

        vec = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 3));

    }

    void Update () {
		if (!MoveControl.gamePause) {//MoveControl.gamePause暂停游戏//false-不暂停 ， true-暂停
		speed= Random.Range(minSpeed + 10, maxSpeed + 10);
		SetSpeed(speed);//设置速度
		BatMove();     //当前点向盘旋点运动

        mark.position = vec;
        BatMove();     //当前点向盘旋点运动

        GetValue();
		
        Attack(vec);    //判断是否攻击，改变盘旋点为攻击点
		}
		else{
			SetSpeed(0.0f);//在暂停时，设置蝙蝠翅膀飞动速度为0
		}
    }

    private void BatInit()
    {
        speed = Random.Range(minSpeed + 10, maxSpeed + 10);
        SetSpeed(speed * 1.5f);
        float batInitX = Random.Range(Screen.width, Screen.width * 1.5f);
        float batInitY = Random.Range(Screen.height * 0.7f, Screen.height);
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(batInitX, batInitY, 700));
    }

    private void BatMove()
    {
        transform.position += transform.TransformDirection(Vector3.forward) * 1.5f * speed * Time.deltaTime;//朝真身正前方运动
        if (lastAttack == 1)
        {
            wayPoint = vec;
            Debug.Log("更新攻击点" + vec);
        }
        if ((new Vector2(transform.position.x, transform.position.y) - new Vector2(wayPoint.x, wayPoint.y)).magnitude < 20)//本位置距waypoint小于
        {
            Wander();
            if (lastAttack == 1)
            {
                lastAttack++;
            }
            else if (lastAttack == 2)
            {
                lastAttack = 0;
            }
        }

        //绕着waypoint旋转
        Quaternion rotation = Quaternion.LookRotation(wayPoint - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);

    }

    private void Wander()
    {
        speed = Random.Range(minSpeed, maxSpeed); //随机速度
        SetSpeed(speed);

        randLeftX = Random.Range(0, Screen.width / 3);
        randLeftY = Random.Range(Screen.height * 7 / 10, Screen.height * 9 / 10);
        randRightX = Random.Range(Screen.width / 3 * 2, Screen.width);
        randRightY = Random.Range(Screen.height * 7 / 10, Screen.height * 9 / 10);

        if (counter++ % 2 == 0) //左右盘旋点
        {
            wayPoint = Camera.main.ScreenToWorldPoint(new Vector3(randLeftX, randLeftY, 700));
        }
        else
        {
            wayPoint = Camera.main.ScreenToWorldPoint(new Vector3(randRightX, randRightY, 700));
        }
    }

    private void SetSpeed(float newSpeed)   //改变飞行和动画帧播放速度
    {
        speed = newSpeed;
        foreach (AnimationState state in transform.FindChild("batModel").animation)
        {
            state.speed = speed / 100 * 7;
        }
    }
    public void Attack(Vector3 attackPos)//判断是否攻击，改变攻击点为盘旋点
    {
        if (batSpawner.attack)
        {
            bool isMin = true;

            distantToFace = (new Vector2(transform.position.x, transform.position.y) - vec).magnitude;

            if (lastAttack != 0)    //排除上次攻击过的蝙蝠算入距离比较
            {
                return;
            }

            foreach (Transform trans in transform.parent)   //判断是否是所有蝙蝠中离目标最近的
            {
                if (trans.GetComponent<BatControl>().lastAttack != 0)
                {
                    continue;
                }
                if (distantToFace <= trans.GetComponent<BatControl>().distantToFace)
                {
                    continue;
                }
                else
                {
                    isMin = false;
                    break;
                }
            }
            if (isMin)
            {
                SetSpeed(2.5f * speed);
                wayPoint = attackPos;
                lastAttack = 1;
                batSpawner.attack = false;
            }
        }
    }

    public void GetValue()     //获得人脸位置
    {
        for (int i = 0; i < OpenCVForUnitySample.DetectFace.rects.Length; i++)
        {
            //矩形坐标
            //Debug.Log("NO: " + i + "  x: " +(float)(OpenCVForUnitySample.DetectFace.rects[i].x/800)*Screen.width+ "  y: " + OpenCVForUnitySample.DetectFace.rects[i].y);

            float x = (float)(OpenCVForUnitySample.DetectFace.rects[i].x + OpenCVForUnitySample.DetectFace.rects[i].width / 2) / 640 * Screen.width;
            float y = (float)(OpenCVForUnitySample.DetectFace.rects[i].y + OpenCVForUnitySample.DetectFace.rects[i].height / 2) / 480 * Screen.height;
            Vector3 ve = Camera.main.ScreenToWorldPoint(new Vector3(x, y, 100));
            vec = new Vector2(ve.x, -ve.y);
        }
    }

    private void Ai()   //蝙蝠的人工智能
    {

    }
}
