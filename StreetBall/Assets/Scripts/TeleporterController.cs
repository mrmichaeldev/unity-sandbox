using UnityEngine;
using System.Linq;
using System.Collections;
using Models;

public class TeleporterController : MonoBehaviour {

    public Teleporter Teleporter { get; set; }

    public Vector3 Teleport { get; set; }


	// Use this for initialization
	void Awake ()
    {
        
	}
	
	// Update is called once per frame
    void Update()
    {
        if (Teleport == null)
        {
            var teleporters = GameObject.FindGameObjectsWithTag("Teleporter");
            if (teleporters != null)
            {
                Teleporter porter;
                foreach (var teleporter in teleporters)
                {
                    var controller = teleporter.GetComponent<TeleporterController>();
                    if (controller != null)
                    {
                        var teleport = controller.Teleporter;
                        if (teleport != null && teleport.Id == Teleporter.TargetId)
                        {
                            porter = teleport;
							Teleport = new Vector3(Teleporter.Position.X - porter.Position.X, Teleporter.Position.Y - porter.Position.Y, 0);
                            break;
                        }
                    }
                }
            }
        }
    }
}
