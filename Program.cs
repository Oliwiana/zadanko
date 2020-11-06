using ConsoleTables;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace zadanko
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("\nEnter file name with extension ");
            var fileName = Console.ReadLine();

            if (!File.Exists(fileName))
            {
                Console.WriteLine("File dosent exist");
                return;
            }

            FileStream filestream = new FileStream( fileName +"_formatted.txt" , FileMode.Create);
            var streamwriter = new StreamWriter(filestream);

            streamwriter.AutoFlush = true;
            Console.SetOut(streamwriter);
            Console.SetError(streamwriter);

            StreamReader sr = new StreamReader(fileName);
            string jsonString = sr.ReadToEnd();

            Root deserialized = JsonConvert.DeserializeObject<Root>(jsonString);

            Console.WriteLine("BOOKS:");

            //write all books automatic
            ConsoleTable.From<Book>(deserialized.store.book).Write();


            //write all books manual

            // var booksTable = new ConsoleTable("title", "author", "category", "isbn", "price");

            // foreach (var book in deserialized.store.book)
            // {
            //     booksTable.AddRow(book.title, book.author, book.category, book.isbn, book.price);
            // }
            // booksTable.Write();


            Console.WriteLine();

            //show bicycle
            Console.WriteLine("BICYCLE:");
            var bicycleTable = new ConsoleTable("color", "price");
            bicycleTable.AddRow(deserialized.store.bicycle.color, deserialized.store.bicycle.price);
            bicycleTable.Write();

        }
    }
}
public class Book
{
    public string category { get; set; }
    public string author { get; set; }
    public string title { get; set; }
    public double price { get; set; }
    public string isbn { get; set; }
}

public class Bicycle
{
    public string color { get; set; }
    public double price { get; set; }
}

public class Store
{
    public List<Book> book { get; set; }
    public Bicycle bicycle { get; set; }
}

public class Root
{
    public Store store { get; set; }
}