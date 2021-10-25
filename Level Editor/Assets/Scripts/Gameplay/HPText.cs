using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class HPText : MonoBehaviour
{
    public Text HP;
    Player hpref;

    // Start is called before the first frame update
    void Start()
    {
        HP = GetComponent<Text>();
        GameObject player = GameObject.Find("Player");
        hpref = player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        HP.text = "Health: " + hpref.health;
    }
}
