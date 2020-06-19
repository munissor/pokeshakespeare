namespace Munisso.PokeShakespeare.Models
{
    public class ShakespeareDescription
    {
        public ShakespeareDescription(string pokemon, string description)
        {
            if (string.IsNullOrEmpty(pokemon))
            {
                throw new System.ArgumentException($"'{nameof(pokemon)}' cannot be null or empty", nameof(pokemon));
            }

            if (string.IsNullOrEmpty(description))
            {
                throw new System.ArgumentException($"'{nameof(description)}' cannot be null or empty", nameof(description));
            }

            this.Pokemon = pokemon;
            this.Description = description;
        }
        public string Pokemon { get; private set; }

        public string Description { get; private set; }
    }
}