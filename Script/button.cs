using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class button : MonoBehaviour
{
	//按钮调用的函数参数
	public string touchEvent;   

	//按钮图片的位置
	public float posX;
	public float posY;

	//判断移动是否结束，是否能点击按钮
	public  static  bool ifCanClick = true;

	//按钮事件检测相关
	private bool pushed = false;
	private bool shooted = false;
	private enum Effect { DOWN, UP };

	//图片精灵 
	private Transform textureNormal;//点击前的图片
	private Transform textureTouch;//点击时的图片
	
	public void Start()//动态获取子组件（图片）对象（减少scene中赋值量）
	{
		Debug.Log("a");
		//按钮的Normal图片，touch图片
		textureNormal = transform.FindChild("textureNormal");
		textureTouch = transform.FindChild("textureTouch");

		//按钮放置位置，转换为screen大小的倍数，以适配各种设备分辨率
		this.transform.position = Camera.main.ScreenToWorldPoint
			(new Vector3(Screen.width *posX, Screen.height / posY, 10));
	}

	void OnGUI()
	{
	}
	
	void  Update()
	{
		EventDetect();
	}
	
	private void EventDetect()//点击检测，射线法（减少对坐标的繁琐判断）
	{
		if (ifCanClick) {
						if (Input.GetMouseButton (0)) {
								pushed = true;  //已经按下
								Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);//从摄像机发出到点击坐标的射线
								RaycastHit hitInfo;
								if (Physics.Raycast (ray, out hitInfo)) {//射中按钮（可能其他按钮）tr
										Debug.DrawLine (ray.origin, hitInfo.point);//划出射线，只有在scene视图中才能看到
										GameObject gameObj = hitInfo.collider.gameObject;
				
										if ((gameObj.name == this.gameObject.name) && !shooted) {//射中本按钮
												shooted = true;
												TouchEffect (Effect.DOWN);
										}
								} else if (shooted) {   //射中按钮后移出
										shooted = false;
										TouchEffect (Effect.UP);
								}
			
						} else if (pushed) { //松开
								pushed = false;
								if (shooted) {        //唯有此处按键成功（在射中的地方松开按钮）
										SendMessage ("Event", touchEvent);    //调用Event函数名参数为touchEvent值
				
										TouchEffect (Effect.UP);
								}
								shooted = false;
						}
				} 
	}
	
	private void TouchEffect(Effect eff)//点击效果。包含按下和松开效果
	{
		if (eff == Effect.DOWN)
		{
			textureNormal.Translate(0, 0, +1);
			textureTouch.Translate(0, 0, 0);
		}
		else if (eff == Effect.UP)
		{
			textureNormal.Translate(0, 0, -1);
			textureTouch.Translate(0, 0, 0);
		}
	}
	
	private void Event(string touchEvent)//按钮事件处理
	{
		Move.move = true;//调用移动特性

		//主页面中的事件////////////////////////////////////////////////
		if (touchEvent == "StartEvent")
		{
			Debug.Log("start");
			Application.LoadLevel("GameScene");
		}
		if (touchEvent == "ExitEvent")
		{
			Debug.Log("exit");
			Application.Quit();
		}
		if (touchEvent == "AboutEvent")
		{
			Debug.Log("about");
			Move.toward = "about";
		}
		if (touchEvent == "HelpEvent")
		{
			Move.timer++;
			Debug.Log("help");
			Move.toward = "help";
			Paging.timer = Move.timer;
		}
		//主页面中的事件////////////////////////////////////////////////

		//关于页面中的事件////////////////////////////////////////////////
		if (touchEvent == "AboutBackEvent")
		{
			Debug.Log("aboutBack");
			Move.toward = "aboutBack";
		}
		//关于页面中的事件////////////////////////////////////////////////

		//帮助页面中的事件////////////////////////////////////////////////
		if (touchEvent == "HelpBackEvent")
		{
			Debug.Log("helpBack");
			Move.toward = "helpBack";
			Move.timer = 0;
			Paging.timer = Move.timer;
		}
		if (touchEvent == "NextEvent")
		{
			Move.timer++;
			Paging.timer = Move.timer;
			if(Move.timer > 1 && Move.timer < Paging.totalNumberOfPages)//向上一页页面2进入
			{
				Debug.Log("next2");
				Move.toward = "next1to2";
			}
			else if(Move.timer == Paging.totalNumberOfPages)//向上一页页面3进入
			{
				Debug.Log("next3");
				Move.toward = "next2to3";
			}
			
		}
		if (touchEvent == "LastEvent")
		{
			Move.timer--;
			Paging.timer = Move.timer;
			if(Move.timer == 1)//向下一页页面1进入
			{
				Debug.Log("last1");
				Move.toward = "last2to1";
			}
			else if(Move.timer > 1 && Move.timer < Paging.totalNumberOfPages)//向下一页页面2进入
			{
				Debug.Log("last2");
				Move.toward = "last3to2";
			}
			
		}
		//帮助页面中的事件////////////////////////////////////////////////

		//游戏页面中的事件////////////////////////////////////////////////
		if (touchEvent == "PauseEvent")
		{
			Debug.Log("Pause");
			OpenCVForUnitySample.DetectFace.ifClick = true;
			MoveControl.gamePause = true;
			Move.toward = "pause";
		}
		//游戏页面中的事件////////////////////////////////////////////////

		//游戏中暂停页面中的事件////////////////////////////////////////////////
		if (touchEvent == "ContinueEvent")
		{
			OpenCVForUnitySample.DetectFace.ifClick = false;
			MoveControl.gamePause = false;
			Debug.Log("Continue");
			Move.toward = "continue";
		}
		if (touchEvent == "RestartEvent")
		{
			OpenCVForUnitySample.DetectFace.ifClick = false;
			Debug.Log("Restart");
			Move.toward = "restart";
			MoveControl.gamePause = false;
			
			Application.LoadLevel(Application.loadedLevelName);
		}
		if (touchEvent == "HomePageEvent")
		{
			OpenCVForUnitySample.DetectFace.ifClick = false;
			MoveControl.gamePause = false;
			Debug.Log("HomePage");
			Move.toward = "homePage";
			Application.LoadLevel("MainMenu");
		}
		//游戏中暂停页面中的事件////////////////////////////////////////////////
	}
}