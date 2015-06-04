using UnityEngine;
using System.Collections;

public class score : MonoBehaviour {
	//图片
	public Transform textureNum0;
	public Transform textureNum1;
	public Transform textureNum2;
	public Transform textureNum3;
	public Transform textureNum4;
	public Transform textureNum5;
	public Transform textureNum6;
	public Transform textureNum7;
	public Transform textureNum8;
	public Transform textureNum9;

	public Transform parent;
	//score
	public int scores;

	//分数的个位、十位、百位、千位
	private int ones = 0;
	private int tens = 0;
	private int hundreds = 0; 
	private int thousands = 0;
	private int myriabit = 0;

	private int number = 0;
	private int timer = 0;
	private int counter = 0;
	// Use this for initialization
	void Start () {

	}
	
	void Update () {
		if( number<=scores){
			NumberDestory ();
			Position(number);
			number++;
			counter = 0;
			Debug.Log("1");
		}
		Debug.Log ("2");
		timer++;
	}
	
	private void Position(int num)
	{
		if(num>=0 && num<10)
		{
			ones = num/1;
			NumPosition(ones, counter);
		}
		else if(num>9 && num<100)
		{
			tens = num/10;
			NumPosition(tens, counter);counter++;
			ones = (num-tens*10)/1;
			NumPosition(ones, counter);
		}
		else if(num>99 && num<1000)
		{
			hundreds = num/100;
			NumPosition(hundreds, counter);counter++;
			tens = (num - hundreds*100)/10;
			NumPosition(tens, counter);counter++;
			ones = (num- hundreds*100-tens*10)/1;
			NumPosition(ones, counter);
		}
		else if(num>999 && num<10000)
		{
			thousands = num/1000;
			NumPosition(thousands, counter);counter++;
			hundreds = (num - thousands*1000)/100;
			NumPosition(hundreds, counter);counter++;
			tens = (num - thousands*1000 - hundreds*100)/10;
			NumPosition(tens, counter);counter++;
			ones = (num - thousands*1000 - hundreds*100-tens*10)/1;
			NumPosition(ones, counter);counter++;
		}
		else if(num>9999 && num<100000)
		{
			myriabit =  num/10000;
			NumPosition(myriabit, counter);counter++;
			thousands = (num - myriabit*1000)/1000;
			NumPosition(thousands, counter);counter++;
			hundreds = (num - myriabit*1000 - thousands*1000)/100;
			NumPosition(hundreds, counter);counter++;
			tens = (num - myriabit*1000 - thousands*1000 - hundreds*100)/10;
			NumPosition(tens, counter);counter++;
			ones = (num - myriabit*1000 - thousands*1000 - hundreds*100-tens*10)/1;
			NumPosition(ones, counter);counter++;
		}
	}

	private void NumPosition(int num, int counter)
	{
		if (num == 0)
		{
			Transform num0 = Instantiate (textureNum0, 
			            Camera.main.ScreenToWorldPoint
			            (new Vector3 (Screen.width * (0.55f + 0.1f*counter), Screen.height / 2.5f, 7)),
			             transform.rotation) as Transform;
			num0.parent = parent;
		}
		else if(num == 1)
		{
			Transform num1 = Instantiate (textureNum1, 
			                              Camera.main.ScreenToWorldPoint
			                              (new Vector3 (Screen.width * (0.55f + 0.1f*counter), Screen.height / 2.5f, 7)),
			                              transform.rotation) as Transform;
			num1.parent = parent;
		}
		else if(num == 2)
		{
			Transform num2 = Instantiate (textureNum2, 
			                              Camera.main.ScreenToWorldPoint
			                              (new Vector3 (Screen.width * (0.55f + 0.1f*counter), Screen.height / 2.5f, 7)),
			                              transform.rotation) as Transform;
			num2.parent = parent;
		}
		else if(num == 3)
		{
			Transform num3 = Instantiate (textureNum3, 
			                              Camera.main.ScreenToWorldPoint
			                              (new Vector3 (Screen.width * (0.55f + 0.1f*counter), Screen.height / 2.5f, 7)),
			                              transform.rotation) as Transform;
			num3.parent = parent;
		}
		else if(num == 4)
		{
			Transform num4 = Instantiate (textureNum4, 
			                              Camera.main.ScreenToWorldPoint
			                              (new Vector3 (Screen.width * (0.55f + 0.1f*counter), Screen.height / 2.5f, 7)),
			                              transform.rotation) as Transform;
			num4.parent = parent;
		}
		else if(num == 5)
		{
			Transform num5 = Instantiate (textureNum5, 
			                              Camera.main.ScreenToWorldPoint
			                              (new Vector3 (Screen.width * (0.55f + 0.1f*counter), Screen.height / 2.5f, 7)),
			                              transform.rotation) as Transform;
			num5.parent = parent;
		}
		else if(num == 6)
		{
			Transform num6 = Instantiate (textureNum6, 
			                              Camera.main.ScreenToWorldPoint
			                              (new Vector3 (Screen.width * (0.55f + 0.1f*counter), Screen.height / 2.5f, 7)),
			                              transform.rotation) as Transform;
			num6.parent = parent;
		}
		else if(num == 7)
		{
			Transform num7 = Instantiate (textureNum7, 
			                              Camera.main.ScreenToWorldPoint
			                              (new Vector3 (Screen.width * (0.55f + 0.1f*counter), Screen.height / 2.5f, 7)),
			                              transform.rotation) as Transform;
			num7.parent = parent;
		}
		else if(num == 8)
		{
			Transform num8 = Instantiate (textureNum8, 
			                              Camera.main.ScreenToWorldPoint
			                              (new Vector3 (Screen.width * (0.55f + 0.1f*counter), Screen.height / 2.5f, 7)),
			                              transform.rotation) as Transform;
			num8.parent = parent;
		}
		else if(num == 9)
		{
			Transform num9 = Instantiate (textureNum9, 
			                              Camera.main.ScreenToWorldPoint
			                              (new Vector3 (Screen.width * (0.55f + 0.1f*counter), Screen.height / 2.5f, 7)),
			                              transform.rotation) as Transform;
			num9.parent = parent;
		}
	}

	private void NumberDestory(){
				foreach (Transform tans in parent) {
						Destroy (tans.gameObject);
				}
		}
}
