using UnityEngine;

public delegate void OnGoldChangeDelegate(int _value);
public delegate void OnPlayerDamagedDelegate();
public delegate void OnUseAmmoDelegate();
public delegate void OnEnemyDeadDelegate();
public delegate void OnEnemyDamagedDelegate(int _dmg);
public delegate void VoidVoidDelegate();
public delegate void OnEnemyClearDelegate();
public delegate void VoidVectorDelegate(Vector3 _vec);