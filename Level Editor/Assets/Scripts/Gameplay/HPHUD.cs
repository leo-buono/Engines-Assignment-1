using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

using UnityEngine.UI;

public class HPHUD : MonoBehaviour
{
    public Text HP;
    public Image HPBar;
    public Player playerRef;

	[DllImport("UIDL")]
	public static extern float GetBarWidth(float health, float maxHealth, float currentWidth, float maxWidth, float dt);

	[DllImport("UIDLL")]
	public static extern Color GetColour(float percentage);

    // Update is called once per frame
    void Update()
    {
        HP.text = "Health: " + playerRef.health;
		HPBar.fillAmount = GetBarWidth(playerRef.health, playerRef.maxHealth, HPBar.fillAmount, 1f, Time.deltaTime);
    }
}
