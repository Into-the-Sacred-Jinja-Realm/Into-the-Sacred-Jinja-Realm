using UnityEngine;
using System;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
//using Windows.Kinect;

[RequireComponent(typeof(KinectGestures))]
public class Gestures_Listener : MonoBehaviour, KinectGestures.GestureListenerInterface
{
	[Tooltip("Index of the player, tracked by this component. 0 means the 1st player, 1 - the 2nd one, 2 - the 3rd one, etc.")]
	public int playerIndex = 0;

	[Tooltip("UI-Text to display gesture-listener messages and gesture information.")]
	public TextMeshProUGUI gestureInfo;


	private CameraMovement cameraMovement;
	// singleton instance of the class
	private static Gestures_Listener instance = null;

    //To avoid handtogether gesture detected to the Handclap gesture
	private float blockHandClapUntil = 0.5f;

	// internal variables to track if progress message has been displayed
	private bool progressDisplayed;
	private float progressGestureTime;
	
	// whether the needed gesture has been detected or not
	private static bool handtogerther = false;
	private static bool raisedBothHand = false;
	private static bool raiseRightHand = false;
	private static bool raiseLeftHand = false;
	private static bool isRun = false;
	private static bool isClap = false;

	

	private Dictionary<KinectGestures.Gestures, Action> GesetureActions = new Dictionary<KinectGestures.Gestures, Action>
	{
		{ KinectGestures.Gestures.HandTogerther, () => handtogerther = true },
		{ KinectGestures.Gestures.RaisedLeftRaisedRightHand, () => raisedBothHand = true },
		{ KinectGestures.Gestures.RaiseRightHand, () => raiseRightHand = true },
		{ KinectGestures.Gestures.RaiseLeftHand, () => raiseLeftHand = true },
		{ KinectGestures.Gestures.Run, () => isRun = true },
		{ KinectGestures.Gestures.HandClap, () => isClap = true }
	};

	KinectGestures.Gestures[] gesturesToDetect =
	{
		KinectGestures.Gestures.RaisedLeftRaisedRightHand,
		KinectGestures.Gestures.RaiseLeftHand,
		KinectGestures.Gestures.RaiseRightHand,
		KinectGestures.Gestures.HandTogerther,
		KinectGestures.Gestures.HandClap,
		KinectGestures.Gestures.Run
	};
	/// <summary>
	/// Gets the singleton CubeGestureListener instance.
	/// </summary>
	/// <value>The CubeGestureListener instance.</value>
	public static Gestures_Listener Instance
	{
		get
		{
			return instance;
		}
	}
	
	public bool IsHandClapBlocked()
	{
		return Time.time < blockHandClapUntil;
	}

	

	
	/// <summary>
	/// Determines whether swipe left is detected.
	/// </summary>
	/// <returns><c>true</c> if swipe left is detected; otherwise, <c>false</c>.</returns>
	public bool IsHandTogerther()
	{
		if (handtogerther)
		{
			handtogerther = false;
			return true;
		}

		return false;
	}
	
	/// <summary>
	/// Determines whether swipe left is detected.
	/// </summary>
	/// <returns><c>true</c> if swipe left is detected; otherwise, <c>false</c>.</returns>
	public bool IsHandClap()
	{
		if (isClap)
		{
			isClap = false;
			return true;
		}

		return false;
	}
	/// <summary>
	/// Determines whether swipe left is detected.
	/// </summary>
	/// <returns><c>true</c> if swipe left is detected; otherwise, <c>false</c>.</returns>
	public bool IsRaisedLeftRaisedRightHand()
	{
		if (raisedBothHand)
		{
			raisedBothHand = false;
			return true;
		}

		return false;
	}

	/// <summary>
	/// Determines whether swipe left is detected.
	/// </summary>
	/// <returns><c>true</c> if swipe left is detected; otherwise, <c>false</c>.</returns>
	public bool IsRaisedRightHand()
	{
		if (raiseRightHand)
		{
			raiseRightHand = false;
			return true;
		}

		return false;
	}

	/// <summary>
	/// Determines whether swipe left is detected.
	/// </summary>
	/// <returns><c>true</c> if swipe left is detected; otherwise, <c>false</c>.</returns>
	public bool IsRaisedLeftHand()
	{
		if (raiseLeftHand)
		{
			raiseLeftHand = false;
			return true;
		}

		return false;
	}

	public bool IsRunCheck()
	{
		if (isRun)
		{
			isRun = false;
			return true;
		}

		return false;
	}
	

	/// <summary>
	/// Invoked when a new user is detected. Here you can start gesture tracking by invoking KinectManager.DetectGesture()-function.
	/// </summary>
	/// <param name="userId">User ID</param>
	/// <param name="userIndex">User index</param>
	public void UserDetected(long userId, int userIndex)
	{
		// the gestures are allowed for the primary user only
		KinectManager manager = KinectManager.Instance;
		if (!manager || (userIndex != playerIndex))
			return;

		// detect these user specific gestures

		foreach (var gesture in gesturesToDetect)
		{
			manager.DetectGesture(userId, gesture);
		}
		if (gestureInfo != null)
		{
			gestureInfo.text = "Raise hand to detect.";
		}
	}

