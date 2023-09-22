using System;
using System.Collections.Generic;

class Product
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    public Product(string name, decimal price, int quantity)
    {
        Name = name;
        Price = price;
        Quantity = quantity;
    }
}

class ShoppingCart
{
    private List<Product> productsInCart = new List<Product>();

    public void AddToCart(Product product)
    {
        productsInCart.Add(product);
    }

    public List<Product> GetCartContents()
    {
        return productsInCart;
    }

    public void ViewCartContents()
    {
        Console.WriteLine("Items in your cart:");
        foreach (Product product in productsInCart)
        {
            Console.WriteLine($"{product.Name} - {product.Price:C} ({product.Quantity} in stock)");
        }
    }
}

public class User
{
    public decimal Currency { get; set; }
    public ShoppingCart Cart { get; } = new ShoppingCart();

    public User(decimal initialCurrency)
    {
        Currency = initialCurrency;
    }

    public bool SpendCurrency(decimal amount)
    {
        if (Currency >= amount)
        {
            Currency -= amount;
            return true;
        }
        else
        {
            Console.WriteLine("Insufficient funds.");
            return false;
        }
    }
}

public class Store
{
    private List<Product> availableProducts = new List<Product>
    {
        new Product("Gold Shoes", 500m, 5),
        new Product("Silver Shoes", 400m, 5),
        new Product("Bronze Shoes", 300m, 5),
        new Product("Copper Shoes", 200m, 5)
    };

    public List<Product> GetAvailableProducts()
    {
        return availableProducts;
    }

    public bool BuyProduct(User user, Product product)
    {
        if (product.Quantity > 0)
        {
            if (user.SpendCurrency(product.Price))
            {
                product.Quantity--;
                user.Cart.AddToCart(product);
                return true;
            }
        }
        else
        {
            Console.WriteLine("Not in stock.");
        }

        return false;
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Shoe Medals Shop");

        Random random = new Random();
        decimal initialCurrency = random.Next(1000, 5000);

        User user = new User(initialCurrency);
        Store store = new Store();

        while (true)
        {
            Console.WriteLine($"Your balance: {user.Currency:C}");
            Console.WriteLine("Available products:");

            List<Product> availableProducts = store.GetAvailableProducts();
            for (int i = 0; i < availableProducts.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {availableProducts[i].Name} - {availableProducts[i].Price:C} ({availableProducts[i].Quantity} in stock)");
            }

            Console.WriteLine("To buy a product, enter its number (1-4),");
            Console.WriteLine("to view your cart, enter 'c', or 'q' to quit:");

            string input = Console.ReadLine();

            if (input == "q")
            {
                break;
            }
            else if (input == "c")
            {
                user.Cart.ViewCartContents();
            }
            else
            {
                int choice;
                if (int.TryParse(input, out choice) && choice >= 1 && choice <= availableProducts.Count)
                {
                    Product selectedProduct = availableProducts[choice - 1];
                    if (store.BuyProduct(user, selectedProduct))
                    {
                        Console.WriteLine("Added to cart.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Try again.");
                }
            }

            Console.Clear();
        }

        List<Product> cartContents = user.Cart.GetCartContents();
        Console.WriteLine("Items in your cart:");
        foreach (Product product in cartContents)
        {
            Console.WriteLine($"{product.Name} - {product.Price:C}");
        }
    }
}