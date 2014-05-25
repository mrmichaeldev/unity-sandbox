using UnityEngine;
using System.Linq;
using System.Collections;
using Models;

public class TeleporterController : MonoBehaviour {

    public Teleporter Teleporter { get; set; }
    public GameObject TeleportTo;

	// Use this for initialization
	void Start ()
    {
        TeleportTo = GameObject.FindGameObjectsWithTag("Teleporter").FirstOrDefault(t => t.GetComponent<TeleporterController>().Teleporter.Id == Teleporter.TargetId);
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}
}
