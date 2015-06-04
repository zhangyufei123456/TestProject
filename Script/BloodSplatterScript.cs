using UnityEngine;
using System.Collections;

// Decompile by Si Borokokok

public class BloodSplatterScript : MonoBehaviour
{
	public Transform bloodPrefab;
	public int maxAmountBloodPrefabs = 20;
    private GameObject[] bloodInstances;
	public Transform bloodRotation;
	public int bloodLocalRotationYOffset;
	//public Camera cam;
	// We need to actually hit an object
	//RaycastHit hitt = new RaycastHit();


    public void Update()
    {
		//Ray ray = cam.ScreenPointToRay(Input.mousePosition);
		//Physics.Raycast(ray, out hitt, 100);
		if (Input.GetMouseButtonDown(0))
        {
            bloodRotation.Rotate((float) 0, (float) bloodLocalRotationYOffset, (float) 0);
            Vector3 position = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 100));
            Debug.Log(position);

            //Transform transform = Object.Instantiate(bloodPrefab, position, bloodRotation.rotation) as Transform;
            //bloodInstances = GameObject.FindGameObjectsWithTag("blood");
            //if ((bloodInstances).Length >= maxAmountBloodPrefabs)
            //{
            //    Destroy(bloodInstances[0]);
            //}
        }

    }
}

