using System;
using Project.Model;

public class StorageRoom : IShowInfo
{
    public IList<string> Ingredients { get; set; } = new List<string>();

    public void AddIngredient(string ingredient) => Ingredients.Add(ingredient);

    public bool CheckStock(string ingredient) => Ingredients.Contains(ingredient);

    public void DisplayStock() => Console.WriteLine(GetInfo());

    public string GetInfo()
    {
        return $"Storage contains {Ingredients.Count} ingredient types.";
    }
}
