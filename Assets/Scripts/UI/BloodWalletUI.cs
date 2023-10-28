using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodWalletUI : MonoBehaviour
{
    public float speed = 0.2f;
    float targetValue = 0;

    private void Awake()
    {
        EventManager.Instance.OnBloodChanged += OnBloodChanged;
    }

    private void Update()
    {
        GetComponent<Image>().fillAmount = Mathf.Lerp(GetComponent<Image>().fillAmount, targetValue, speed);
    }

    public void OnBloodChanged(float newBlood, float maxBlood)
    {
        targetValue = newBlood / maxBlood;
    }
}
