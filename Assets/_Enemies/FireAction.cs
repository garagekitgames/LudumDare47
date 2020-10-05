using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class FireAction : Action
{

	private EnemyController myEnemyController;

	public override void OnAwake()
	{
		base.OnAwake();
		myEnemyController = GetComponent<EnemyController>();
	}

	public override void OnStart()
	{
		
	}

	public override TaskStatus OnUpdate()
	{
		myEnemyController.Fire();
		return TaskStatus.Success;
	}
}