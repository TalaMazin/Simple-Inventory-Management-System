namespace InventoryManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Inventory inventory = new Inventory();
            bool running = true;

            while (running)
            {
                Console.WriteLine();
                Console.WriteLine("===== Inventory Management System =====");
                Console.WriteLine("1. Add a product");
                Console.WriteLine("2. View all products");
                Console.WriteLine("3. Edit a product");
                Console.WriteLine("4. Delete a product");
                Console.WriteLine("5. Search for a product");
                Console.WriteLine("6. Exit");
                Console.Write("Choose an option (1-6): ");

                string choice = (Console.ReadLine() ?? "").Trim();
                Console.WriteLine();

                if (choice is "2" or "3" or "4" or "5" && inventory.IsEmpty())
                {
                    Console.WriteLine("The inventory is empty. Add a product first.");
                    continue;
                }

                switch (choice)
                {
                    case "1":
                        AddProduct(inventory);
                        break;
                    case "2":
                        ViewProducts(inventory);
                        break;
                    case "3":
                        EditProduct(inventory);
                        break;
                    case "4":
                        DeleteProduct(inventory);
                        break;
                    case "5":
                        SearchProduct(inventory);
                        break;
                    case "6":
                        Console.WriteLine("Goodbye!");
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please choose 1-6.");
                        break;
                }
            }
        }

        static void AddProduct(Inventory inventory)
        {
            Console.WriteLine("(Type 'cancel' at any time to go back to the menu.)");

            string? name;
            while (true)
            {
                name = ReadInput("Enter product name: ");
                if (name == null) return;

                if (name == "")
                {
                    Console.WriteLine("Name cannot be empty. Try again.");
                }
                else if (inventory.FindProductByName(name) != null)
                {
                    Console.WriteLine($"A product named '{name}' already exists. Try another name.");
                }
                else
                {
                    break;
                }
            }

            double price;
            while (true)
            {
                string? input = ReadInput("Enter product price: ");
                if (input == null) return;

                if (double.TryParse(input, out price) && price >= 0)
                {
                    break;
                }
                Console.WriteLine("Please enter a valid non-negative number.");
            }

            int quantity;
            while (true)
            {
                string? input = ReadInput("Enter product quantity: ");
                if (input == null) return;

                if (int.TryParse(input, out quantity) && quantity >= 0)
                {
                    break;
                }
                Console.WriteLine("Please enter a valid non-negative whole number.");
            }

            try
            {
                inventory.AddProduct(new Product(name, price, quantity));
                Console.WriteLine("Product added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void ViewProducts(Inventory inventory)
        {
            Console.WriteLine("----- All Products -----");
            foreach (Product product in inventory.GetAllProducts())
            {
                Console.WriteLine(product);
            }
        }

        static void EditProduct(Inventory inventory)
        {
            Console.WriteLine("(Type 'cancel' at any time to go back to the menu.)");

            Product? product;
            while (true)
            {
                string? name = ReadInput("Enter the name of the product to edit: ");
                if (name == null) return;

                if (name == "")
                {
                    Console.WriteLine("Name cannot be empty. Try again.");
                    continue;
                }

                product = inventory.FindProductByName(name);
                if (product != null)
                {
                    break;
                }
                Console.WriteLine($"Product '{name}' not found. Try again.");
            }

            Console.WriteLine($"Current details: {product}");
            Console.WriteLine("Press Enter to keep the current value.");

            string? newName;
            while (true)
            {
                newName = ReadInput($"Enter new name for [{product.Name}]: ");
                if (newName == null) return;

                if (newName == "")
                {
                    newName = product.Name;
                    break;
                }

                Product? sameName = inventory.FindProductByName(newName);
                if (sameName == null || sameName == product)
                {
                    break;
                }
                Console.WriteLine($"A product named '{newName}' already exists. Try another name.");
            }

            double newPrice;
            while (true)
            {
                string? input = ReadInput($"Enter new price [{product.Price}]: ");
                if (input == null) return;

                if (input == "")
                {
                    newPrice = product.Price;
                    break;
                }
                if (double.TryParse(input, out newPrice) && newPrice >= 0)
                {
                    break;
                }
                Console.WriteLine("Please enter a valid non-negative number.");
            }

            int newQuantity;
            while (true)
            {
                string? input = ReadInput($"Enter new quantity [{product.Quantity}]: ");
                if (input == null) return;

                if (input == "")
                {
                    newQuantity = product.Quantity;
                    break;
                }
                if (int.TryParse(input, out newQuantity) && newQuantity >= 0)
                {
                    break;
                }
                Console.WriteLine("Please enter a valid non-negative whole number.");
            }

            try
            {
                inventory.UpdateProduct(product.Name, newName, newPrice, newQuantity);
                Console.WriteLine("Product updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void DeleteProduct(Inventory inventory)
        {
            Console.WriteLine("(Type 'cancel' at any time to go back to the menu.)");

            while (true)
            {
                string? name = ReadInput("Enter the name of the product to delete: ");
                if (name == null) return;

                if (name == "")
                {
                    Console.WriteLine("Name cannot be empty. Try again.");
                    continue;
                }

                Product? product = inventory.FindProductByName(name);
                if (product != null)
                {
                    inventory.RemoveProduct(product.Name);
                    Console.WriteLine("Product deleted successfully.");
                    return;
                }
                Console.WriteLine($"Product '{name}' not found. Try again.");
            }
        }

        static void SearchProduct(Inventory inventory)
        {
            Console.WriteLine("(Type 'cancel' at any time to go back to the menu.)");

            while (true)
            {
                string? name = ReadInput("Enter the name of the product to search for: ");
                if (name == null) return;

                if (name == "")
                {
                    Console.WriteLine("Name cannot be empty. Try again.");
                    continue;
                }

                Product? product = inventory.FindProductByName(name);
                if (product != null)
                {
                    Console.WriteLine("Product found:");
                    Console.WriteLine(product);
                    return;
                }
                Console.WriteLine($"Product '{name}' not found. Try again.");
            }
        }

        static string? ReadInput(string prompt)
        {
            Console.Write(prompt);
            string input = (Console.ReadLine() ?? "").Trim();

            if (input.Equals("cancel", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Cancelled. Returning to the main menu.");
                return null;
            }
            return input;
        }
    }
}
