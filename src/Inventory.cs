class Inventory
{
    // fields 
    private int maxWeight;
    private Dictionary<string, Item> items;

    // constructor
    public Inventory(int maxWeight)
    {
        this.maxWeight = maxWeight;
        this.items = new Dictionary<string, Item>();
    }

    // methods
    public bool Put(string itemName, Item item)
    {
        if (item.Weight > FreeWeight()) { return false; }

        items.Add(itemName, item);

        return true;
    }

    public Item Get(string itemName)
    {

        if (items.ContainsKey(itemName))
        {
            Item item = items[itemName];

            items.Remove(itemName);

            return item;
        }
        return null;
    }

    // methods 
    public int TotalWeight()
    {
        int total = 0;

        foreach (Item item in items.Values)
        {
            total += item.Weight;
        }

        return total;
    }

    public int FreeWeight()
    {

        return maxWeight - TotalWeight();
    }

    public string GetItems()
    {
        string str = "";
        int index = 0;

        foreach (string itemName in items.Keys)
        {
            str += itemName + (index < items.ToArray().Length - 1? ", ": ".");
            
            index++;
        }

        return str;
    }

public bool Remove(string itemName)
{
    if (items.ContainsKey(itemName))
    {
        items.Remove(itemName);
        return true;    
    }
    else
    {
        return false; 
    }
}




}