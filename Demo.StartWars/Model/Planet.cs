using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.StartWars.Model
{
    public enum Climates
    {
        Arid = 1,
        Temperate = 2,
        Tropical = 3,
        Hot = 4,
        Polluted = 5
    }

    [Flags]
    public enum Terrains
    {
        Desert = 1,
        Plains = 2,
        Grass = 4,
        Grasslands = 8,
        GrassyHills = 16,
        Hills = 32,
        Swamps = 64,
        Bogs = 128,
        Forests = 256,
        Jungle = 512,
        Lakes = 1024,
        Rivers = 2048,
        Oceans = 4096,
        RockyIslands = 8192,
        Mountains = 16384,
        Cityscape = 32768,
        Urban = 65536
    }

    public class Planet
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? Diameter { get; set; }

        public int? RotationPeriod { get; set; }

        public int? OrbitalPeriod { get; set; }

        public string Gravity { get; set; }

        public Climates Climate { get; set; }

        public Terrains Terrain { get; set; }

        public ulong? Population { get; set; }

    }
}
