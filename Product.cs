namespace InventoryManagementSystem
{
    public class Product
    {
        public string Name {get; set;}
        public double Price {get; set;}
        public int Quantity {get; set;}

        public Product(string name, double price, int quantity)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Product name cannot be null or empty.");
            }
            if (price < 0)
            {
                throw new ArgumentException($"Price {price} cannot be negative.");
            }
            if (quantity < 0)
            {
                throw new ArgumentException($"Quantity {quantity} cannot be negative.");
            }

            this.Name = name;
            this.Price = price;
            this.Quantity = quantity;
        }

        public override string ToString()
        {
            return $"Product Name: {Name}, Price: ${Price}, Quantity: {Quantity}";
        }
    }
}
