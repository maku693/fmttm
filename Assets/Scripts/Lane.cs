using UnityEngine;

class Lane : MonoBehaviour
{
    public enum Position
    {
        Left,
        Center,
        Right
    }

    [SerializeField]
    GameObject leftTarget;
    [SerializeField]
    GameObject centerTarget;
    [SerializeField]
    GameObject rightTarget;

    public float GetTargetXFromPosition(Position position)
    {
        GameObject target;
        switch (position)
        {
            case Position.Left:
                target = leftTarget;
                break;
            case Position.Right:
                target = rightTarget;
                break;
            default:
                target = centerTarget;
                break;
        }
        return target.transform.position.x;
    }
}