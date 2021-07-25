using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UserHUD : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameText = null;
    [SerializeField] TextMeshProUGUI hpText = null;
    [SerializeField] TextMeshProUGUI atkText = null;

    [Space]

    [SerializeField] GameObject inputs = null;
    [SerializeField] TMP_InputField input_currentHP = null;
    [SerializeField] TMP_InputField input_maxHP = null;
    [SerializeField] TMP_InputField input_atk = null;

    User user;

    private void Awake() 
    {
        user = GetComponent<User>();
    }

    private void Start() 
    {
        UpdateUserTexts();    
        SetInputFields();
    }

    public void UpdateUserTexts()
    {
        nameText.text = user.userName;
        hpText.text = user.currentHP + "/" + user.maxHP;
        atkText.text = user.atk.ToString();
    }

    public void SetInputFields()
    {
        input_currentHP.text = user.currentHP.ToString();
        input_maxHP.text = user.maxHP.ToString();
        input_atk.text = user.atk.ToString();
    }

    public void SetUserDataByInput()
    {
        user.currentHP = Int32.Parse(input_currentHP.text);
        user.maxHP = Int32.Parse(input_maxHP.text);
        user.atk = Int32.Parse(input_atk.text);

        UpdateUserTexts();
    }
}
