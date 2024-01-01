using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace Laba3_
{
    public sealed class NoteBook
    {
        private static Book contacts;
        private static List<Contact> c;

        public NoteBook()
        {
            Instruction();
            ConfigBook();
            Contact k = new Contact();
            k.Name = "pou";
            k.Surname = "Konn";
            k.Phone = "555";
            k.Email = "pou";
            Contact p = new Contact();
            p.Name = "pou";
            p.Surname = "Konn";
            p.Phone = "555";
            p.Email = "pou";
            contacts.Contacts.Add(k);
            //contacts.Contacts.Add(p);
            while (true) { GetInformation(); }
        }
        
        private static void ConfigBook()
        {
            contacts = new Book() { Contacts = new List<Contact>() };
        }

        private static void Instruction()
        {
            Console.WriteLine("Enter the number of action and press [Enter]. Then follow instructions.");
            Console.WriteLine("Menu: ");
            Console.WriteLine("1. View all contacts");
            Console.WriteLine("2. Search");
            Console.WriteLine("3. New contact");
            Console.WriteLine("4. Exit");
            Console.WriteLine("5. Save File");
            Console.WriteLine("6. Open File");
        }

        private static void GetInformation()
        {
            Console.Write(">");
            string menu = Console.ReadLine();

            if (menu.Length > 1)
            {
                Console.WriteLine("Try again.");
            }
            else
            {
                try
                {
                    switch (menu)
                    {
                        case "1":
                            PrintAll();
                            break;
                        case "2":
                            SearchMethod();
                            break;
                        case "3":
                            ConstructContact();
                            break;
                        case "4":
                            Environment.Exit(0);
                            break;
                        case "5":
                            Save();
                            break;
                        case "6":
                            Open();
                            break;
                        default: throw new Exception();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Impossible!");
                }
            }
            Instruction();
        }

        public static void Add(Contact contact)
        {
            if (contact == null)
                Console.WriteLine("Fill the form again.");
            contacts.Contacts.Add(contact);
            if (contacts.Contacts.Contains(contact))
            {
                Console.WriteLine("Contact added.");
            }
            else
            {
                Console.WriteLine("Problem occured");
            }

        }
        public static void PrintAll()
        {
            for (int i = 0; i < contacts.Contacts.Count; i++)
            {
                if (contacts.Contacts[i] == null) { contacts.Contacts.RemoveAt(i); continue; }
                Console.WriteLine(contacts.Contacts[i].ToString() + Environment.NewLine);
            }
            Console.WriteLine("All contacts are printed");
        }

        public static void SearchMethod()
        {
            Console.WriteLine("Search: ");
            Console.WriteLine("1. Name");
            Console.WriteLine("2. Surname");
            Console.WriteLine("3. Name and Surname");
            Console.WriteLine("4. Phone");
            Console.WriteLine("5. E-mail");

            Console.Write(">");
            string menu = Console.ReadLine();


            if (menu.Length > 1)
            {
                Console.WriteLine("Try again.");
            }
            else
            {
                try
                {
                    string str = InsertSearch();
                    switch (menu)
                    {
                        case "1":
                            c = contacts.Contacts.FindAll(s => s.Name.IndexOf(str, StringComparison.OrdinalIgnoreCase) >= 0);
                            Print(c);
                            break;
                        case "2":
                            c = contacts.Contacts.FindAll(s => s.Surname.IndexOf(str, StringComparison.OrdinalIgnoreCase) >= 0);
                            Print(c);
                            break;
                        case "3":
                            c = contacts.Contacts.FindAll(s => s.Name.IndexOf(str, StringComparison.OrdinalIgnoreCase) >= 0 || s.Surname.IndexOf(str, StringComparison.OrdinalIgnoreCase) >= 0);
                            Print(c);
                            break;
                        case "4":
                            c = contacts.Contacts.FindAll(s => s.Phone.IndexOf(str, StringComparison.OrdinalIgnoreCase) >= 0);
                            Print(c);
                            break;
                        case "5":
                            c = contacts.Contacts.FindAll(s => s.Email.IndexOf(str, StringComparison.OrdinalIgnoreCase) >= 0);
                            Print(c);
                            break;
                        default: throw new Exception();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Impossible");
                }
            }
        }

        private static void Print(List<Contact> c)
        {
            int k = c.Count;
            Console.WriteLine("Searching..." + Environment.NewLine + "Results " + "(" + k + ")");
            for (int i = 0; i < k; i++)
            {
                Console.WriteLine("#" + (i + 1) + " " + c[i].ToString());
                Console.WriteLine();
            }
            c.Clear();
        }
        private static string InsertSearch()
        {
            Console.Write(">");
            string str = Console.ReadLine();
            Console.WriteLine("Request: " + str);
            return str;
        }

        private static void InsertInfo(Contact c)
        {
            Console.WriteLine("Nickname:");
            c.Nickname = Console.ReadLine();
            Console.WriteLine("Name:");
            c.Name = Console.ReadLine();
            Console.WriteLine("Surname:");
            c.Surname = Console.ReadLine();
            Console.WriteLine("Phone:");
            c.Phone = Console.ReadLine();
            Console.WriteLine("Email:");
            c.Email = Console.ReadLine();
            Console.WriteLine("Birth date in US style:");
            c.Birthday = DateTime.Parse(Console.ReadLine());
        }
        
        private static void ConstructContact()
        {
            Contact c = new Contact();
            InsertInfo(c);
            Add(c);
        }
        
        public static void Save()
        {
            Console.WriteLine("1. Json");
            Console.WriteLine("2. Xml");
            Console.WriteLine("3. SQLite");
            
            var tryParseInput = int.TryParse(Console.ReadLine(), out var input) && (input >= 1 && input <= 3);
            while (!tryParseInput)
            {
                Console.WriteLine("Try again.");
                tryParseInput = int.TryParse(Console.ReadLine(), out input) && (input >= 1 && input <= 3);
            }
            int format = input;
            Console.WriteLine("Choose path.");
            String path = Console.ReadLine();
            try
            {
                switch (format)
                {
                    case 1:
                        File.WriteAllText(path, JsonSerializer.Serialize(NoteBook.contacts));
                        Console.WriteLine("File saved");
                        break;
                    case 2:
                        StreamWriter writer = new StreamWriter(path);
                        new XmlSerializer(typeof(Book)).Serialize(writer, NoteBook.contacts);
                        Console.WriteLine("File saved.");
                        break;
                    case 3:
                        AppDbContext context = new AppDbContext(path);
                        context.Database.EnsureCreated();
                        context.Contacts.ExecuteDelete();
                        context.Contacts.AddRange(NoteBook.contacts.Contacts);
                        context.SaveChanges();
                        Console.WriteLine("File saved.");
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private static void Open()
        {
            Console.WriteLine("1. Json");
            Console.WriteLine("2. Xml");
            Console.WriteLine("3. SQLite");
            
            var tryParseInput = int.TryParse(Console.ReadLine(), out var input) && (input >= 1 && input <= 3);
            while (!tryParseInput)
            {
                Console.WriteLine("Try again.");
                tryParseInput = int.TryParse(Console.ReadLine(), out input) && (input >= 1 && input <= 3);
            }
            int format = input;
            Console.WriteLine("Choose path.");
            String path = Console.ReadLine();
            try
            {
                Book storage = null;
                switch (format)
                {
                    case 1:
                        storage = JsonSerializer.Deserialize<Book>(File.ReadAllText(path))!;
                        break;
                    case 2:
                        StreamReader reader = new StreamReader(path);
                        storage = (Book)new XmlSerializer(typeof(Book)).Deserialize(reader)!;
                        break;
                    case 3:
                        AppDbContext context = new AppDbContext(path);
                        context.Database.EnsureCreated();
                        storage = new Book() { Contacts = context.Contacts.ToList() };
                        break;
                }

                NoteBook.contacts = storage;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        }
    }