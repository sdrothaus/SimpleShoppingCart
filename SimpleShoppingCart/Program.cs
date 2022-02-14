// See https://aka.ms/new-console-template for more information
using System;
using System.Collections;
using System.Collections.Generic;

public interface ITerminal
{
    void Scan(string item);
    decimal Total();
}

class Resullt : ITerminal
{
    List<string> scannedItems = new List<string>();
    Hashtable productDetails = new Hashtable();
    public enum ProductType
    {
        A,
        B,
        C,
        D,
    }
    public void Scan(string item)
    {
        scannedItems.Add(item.ToUpper());
    }
    public decimal Total()
    {
        double totalValue = 0;
        foreach (DictionaryEntry entry in productDetails)
        {
            if (scannedItems.Contains(entry.Key))
            {
                double price;
                double count;
                double bulkSize;
                double bulkPrice;
                List<double>? t = entry.Value as List<double>;
                price = t.ElementAt(0);
                count = t.ElementAt(1);
                bulkSize = t.ElementAt(2);
                bulkPrice = t.ElementAt(3);
                do
                {
                    if(bulkSize == 0) {
                        totalValue = totalValue + price * count;
                        count = 0;
                    }
                    else
                    {   if (count >= bulkSize)
                        {
                            totalValue = totalValue + bulkPrice;
                            count = count - bulkSize;
                        }
                        else
                        {
                            totalValue = totalValue + price * count;
                            count = 0;
                        }
                    }
                }
                while (count > 0);
            }
        }
        return (decimal)totalValue;
    }
    public void setDetails(ProductType item)
    {   
        List<double> details = new List<double>();
        if (item == ProductType.A)
        {
            details.Add(2.00);
            details.Add((double)scannedItems.Where(x => x == item.ToString()).Count());
            details.Add(4.00);
            details.Add(7.00);
        }
        else
        {
            if (item == ProductType.C)
            {
                details.Add(1.25);
                details.Add((double)scannedItems.Where(x => x == item.ToString()).Count());
                details.Add(6.00);
                details.Add(6.00);
            }
            else
            {
                if (item == ProductType.D)
                {
                    details.Add(0.15);
                    details.Add((double)scannedItems.Where(x => x == item.ToString()).Count());
                    details.Add(0.00);
                    details.Add(0.00);
                }
                else
                    if (item == ProductType.B)
                    {
                        details.Add(12.00);
                        details.Add((double)scannedItems.Where(x => x == item.ToString()).Count());
                        details.Add(0.00);
                        details.Add(0.00);
                    }
            }
        }
        productDetails.Add(item.ToString(), details);
    }
    public void setValues()
    {
        setDetails(ProductType.A);
        setDetails(ProductType.B);
        setDetails(ProductType.C);
        setDetails(ProductType.D);
    }
}
public class Program
{
    public static void Main(string[] args)
    {
        Resullt result = new Resullt();
        int totalScannedItems = 0;
        Console.WriteLine("Enter the number of items you want to Scan:");
        do
        {
            if (int.TryParse(Console.ReadLine(), out totalScannedItems))
                if (totalScannedItems > 0)
                    break;
            Console.WriteLine("Error: enter a valid Number!");
        }
        while (totalScannedItems <= 0);

        Console.WriteLine("Enter the items you want to scan:");
        for (int i = 0; i < totalScannedItems; i++)
        {
            result.Scan(Console.ReadLine());
        }
        result.setValues();
        string total = result.Total().ToString("0.00");
        Console.WriteLine("Total Price is ${0}", total);
    }
}
