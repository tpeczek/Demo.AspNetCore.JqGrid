using Demo.StartWars.Model;
using System.Collections.Generic;

namespace Demo.StartWars
{
    /// <summary>
    /// Based on swapi (https://github.com/phalt/swapi)
    /// </summary>
    public static class StarWarsContext
    {
        #region Properties
        public static ICollection<Planet> Planets { get; private set; }

        public static ICollection<Character> Characters { get; private set; }
        #endregion

        #region Constructor
        static StarWarsContext()
        {
            Planets = SeedPlanets();
            Characters = SeedCharacters();
        }
        #endregion

        #region Methods
        private static ICollection<Planet> SeedPlanets()
        {
            return new List<Planet>
            {
                new Planet { Id = 1, Name = "Tatooine", Diameter = 10465, RotationPeriod = 23, OrbitalPeriod = 304, Gravity = "1 standard", Climate = Climates.Arid, Terrain = Terrains.Desert, Population = 200000 },
                new Planet { Id = 2, Name = "Alderaan", Diameter = 12500, RotationPeriod = 24, OrbitalPeriod = 364, Gravity = "1 standard", Climate = Climates.Temperate, Terrain = Terrains.Grasslands | Terrains.Mountains, Population = 2000000000 },
                new Planet { Id = 8, Name = "Naboo", Diameter = 12120, RotationPeriod = 26, OrbitalPeriod = 312, Gravity = "1 standard", Climate = Climates.Temperate, Terrain = Terrains.GrassyHills | Terrains.Swamps | Terrains.Forests | Terrains.Mountains, Population = 4500000000 },
                new Planet { Id = 10, Name = "Kamino", Diameter = 19720, RotationPeriod = 27, OrbitalPeriod = 463, Gravity = "1 standard", Climate = Climates.Temperate, Terrain = Terrains.Oceans, Population = 1000000000 },
                new Planet { Id = 14, Name = "Kashyyyk", Diameter = 12765, RotationPeriod = 26, OrbitalPeriod = 381, Gravity = "1 standard", Climate = Climates.Tropical, Terrain = Terrains.Jungle | Terrains.Forests | Terrains.Lakes | Terrains.Rivers, Population = 45000000 },
                new Planet { Id = 20, Name = "Stewjon", Gravity = "1 standard", Climate = Climates.Temperate, Terrain = Terrains.Grass  },
                new Planet { Id = 21, Name = "Eriadu", Diameter = 13490, RotationPeriod = 24, OrbitalPeriod = 360, Gravity = "1 standard", Climate = Climates.Polluted, Terrain = Terrains.Cityscape, Population = 22000000000 },
                new Planet { Id = 22, Name = "Corellia", Diameter = 11000, RotationPeriod = 25, OrbitalPeriod = 329, Gravity = "1 standard", Climate = Climates.Temperate, Terrain = Terrains.Plains | Terrains.Urban | Terrains.Hills | Terrains.Forests, Population = 3000000000 },
                new Planet { Id = 23, Name = "Rodia", Diameter = 7549, RotationPeriod = 29, OrbitalPeriod = 305, Gravity = "1 standard", Climate = Climates.Hot, Terrain = Terrains.Jungle | Terrains.Oceans | Terrains.Urban | Terrains.Swamps, Population = 1300000000 },
                new Planet { Id = 24, Name = "Nal Hutta", Diameter = 12150, RotationPeriod = 87, OrbitalPeriod = 413, Gravity = "1 standard", Climate = Climates.Temperate, Terrain = Terrains.Urban | Terrains.Oceans | Terrains.Swamps | Terrains.Bogs, Population = 7000000000 },
                new Planet { Id = 26, Name = "Bestine IV", Diameter = 6400, RotationPeriod = 26, OrbitalPeriod = 680, Climate = Climates.Temperate, Terrain = Terrains.RockyIslands | Terrains.Oceans, Population = 62000000 }
            };
        }

