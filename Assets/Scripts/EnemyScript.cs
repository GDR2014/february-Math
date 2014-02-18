﻿using System.Collections;
using Assets.Scripts.Data;
using UnityEngine;

[RequireComponent(typeof(OperatorNumberSetScript))]
public class EnemyScript : MonoBehaviour {

    public PlayerScript player;
    public int attackTarget;
    public int attackModifier;
    public Operator attackOperator;
    public float Speed;
    public float AttackRange = 1f;

    protected OperatorNumberSetScript opNumSet;
    public OperatorNumberSetScript.OperatorSide operatorSide = OperatorNumberSetScript.OperatorSide.LEFT;

    void Start() {
        player = GameObject.FindWithTag( "Player" ).GetComponent<PlayerScript>();
        opNumSet = GetComponent<OperatorNumberSetScript>();
        opNumSet.operatorSide = operatorSide;
        StartCoroutine( UpdateNumberRenderer() );
        StartCoroutine( Move() );
    }

    IEnumerator Move() {
        float playerX = player.transform.position.x;
        Vector2 pos = transform.position;
        float distance = Mathf.Abs( pos.x - playerX );
        while ( distance > AttackRange ) {
            pos.x += Speed * Time.deltaTime;
            transform.position = pos;
            yield return null;
        }
    }

    public void UpdateAttackTarget( int playerVal ) {
        switch( attackOperator ) {
            case Operator.PLUS:
                attackTarget = playerVal + attackModifier;
                break;
            case Operator.MINUS:
                attackTarget = playerVal - attackModifier;
                break;
            case Operator.MULTIPLY:
                attackTarget = playerVal * attackModifier;
                break;
            case Operator.DIVIDE:
                attackTarget = playerVal / attackModifier;
                break;
        }
    }

    public IEnumerator UpdateNumberRenderer() {
        yield return new WaitForEndOfFrame();
        opNumSet.value = attackModifier;
        opNumSet.op = attackOperator;
        opNumSet.UpdateSetRenderer();
    }
}