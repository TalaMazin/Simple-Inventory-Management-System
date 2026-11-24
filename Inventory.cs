namespace InventoryManagementSystem
{
    public class Inventory
    {
        private List<Product> products = new List<Product>();
        public void AddProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentException("Product cannot be null.");
            }
            if (FindProductByName(product.Name) != null)
            {
                throw new ArgumentException($"Product with name '{product.Name}' already exists in the inventory.");
            }
            products.Add(product);
        }
        public void RemoveProduct(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Product name cannot be null or empty.");
            }
            Product? productToRemove = FindProductByName(name);
            if (productToRemove == null)
            {
                throw new ArgumentException($"Product with name '{name}' does not exist in the inventory.");
            }
            products.Remove(productToRemove);
        }

        public List<Product> GetAllProducts()
        {
            return products;
        }

        public bool IsEmpty()
        {
            return products.Count == 0;
        }

        public Product? FindProductByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Product name cannot be null or empty.");
            }
            foreach (Product product in products)
            {
                if (string.Equals(product.Name, name, StringComparison.OrdinalIgnoreCase))
                {
                    return product;
                }
            }
            return null;
        }

        public void UpdateProduct(string name, string newName, double newPrice, int newQuantity)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Product name cannot be null or empty.");
            }
            if (string.IsNullOrWhiteSpace(newName))
            {
                throw new ArgumentException("New product name cannot be null or empty.");
            }
            if (newPrice < 0)
            {
                throw new ArgumentException("Price cannot be negative.");
            }
            if (newQuantity < 0)
            {
                throw new ArgumentException("Quantity cannot be negative.");
            }

            Product? productToUpdate = FindProductByName(name);
            if (productToUpdate == null)
            {
                throw new ArgumentException($"Product with name '{name}' does not exist in the inventory.");
            }

            Product? sameName = FindProductByName(newName);
            if (sameName != null && sameName != productToUpdate)
            {
                throw new ArgumentException($"Product with name '{newName}' already exists in the inventory.");
            }

            productToUpdate.Name = newName;
            productToUpdate.Price = newPrice;
            productToUpdate.Quantity = newQuantity;
        }
    }
}