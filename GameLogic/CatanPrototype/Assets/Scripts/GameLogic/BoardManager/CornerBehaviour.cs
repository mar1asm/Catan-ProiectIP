using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerBehaviour : MonoBehaviour
{
    [SerializeField]
    private Corner _corner;
    public Corner corner
    {
        get
        {
            return _corner;
        }
        set
        {
           _corner = value;
            //fac asta pentru a putea fi folosit in viitor
            _corner.inGameObject = this.gameObject;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(255, 0, 0, 0.5f);
        Gizmos.DrawWireCube(transform.position, new Vector3(0.2f, 0.2f, 0.2f));
    }
}
