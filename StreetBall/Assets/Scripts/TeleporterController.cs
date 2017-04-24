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
                        var rotationQuaternion = Quaternion.Euler(To.transform.rotation.x - transform.rotation.x, To.transform.rotation.y - transform.rotation.y, To.transform.rotation.z - transform.rotation.z);
                        var rigidbody = controller.GetComponent<Rigidbody2D>();
                        rigidbody.velocity *= -1;
                        var rotationoffset = transform.position - new Vector3(rigidbody.position.x, rigidbody.position.y, 0);
                        var rotationoffset2 = To.transform.position - new Vector3(rigidbody.position.x, rigidbody.position.y, 0);
                        rigidbody.velocity = rotationQuaternion * rigidbody.velocity;
                        
                        collider.transform.Translate(TeleportTo);
                        rotationoffset = rotationQuaternion * rotationoffset;
                        rotationoffset2 = rotationQuaternion * rotationoffset2;
                        collider.transform.Translate(rotationoffset);
                        collider.transform.Translate(rotationoffset2);
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
