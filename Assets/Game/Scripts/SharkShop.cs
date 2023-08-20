using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkShop : MonoBehaviour
{

    private AudioSource _AudioSource;

    private void Start()
    {
        _AudioSource = GetComponent<AudioSource>();
    }
    public void OnTriggerStay(Collider other)
    {

        if (other.tag == "Player" && Input.GetKey(KeyCode.E))
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                if (player.hasCoin)
                {
                    _AudioSource.Play();
                    player.hasCoin = false;
                    UIManager uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
                    if (uiManager != null)
                    {
                        uiManager.UpdateCoin();
                        uiManager.UpdateAmmo(player.maxAmmo);
                    }
                    player.EnableWeapons();
                }
                else
                {
                    Debug.Log("Get out of here!");
                }
            }
        }
    }

}
