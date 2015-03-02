using UnityEngine;
using System.Collections;

public class ClockAnimator : MonoBehaviour
{
	public Transform hours, minutes, sun;

	private const float
		hoursToDegrees = 360f / 12f,
		minutesToDegrees = 360f / 60f;
	
	void Start ()
	{
	}

	// Update is called once per frame
	void Update ()
	{
		hours.localRotation = Quaternion.Euler(0f, 0f, DayNightController.worldTimeHour * -hoursToDegrees + 180);
		minutes.localRotation = Quaternion.Euler(0f, 0f, DayNightController.minutes * -minutesToDegrees + 180);
	}
}