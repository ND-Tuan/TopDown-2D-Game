using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddColumn : MonoBehaviour
{
    private RoomTemplates templates;

	void Start(){

		templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
		templates.Column.Add(this.gameObject);
	}

}