	/// <summary>
	/// Invoked when a user gets lost. All tracked gestures for this user are cleared automatically.
	/// </summary>
	/// <param name="userId">User ID</param>
	/// <param name="userIndex">User index</param>
	public void UserLost(long userId, int userIndex)
	{
		// the gestures are allowed for the primary user only
		if(userIndex != playerIndex)
			return;
		
		if(gestureInfo != null)
		{
			gestureInfo.text = string.Empty;
		}
	}

	/// <summary>
	/// Invoked when a gesture is in progress.
	/// </summary>
	/// <param name="userId">User ID</param>
	/// <param name="userIndex">User index</param>
	/// <param name="gesture">Gesture type</param>
	/// <param name="progress">Gesture progress [0..1]</param>
	/// <param name="joint">Joint type</param>
	/// <param name="screenPos">Normalized viewport position</param>
	public void GestureInProgress(long userId, int userIndex, KinectGestures.Gestures gesture,
								  float progress, KinectInterop.JointType joint, Vector3 screenPos)
	{
		// the gestures are allowed for the primary user only
		if (userIndex != playerIndex)
			return;
			
		if ((gesture == KinectGestures.Gestures.ZoomOut || gesture == KinectGestures.Gestures.ZoomIn) && progress > 0.5f)
		{
			if (gestureInfo != null)
			{
				string sGestureText = string.Format("{0} - {1:F0}%", gesture, screenPos.z * 100f);
				gestureInfo.text = sGestureText;

				progressDisplayed = true;
				progressGestureTime = Time.realtimeSinceStartup;
			}
		}
		else if ((gesture == KinectGestures.Gestures.Wheel || gesture == KinectGestures.Gestures.LeanLeft ||
				 gesture == KinectGestures.Gestures.LeanRight) && progress > 0.5f)
		{
			if (gestureInfo != null)
			{
				string sGestureText = string.Format("{0} - {1:F0} degrees", gesture, screenPos.z);
				gestureInfo.text = sGestureText;

				progressDisplayed = true;
				progressGestureTime = Time.realtimeSinceStartup;
			}
		}

		else if (gesture == KinectGestures.Gestures.Run && progress > 0.5f)
		{
			if (gestureInfo != null)
			{
				string sGestureText = string.Format("{0} - progress: {1:F0}%", gesture, progress * 100);
				gestureInfo.text = sGestureText;

				progressDisplayed = true;
				progressGestureTime = Time.realtimeSinceStartup;
			}

			isRun = true;

			if (SceneManager.GetActiveScene().name != "Activity_Test")
			{
				Debug.LogWarning("Running");
				cameraMovement.Move(true);
			}
				
		}
	}

	/// <summary>
	/// Invoked if a gesture is completed.
	/// </summary>
	/// <returns>true</returns>
	/// <c>false</c>
	/// <param name="userId">User ID</param>
	/// <param name="userIndex">User index</param>
	/// <param name="gesture">Gesture type</param>
	/// <param name="joint">Joint type</param>
	/// <param name="screenPos">Normalized viewport position</param>
	public bool GestureCompleted (long userId, int userIndex, KinectGestures.Gestures gesture, 
	                              KinectInterop.JointType joint, Vector3 screenPos)
	{
		// the gestures are allowed for the primary user only
		if(userIndex != playerIndex)
			return false;
			

		if (gestureInfo != null)
		{
			string sGestureText = gesture + " detected";
			Gesture_imgChange Gesture_imgChange = FindFirstObjectByType<Gesture_imgChange>();
			if (SceneManager.GetActiveScene().name == "Activity_Test")
				Gesture_imgChange.ChangeImage(gesture.ToString());
			gestureInfo.text = sGestureText;
		}
		
		if (GesetureActions.ContainsKey(gesture))
		{
    		GesetureActions[gesture].Invoke();
		}
		
		if (SceneManager.GetActiveScene().name != "Activity_Test")
			cameraMovement.Move(false);
		return true;
	}

	/// <summary>
	/// Invoked if a gesture is cancelled.
	/// </summary>
	/// <returns>true</returns>
	/// <c>false</c>
	/// <param name="userId">User ID</param>
	/// <param name="userIndex">User index</param>
	/// <param name="gesture">Gesture type</param>
	/// <param name="joint">Joint type</param>
	public bool GestureCancelled (long userId, int userIndex, KinectGestures.Gestures gesture, 
	                              KinectInterop.JointType joint)
	{
		// the gestures are allowed for the primary user only
		if(userIndex != playerIndex)
			return false;
		
		if(progressDisplayed)
		{
			progressDisplayed = false;
			
			if(gestureInfo != null)
			{
				gestureInfo.text = String.Empty;
			}
		}

		if(SceneManager.GetActiveScene().name != "Activity_Test")
			cameraMovement.Move(false);
		return true;
	}

	
	void Awake()
	{
		instance = this;
	}
    void Start()
    {
		cameraMovement = FindFirstObjectByType<CameraMovement>();
    }
    void Update()
	{
		if(progressDisplayed && ((Time.realtimeSinceStartup - progressGestureTime) > 2f))
		{
			progressDisplayed = false;
			gestureInfo.text = String.Empty;

			Debug.Log("Forced progress to end.");
		}
	}

}
