using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public Color Healthy;
    public Color Dead;

    public Image healthSlider;
    public float finalXposition;

    private Vector3 originalLocation;
    private Vector3 finalLocation;

    private void Awake()
    {
        originalLocation = healthSlider.transform.localPosition;
        finalLocation = new Vector3(finalXposition, originalLocation.y, originalLocation.z);
    }


    /// <summary>
    /// Input a health and the healthbar will magically work!
    /// </summary>
    /// <param name="value">Between 0-1, denotes 0% health to 100% health</param>
    public void SetHealthSlider(float value)
    {
        healthSlider.transform.localPosition = Vector3.Lerp(finalLocation, originalLocation, value);
        healthSlider.color = Color.Lerp(Dead, Healthy, value);
    }
}
