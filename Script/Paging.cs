using UnityEngine;
using System.Collections;

public class Paging : MonoBehaviour {

	public GameObject lastButton;//取得向上翻页按钮对象
	public GameObject nextButton;//取得向下翻页按钮对象
	public static int timer;
	public static int totalNumberOfPages;
	// Use this for initialization
	void Start () {
		timer = 0;
		totalNumberOfPages = 3;
	}
	
	// Update is called once per frame
	void Update () {
		PageButtonShow (timer);
	}
	//函数——参数：Move.timer 计数器
	//函数功能： 判断是否需要隐藏翻页按钮
	public void PageButtonShow(int timer)
	{
		if(timer == 1){
			nextButton.gameObject.SetActive(true);
			lastButton.gameObject.SetActive(false);
		}
		else if(timer >1 && timer <totalNumberOfPages){
			nextButton.gameObject.SetActive(true);
			lastButton.gameObject.SetActive(true);
		}
		else if(timer == totalNumberOfPages){
			nextButton.gameObject.SetActive(false);
			lastButton.gameObject.SetActive(true);
		}
		else{
			nextButton.gameObject.SetActive(false);
			lastButton.gameObject.SetActive(false);
		}
	}
}
