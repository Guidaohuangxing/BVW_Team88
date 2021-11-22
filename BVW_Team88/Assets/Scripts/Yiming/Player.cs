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


    //combo parameter
    [SerializeField]
    public int combo = 0;
    private int previousCombo = 0;
    public List<int> comboStandard = new List<int>();
    public Image ComboBar;
    private bool turnBig = false;
    public float largerScale = -2f;
    private Vector3 OriginalScale;
    private Vector3 BiggerScale;
    public TextMeshProUGUI comboTxt;
    private void Start()
    {
        health = MaxHealth;
        previousHealth = health;
        gameManager = FindObjectOfType<GameManager>();
        SetPlayerPosition();
        combo = 0;
        previousCombo = combo;
        OriginalScale = comboTxt.transform.localScale;
        BiggerScale = OriginalScale * largerScale;
    }
    private void Update()
    {
        CheckEnterDying();
        UpdateHealth();
        UpdateCombo();
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
        //sound
        
        
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
        
        if (combo == comboStandard[0])
        {
            SoundFXManager.instance.PlayComboPraise();
        }
        else if (combo == comboStandard[1])
        {
            SoundFXManager.instance.PlayComboPraise();
        }
        else if (combo == comboStandard[2])
        {
            SoundFXManager.instance.PlayComboPraise();
        }
        else if (combo >= comboStandard[3])
        {
            if(Random.Range(0,5) == 1)
            {
                SoundFXManager.instance.PlayComboPraise();
            }
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
        if(health>= 0)
        {
            healthUI.text = health + " / " + MaxHealth;
        }
        else if (health < 0)
        {
            healthUI.text = "0" + " / " + MaxHealth;
        }
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

    public void UpdateCombo()
    {
        if (combo != 0)
        {
            comboTxt.text = "x" + combo;
        }
        else if(combo == 0)
        {
            comboTxt.text = " ";
        }
        ComboBar.fillAmount = Mathf.Clamp((float)combo / comboStandard[comboStandard.Count - 1], 0, 1);
        if (previousCombo != combo)
        {
            turnBig = true;
            previousCombo = combo;
        }
        if (Vector3.Distance(comboTxt.transform.localScale, BiggerScale) < 0.01f)
        {
            turnBig = false;
        }
        LargerNumber();
    }
    private void LargerNumber()
    {
        if (turnBig)
        {
            comboTxt.transform.localScale = Vector3.Lerp(comboTxt.transform.localScale, BiggerScale, 0.2f);
        }
        else if (!turnBig)
        {
            comboTxt.transform.localScale = Vector3.Lerp(comboTxt.transform.localScale, OriginalScale, 0.2f);
        }
    }
    public void ResetCombo()
    {
        combo = 0;
    } 
}
