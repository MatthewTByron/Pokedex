using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex
{
    public class UserInputHandler
    {
        private PokeDexRepository PokeDexRepository;
        public UserInputHandler(PokeDexRepository pokeDexRepository)
        {
            PokeDexRepository = pokeDexRepository;
        }
        /// <summary>
        /// Handles user interaction to delete a Pokemon from the Pokedex.
        /// </summary>
        /// <param name="pokeDexRepository"></param>
        public void HandleDelete()
        {
            Console.WriteLine("╔══════════════════════════╗");
            Console.WriteLine("║  Welcome to PokéDelete!  ║");
            Console.WriteLine("╚══════════════════════════╝");
            while (true)
            {
                Console.WriteLine("Enter the Pokémon ID# of the PokéDex entry you wish to delete:");
                var userInput = Console.ReadLine();
                int id;
                if (int.TryParse(userInput, out id))
                {
                    var selectedPokemon = PokeDexRepository.Search(id);
                    if (selectedPokemon == null)
                    {
                        Console.WriteLine("Invalid entry. Please try again.");
                    }
                    else
                    {
                        PokeDexRepository.Delete(selectedPokemon);
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid entry. Please try again.");
                }
            }
        }
        /// <summary>
        /// Handles user interaction to edit a Pokemon in the Pokedex.
        /// </summary>
        /// <param name="pokeDexRepository"></param>
        public void HandlePokedexEdit()
        {
            Console.WriteLine("╔════════════════════════╗");
            Console.WriteLine("║  Welcome to PokéEdit!  ║");
            Console.WriteLine("╚════════════════════════╝");
            Console.WriteLine("What? Made a mistake? Let's fix that.");
            Console.WriteLine("Which Pokémon ID# do you want to edit?");
            var userInput = Console.ReadLine();
            var selectedPokemon = Search(PokeDexRepository, userInput);
            selectedPokemon.Name = HandleChange("Name", selectedPokemon.Name);
            selectedPokemon.Type1 = HandleChange("Type 1", selectedPokemon.Type1);
            selectedPokemon.Type2 = HandleChange("Type 2", selectedPokemon.Type2);
            string UserInputHP = HandleChange("HP", selectedPokemon.HP.ToString());
            string UserInputAttack = HandleChange("Attack", selectedPokemon.Attack.ToString());
            string UserInputDefense = HandleChange("Defense", selectedPokemon.Defense.ToString());
            string UserInputSpAtk = HandleChange("SpAtk", selectedPokemon.SpAtk.ToString());
            string UserInputSpDef = HandleChange("SpDef", selectedPokemon.SpDef.ToString());
            string UserInputSpeed = HandleChange("Speed", selectedPokemon.Speed.ToString());
            string UserInputLegendary = HandleChange("Legendary", selectedPokemon.Legendary.ToString());
            selectedPokemon.HP = IntTryParse(UserInputHP, selectedPokemon.HP);
            selectedPokemon.Attack = IntTryParse(UserInputAttack, selectedPokemon.HP);
            selectedPokemon.Defense = IntTryParse(UserInputDefense, selectedPokemon.HP);
            selectedPokemon.SpAtk = IntTryParse(UserInputSpAtk, selectedPokemon.HP);
            selectedPokemon.SpDef = IntTryParse(UserInputSpDef, selectedPokemon.HP);
            selectedPokemon.Speed = IntTryParse(UserInputSpeed, selectedPokemon.HP);
            selectedPokemon.Legendary = BoolTryParse(UserInputLegendary, selectedPokemon.Legendary);
            PokeDexRepository.Edit(selectedPokemon);
        }
        /// <summary>
        /// Parses an int value and print message on error.
        /// </summary>
        /// <param name="inputValue"></param>
        /// <param name="currentValue"></param>
        /// <returns></returns>
        private static int IntTryParse(string inputValue, int currentValue)
        {
            int result = 0;
            if (int.TryParse(inputValue, out result))
            {
                return result;
            }
            else
            {
                Console.WriteLine("That is not a valid entry.");
                return currentValue;
            }
        }
        /// <summary>
        /// Parses a bool value and print message on error.
        /// </summary>
        /// <param name="inputValue"></param>
        /// <param name="currentValue"></param>
        /// <returns></returns>
        private static bool BoolTryParse(string inputValue, bool currentValue)
        {
            bool result = true;
            if (bool.TryParse(inputValue, out result))
            {
                return result;
            }
            else
            {
                Console.WriteLine("That is not a valid entry.");
                return currentValue;
            }
        }
        /// <summary>
        /// Interacts with the user, returns original value or changed value.
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="currentValue"></param>
        /// <returns></returns>
        private static string HandleChange(string fieldName, string currentValue)
        {
            Console.WriteLine($"The {fieldName} is currently {currentValue}. Do you want to change it? Y/N");
            var answer = Console.ReadLine();
            if (answer.ToLower() == "y")
            {
                Console.WriteLine($"What do you want to change the {fieldName} to?");
                return Console.ReadLine();
            }
            else
            {
                return currentValue;
            }
        }
        /// <summary>
        /// Interacts with user, adds Pokemon to Pokedex.
        /// </summary>
        /// <param name="pokeDexRepository"></param>
        public void HandleAdd()
        {
            Pokemon pokemon = new Pokemon();
            Console.WriteLine("╔═════════════════════════════════════════════════╗");
            Console.WriteLine("║  Congratulations on discovering a new Pokémon!  ║");
            Console.WriteLine("╚═════════════════════════════════════════════════╝");
            Console.WriteLine("What did you name this new Pokémon?");
            pokemon.Name = Console.ReadLine();
            Console.WriteLine("What type of Pokémon is it?");
            pokemon.Type1 = Console.ReadLine();
            Console.WriteLine("Did it have a secondary type? Y/N");
            var answer = Console.ReadLine();
            if (answer.ToLower() == "y")
            {
                Console.WriteLine("What was the secondary type?");
                pokemon.Type2 = Console.ReadLine();
            }
            Console.WriteLine("Saving...");
            Console.Beep();
            Console.Beep();
            Console.Beep();
            PokeDexRepository.AddPokemon(pokemon);
        }
        /// <summary>
        /// Interacts with user, searches for Pokemon entries by ID#.
        /// </summary>
        /// <param name="pokeDexRepository"></param>
        public void HandlePokeSearch()
        {
            Console.WriteLine("╔══════════════════════════╗");
            Console.WriteLine("║  Welcome to PokéSearch!  ║");
            Console.WriteLine("╚══════════════════════════╝");
            while (true)
            {
                Console.WriteLine("Please enter ID# Or 'back' to return the main menu.");
                var userInputId = Console.ReadLine();
                if (userInputId.ToLower() == "back")
                {
                    return;
                }
                Pokemon selectedPokemon = Search(PokeDexRepository, userInputId);
                if (selectedPokemon != null)
                {
                    Console.WriteLine($"You have selected {selectedPokemon.Name}");
                    Console.WriteLine($"ID#:{selectedPokemon.ID}");
                    Console.WriteLine($"Type 1:{selectedPokemon.Type1}");
                    Console.WriteLine($"Type 2:{selectedPokemon.Type2}");
                    Console.WriteLine($"HP:{selectedPokemon.HP}");
                    Console.WriteLine($"Attack:{selectedPokemon.Attack}");
                    Console.WriteLine($"Defense:{selectedPokemon.Defense}");
                    Console.WriteLine($"Special Attack:{selectedPokemon.SpAtk}");
                    Console.WriteLine($"Special Defense:{selectedPokemon.SpDef}");
                    Console.WriteLine($"Speed:{selectedPokemon.Speed}");
                    Console.WriteLine($"Legendary:{selectedPokemon.Legendary}");
                    break;
                }
            }
        }
        /// <summary>
        /// Parses user input string into a Pokemon ID#, searchs repository.
        /// </summary>
        /// <param name="pokeDexRepository"></param>
        /// <param name="userInputId"></param>
        /// <returns></returns>
        private static Pokemon Search(PokeDexRepository pokeDexRepository, string userInputId)
        {
            Pokemon selectedPokemon = null;
            int id;
            if (int.TryParse(userInputId, out id))
            {
                selectedPokemon = pokeDexRepository.Search(id);
            }
            if (selectedPokemon == null)
            {
                Console.WriteLine("Invalid entry. Please try again.");
            }
            return selectedPokemon;
        }
        /// <summary>
        /// Prints a message and clears console.
        /// </summary>
        public void Clear()
        {
            Console.WriteLine("Hit enter to continue...");
            Console.ReadKey();
            Console.Beep();
            Console.Clear();
        }
    }
}
