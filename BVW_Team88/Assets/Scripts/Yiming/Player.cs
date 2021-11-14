using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    public enum State
    {
        Wait,
        Dying,//almost dead still can heal
        Alive,
        PowerUp,//when get enought combo can fight with other and use Trick
        CombineAttack//Two people combine with each other so can not slash anymore 
    }

    public State playerState = State.Alive;
    public int dyingHealth = -30; //when player in dying mode how many heal they have;
    public bool isRide = false;//when two player at the same place;
    public Transform tracker;
    GameManager gameManager;
    public int position;
    public Vector3 SwordStartPosition;
    public Vector3 SwordSpritePostion;//for the sword Animation
    public Sword childSword;
    private float timer = 0;
    public float threshold = .1f;
    public string tagName;

    public Slider healthBar;
    public Text healthVal;
    public int MaxHealth = 100;
    [SerializeField]
    private int health = 100;
    [SerializeField]
    public int combo = 0;
   




    public TextMeshProUGUI comboTxt;
    private void Start()
    {
        health = MaxHealth;
        healthBar.value = health;
        healthVal.text = health + "/" + MaxHealth;
        gameManager = FindObjectOfType<GameManager>();
        SetPlayerPosition();
    }
    private void Update()
    {
        CheckEnterDying();
    }


    public void SetPlayerPosition()
    {
        foreach (var item in gameManager.swordAreas)
        {
            if (item.number == position)
            {
                SwordStartPosition = item.swordPos.position;
                transform.position = item.playerPos.position;
                SwordSpritePostion = item.swordSpritePos.position;
            }
        }
    }

    /// <summary>
    /// Enter the state of dying can not slash anymore and need other player's help
    /// </summary>
    public void CheckEnterDying()
    {
        if (health <= 0 && playerState == State.Alive)
        {
            health = dyingHealth;
            playerState = State.Dying;
        }
        else if(health > 0 && playerState == State.Dying)
        {
            playerState = State.Alive;
        }
        
    }






    /// <summary>
    /// when was hit, get damage and reset the combo
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        if(health > 0)
        {
            health -= damage;
        }
        healthBar.value = health;
        healthVal.text = health + "/"+MaxHealth;
        combo = 0;
        comboTxt.text = combo.ToString();
        
    }


    public void TakeHeal(int heal)
    {
        if(health + heal <= MaxHealth)
        {
            health += heal;
        }
        GetCombo();
        healthBar.value = health;
        healthVal.text = health + "/" + MaxHealth;
    }
    /// <summary>
    /// cut thing in combo
    /// </summary>
    public void GetCombo()
    {
        combo++;
        comboTxt.text = combo + "Combo";
        if (combo >= 2)
        {
            print("combo!");
        }
        else if (combo >= 5)
        {
            print("nice combo!");
        }
        else if (combo >= 10)
        {
            print("super combo!");
        }
        else if (combo >= 20)
        {
            print("crazzzzzy combo!");
            playerState = State.PowerUp;
        }
    }


    public int Health
    {
        get { return health; }
        set { health = value; }
    }

    public bool CheckHealth()
    {
        if (health <= 0)
        {
            return true;
        }
        else { return false; }
    }
}
