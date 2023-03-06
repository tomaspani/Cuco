using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CucoUpgrades : MonoBehaviour
{
    [Header("References")]
    Consume _consume;
    Dash _dash;
    Energy _energy;
    PlayerMovement pm;
    PlayerController _player;
    ThrowObject _throw;
    FindEntities _cucovision;
    Invisibility _invisibility;
    [SerializeField] GameObject _energyBar;
    [SerializeField] UIManager _uiManager;

    [SerializeField] GameObject Txt;

    [Header("Values")]
    [SerializeField] int lvl;
    [SerializeField] float _dashUpgrade;
    [SerializeField] float _speedUpgrade;
    [SerializeField] float feedbackDuration;
    

    [Header("Bools")]

    [SerializeField] bool InvisibilityUnlocked;


    private void Start()
    {
        _consume = GetComponent<Consume>();
        _dash = GetComponent<Dash>();
        _energy = GetComponent<Energy>();
        pm = GetComponent<PlayerMovement>();
        _throw = GetComponent<ThrowObject>();
        _cucovision = GetComponent<FindEntities>();
        _invisibility = GetComponent<Invisibility>();
    }

    private void FixedUpdate()
    {
        _energy.RegenerateEnergy(lvl);
    }
    public void LevelUp(int totalConsumedKids)
    {

        if (_consume.totalConsumedKids >= 9 && lvl < 4)
        {
            lvl = 4;
            InvisibilityUnlocked = true;
            _dash.LevelUpDash(_dashUpgrade * lvl);
            _invisibility.UnlockInvisibility();
            _uiManager.ShowIcon(_uiManager.InvisibilityIcon);
            FeedbackUpgrade("Level 4. Invisibility Unlocked. Press F to activate it");
        }
        else if (totalConsumedKids >= 5 && lvl < 3)
        {
            lvl = 3;
            _dash.UnlockDash();
            pm.LevelUpSpeed(_speedUpgrade * lvl);
            FeedbackUpgrade("Level 3. Dash Unlocked. Press Q to Dash");
            _uiManager.ShowIcon(_uiManager.DashIcon);
            
        }
        else if (totalConsumedKids >= 2 && lvl < 2)
        {
            lvl = 2;
            _cucovision.UnlockCucoVision();
            pm.LevelUpSpeed(_speedUpgrade * lvl);
            _uiManager.ShowIcon(_uiManager.CucoVisionIcon);
            _energyBar.SetActive(true);
            FeedbackUpgrade("Level 2. CucoVision Unlocked. Press TAB to activate it");
        }
    }

    void FeedbackUpgrade(string _feedback)
    {
        
        Txt.GetComponent<Text>().text = _feedback;
        Invoke(nameof(HideFeedback), feedbackDuration);
    }

    void HideFeedback()
    {
        Txt.GetComponent<Text>().text = "";
    }
}
