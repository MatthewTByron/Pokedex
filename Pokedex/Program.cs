using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Pokedex
{
    class Program
    {
        static void Main(string[] args)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo directory = new DirectoryInfo(currentDirectory);
            var fileName = Path.Combine(directory.FullName, "pokemon.csv");
            PokeDexRepository pokeDexRepository = new PokeDexRepository(fileName);
            UserInputHandler userInputHandler = new UserInputHandler(pokeDexRepository);
            Console.WriteLine("╔═════════════════════════════════════════════════════╗");
            Console.WriteLine("║  Hello! Welcome to the wonderful world of Pokémon!  ║");
            Console.WriteLine("╚═════════════════════════════════════════════════════╝");
            while (true)
            {
                Console.WriteLine("╔══════════════════════════════════════════════╗");
                Console.WriteLine("║           What do you want to do?            ║");
                Console.WriteLine("╠══════════════════════════════════════════════╣");
                Console.WriteLine("║                                              ║");
                Console.WriteLine("║  > Press 1 for PokéSearch!                   ║");
                Console.WriteLine("║                                              ║");
                Console.WriteLine("║  > Press 2 to add newly discovered Pokémon!  ║");
                Console.WriteLine("║                                              ║");
                Console.WriteLine("║  > Press 3 to edit Pokédex entries!          ║");
                Console.WriteLine("║                                              ║");
                Console.WriteLine("║  > Press 4 to delete Pokédex entries!        ║");
                Console.WriteLine("║                                              ║");
                Console.WriteLine("║  > Press 5 to quit.                          ║");
                Console.WriteLine("║                                              ║");
                Console.WriteLine("╚══════════════════════════════════════════════╝");
                var userInput = Console.ReadKey();
                Console.Clear();
                if (userInput.KeyChar == '1')
                {
                    userInputHandler.HandlePokeSearch();
                }
                else if (userInput.KeyChar == '2')
                {
                    userInputHandler.HandleAdd();
                }
                else if (userInput.KeyChar == '3')
                {
                    userInputHandler.HandlePokedexEdit();
                }
                else if (userInput.KeyChar == '4')
                {
                    userInputHandler.HandleDelete();
                }
                else if (userInput.KeyChar == '5')
                {
                    break;
                }
                else
                {
                    Console.WriteLine("That is not a valid input. Please try again.");
                }
                userInputHandler.Clear();
            }
        }        
    }
}
