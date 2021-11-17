using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter_Base : MonoBehaviour
{
    //Manager
    public BattleManager bm;
    bool fighting = true;

    //Animator
	public Animator anim;

    //Sound
    public AudioSource aSource;
    public AudioClip hurt;
    public AudioClip dodge;

    //State stuff
    public float stateTimer = 0f;
    Dictionary<State, float> stateTime = new Dictionary<State, float>();

	protected enum State { Standing, RightHandPrep, RightHandHit, LeftHandPrep, LeftHandHit, Shielding, Hurt, Dodging, Dead };
	protected State currentState = State.Standing;

    float hurtRecover = 1f;
    float dodgeTime = .75f;

    //Health stuff
	public int maxHealth = 10;
	protected int currentHealth = 10;

    //Stamina stuff
	public int maxStamina = 10;
	protected int currentStamina = 10;
    public float staminaTimer = 0;
    public float staminaTime = .5f;

    public int dodgeCost = 5;


	public Fighter_Base enemy;

	public Weapon rightHand;
    public Weapon leftHand;

    public DamageCounter damageCounter;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        //Create various state timers based on each possible state
        stateTime.Add(State.RightHandPrep, rightHand.chargeTime );
        stateTime.Add(State.RightHandHit, rightHand.hitRecover );
        stateTime.Add(State.Hurt, hurtRecover );
        stateTime.Add(State.Dodging, dodgeTime );
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if ( fighting )
        	StateLogic();
    }

    void StateLogic()
    {
    	if ( currentState != State.Standing && currentState != State.Shielding && currentState != State.Dead )
    	{
	        stateTimer += Time.deltaTime;

	        if ( stateTimer > stateTime[currentState] )
	        {
	        	if ( currentState == State.RightHandPrep && rightHand.GetWeaponType() == "sword" )
	        	{
	        		stateTimer = 0;
	        		ChangeState(State.RightHandHit);
	        	}
	        	else if ( currentState == State.LeftHandPrep)
	        	{
	        		stateTimer = 0;
	        		ChangeState(State.LeftHandHit);
	        	}
	        	else if ( currentState == State.LeftHandHit ||  currentState == State.RightHandHit ||  currentState == State.Hurt ||  currentState == State.Dodging )
	        	{
	        		stateTimer = 0;
	        		ChangeState(State.Standing);
	        	}
	        }
    	}
        else if ( currentState == State.Standing )
        {
            if ( currentStamina < maxStamina )
            {
                staminaTimer += Time.deltaTime;
                if ( staminaTimer > staminaTime )
                {
                    staminaTimer = 0;
                    currentStamina += 1;
                }
            }
        }
    }

    void Attack( int damage )
    {
    	enemy.Hurt(damage);
    }

    public void SapStamina(int amount)
    {
        currentStamina -= amount;
        if ( currentStamina < 0 )
        {
            currentStamina = 0;
        }
    }

    public void Hurt(int damage)
    {
    	if ( currentState != State.Shielding && currentState != State.Dodging )
        {
            aSource.clip = hurt;
            aSource.Play();
            ChangeState(State.Hurt);
    		currentHealth -= damage;
        }
        else if ( currentState == State.Shielding )
        {
            int shieldedDamage = Mathf.RoundToInt(damage/2);
            currentHealth -= shieldedDamage;
            currentStamina -= shieldedDamage;
            enemy.SapStamina(damage);
            aSource.clip = leftHand.blockSound;
            aSource.Play();
        }

        if ( currentHealth <= 0 )
        {
            currentHealth = 0;
            Die();
        }

        damageCounter.Cancel();
    }

    protected void Die()
    {
        if ( currentState != State.Dead )
        {
            ChangeState(State.Dead);
            bm.EndBattle();
        }
    }

    protected void Dodge()
    {
        if ( currentStamina >= dodgeCost )
        {
            currentStamina -= dodgeCost;
            aSource.clip = dodge;
            aSource.Play();
            ChangeState(State.Dodging);
        }
    }

    protected void LeftHandAction(bool shielding=false)
    {
        if ( currentState == State.Standing )
        {
        	if ( shielding )
    	    	ChangeState(State.Shielding);
    	    else
        		ChangeState(State.LeftHandPrep);
        }
    }

    protected void RightHandAction()
    {
        if ( currentState == State.Standing || currentState == State.Shielding )
        {
            if ( currentStamina >= rightHand.GetStaminaCost() )
            {
                currentStamina -= rightHand.GetStaminaCost();
                if ( rightHand.GetWeaponType() == "sword" )
                    damageCounter.Roll(rightHand.GetDamage(), .1f);
            	ChangeState(State.RightHandPrep);
            }
        }
    }

    protected void Stand()
    {
    	ChangeState(State.Standing);
    }

    void ChangeState( State newState )
    {
        stateTimer = 0;
    	anim.Play(newState.ToString());
    	rightHand.ChangeState(newState.ToString());
    	leftHand.ChangeState(newState.ToString());
    	currentState = newState;

    	if ( currentState == State.RightHandHit )
    	{
    		Attack(rightHand.GetDamage());
    	}
    }

    public void ChangeState(string newState)
    {
        if ( newState == "RightHandHit")
            ChangeState(State.RightHandHit);
    }

    public virtual void Heal(int amount)
    {
        currentHealth += amount;
        if ( currentHealth > maxHealth )
            currentHealth = maxHealth;
        ChangeState(State.Standing);
    }
    
    public void StopFighting()
    {
        fighting = false;
    }

    public int GetMaxHealth()
    {
    	return maxHealth;
    }

    public int GetCurrentHealth()
    {
    	return currentHealth;
    }

    public int GetMaxStamina()
    {
    	return maxStamina;
    }

    public int GetCurrentStamina()
    {
    	return currentStamina;
    }
}
