using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Pokedex
{
    public class PokeDexRepository
    {
        private string FileName;
        private List<Pokemon> FileContents;
        public PokeDexRepository(string fileName)
        {
            FileName = fileName;
            FileContents = ReadPokemonStats();
        }
        /// <summary>
        /// This reads the CSV and parses the values.
        /// </summary>
        /// <returns></returns>
        public List<Pokemon> ReadPokemonStats()
        {
            var PokeStats = new List<Pokemon>();
            using (var reader = new StreamReader(FileName))
            {
                string line = "";
                reader.ReadLine();
                while ((line = reader.ReadLine()) != null)
                {
                    string[] values = line.Split(",");
                    int parseInt;
                    bool parseBool;
                    int.TryParse(values[0], out parseInt);
                    var columns = new Pokemon(parseInt);
                    columns.Name = values[1];
                    columns.Type1 = values[2];
                    columns.Type2 = values[3];
                    // Not including the value [4] total
                    if (int.TryParse(values[5], out parseInt))
                    {
                        columns.HP = parseInt;
                    }
                    if (int.TryParse(values[6], out parseInt))
                    {
                        columns.Attack = parseInt;
                    }
                    if (int.TryParse(values[7], out parseInt))
                    {
                        columns.Defense = parseInt;
                    }
                    if (int.TryParse(values[8], out parseInt))
                    {
                        columns.SpAtk = parseInt;
                    }
                    if (int.TryParse(values[9], out parseInt))
                    {
                        columns.SpDef = parseInt;
                    }
                    if (int.TryParse(values[10], out parseInt))
                    {
                        columns.Speed = parseInt;
                    }
                    // Leaving out Value [11] "generation" as I am only using Generation 1
                    if (bool.TryParse(values[12], out parseBool))
                    {
                        columns.Legendary = parseBool;
                    }
                    PokeStats.Add(columns);
                }
            }
            return PokeStats;
        }
        /// <summary>
        /// This allows the user to save all entires to the CSV.
        /// </summary>
        public void SaveCSV()
        {
            List<string> pokeStrings = new List<string>();
            string fileString = "#,Name,Type 1,Type 2,Total,HP,Attack,Defense,Sp. Atk,Sp. Def,Speed,Generation,Legendary\n";
            foreach (Pokemon pokemon in FileContents)
            {
                // leaving out the total column
                fileString += $"{pokemon.ID},{pokemon.Name},{pokemon.Type1},{pokemon.Type2},," +
                    $"{pokemon.HP},{pokemon.Attack},{pokemon.Defense},{pokemon.SpAtk},{pokemon.SpDef},{pokemon.Speed},,{pokemon.Legendary}\n";
            }
            File.WriteAllText(FileName, fileString, Encoding.UTF8);
        }
        /// <summary>
        /// This allows the user to delete entires from the Pokedex.
        /// </summary>
        /// <param name="selectedPokemon"></param>
        public void Delete(Pokemon selectedPokemon)
        {
            FileContents.Remove(selectedPokemon);
            SaveCSV();
        }
        /// <summary>
        /// This allows the user to search through CSV via Pokemon ID#s.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Pokemon Search(int id)
        {
            Pokemon selectedPokemon = null;
            foreach (Pokemon pokemon in FileContents)
            {
                if (id == pokemon.ID)
                {
                    selectedPokemon = pokemon;
                }
            }

            return selectedPokemon;
        }
        /// <summary>
        /// Allows user to add Pokemon entries to the Pokedex.
        /// </summary>
        /// <param name="pokemonToSave"></param>
        public void AddPokemon(Pokemon pokemonToSave)
        {
            int id = GetNewPokemonID();
            pokemonToSave.ID = id;
            FileContents.Add(pokemonToSave);
            SaveCSV();
        }
        /// <summary>
        /// Allows users to edit entries in the Pokedex.
        /// </summary>
        /// <param name="selectedPokemon"></param>
        public void Edit(Pokemon selectedPokemon)
        {
            FileContents.Remove(selectedPokemon);
            AddPokemon(selectedPokemon);
        }
        /// <summary>
        /// Generates a new Pokemon ID 1 higher than the largest ID currently present.
        /// </summary>
        /// <returns></returns>
        private int GetNewPokemonID()
        {
            int newid = 1;
            foreach (var item in FileContents)
            {
                if (item.ID >= newid)
                {
                    newid = item.ID + 1;
                }
            }
            return newid;
        }
    }
}
