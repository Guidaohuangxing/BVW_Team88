using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int maxHealth;
    private int health;
    [System.Serializable]
    public struct BossRounds
    {
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




    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (startTimer)
        {
            timer += Time.deltaTime;
            if (timer >= bossRounds[currentRound].waitTimeForAttack)
            {
                BossAttack();
                timer = 0;
            }
        }
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
        Destroy(decisionBar);
        bossAnimator.SetBool("BossAttack", true);
        
    }

    /// <summary>
    /// be called at go back animation
    /// </summary>
    public void GoBackOriginalPlace()
    {
        print("reset all parameter");
        //reset all possible parameter
        startTimer = false;
        bossAnimator.SetBool("BossAttack", false);
        bossAnimator.SetBool("BossPreAttack", false);
        bossAnimator.SetBool("BossWasAttacked", false);
        gameManager.SetPlayerStateToCertainState(Player.State.Alive);
    }


    public void BossWasAttacked(int damage)
    {
        health -= damage;
        bossAnimator.SetBool("BossWasAttacked", true);
    }
}
