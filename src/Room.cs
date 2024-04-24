using System.Collections.Generic;

class Room
{
	// Private fields
	private string description;
	private Inventory chest;
	private Dictionary<string, Room> exits; // stores exits of this room.

 private List<Item> items; // Store items directly in the room
	public Inventory Chest
	{
		get { return chest; }
	}
	// Create a room described "description". Initially, it has no exits.
	// "description" is something like "in a kitchen" or "in a court yard".
	public Room(string desc)
	{
		description = desc;
		chest = new Inventory(999999);
		exits = new Dictionary<string, Room>();
		items = new List<Item>(); // Initialize the list of items
	}

	    // Add an item to the room
    public void AddItem(Item item)
    {
        items.Add(item);
    }

    // Get the items in the room
    public List<Item> GetItems()
    {
        return items;
    }

	// Define an exit for this room.
	public void AddExit(string direction, Room neighbor)
	{
		exits.Add(direction, neighbor);
	}

	// Return the description of the room.
	public string GetShortDescription()
	{
		return description;
	}

	// Return a long description of this room, in the form:
	//     You are in the kitchen.
	//     Exits: north, west
public string GetLongDescription()
{
    string str = "You are " + description + ".\n"; // Concatenate "You are" and the description
		str += "items in room: " + chest.GetItems();
    str += "\n"; // Add "Look for items" on a new line
	str += GetExitString();
    return str;



}

	// Return the room that is reached if we go from this room in direction
	// "direction". If there is no room in that direction, return null.
	public Room GetExit(string direction)
	{
		if (exits.ContainsKey(direction))
		{
			return exits[direction];
		}
		return null;
	}

	// Return a string describing the room's exits, for example
	// "Exits: north, west".
	public string GetExitString()
	{
		string str = "Exits:";

		// Build the string in a `foreach` loop.
		// We only need the keys.
		int countCommas = 0;
		foreach (string key in exits.Keys)
		{
			if (countCommas != 0)
			{
				str += ",";
			}
			str += " " + key;
			countCommas++;
		}

		return str;
	}





}
