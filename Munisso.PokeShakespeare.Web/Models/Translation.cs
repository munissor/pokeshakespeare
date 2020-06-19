namespace Munisso.PokeShakespeare.Models
{
    public class Translation
    {
        public Translation(string original, string translated)
        {
            if (string.IsNullOrEmpty(original))
            {
                throw new System.ArgumentException($"'{nameof(original)}' cannot be null or empty", nameof(original));
            }

            if (string.IsNullOrEmpty(translated))
            {
                throw new System.ArgumentException($"'{nameof(translated)}' cannot be null or empty", nameof(translated));
            }

            this.Original = original;
            this.Translated = translated;
        }
        public string Original { get; private set; }

        public string Translated { get; private set; }
    }
}