using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private AudioClip _clip;


    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Input.GetKey(KeyCode.E))
        {
            Player player = other.GetComponent<Player>();
            if(player != null)
            {
                player.hasCoin = true;
                AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1);
                UIManager uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
                if(uiManager != null )
                {
                    uiManager.UpdateCoin();
                }
                Destroy(this.gameObject);
            }
        }
    }
}
