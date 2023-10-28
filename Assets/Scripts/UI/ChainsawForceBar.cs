using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChainsawForceBar : MonoBehaviour
{
    public float speed = 0.2f;
    float targetValue = 0;

    private void Awake()
    {
        EventManager.Instance.OnChainsawForceChanged += OnChainsawForceChanged;
    }

    private void Update()
    {
        GetComponent<Image>().fillAmount = Mathf.Lerp(GetComponent<Image>().fillAmount, targetValue, speed);
    }

    public void OnChainsawForceChanged(Chainsaw chainsaw)
    {
        targetValue = chainsaw.force / chainsaw.maxForce;
    }
}
