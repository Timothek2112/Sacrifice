using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{
    private static EventManager _instance;
    public static EventManager Instance
    {
        get
        {
            if(_instance == null )
                _instance = new EventManager();
            return _instance;
        }
    }

    public delegate void OnChainsawForceChangedDelegate(Chainsaw chainsaw);
    public event OnChainsawForceChangedDelegate OnChainsawForceChanged;

    public delegate void OnBloodChangedDelegate(float newBlood, float maxBlood);
    public event OnBloodChangedDelegate OnBloodChanged;

    public delegate void OnChainsawRestartedDelegate(Chainsaw chainsaw);
    public event OnChainsawRestartedDelegate OnChainsawRestarted;

    public void ChainsawForceChanged(Chainsaw chainsaw)
    {
        OnChainsawForceChanged?.Invoke(chainsaw);
    }

    public void BloodChanged(float newBlood, float maxBlood)
    {
        OnBloodChanged?.Invoke(newBlood, maxBlood);
    }

    public void ChainsawRestarted(Chainsaw chainsaw)
    {
        OnChainsawRestarted?.Invoke(chainsaw);
    }
}
