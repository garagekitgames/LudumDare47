using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class isEnemyAlive : Conditional
{

	public SharedBool isAlive;
	private EnemyController myEnemyController;
	public override void OnAwake()
	{
		base.OnAwake();
		myEnemyController = GetComponent<EnemyController>();
	}
	public override TaskStatus OnUpdate()
	{
		isAlive.Value = myEnemyController.isAlive;
		if (myEnemyController.isAlive)
		{
			return TaskStatus.Success;
		}
		else
		{
			return TaskStatus.Failure;
		}
	}
}