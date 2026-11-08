namespace DomowyBudzet.Model.DataModels;

public class Category {
    public int Id { get; set; }
    public CategoryType Type { get; set; }

    public Category() {
        Id = 0;
        Type = CategoryType.Other;
    }
    public Category(int id, CategoryType type) {
        if(id < 0)
            throw new ArgumentException("ID can't be less than 0");

        Id = id;
        Type = type;
    }

    public override string ToString() {
        return $"ID: {Id}, Category: {Type}";
    }
}
