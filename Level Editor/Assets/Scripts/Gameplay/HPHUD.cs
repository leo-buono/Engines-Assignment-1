using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class HPHUD : MonoBehaviour
{
    public Text HP;
    public Image HPBar;
    public Player playerRef;

	private void Start() {
		UpdateHUD();
	}

	private void OnEnable() {
		ActionEventPlayer.instance.damaged += UpdateHUD;
	}

	private void OnDisable() {
		ActionEventPlayer.instance.damaged -= UpdateHUD;
	}

    // Update is called once per frame
    void UpdateHUD()
    {
        HP.text = "Health: " + playerRef.health;
		HPBar.fillAmount = playerRef.health / playerRef.maxHealth;
    }
}
