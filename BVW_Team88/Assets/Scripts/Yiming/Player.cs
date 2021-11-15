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


    public Image healthBar;
    public Image delayHealth;
    private bool startMoveDelayHealth = false;
    public TextMeshProUGUI healthUI;

    public int MaxHealth = 100;
    [SerializeField]
    private int health = 100;
    private float previousHealth = 0;
    [SerializeField]
    public int combo = 0;
    public List<int> comboStandard = new List<int>();




    public TextMeshProUGUI comboTxt;
    private void Start()
    {
        health = MaxHealth;
        previousHealth = health;
        gameManager = FindObjectOfType<GameManager>();
        SetPlayerPosition();
    }
    private void Update()
    {
        CheckEnterDying();
        UpdateHealth();
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
        if (!FindObjectOfType<Boss>().bossAnimator.GetBool("BossPreAttack"))
        {
            combo = 0;
            playerState = State.Alive;
        } 
        comboTxt.text = combo.ToString();
        
    }


    public void TakeHeal(int heal)
    {
        if(health + heal <= MaxHealth)
        {
            health += heal;
        }
        //GetCombo();
        
    }
    /// <summary>
    /// cut thing in combo
    /// </summary>
    public void GetCombo()
    {
        combo++;
        comboTxt.text = "x" + combo;
        if (combo == comboStandard[0])
        {
            print("combo!");
        }
        else if (combo == comboStandard[1])
        {
            print("nice combo!");
        }
        else if (combo == comboStandard[2])
        {
            print("super combo!");
        }
        else if (combo >= comboStandard[3])
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


    public void UpdateHealth()
    {
        healthBar.fillAmount = (float)health / MaxHealth;
        healthUI.text = health + " / " + MaxHealth;
        if (health != previousHealth)
        {

            if (startMoveDelayHealth)
            {
                previousHealth = Mathf.Lerp(previousHealth, health, 0.02f);
                delayHealth.fillAmount = (float)previousHealth / MaxHealth;
            }
            else if (!startMoveDelayHealth)
            {
                StartCoroutine(MoveDelayHealth());
            }
        }
        else if (health == previousHealth)
        {
            startMoveDelayHealth = false;
        }
    }

    IEnumerator MoveDelayHealth()
    {
        yield return new WaitForSeconds(.5f);
        startMoveDelayHealth = true;
    }
}
