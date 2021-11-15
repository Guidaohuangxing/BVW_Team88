using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Boss : MonoBehaviour
{
    public int maxHealth;
    public int health;
    public  float previousHealth;

    [System.Serializable]
    public struct BossRounds
    {
        public int damage;
        public int roundNum;
        public float waitTimeForAttack;
        public GameObject attackedDecisionBar;
    }
    public int currentRound;
    public List<BossRounds> bossRounds;
    public Animator bossAnimator;
    public bool startTimer = false;
    private float timer = 0;
    public GameManager gameManager;
    private GameObject decisionBar;
    public Transform DecisionBarInstantiatePlace;
    public SpawnAdvance spawn;
    public Image healthBar;
    public Image delayHealth;
    private bool startMoveDelayHealth = false;
    public TextMeshProUGUI healthUI;
    public CameraShake cameraShake;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        health = maxHealth;
        previousHealth = health;
    }

    private void Update()
    {
        if (startTimer && currentRound < bossRounds.Count)
        {
            timer += Time.deltaTime;
            if (timer >= bossRounds[currentRound].waitTimeForAttack)
            {
                print("Tooooo Slow!");
                BossAttack();
                timer = 0;
            }
        }
        UpdateBossHealth();
        BossDead();
    }
    

    public void UpdateBossHealth()
    {
        healthBar.fillAmount = (float)health / maxHealth;
        healthUI.text = health + " / " + maxHealth;
        if (health != previousHealth)
        {
            
            if (startMoveDelayHealth)
            {
                previousHealth = Mathf.Lerp(previousHealth, health, 0.02f);
                delayHealth.fillAmount = (float)previousHealth / maxHealth;
            }
            else if (!startMoveDelayHealth)
            {
                StartCoroutine(MoveDelayHealth());
            }  
        }
        else if(health == previousHealth)
        {
            startMoveDelayHealth = false;
        }
    }
    IEnumerator MoveDelayHealth()
    {
        yield return new WaitForSeconds(.5f);
        startMoveDelayHealth = true;
    }

    /// <summary>
    /// Start Attack Movement
    /// </summary>
    public void StartPreAttack()
    {
        bossAnimator.SetBool("BossPreAttack", true);
    }

    /// <summary>
    /// boss enter preattack mode and wait for palyer combo attack
    /// be called at certain frame
    /// </summary>
    public void WaitForPlayerAttack()
    {
        if (bossRounds[currentRound].attackedDecisionBar)
        {
            decisionBar = Instantiate(bossRounds[currentRound].attackedDecisionBar, DecisionBarInstantiatePlace.position, Quaternion.identity);           
        }
        startTimer = true;
        gameManager.SetPlayerStateToCertainState(Player.State.CombineAttack);
    }

    public void BossAttack()
    {
        startTimer = false;
        Destroy(decisionBar, .5f);
        bossAnimator.SetBool("BossAttack", true);
        EndBossSection(currentRound);
    }
    /// <summary>
    /// called in the animation and make damage
    /// </summary>
    public void BossAttackAndMakeDamage()
    {
        Player[] players = FindObjectsOfType<Player>();
        foreach(var item in players)
        {
            item.TakeDamage(bossRounds[currentRound].damage);
        }
    }
    /// <summary>
    /// be called at go back animation
    /// </summary>
    public void GoBackOriginalPlace()
    {
        //print("reset all parameter");
        //reset all possible parameter
        
        bossAnimator.SetBool("BossAttack", false);
        bossAnimator.SetBool("BossPreAttack", false);
        bossAnimator.SetBool("BossWasAttacked", false);
        gameManager.SetPlayerStateToCertainState(Player.State.Alive);
    }


    public void BossWasAttacked(int damage)
    {
        startTimer = false;
        //print("was attacked");
        health -= damage;
        bossAnimator.SetBool("BossWasAttacked", true);
        //adjust GM and Boss's Rounds pointer;
        currentRound++;
        EndBossSection(currentRound);
    }

    /// <summary>
    /// after boss attack/was attacked send message to Spawn to see what round it is
    /// </summary>
    public void EndBossSection(int roundPointer)
    {
        spawn.currentRoundsNumber = roundPointer;
    }

    public void BossDead()
    {
        if (health < 0)
        {
            gameManager.isWin = true;
        }
    }


    //alot of camera shake function to add in animation
    public void AppearShake()
    {
        cameraShake.BigShake();
    }

    public void ApproachShake()
    {
        cameraShake.Shake();
    }
    public void WasAttackShake()
    {
        cameraShake.TinyShake();
    }
    public void AttackShake()
    {
        cameraShake.BigShake();
    }
    public void StartGame()
    {
        gameManager.StartGame();
    }
}
