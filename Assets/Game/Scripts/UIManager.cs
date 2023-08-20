using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _ammoText;

    [SerializeField]
    private GameObject _coin;


    public void UpdateAmmo(int count)
    {
        _ammoText.text = count.ToString();
    }

    public void UpdateCoin()
    {
        _coin.SetActive(!_coin.activeSelf);
    }
}
