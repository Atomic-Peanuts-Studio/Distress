using UnityEngine;

public class WeaponInformation : MonoBehaviour
{
    [SerializeField]
    public bool isRangedWeaponType; //Boolean used to identify and categorize weapons, if it is not a ranged weapon, it is a melee
    public AttackInfo[] attackInfo;
    public string animationName;
    public Sprite sprite;

    private void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
    }
}
