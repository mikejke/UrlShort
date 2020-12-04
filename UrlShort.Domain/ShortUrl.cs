using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UrlShort.Domain
{
    public class ShortUrl
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Full { get; set; }
        public string Short { get; private set; }
        public int Clicks { get; set; } = 0;

        private GenerationOptions _options;
        
        public ShortUrl(bool useNumbers, bool useSpecial, int length)
        {
            _options = new GenerationOptions
            {
                UseNumbers = useNumbers,
                UseSpecialCharacters = useSpecial,
                Length = length
            };
            Short = Generate();
        }

        public ShortUrl(bool useNumbers, bool useSpecial)
        {
            _options = new GenerationOptions
            {
                UseNumbers = useNumbers,
                UseSpecialCharacters = useSpecial
            };
            Short = Generate();
        }
        
        public ShortUrl(int length)
        {
            _options = new GenerationOptions
            {
                Length = length
            };
            Short = Generate();
        }
        
        public ShortUrl()
        {
            _options = new GenerationOptions();
            Short = Generate();
        }

        private readonly Random _random = new Random();
        // variables
        private const string Bigs = "ABCDEFGHIJKLMNPQRSTUVWXY";
        private const string Smalls = "abcdefghjklmnopqrstuvwxyz";
        private const string Numbers = "0123456789";
        private const string Specials = "_-";
        
        private static string _pool = $"{Smalls}{Bigs}";
        private string Generate()
        {
            if (_options == null)
            {
                throw new ArgumentNullException(nameof(_options));
            }

            var characterPool = _pool;
            var poolBuilder = new StringBuilder(characterPool);
            
            if (_options.UseNumbers)
            {
                poolBuilder.Append(Numbers);
            }

            if (_options.UseSpecialCharacters)
            {
                poolBuilder.Append(Specials);
            }
            
            var pool = poolBuilder.ToString();

            var output = new char[_options.Length];
            for (var i = 0; i < output.Length; i++)
            {
                var charIndex = _random.Next(0, pool.Length);
                output[i] = pool[charIndex];
            }

            return new string(output);
        }
        
        /// <summary>
        /// Options for URL generation
        /// </summary>
        private class GenerationOptions
        {
            // Constants
            private const int MinAutoLength = 6;
            private const int MaxAutoLength = 12;
            //private const int MinCharacterSetLength = 50;
            private static readonly Random Random = new Random();
            
            /// <summary>
            /// Determines whether numbers are used in generating the id.
            /// </summary>
            public bool UseNumbers { get; set; }

            /// <summary>
            /// Determines whether special characters are used in generating the id.
            /// </summary>
            public bool UseSpecialCharacters { get; set; } = true;

            /// <summary>
            /// Determines the length of the generated id.
            /// </summary>
            public int Length { get; set; } = Random.Next(MinAutoLength, MaxAutoLength);
        }
    }
}