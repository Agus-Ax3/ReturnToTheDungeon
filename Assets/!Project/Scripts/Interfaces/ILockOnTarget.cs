using UnityEngine;

public interface ILockOnTarget
{
    Transform TargetPoint { get; }
    bool IsTargetable { get; }
}