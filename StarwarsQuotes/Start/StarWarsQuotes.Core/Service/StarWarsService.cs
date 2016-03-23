using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StarWarsQuotes.Core.Model;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace StarWarsQuotes.Core.Service
{
    public class StarWarsService
    {
        public async Task<List<CharacterQuote>> GetQuotes()
        {
            var fileName = "starwarsquotes.json";
            var json = await GetResourceString(fileName);
            if (!string.IsNullOrEmpty(json))
            {
                var starWarsQuotes = JsonConvert.DeserializeObject<StartWarsQuotes>(json);
                if (starWarsQuotes.Quotes.Any())
                {
                    return starWarsQuotes.Quotes;
                }
            }
            return new List<CharacterQuote>();
        }

        public static async Task<string> GetResourceString(string resourceName)
        {
            Stream stream = null;
            StreamReader streamReader = null;

            var resourcePath = $"StarWarsQuotes.Core.Data.{resourceName}";
            var assembly = typeof(StarWarsService).GetTypeInfo().Assembly;

            try
            {
                stream = assembly.GetManifestResourceStream(resourcePath);
                streamReader = new StreamReader(stream);
                return await streamReader.ReadToEndAsync();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (streamReader != null)
                {
                    streamReader.Dispose();
                    streamReader = null;
                }

                if (stream != null)
                {
                    stream.Dispose();
                    stream = null;
                }
            }
        }

    }
}
