using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Group {

	List<Stone> stones;

	public Group(List<Stone> stones) {
		this.stones = stones;
	}

	public Group(Stone stone) {
		this.stones = new List<Stone> ();
		stones.Add (stone);
	}


}
