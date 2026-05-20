using UnityEngine;

public class Moviment : MonoBehaviour
{
[SerializeField]private float VDM = 5f;
[SerializeField]private Vector3 destination;

private float stopDistance = .1f;
 

    void Update()
    {
        destination = MouseWorld.Instance.GetWorldMousePosition().point;
        if (Vector3.Distance(destination, transform.position) > stopDistance)
        {
            MoveAt(destination);
        }
    }

    private void MoveAt(Vector3 position)
    {
        var moveDir = (position - transform.position).normalized;

        transform.position += moveDir * Time.deltaTime * VDM;
    }

}
