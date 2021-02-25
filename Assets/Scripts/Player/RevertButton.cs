using UnityEngine;

public class RevertButton : MonoBehaviour
{
    public delegate void RevertDel();
    public static event RevertDel OnRevert;

    public void Revert()
    {
        OnRevert?.Invoke();    
    }
}