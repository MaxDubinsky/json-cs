using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace ConsoleApp13
{
    internal class Program
    {
        enum TypeOfItem
        {
            Weapon, 
            Healing, 
            Usage,
            Nothing
        }
        class Item
        {
            public string Name;
            public TypeOfItem TypeOfItem;
            public int Price;
            public Item(string name, TypeOfItem typeOfItem, int price)
            {
                Name = name;
                TypeOfItem = typeOfItem;
                Price = price;
            }

        }
        class Character
        {
            public string name;
            public int level = 0;
            public int hp = 0;
            public int strength = 0;
            public int agility = 0;
            public int intellegence = 0;
            public int money = 0;
            public List<Item> inventory = new List<Item>();
            public Character(string name, int level, int hp, int strength, int agility, int intellegence, int money, List<Item> inventory)
            {
                this.name = name ?? "[No name]";
                this.level = level;
                this.hp = hp;
                this.strength = strength;
                this.agility = agility;
                this.intellegence = intellegence;
                this.money = money;
                this.inventory = inventory;
            }
        }
        class Game
        {
            public List<Character> characters = new List<Character>();
            public List<Item> items = new List<Item>();

            /// ----------------
            /// 
            /// CHARACTERS FUNCTIONS
            /// 
            /// ----------------
            public bool IsCharacterExist(string name)
            {
                foreach (var item in this.characters)
                {
                    if (item.name == name)
                    {
                        return true;
                    }
                }
                return false;
            }
            public Character FindCharacter(string name)
            {
                foreach (var item in this.characters)
                {
                    if (item.name == name)
                    {
                        return item;
                    }
                }
                return null;
            }

            /// ----------------
            /// 
            /// ITEMS FUNCTIONS
            /// 
            /// ----------------
            public bool IsItemExist(string name)
            {
                foreach (var item in this.items)
                {
                    if (item.Name == name)
                    {
                        return true;
                    }
                }
                return false;
            }
            public Item FindItem(string name)
            {
                foreach (var item in this.items)
                {
                    if (item.Name == name)
                    {
                        return item;
                    }
                }
                return null;
            }
            public Game(List<Character> characters, List<Item> items)
            {
                this.characters = characters;
                this.items = items;
            }
        }
        static void Main(string[] args)
        {
            List<Character> characters = new List<Character>();
            List<Item> items = new List<Item>();

            int choice;
            // Doing cycle of program.
            do
            {
                // Creating table.
                Console.WriteLine("/\\                                   /\\");
                Console.WriteLine("---------------------------------------");
                Console.WriteLine("| 1 - Add character                   |");
                Console.WriteLine("| 2 - Add item                        |");
                Console.WriteLine("| 3 - Delete character                |");
                Console.WriteLine("| 4 - Delete item                     |");
                Console.WriteLine("| 5 - Show all characters             |");
                Console.WriteLine("| 6 - Show all items                  |");
                Console.WriteLine("| 7 - Add item to character           |");
                Console.WriteLine("| 8 - Save everything and close file  |");
                Console.WriteLine("---------------------------------------");
                Console.Write("Enter a choice: ");

                // Choosing some variant from the tabel.
                choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    // Add character.
                    case 1:
                        Console.Clear();

                        Console.WriteLine("Enter name of character: ");
                        string name = Console.ReadLine();

                        Console.WriteLine("Enter level of character: ");
                        int level = int.Parse(Console.ReadLine());

                        Console.WriteLine("Enter HP of character: ");
                        int hp = int.Parse(Console.ReadLine());

                        Console.WriteLine("Enter strength of character: ");
                        int strength = int.Parse(Console.ReadLine());

                        Console.WriteLine("Enter agility of character: ");
                        int agility = int.Parse(Console.ReadLine());

                        Console.WriteLine("Enter intellegence of character: ");
                        int intellegence = int.Parse(Console.ReadLine());

                        Console.WriteLine("Enter money of character: ");
                        int money = int.Parse(Console.ReadLine());
                        characters.Add(new Character(name, level, hp, strength, agility, intellegence, money, new List<Item>()));
                        Console.Clear();
                        continue;

                    // Add item.
                    case 2:
                        Console.Clear();

                        Console.Write("Enter a name of item: ");
                        string nameitem = Console.ReadLine();

                        Console.Write("Enter a price of item: ");
                        int price = int.Parse(Console.ReadLine());

                        Console.WriteLine("Choose a type of item");
                        Console.WriteLine("----------------");
                        Console.WriteLine("1 - Weapon");
                        Console.WriteLine("2 - Usage");
                        Console.WriteLine("3 - Healing");
                        Console.WriteLine("----------------");
                        int choiceType = int.Parse(Console.ReadLine());
                        switch (choiceType)
                        {
                            case 1:
                                items.Add(new Item(nameitem, TypeOfItem.Weapon, price));
                                break;
                            case 2:
                                items.Add(new Item(nameitem, TypeOfItem.Usage, price));
                                break;
                            case 3:
                                items.Add(new Item(nameitem, TypeOfItem.Healing, price));
                                break;
                            default:
                                Console.WriteLine("Wrong choice.");
                                continue;
                        }
                        Console.Clear();
                        continue;

                    // Remove character.
                    case 3:
                        Console.Clear();
                        Console.Write("Enter a name of charater to delete: ");
                        string titleToDelete = Console.ReadLine();
                        foreach (var character in characters)
                        {
                            if (character.name == titleToDelete)
                            {
                                characters.Remove(character);
                                Console.WriteLine("Character removed!");
                                Thread.Sleep(2000);
                                break;
                            }
                        }
                        Console.Clear();
                        continue;

                    // Remove item.
                    case 4:
                        Console.Clear();

                        Console.Write("Enter a name of item to delete: ");
                        string titleToDeleteItem = Console.ReadLine();
                        foreach (var item in items)
                        {
                            if (item.Name == titleToDeleteItem)
                            {
                                items.Remove(item);
                                Console.WriteLine("Item removed!");
                                Thread.Sleep(2000);
                                break;
                            }
                        }
                        Console.Clear();
                        continue;

                    // Show all characters.
                    case 5:
                        Console.Clear();

                        if (characters.Count() == 0)
                        {
                            Console.WriteLine("[No items]");
                            Console.WriteLine("Press any key to continue.");
                            Console.ReadKey();
                            Console.Clear();
                        }
                        else
                        {
                            foreach (var item in characters)
                            {
                                Console.WriteLine($"Name: {item.name}, level: {item.level}, agility: {item.agility}, strength: {item.strength}, HP: {item.hp}, Intellegence: {item.intellegence}, Money: {item.money}.");
                                if (item.inventory.Count() == 0)
                                {
                                    Console.WriteLine("No items in inventory");
                                    Console.WriteLine();
                                }
                                else
                                {
                                    foreach (var iteminv in item.inventory)
                                    {
                                        string typeitem = "";
                                        if (iteminv.TypeOfItem == TypeOfItem.Weapon)
                                        {
                                            typeitem = "Weapon";
                                        }
                                        if (iteminv.TypeOfItem == TypeOfItem.Usage)
                                        {
                                            typeitem = "Usage";
                                        }
                                        if (iteminv.TypeOfItem == TypeOfItem.Healing)
                                        {
                                            typeitem = "Healing";
                                        }
                                        Console.WriteLine($"\t{iteminv.Name}, {iteminv.Price}, it's {typeitem}.");
                                    }
                                    Console.WriteLine();
                                }

                            }
                            Console.WriteLine("Press any key to continue.");
                            Console.ReadKey();
                            Console.Clear();

                        }
                        continue;
                    // Show all items.
                    case 6:
                        Console.Clear();

                        if (items.Count() == 0)
                        {
                            Console.WriteLine("[No items]");
                            Console.WriteLine("Press any key to continue.");
                            Console.ReadKey();
                            Console.Clear();
                        }
                        else
                        {
                            foreach (var item in items)
                            {
                                string typeitem = "";
                                if (item.TypeOfItem == TypeOfItem.Weapon)
                                {
                                    typeitem = "Weapon";
                                }
                                if (item.TypeOfItem == TypeOfItem.Usage)
                                {
                                    typeitem = "Usage";
                                }
                                if (item.TypeOfItem == TypeOfItem.Healing)
                                {
                                    typeitem = "Healing";
                                }
                                Console.WriteLine($"{item.Name}, {item.Price}, it's {typeitem}");
                                Console.WriteLine();
                            }
                            Console.WriteLine("Press any key to continue.");
                            Console.ReadKey();
                            Console.Clear();
                        }
                        continue;

                    // Add item to character.
                    case 7:
                        Console.Clear();
                        Console.Write("Enter a name of character: ");
                        string nameOfCharacter = Console.ReadLine();

                        bool characterFound = false;
                        foreach (var character in characters)
                        {
                            if (character.name == nameOfCharacter)
                            {
                                Console.Write("Enter a name of item: ");
                                string nameitemchar = Console.ReadLine();

                                Console.Write("Enter a price of item: ");
                                int pricechar = int.Parse(Console.ReadLine());

                                Console.WriteLine("Choose a type of item");
                                Console.WriteLine("----------------");
                                Console.WriteLine("1 - Weapon");
                                Console.WriteLine("2 - Usage");
                                Console.WriteLine("3 - Healing");
                                Console.WriteLine("----------------");
                                int choiceTypechar = int.Parse(Console.ReadLine());
                                switch (choiceTypechar)
                                {
                                    case 1:
                                        character.inventory.Add(new Item(nameitemchar, TypeOfItem.Weapon, pricechar));
                                        break;
                                    case 2:
                                        character.inventory.Add(new Item(nameitemchar, TypeOfItem.Usage, pricechar));
                                        break;
                                    case 3:
                                        character.inventory.Add(new Item(nameitemchar, TypeOfItem.Healing, pricechar));
                                        break;
                                    default:
                                        Console.WriteLine("Wrong choice.");
                                        break;
                                }
                                characterFound = true;
                                Console.Clear();
                                break;
                            }
                        }
                        if (!characterFound)
                        {
                            Console.WriteLine("No character found with that name! :(");
                            Thread.Sleep(2000);
                            Console.Clear();
                        }
                        continue;

                    // Save JSON file and close the program.
                    case 8:
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine("Wrong choice!");
                        Thread.Sleep(1000);
                        Console.Clear();
                        continue;
                }
            } while (choice != 8);

            /// --------------
            ///
            /// SAVING FILE
            /// 
            /// --------------
            Game game = new Game(characters, items);
            
            var Characters = new
            {
                game.characters,
            };
            var Items = new
            {
                game.items
            };
            
            string jsonCharacters = JsonConvert.SerializeObject(Characters, Formatting.Indented);
            string jsonItems = JsonConvert.SerializeObject(Items, Formatting.Indented);

            using (StreamWriter file = File.CreateText("characters.json"))
            {
                file.Write(jsonCharacters);
                file.Write(jsonItems);
            }
            Console.WriteLine("end");
        }
    }
}
