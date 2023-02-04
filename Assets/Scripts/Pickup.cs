using System.Collections;
using UnityEngine;

public abstract class Pickup: MonoBehaviour {

    abstract public void Activate(RootRenderer activatedBy);

}