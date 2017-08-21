namespace Demo.StartWars.Model
{
    public enum Genders
    {
        Female = 1,
        Male = 2,
        Hermaphrodite = 3
    }

    public enum SkinColors
    {
        Light = 1,
        Fair = 2,
        Pale = 3,
        White = 4,
        Gold = 5,
        Blue = 6,
        Red = 7,
        Green = 8,
        GreenTan = 9
    }

    public enum HairColors
    {
        None = 0,
        Blond = 1,
        Brown = 2,
        Black = 3,
        Auburn = 4,
        Grey = 5,
        White = 6
    }

    public enum EyeColors
    {
        Blue = 1,
        Brown = 2,
        Yellow = 3,
        Hazel = 4,
        Red = 5,
        Black = 6,
        Orange = 7
    }

    public class Character
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Genders? Gender { get; set; }

        public int Height { get; set; }

        public int? Weight { get; set; }

        public string BirthYear { get; set; }

        public SkinColors? SkinColor { get; set; }
        
        public HairColors? HairColor { get; set; }

        public EyeColors EyeColor { get; set; }

        public int? HomeworldId { get; set; }
    }
}
