using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter_Base : MonoBehaviour
{
    //Animator
	public Animator anim;
	public Animator rhAnim;
	public Animator lhAnim;

    //Sound
    public AudioSource aSource;
    public AudioClip hurt;
    public AudioClip dodge;

    //State stuff
    public float stateTimer = 0f;
    public float stateTime = .75f;

	enum State { Standing, RightHandPrep, RightHandHit, LeftHandPrep, LeftHandHit, Shielding, Hurt, Dodging, Dead };
	State currentState = State.Standing;

    //Health stuff
	public int maxHealth = 10;
	protected int currentHealth = 10;

    //Stamina stuff
	public int maxStamina = 10;
	protected int currentStamina = 10;
    public float staminaTimer = 0;
    public float staminaTime = 1;

    public int dodgeCost = 5;


	public Fighter_Base enemy;

	public Weapon rightHand;
    public Weapon leftHand;

    public DamageCounter damageCounter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected void Update()
    {
    	StateLogic();
    }

    void StateLogic()
    {
    	if ( currentState != State.Standing && currentState != State.Shielding && currentState != State.Dead )
    	{
	        stateTimer += Time.deltaTime;
	        if ( stateTimer > stateTime )
	        {
	        	if ( currentState == State.RightHandPrep)
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
            aSource.clip = leftHand.blockSound;
            aSource.Play();
        }

        if ( currentHealth <= 0 )
        {
            currentHealth = 0;
            ChangeState(State.Dead);
        }

        damageCounter.Cancel();
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
        if ( currentState == State.Standing )
        {
            if ( currentStamina >= rightHand.GetStaminaCost() )
            {
                currentStamina -= rightHand.GetStaminaCost();
                damageCounter.Roll(rightHand.GetDamage(), 1);
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
    	rhAnim.Play(newState.ToString());
    	lhAnim.Play(newState.ToString());
    	currentState = newState;

    	if ( currentState == State.RightHandHit )
    	{
    		Attack(rightHand.GetDamage());
    	}
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
