using UnityEngine;
using System.Collections;

using OpenCVForUnity;
using System.Threading;

namespace OpenCVForUnitySample
{
    /// <summary>
    /// WebCamTexture detect face sample.
    /// </summary>
    public class DetectFace : MonoBehaviour
    {

        public static bool ifClick = false;

        public static OpenCVForUnity.Rect[] rects;
        /// The web cam texture.
        WebCamTexture webCamTexture;

        /// The web cam device.
        WebCamDevice webCamDevice;

        /// The colors.
        Color32[] colors;

        /// The is front facing.
        public bool isFrontFacing = false;

        /// The width.
        int width = 800;

        /// The height.
        int height = 480;

        /// The rgba mat.
        Mat rgbaMat;

        /// The gray mat.
        Mat grayMat;

        /// The texture.
        Texture2D texture;

        /// The cascade.
        CascadeClassifier cascade;

        bool stop = false;

        /// The faces.
        MatOfRect faces;

        //人脸识别线程
        Thread thread;

        int webCamTextureWidth = 0;

        /// The init done.
        bool initDone = false;

        bool firstDetect = true;

        void Start()
        {
            StartCoroutine(init());
        }

        private IEnumerator init()
        {
            //删除上一个状态
            if (webCamTexture != null)
            {
                webCamTexture.Stop();
                initDone = false;
                rgbaMat.Dispose();
                grayMat.Dispose();
            }
            // Checks how many and which cameras are available on the device
            //找后置摄像头
            for (int cameraIndex = 0; cameraIndex < WebCamTexture.devices.Length; cameraIndex++)
            {

                if (WebCamTexture.devices[cameraIndex].isFrontFacing == isFrontFacing)
                {

                    Debug.Log(cameraIndex + " name " + WebCamTexture.devices[cameraIndex].name + " isFrontFacing " + WebCamTexture.devices[cameraIndex].isFrontFacing);

                    webCamDevice = WebCamTexture.devices[cameraIndex];

                    webCamTexture = new WebCamTexture(webCamDevice.name, width, height);
                    Debug.Log(width + "  " + height);

                    break;
                }
            }
            //没有就用第一个摄像头
            if (webCamTexture == null)
            {
                webCamDevice = WebCamTexture.devices[0];
                webCamTexture = new WebCamTexture(webCamDevice.name, width, height);
                Debug.Log(width + "  " + height);

            }
            Debug.Log("width " + webCamTexture.width + " height " + webCamTexture.height + " fps " + webCamTexture.requestedFPS);
            // Starts the camera
            webCamTexture.Play();

            while (true)
            {
                //If you want to use webcamTexture.width and webcamTexture.height on iOS, you have to wait until webcamTexture.didUpdateThisFrame == 1, otherwise these two values will be equal to 16. (http://forum.unity3d.com/threads/webcamtexture-and-error-0x0502.123922/)
#if UNITY_IPHONE && !UNITY_EDITOR
				if (webCamTexture.width > 16 && webCamTexture.height > 16) {
#else
                if (webCamTexture.didUpdateThisFrame)  //此帧已保存至摄像机buffer
                {
#endif
                    Debug.Log("width " + webCamTexture.width + " height " + webCamTexture.height + " fps " + webCamTexture.requestedFPS);
                    Debug.Log("videoRotationAngle " + webCamTexture.videoRotationAngle + " videoVerticallyMirrored " + webCamTexture.videoVerticallyMirrored + " isFrongFacing " + webCamDevice.isFrontFacing);

                    colors = new Color32[webCamTexture.width * webCamTexture.height];

                    rgbaMat = new Mat(webCamTexture.height, webCamTexture.width, CvType.CV_8UC4);
                    grayMat = new Mat(webCamTexture.height, webCamTexture.width, CvType.CV_8UC1);

                    texture = new Texture2D(webCamTexture.width, webCamTexture.height, TextureFormat.RGBA32, false);

                    gameObject.transform.eulerAngles = new Vector3(0, 0, 0);

                    cascade = new CascadeClassifier(Utils.getFilePath("haarcascade_frontalface_alt.xml"));

                    faces = new MatOfRect();

                    gameObject.GetComponent<Renderer>().material.mainTexture = texture;

                    initDone = true;  //初始化完成

                    break;
                }
                else
                {
                    yield return 0;
                }
            }
        }

        void Update()
        {
            if (!initDone)
                return;

#if UNITY_IPHONE && !UNITY_EDITOR
			if (webCamTexture.width > 16 && webCamTexture.height > 16) {
#else
            if (webCamTexture.didUpdateThisFrame)
            {
#endif
                Utils.webCamTextureToMat(webCamTexture, rgbaMat, colors);

                webCamTextureWidth = webCamTexture.width;

                if (firstDetect)
                {
                    startThread();
                }
                //画出矩形
                rects = faces.toArray();
                for (int i = 0; i < rects.Length; i++)
                {
                    Core.rectangle(rgbaMat, new Point(rects[i].x, rects[i].y),
                        new Point(rects[i].x + rects[i].width, rects[i].y + rects[i].height),
                        new Scalar(255, 0, 0, 255), 2);
                    //矩形坐标
                  //  Debug.Log("NO: " + i + "  x: " + rects[i].x + "  y: " + rects[i].y);
                }
                Utils.matToTexture2D(rgbaMat, texture, colors);
            }
        }
        private void startThread()
        {
            firstDetect = false;
            thread = new Thread(new ThreadStart(FaceDetect));
            thread.Start();
        }
        private void FaceDetect()
        {
            while(!stop)
            {
                if (!ifClick)
                {
                    //先将图像变成灰度图，对它应用直方图均衡化，做一些预处理的工作。接下来检测人脸，调用detectMultiScale函数，该函数在输入图像的不同尺度中检测物体
                    Imgproc.cvtColor(rgbaMat, grayMat, Imgproc.COLOR_RGBA2GRAY);
                    Imgproc.equalizeHist(grayMat, grayMat);
                    // 对不同大小的输入图像进行物体识别，并返回一个识别到的物体的矩阵列表。 
                    if (cascade != null)
                        cascade.detectMultiScale(grayMat, faces, 1.1, 2, 2, // TODO: objdetect.CV_HAAR_SCALE_IMAGE
                              new Size(webCamTextureWidth * 0.15, webCamTextureWidth * 0.15), new Size());
                }
            }
          
        }

        void OnDisable()
        {
            webCamTexture.Stop();
            stop = true;
            thread.Abort();
        }

        void OnGUI()
        {
            //适配分辨率
            float screenScale = Screen.width / 240.0f;
            Matrix4x4 scaledMatrix = Matrix4x4.Scale(new Vector3(screenScale, screenScale, screenScale));
            GUI.matrix = scaledMatrix;
            GUILayout.BeginVertical();
            GUILayout.EndVertical();

            Pause(ifClick);

        }

		//暂停相机ifClick
		//false-不暂停 ， true-暂停
        void Pause(bool ifClick)
        {
            if (ifClick)
            {
                webCamTexture.Pause();
                Debug.Log("stop");
            }
            else
            {
                webCamTexture.Play();
				Debug.Log("Continue");
            }
        }
    }
}
