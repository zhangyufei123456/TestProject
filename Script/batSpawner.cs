using UnityEngine;
using System.Collections;

public class batSpawner : MonoBehaviour {

    public static bool attack = false;
    public bool att = false;
    public Transform batPrefab;        // Insert bat prefab
   public int batAmount = 5;         // Number of bats to be spawned instantly
   private int counter = 1;
    void Start()
    {
        for (int i = 0; i < batAmount; i++)
        {
             Transform  obj = (Transform)Instantiate(batPrefab);
            obj.parent = transform;
        }
    }
  
	
	// Update is called once per frame
	void Update () {
		if (!MoveControl.gamePause) {//MoveControl.gamePause暂停游戏//false-不暂停 ， true-暂停
			if (counter++ % 500 == 0) { //每_帧发动一次攻击
				attack = true;
				Debug.Log ("Attack");
			}
		}
		else{

		}     
	}
}
