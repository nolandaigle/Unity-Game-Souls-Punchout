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
    public AudioClip swing;

    //State stuff
    float stateTimer = 0f;
    Dictionary<State, float> stateTime = new Dictionary<State, float>();

	public enum State { Standing, RightHandPrep, RightHandHit, LeftHandPrep, LeftHandHit, Shielding, Hurt, Dodging, Dead };
	protected State currentState = State.Standing;

    protected float hurtRecover = 1f;
    protected float dodgeTime = .75f;

    //Health stuff
	public float maxHealth = 10;
	protected float currentHealth = 10;
    protected float damageScale = 1;

    //Stamina stuff
	public float maxStamina = 10;
	protected float currentStamina = 10;
    public float staminaTimer = 0;
    public float staminaTime = .5f;

    public float dodgeCost = 5;


	public Fighter_Base enemy;

	public Weapon rightHand;
    public Weapon leftHand;

    public DamageCounter damageCounter;

    public bool staggerable = true;

    private string astrology = "";

    // Start is called before the first frame update
    protected virtual void Start()
    {
        //Create various state timers based on each possible state
        stateTime.Add(State.RightHandPrep, rightHand.chargeTime );
        stateTime.Add(State.RightHandHit, rightHand.hitRecover );
        stateTime.Add(State.LeftHandPrep, leftHand.chargeTime );
        stateTime.Add(State.LeftHandHit, leftHand.hitRecover );
        stateTime.Add(State.Hurt, hurtRecover );
        stateTime.Add(State.Dodging, dodgeTime );

        currentStamina = maxStamina;
        currentHealth = maxHealth;


        SaveState save= (SaveState)FindObjectOfType(typeof(SaveState));
        astrology = save.astrology;
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

    void Attack( float damage, string type )
    {
        if ( type == "healing" )
            Heal(damage);
        else
        	enemy.Hurt(damage, type);
    }

    public void SapStamina(float amount)
    {
        currentStamina -= amount;
        if ( currentStamina < 0 )
        {
            currentStamina = 0;
        }
    }

    public void Hurt(float damage, string type )
    {
    	if ( ( currentState != State.Shielding  || type == "IgnoreBlock"  ) 
        && ( currentState != State.Dodging || type == "IgnoreDodge" ) )
        {
            if ( staggerable || currentState == State.Standing  )
                ChangeState(State.Hurt);
            aSource.clip = hurt;
            aSource.Play();
    		currentHealth -= damage*damageScale;
        }
        else if ( currentState == State.Shielding )
        {
            float shieldedDamage = damage/2;
            if ( astrology == "scorpio" )
                shieldedDamage = damage/4;
            currentHealth -= shieldedDamage;
            currentStamina -= shieldedDamage;
            enemy.SapStamina(10);
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

    public virtual void Die()
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
        if ( currentState == State.Standing || currentState == State.Shielding  )
        {
            if ( shielding )
    	    	ChangeState(State.Shielding);
        	else if ( currentStamina >= leftHand.GetStaminaCost() )
            {
                currentStamina -= leftHand.GetStaminaCost();
                // if ( rightHand.GetWeaponType() == "sword" )
                //     damageCounter.Roll(leftHand.GetDamage(), .1f);
            	ChangeState(State.LeftHandPrep);
                aSource.clip = swing;
                aSource.Play();
            }
        }
    }

    protected void RightHandAction()
    {
        if ( currentState == State.Standing || currentState == State.Shielding )
        {
            if ( currentStamina >= rightHand.GetStaminaCost() )
            {
                currentStamina -= rightHand.GetStaminaCost();
                // if ( rightHand.GetWeaponType() == "sword" )
                //     damageCounter.Roll(rightHand.GetDamage(), .1f);
            	ChangeState(State.RightHandPrep);
                aSource.clip = swing;
                aSource.Play();
            }
        }
    }

    protected void Stand()
    {
    	ChangeState(State.Standing);
    }

    public void ChangeState( State newState )
    {
        stateTimer = 0;
    	anim.Play(newState.ToString());
    	rightHand.ChangeState(newState.ToString());
    	leftHand.ChangeState(newState.ToString());
    	currentState = newState;

    	if ( currentState == State.RightHandHit )
    	{
    		Attack(rightHand.GetDamage(), rightHand.GetDamageType() );
    	}
        else if ( currentState == State.LeftHandHit )
    	{
    		Attack(leftHand.GetDamage(), leftHand.GetDamageType() );
    	}
    }

    public void ChangeState(string newState)
    {
        if ( newState == "RightHandHit")
            ChangeState(State.RightHandHit);
        else 
        if ( newState == "LeftHandHit")
            ChangeState(State.LeftHandHit);
    }

    public virtual void Heal(float amount)
    {
        currentHealth += amount;
        if ( currentHealth > maxHealth )
            currentHealth = maxHealth;
        ChangeState(State.Standing);
    }
    
    public virtual void StopFighting()
    {
        fighting = false;
    }

    public float GetMaxHealth()
    {
    	return maxHealth;
    }

    public float GetCurrentHealth()
    {
    	return currentHealth;
    }

    public float GetMaxStamina()
    {
    	return maxStamina;
    }

    public float GetCurrentStamina()
    {
    	return currentStamina;
    }

    public State GetCurrentState()
    {
        return currentState;
    }
}
