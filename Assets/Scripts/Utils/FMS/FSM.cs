using System;
using System.Collections.Generic;

public class FSM
{
	private readonly IStateFactory _stateFactory;
	private IState _currentState;
	public IState CurrentState => _currentState;
	private readonly Dictionary<Type, IState> _statePool;

	public FSM(IStateFactory stateFactory, IState initialState)
	{
		_stateFactory = stateFactory;
		_statePool = new();
		AddToPool(initialState);
		_currentState = initialState;
		_currentState.Initialize(this);
		_currentState.OnEnter();
	}

	public void ChangeState<T>() where T : IState
	{
		if (!_statePool.TryGetValue(typeof(T), out var newState))
		{
			newState = _stateFactory.Create<T>();
			AddToPool(newState);
		}

		_currentState.OnExit();
		_currentState = newState;
		_currentState.Initialize(this);
		_currentState.OnEnter();
	}

	private void AddToPool(IState state)
	{
		_statePool[state.GetType()] = state;
	}

	public void Update(float deltaTime)
	{
		_currentState.Update(deltaTime);
	}
}
