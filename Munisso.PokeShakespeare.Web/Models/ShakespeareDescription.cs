namespace Munisso.PokeShakespeare.Models
{
    public class ShakespeareDescription
    {
        public ShakespeareDescription(string name, string description)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new System.ArgumentException($"'{nameof(name)}' cannot be null or empty", nameof(name));
            }

            if (string.IsNullOrEmpty(description))
            {
                throw new System.ArgumentException($"'{nameof(description)}' cannot be null or empty", nameof(description));
            }

            this.Name = name;
            this.Description = description;
        }
        public string Name { get; private set; }

        public string Description { get; private set; }
    }
}