class Player
{

    //auto property

    //fields
       private bool medkitUsed = false;
    private int health;

    private Inventory backpack;

    public Room CurrentRoom { get; set; }

    //constructor
    public Player()
    {
        backpack = new Inventory(25);

        CurrentRoom = null;

        health = 100;
    }

public string Use(string itemName, string target) {
    switch (itemName) {
        case "medkit":
            if (!medkitUsed) {
                medkitUsed = true;
                int amountHealed = medkit();
                if (backpack.Remove("medkit")) { 
                    return "You've healed " + amountHealed + " health! The medkit has been consumed.";
                }
                else {
                    return "Error: Medkit not found in inventory."; // Medkit not found in inventory
                }
            }
            else {
                return "Medkit has already been used!";
            }
        default:
            return "Couldn't find item " + itemName;
            
    }
}

private int medkit() {
    int amountHealed = Heal(50);
    return amountHealed;
}

    public int GetHealth()
    {
        return health;
    }

    public int GetWeight()
    {
        return backpack.FreeWeight();
    }

    public string GetItems() {
        return backpack.GetItems();
    }

    public void Damage(int amount)
    {
        health -= amount;
    }

private int Heal(int amount)
{
    int healthBeforeHeal = health;
    health += amount;
    
    if (health > 100)
    {
        int excess = health - 100;
        health = 100;
        return amount - excess; // Return the actual amount healed
    }
    return amount; 
}


    public bool IsAlive()
    {
        return health > 0;
    }

    //methods
   // Player class
public bool TakeFromChest(string itemName)
{
    Item item = CurrentRoom.Chest.Get(itemName);

    if (item != null)
    {
        if (backpack.Put(itemName, item))
        {
            Console.WriteLine("Successfully picked up " + itemName);

            // Check if the player has picked up the mythical bag
            if (itemName == "mythical-bag")
            {
                Console.WriteLine("Congratulations!");
                Console.WriteLine(" You've found the mythical bag with 10.000 v-bucks");
                Console.WriteLine("You win!");
                EndGame(); // End the game
            }

            return true;
        }
        else
        {
            Console.WriteLine("Can't pick up item! it's too heavy, no storage left");

            CurrentRoom.Chest.Put(itemName, item);

            return false;
        }
    }

    Console.WriteLine("Couldn't find item!");

    return false;
}

private void EndGame()
{
    // Perform any endgame tasks here
    Environment.Exit(0); // Exit the game
}


    public bool DropToChest(string itemName)
    {
        Item item = backpack.Get(itemName);

        if (item != null)
        {
            if (CurrentRoom.Chest.Put(itemName, item))
            {
                Console.WriteLine("Succesfully dropped " + itemName);

                return true;
            }
            else
            {
                backpack.Put(itemName, item);
            }
        }

        Console.WriteLine("Couldn't find item in your inventory");

        return false;
    }


public bool HasItem(string itemName)
{
    // Iterate over the items in the inventory and check if any match the specified item
    foreach (var item in backpack.GetItems())
    {
        if (itemName == "key") // Assuming GetItemDescription returns the item name
        {
            return true; // Found the item
        }
    }
    return false; // Item not found
}

    
}