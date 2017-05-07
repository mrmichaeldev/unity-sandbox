using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterController : MonoBehaviour
{

    public TeleporterController To;

    public Vector3 TeleportTo
    {
        get
        {
            return To.transform.position - transform.position;
        }
    }

    // Use this for initialization
    void Start()
    {
        if (To == null)
            throw new System.Exception("Teleport To is null");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        switch (collider.tag)
        {
            case "Player":
                {
                    var controller = collider.GetComponent<PlayerController>();
                    if (!controller.IsTeleporting)
                    {
                        controller.IsTeleporting = true;
                        var rotationQuaternion = Quaternion.Euler(To.transform.eulerAngles.x - transform.eulerAngles.x, To.transform.eulerAngles.y - transform.eulerAngles.y, To.transform.eulerAngles.z - transform.eulerAngles.z);
                        var rigidbody = controller.GetComponent<Rigidbody2D>();
                        rigidbody.velocity *= -1;
                        rigidbody.velocity = rotationQuaternion * rigidbody.velocity;

                        var distance = collider.transform.position - GetComponent<Collider2D>().transform.position;
                        var offset = rotationQuaternion * (distance);
                        collider.transform.Translate(TeleportTo);
                        collider.transform.Translate(offset - distance);
                    }
                }
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Player":
                {
                    var controller = collision.GetComponent<PlayerController>();
                    controller.IsTeleporting = false;
                }
                break;
        }
    }
}
