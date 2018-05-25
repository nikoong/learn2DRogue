﻿using UnityEngine;
using System.Collections;


public class Enemy : MovingObject{

	public int playerDamage;

	private Animator animator;
	private Transform target;
	private bool skipMove;
	public AudioClip enemyAttck1;
	public AudioClip enemyAttck2;

	protected override void Start () {
		animator = GetComponent<Animator> ();
		GameManager.instance.AddEnemyToList (this);//加上这句话game manager可以调用定义在Enemy脚本里的MoveEnemy方法
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		base.Start();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	protected override void AttemptMove<T> (int xDir, int yDir)
	{
		if(skipMove)
		{
			skipMove = false;
			return;
		}
		base.AttemptMove<T> (xDir,yDir);
		skipMove = true;
	}

	public void MoveEnemy()
	{
		int xDir = 0;
		int yDir = 0;

		if (Mathf.Abs (target.position.x - transform.position.x) < float.Epsilon)
			yDir = target.position.y > transform.position.y ? 1 : -1;
		else
			xDir = target.position.x > transform.position.x ? 1 : -1;
		AttemptMove<Player> (xDir, yDir); 
	}

	protected override void OnCantMove <T> (T component)
	{
		Player hitPlayer = component as Player;

		hitPlayer.LoseFood (playerDamage);

		animator.SetTrigger ("enemyAttack");

		SoundManager.instance.RandomizeSfx (enemyAttck1, enemyAttck2);


	}
}
