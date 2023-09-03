using UnityEngine;

public class SacrificeSlot : MonoBehaviour
{
    [SerializeField] private Sacrifice _sacrifice;

    public void SetSacrifice(Sacrifice sacrifice)
    {
        _sacrifice = sacrifice;
    }

    public Sacrifice GetSacrifice()
    {
        return _sacrifice;
    }
}
