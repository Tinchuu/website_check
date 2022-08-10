using Newtonsoft.Json;
using System.Collections.Generic;

namespace website_check.Models
{
    public class University
    {
        public List<string> web_pages { get; set; }

        [JsonProperty("state-province")]
        public object StateProvince { get; set; }
        public string alpha_two_code { get; set; }
        public List<string> domains { get; set; }
        public string country { get; set; }
        public string name { get; set; }

        public string printAll()
        {
            string pages = "";
            foreach (string current in web_pages)
            {
                pages += current + " ";
            }
            return name + ": " + pages;
        }

    }
}
