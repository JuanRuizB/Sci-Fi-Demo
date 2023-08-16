using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _ammoText;
    public void UpdateAmmo(int count)
    {
        _ammoText.text = count.ToString();
    }
}
