using System;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Threading.Tasks.Dataflow;

class Game
{
	// Private fields	
	private Parser parser;
	private Player player;

private Room basement;

	// Constructor
	public Game()
	{
		parser = new Parser();
		player = new Player();
		CreateRooms();
	}

	// Initialise the Rooms (and the Items)
	public void CreateRooms()
	{
		// Create the rooms
		Room outside = new Room("outside the main entrance of the university" );
		Room theatre = new Room("in a lecture theatre");
		Room pub = new Room("in the campus pub");
		Room lab = new Room("in a computing lab");
		Room office = new Room("in the computing admin office");
		Room attic = new Room("up in the attic");
		 basement = new Room("down in the basement");

		// Initialise room exits
		outside.AddExit("east", theatre);
		outside.AddExit("south", lab);
		outside.AddExit("west", pub);

		theatre.AddExit("west", outside);

		pub.AddExit("east", outside);

		lab.AddExit("north", outside);
		lab.AddExit("east", office);

		office.AddExit("west", lab);

		office.AddExit("up", attic);

		attic.AddExit("down", office);

		basement.AddExit("up", office);
		office.AddExit("down", basement);

		// Create your Items here
		Item gun = new Item(10, "gun");

		Item medkit = new Item(5, "medkit");

		Item MBag = new Item(25, "mythical-bag");

		Item key = new Item(5, "key");


		// ...


		// And add them to the Rooms
		attic.Chest.Put("gun", gun);

		lab.Chest.Put("medkit", medkit);

		basement.Chest.Put("mythical-bag", MBag);

		pub.Chest.Put("key", key);


		// ...

		// Start game outside
		player.CurrentRoom = outside;
	}

	//  Main play routine. Loops until end of play.
	public void Play()
	{
		PrintWelcome();

		// Enter the main command loop. Here we repeatedly read commands and
		// execute them until the player wants to quit.
		bool finished = false;
		while (!finished)
		{
			if (player.IsAlive() == false)
			{
				Console.WriteLine("You died!");

				break;
			}

			Command command = parser.GetCommand();
			finished = ProcessCommand(command);
		}
		Console.WriteLine("Thank you for playing.");
		Console.WriteLine("Press [Enter] to continue.");
		Console.ReadLine();
	}

	// Print out the opening message for the player.
	private void PrintWelcome()
	{
		Console.WriteLine();
		Console.WriteLine("Welcome to Zuul!");
		Console.WriteLine("Your goal to complete is to find the mythical bag that has been lost");
		Console.WriteLine("for that you first need to find a key.");
		Console.WriteLine("you lose health every time you enter a room, so make smart decisions");
		Console.WriteLine("Type 'help' if you need help.");
		Console.WriteLine();
		Console.WriteLine(player.CurrentRoom.GetLongDescription());
	}

	// Given a command, process (that is: execute) the command.
	// If this command ends the game, it returns true.
	// Otherwise false is returned.
	private bool ProcessCommand(Command command)
	{
		bool wantToQuit = false;

		if (command.IsUnknown())
		{
			Console.WriteLine("I don't know what you mean...");
			return wantToQuit; // false
		}

		switch (command.CommandWord)
		{
			case "help":
				PrintHelp();
				break;
			case "go":
				GoRoom(command);
				break;
			case "quit":
				wantToQuit = true;
				break;
			case "look":
				look();
				break;
			case "status":
				PlayerStatus();
				break;
			case "take":
				Take(command.SecondWord);
				break;
			case "drop":
				Drop(command.SecondWord);
				break;
			case "use":
				Use(command);
				break;
			case "exits":
				Console.WriteLine(player.CurrentRoom.GetExitString());
				break;
		}

		return wantToQuit;
	}

	// ######################################
	// implementations of user commands:
	// ######################################

	// Print out some help information.
	// Here we print the mission and a list of the command words.
	private void PrintHelp()
	{
		Console.WriteLine("You are lost. You are alone.");
		Console.WriteLine("You wander around at the university.");
		Console.WriteLine();
		// let the parser print the commands
		parser.PrintValidCommands();
	}

	// Try to go to one direction. If there is an exit, enter the new
	// room, otherwise print an error message.
private void GoRoom(Command command)
{
    if (!command.HasSecondWord())
    {
        // if there is no second word, we don't know where to go...
        Console.WriteLine("Go where?");
        return;
    }

    string direction = command.SecondWord;

    // Try to go to the next room.
    Room nextRoom = player.CurrentRoom.GetExit(direction);
    if (nextRoom == null)
    {
        Console.WriteLine("There is no door to " + direction + "!");
        return;
    }
	 if (nextRoom == basement && !player.HasItem("key"))
        {
            Console.WriteLine("The door is locked. You need a key to enter the basement.");
            return;
        }

    player.CurrentRoom = nextRoom;
    player.Damage(15);
    Console.WriteLine(player.CurrentRoom.GetLongDescription());
}


	private void look()
	{
		Console.WriteLine(player.CurrentRoom.GetLongDescription()); 

	}

	private void PlayerStatus()
	{
		Console.WriteLine("you have " + player.GetHealth() + " health left");
		Console.WriteLine("you have the items: "+player.GetItems());
		Console.WriteLine("you have " + player.GetWeight() + "kg free space left in your inventory");
	}

	//methods
	private void Take(string itemName)
	{
		player.TakeFromChest(itemName);
	}

	private void Drop(string itemName)
	{
		player.DropToChest(itemName);
	}

	private void Use(Command command)
	{
		if (!command.HasSecondWord()) {
			Console.WriteLine("Use what item?");
			return;
		}

		string status = player.Use(command.SecondWord, command.ThirdWord);
		Console.WriteLine(status);
	}
}




