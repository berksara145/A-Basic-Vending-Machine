using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FirstAplication
{
    internal class VendingMachine
    {
        private List<Product> products;
        public List<Product> Products
        {
            get { return products; }
            set { products = value; }
        }
        private double money;
        public double Money
        {
            get { return money; }
            set { money = value; }
        }
        private double givenMoney;
        public double GivenMoney
        {
            get { return givenMoney; }
            set { givenMoney = value; }
        }

        public VendingMachine(double money, List<Product> products)
        {
            if (money < 0) { money = 0; }
            else { this.money = money; }
            if (products == null) { products = new List<Product>(); }
            else { this.products = products; }
            this.givenMoney = 0;
        }

        public double giveChange()
        {
            return givenMoney;
        }

        

        //method to add product
        public bool addProduct(Product p)
        {
            if (products.All(a => a.Id != p.Id))
            {
                products.Add(p);
            }
            return true;
        }
        //method to give money
        public void depositMoney(double enteredmoney)
        {
            givenMoney += enteredmoney;
        }
        //method take the product
        public Product giveProduct(string selectedProduct)
        {
            for (int i = 0; i < this.products.Count; i++)
            {
                if (Equals(selectedProduct, products[i].Name))
                {
                    if (givenMoney - products[i].Price < 0)
                    {
                        Console.WriteLine("You dont have moneyyyy");
                        return null;
                    }
                    else
                    {
                        this.givenMoney -= products[i].Price;
                        Product pro = this.products[i];
                        products.RemoveAt(i);
                        return pro;

                    }

                }
            }
            return null;
        }
    }

    internal class Product
    {
        private int _Id;
       
        public int Id
        {
            get { return _Id; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { this.name = value; }
        }
        private double price;
        public double Price
        {
            get { return price; }
            set { this.price = value; }
        }

        public Product(string name, double price)
        {
            this.name = name;
            if (price < 0) { price = 0; } else { this.price = price; }
        }
        public override string ToString()
        {
            return name + " is only " + price + " TL" + "\n";
        }
    }
    internal class VendingMachineSimulator
    {
        public static void saveData(VendingMachine vmac)
        {
            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(vmac);
            File.WriteAllText(@"C:\Users\berk.sara\source\repos\FirstAplication\FirstAplication\tsconfig2.json", jsonString);
        }

        public static Object loadData()
        {
            using (StreamReader r = new StreamReader(@"C:\Users\berk.sara\source\repos\FirstAplication\FirstAplication\tsconfig2.json"))
            {
                var jsonStr = r.ReadToEnd();
                VendingMachine objec = Newtonsoft.Json.JsonConvert.DeserializeObject<VendingMachine>(jsonStr);
                return objec;
            }

        }
        public static void Main(string[] args)
        {
            VendingMachine vmac = new VendingMachine(4, new List<Product>());
            
            Product p1 = new Product("elma", 1);
            Product p2 = new Product("armut", 1.5);
            Product p3 = new Product("patates", 3);
            Product p4 = new Product("jumbo karides", 10);

            vmac.Products.Add(p1);
            vmac.Products.Add(p2);
            vmac.Products.Add(p3);
            vmac.Products.Add(p4);

            saveData(vmac);
            
            VendingMachine vender = new VendingMachine(5, new List<Product>());
            vmac = (VendingMachine ) loadData();

            Console.WriteLine("WELCOME TO THE VENDING MACHINE");

            string answer = "n";

            while (answer != "0")
            {
                Console.WriteLine("To exit select 0");
                Console.WriteLine("To display products select 1");
                Console.WriteLine("To deposit money select 2");
                Console.WriteLine("To purchase product select 3");
                Console.WriteLine("To display the current deposit select 4");
                answer = Console.ReadLine();
                if (answer == "1")
                {
                    Console.WriteLine("products avaiable:");
                    for (int i = 0; i < vmac.Products.Count; i++)
                    {
                        Console.WriteLine(vmac.Products[i].ToString());
                    }
                }
                else if (answer == "2")
                {
                    Console.WriteLine("write the amount of money to deposit");
                    double deposit = Convert.ToDouble(Console.ReadLine());
                    vmac.depositMoney(deposit);
                    Console.WriteLine("thank you");
                }
                else if (answer == "3")
                {
                    Console.WriteLine("write the name of the product");
                    string name = Console.ReadLine();
                    if (vmac.giveProduct(name) == null)
                    {
                        Console.WriteLine("You couldn't buy");
                    }
                    else
                    {
                        Console.WriteLine("Congratss!");
                    }

                }
                else if (answer == "4")
                {
                    Console.WriteLine("current deposited money is " + vmac.GivenMoney);
                }
                else
                {
                    Console.WriteLine("Please input a proper number!!!");
                }
            }
            loadData();

        }
    }
}


