using System;

namespace PostcodeParser
{
    class Program
    {
        static void Main()
        {
            var postcode = new Postcode("w1a0ax");

            Console.WriteLine($"Normalized: {postcode}");
            Console.WriteLine($"Outward Code: {postcode.OutwardCode}");
            Console.WriteLine($"Area: {postcode.Area}");
            Console.WriteLine($"District: {postcode.District}");
            Console.WriteLine($"Inward Code: {postcode.InwardCode}");
            Console.WriteLine($"Sector: {postcode.Sector}");
            Console.WriteLine($"Unit: {postcode.Unit}");

            Console.ReadLine();
        }
    }
}
