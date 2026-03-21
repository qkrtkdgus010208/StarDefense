using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public Status Status { get; protected set; }

    public virtual void Init()
    {
        Status = GetComponent<Status>();
        Status.Init();
    }
}
