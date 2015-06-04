using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {
	//判断按钮等对象是否在移动中
	//false-未在移动中 ， true-正在移动，按钮不可点击
	public static bool move = false;

	//变量，传递参数给函数Moved()
	public static string  toward ;

	//计数器，记入循环次数
	private int counter = 0;

	//计数器，记入左右翻页的选择
	public static int timer = 0;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Moved();
	}
	
	public void Moved(){
		if (move && counter<20 && ( Time.timeScale!=0 || Time.timeScale==0))
		{
			button.ifCanClick = false;
			counter++;
			switch (toward)
			{
			case "start": 
				GameObject.Find("MainObject").transform.Translate(-1*Camera.main.ScreenToWorldPoint(new Vector3((Screen.width * 1.5f), 0, 0)).x / 20, 0, 0);
				break;

			case "help":
				GameObject.Find("HelpButtonObject").transform.Translate(-2*Camera.main.ScreenToWorldPoint(new Vector3((Screen.width), 0, 0)).x / 20, 0, 0);
				GameObject.Find("MainObject").transform.Translate(-1*Camera.main.ScreenToWorldPoint(new Vector3((Screen.width * 1.5f), 0, 0)).x / 20, 0, 0);
				GameObject.Find("HelpText1").transform.Translate(-2*Camera.main.ScreenToWorldPoint(new Vector3((Screen.width), 0, 0)).x / 20, 0, 0);
				break;

			case "about":
				GameObject.Find("AboutObject").transform.Translate(-2*Camera.main.ScreenToWorldPoint(new Vector3((Screen.width), 0, 0)).x / 20, 0, 0);
				GameObject.Find("MainObject").transform.Translate(-1*Camera.main.ScreenToWorldPoint(new Vector3((Screen.width * 1.5f), 0, 0)).x / 20, 0, 0);
				break;

			case "helpBack":
				GameObject.Find("HelpText1").transform.position = Camera.main.ScreenToWorldPoint
					(new Vector3(Screen.width * 1.7f, Screen.height / 2f, 10));
				GameObject.Find("HelpText2").transform.position = Camera.main.ScreenToWorldPoint
					(new Vector3(Screen.width * 1.7f, Screen.height / 2f, 10));
				GameObject.Find("HelpText3").transform.position = Camera.main.ScreenToWorldPoint
					(new Vector3(Screen.width * 1.7f, Screen.height / 2f, 10));
				GameObject.Find("HelpButtonObject").transform.Translate(2*Camera.main.ScreenToWorldPoint(new Vector3((Screen.width), 0, 0)).x / 20, 0, 0);
				GameObject.Find("MainObject").transform.Translate(Camera.main.ScreenToWorldPoint(new Vector3((Screen.width  * 1.5f), 0, 0)).x / 20, 0, 0);
				break;

			case "next1":
				break;

			case "next1to2":
				GameObject.Find("HelpText1").transform.Translate(-2*Camera.main.ScreenToWorldPoint(new Vector3((Screen.width), 0, 0)).x / 20, 0, 0);
				GameObject.Find("HelpText2").transform.Translate(-2*Camera.main.ScreenToWorldPoint(new Vector3((Screen.width), 0, 0)).x / 20, 0, 0);
				break;

			case "next2to3":
				GameObject.Find("HelpText2").transform.Translate(-2*Camera.main.ScreenToWorldPoint(new Vector3((Screen.width), 0, 0)).x / 20, 0, 0);
				GameObject.Find("HelpText3").transform.Translate(-2*Camera.main.ScreenToWorldPoint(new Vector3((Screen.width), 0, 0)).x / 20, 0, 0);
				break;

			case "last2to1":
				GameObject.Find("HelpText1").transform.Translate(2*Camera.main.ScreenToWorldPoint(new Vector3((Screen.width), 0, 0)).x / 20, 0, 0);
				GameObject.Find("HelpText2").transform.Translate(2*Camera.main.ScreenToWorldPoint(new Vector3((Screen.width), 0, 0)).x / 20, 0, 0);
				break;
			case "last3to2":
				GameObject.Find("HelpText2").transform.Translate(2*Camera.main.ScreenToWorldPoint(new Vector3((Screen.width), 0, 0)).x / 20, 0, 0);
				GameObject.Find("HelpText3").transform.Translate(2*Camera.main.ScreenToWorldPoint(new Vector3((Screen.width), 0, 0)).x / 20, 0, 0);
				break;
			case "last3":
				break;

			case "aboutBack":
				GameObject.Find("AboutObject").transform.Translate(2*Camera.main.ScreenToWorldPoint(new Vector3((Screen.width), 0, 0)).x / 20, 0, 0);
				GameObject.Find("MainObject").transform.Translate(Camera.main.ScreenToWorldPoint(new Vector3((Screen.width * 1.5f), 0, 0)).x / 20, 0, 0);
				break;

			case "pause":
				GameObject.Find("PauseButton").transform.position = Camera.main.ScreenToWorldPoint
					(new Vector3(Screen.width * 0.5f, Screen.height / 0.4f, 10));
				if(counter < 17){
				GameObject.Find("ContinueButton").transform.Translate(0, -3*Camera.main.ScreenToWorldPoint(new Vector3(0, (Screen.height), 0)).y / 20, 0);
				GameObject.Find("HomePageButton").transform.Translate(0, -3*Camera.main.ScreenToWorldPoint(new Vector3(0, (Screen.height), 0)).y / 20, 0);
				GameObject.Find("RestartButton").transform.Translate(0, -3*Camera.main.ScreenToWorldPoint(new Vector3(0, (Screen.height), 0)).y / 20, 0);
				GameObject.Find("pad").transform.Translate(0, -3*Camera.main.ScreenToWorldPoint(new Vector3(0, (Screen.height), 0)).y / 20, 0);
				}
				break;

			case "continue":
				GameObject.Find("PauseButton").transform.position = Camera.main.ScreenToWorldPoint
					(new Vector3(Screen.width * 0.1f, Screen.height / 1.2f, 10));
				if(counter < 17){
					GameObject.Find("ContinueButton").transform.Translate(0, 3*Camera.main.ScreenToWorldPoint(new Vector3(0, (Screen.height), 0)).y / 20, 0);
					GameObject.Find("HomePageButton").transform.Translate(0, 3*Camera.main.ScreenToWorldPoint(new Vector3(0, (Screen.height), 0)).y / 20, 0);
					GameObject.Find("RestartButton").transform.Translate(0, 3*Camera.main.ScreenToWorldPoint(new Vector3(0, (Screen.height), 0)).y / 20, 0);
					GameObject.Find("pad").transform.Translate(0, 3*Camera.main.ScreenToWorldPoint(new Vector3(0, (Screen.height), 0)).y / 20, 0);
				}
				break;
 
			case "restart":
				break;

			case "homePage":
				break;

			case "exit":
				break;
			}
		}
		else if (counter == 20)
		{
			button.ifCanClick = true;
			move = false;
			counter = 0;
		}
	}
}
