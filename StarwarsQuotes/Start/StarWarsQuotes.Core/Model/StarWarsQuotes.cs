using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace StarWarsQuotes.Core.Model
{
    public class StartWarsQuotes
    {
        [JsonProperty("quotes")]
        public List<CharacterQuote> Quotes { get; set; }
    }

    public class CharacterQuote
    {
        
        [JsonProperty("quote")]
        public string Quote { get; set; }

        [JsonProperty("movie")]
        public string Film { get; set; }

        [JsonProperty("character")]
        public string Character { get; set; }
    }
}
