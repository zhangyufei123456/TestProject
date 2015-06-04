using UnityEngine;
using System.Collections;

public class Position : MonoBehaviour {
	
	public float posX;//调试x轴位置
	public float posY;//调试y轴位置

	// Use this for initialization
	void Start () {
		//按钮放置位置，转换为screen大小的倍数，以适配各种设备分辨率
		this.transform.position = Camera.main.ScreenToWorldPoint
			(new Vector3(Screen.width *posX, Screen.height / posY, 10));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