        private static ICollection<Character> SeedCharacters()
        {
            return new List<Character>
            {
                new Character { Id = 1, Name = "Luke Skywalker", Gender = Genders.Male, Height = 172, Weight = 77, BirthYear = "19BBY", SkinColor = SkinColors.Fair, HairColor = HairColors.Blond, EyeColor = EyeColors.Blue, HomeworldId = 1 },
                new Character { Id = 2, Name = "C-3PO", Height = 167, Weight = 75, BirthYear = "112BBY", SkinColor = SkinColors.Gold, EyeColor = EyeColors.Yellow, HomeworldId = 1 },
                new Character { Id = 3, Name = "R2-D2", Height = 96, Weight = 32, BirthYear = "33BBY", SkinColor = SkinColors.Blue, EyeColor = EyeColors.Red, HomeworldId = 8 },
                new Character { Id = 4, Name = "Darth Vader", Gender = Genders.Male, Height = 202, Weight = 136, BirthYear = "41.9BBY", SkinColor = SkinColors.White, HairColor = HairColors.None, EyeColor = EyeColors.Yellow, HomeworldId = 1 },
                new Character { Id = 5, Name = "Leia Organa", Gender = Genders.Female, Height = 150, Weight = 49, BirthYear = "19BBY", SkinColor = SkinColors.Light, HairColor = HairColors.Brown, EyeColor = EyeColors.Brown, HomeworldId = 2 },
                new Character { Id = 6, Name = "Owen Lars", Gender = Genders.Male, Height = 178, Weight = 120, BirthYear = "52BBY", SkinColor = SkinColors.Light, HairColor = HairColors.Grey, EyeColor = EyeColors.Blue, HomeworldId = 1 },
                new Character { Id = 7, Name = "Beru Whitesun Lars", Gender = Genders.Female, Height = 165, Weight = 75, BirthYear = "47BBY", SkinColor = SkinColors.Light, HairColor = HairColors.Brown, EyeColor = EyeColors.Blue, HomeworldId = 1 },
                new Character { Id = 8, Name = "R5-D4", Height = 97, Weight = 32, BirthYear = "Unknown", SkinColor = SkinColors.Red, EyeColor = EyeColors.Red, HomeworldId = 1 },
                new Character { Id = 9, Name = "Biggs Darklighter", Gender = Genders.Male, Height = 183, Weight = 84, BirthYear = "24BBY", SkinColor = SkinColors.Light, HairColor = HairColors.Black, EyeColor = EyeColors.Brown, HomeworldId = 1 },
                new Character { Id = 10, Name = "Obi-Wan Kenobi", Gender = Genders.Male, Height = 182, Weight = 77, BirthYear = "57BBY", SkinColor = SkinColors.Fair, HairColor = HairColors.Auburn, EyeColor = EyeColors.Blue, HomeworldId = 20 },
                new Character { Id = 11, Name = "Anakin Skywalker", Gender = Genders.Male, Height = 188, Weight = 84, BirthYear = "41.9BBY", SkinColor = SkinColors.Fair, HairColor = HairColors.Brown, EyeColor = EyeColors.Blue, HomeworldId = 1 },
                new Character { Id = 12, Name = "Wilhuff Tarkin", Gender = Genders.Male, Height = 180, BirthYear = "64BBY", SkinColor = SkinColors.Fair, HairColor = HairColors.Auburn, EyeColor = EyeColors.Blue, HomeworldId = 21 },
                new Character { Id = 13, Name = "Chewbacca", Gender = Genders.Male, Height = 228, Weight = 112, BirthYear = "200BBY", HairColor = HairColors.Brown, EyeColor = EyeColors.Blue, HomeworldId = 14 },
                new Character { Id = 14, Name = "Han Solo", Gender = Genders.Male, Height = 180, Weight = 80, BirthYear = "29BBY", SkinColor = SkinColors.Fair, HairColor = HairColors.Brown, EyeColor = EyeColors.Brown, HomeworldId = 22 },
                new Character { Id = 15, Name = "Greedo", Gender = Genders.Male, Height = 173, Weight = 74, BirthYear = "44BBY", SkinColor = SkinColors.Green, EyeColor = EyeColors.Black, HomeworldId = 23 },
                new Character { Id = 16, Name = "Jabba Desilijic Tiure", Gender = Genders.Hermaphrodite, Height = 175, Weight = 1358, BirthYear = "600BBY", SkinColor = SkinColors.GreenTan, EyeColor = EyeColors.Orange, HomeworldId = 24 },
                new Character { Id = 18, Name = "Wedge Antilles", Gender = Genders.Male, Height = 170, Weight = 77, BirthYear = "21BBY", SkinColor = SkinColors.Fair, HairColor = HairColors.Brown, EyeColor = EyeColors.Hazel, HomeworldId = 22 },
                new Character { Id = 19, Name = "Jek Tono Porkins", Gender = Genders.Male, Height = 180, Weight = 110, BirthYear = "Unknown", SkinColor = SkinColors.Fair, HairColor = HairColors.Brown, EyeColor = EyeColors.Blue, HomeworldId = 26 },
                new Character { Id = 20, Name = "Yoda", Gender = Genders.Male, Height = 66, Weight = 17, BirthYear = "896BBY", SkinColor = SkinColors.Green, HairColor = HairColors.White, EyeColor = EyeColors.Brown },
                new Character { Id = 21, Name = "Palpatine", Gender = Genders.Male, Height = 170, Weight = 75, BirthYear = "82BBY", SkinColor = SkinColors.Pale, HairColor = HairColors.Grey, EyeColor = EyeColors.Yellow, HomeworldId = 8 },
                new Character { Id = 22, Name = "Boba Fett", Gender = Genders.Male, Height = 183, Weight = 78, BirthYear = "31.5BBY", SkinColor = SkinColors.Fair, HairColor = HairColors.Black, EyeColor = EyeColors.Brown, HomeworldId = 10 },
            };
        }
        #endregion
    }
}