using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        int start = 0;
        int end = 100;
        
        List<int> primeNumbers = await GetPrimeNumbersAsync(start, end);

        Console.WriteLine($"Prime numbers between {start} and {end}:");
        Console.WriteLine(string.Join(", ", primeNumbers));
    }

    static async Task<List<int>> GetPrimeNumbersAsync(int start, int end)
    {
        var tasks = new List<Task<int>>();

        for (int i = start; i <= end; i++)
        {
            int number = i;
            tasks.Add(Task.Run(() => IsPrime(number)));
        }

        var results = await Task.WhenAll(tasks);

        return results.Where(result => result != -1).ToList(); //remove the -1 returns
    }

    static int IsPrime(int number)
    {
        if (number <= 1) return -1;
        if (number == 2) return number;

        for (int i = 2; i <= Math.Sqrt(number); i++)
        {
            if (number % i == 0) return -1;
        }

        return number;
    }
}
