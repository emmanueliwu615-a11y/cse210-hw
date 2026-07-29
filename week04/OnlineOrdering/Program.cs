using System;

class Program
{
    static void Main(string[] args)
    {
        // Order 1 - USA customer
        Address address1 = new Address("123 Palm Street", "Austin", "TX", "USA");
        Customer customer1 = new Customer("Grace Whitfield", address1);
        Order order1 = new Order(customer1);

        order1.AddProduct(new Product("Wireless Mouse", "A100", 25.00, 2));
        order1.AddProduct(new Product("Mechanical Keyboard", "A101", 75.00, 1));
        order1.AddProduct(new Product("USB-C Hub", "A102", 30.00, 3));

        // Order 2 - Non-USA customer
        Address address2 = new Address("15 Adeola Odeku Street", "Lagos", "Lagos State", "Nigeria");
        Customer customer2 = new Customer("Chinedu Okafor", address2);
        Order order2 = new Order(customer2);

        order2.AddProduct(new Product("Bluetooth Speaker", "B200", 45.00, 1));
        order2.AddProduct(new Product("Phone Case", "B201", 15.00, 2));

        Order[] orders = { order1, order2 };

        foreach (Order order in orders)
        {
            Console.WriteLine("Packing Label:");
            Console.WriteLine(order.GetPackingLabel());
            Console.WriteLine();

            Console.WriteLine("Shipping Label:");
            Console.WriteLine(order.GetShippingLabel());
            Console.WriteLine();

            Console.WriteLine($"Total Price: ${order.GetTotalCost():0.00}");
            Console.WriteLine(new string('=', 40));
        }
    }
}