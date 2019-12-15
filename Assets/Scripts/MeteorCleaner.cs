using UnityEngine;

class MeteorCleaner : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        var meteor = other.GetComponentInParent<Meteor>();
        if (meteor)
        {
            Destroy(meteor.gameObject);
        }
    }
}