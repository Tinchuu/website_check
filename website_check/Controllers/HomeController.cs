using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using website_check.Models;

namespace MSA.Phase2.AmazingApi.Controllers
{


    [ApiController]
    [Route("[controller]")]
    public class UniversityController : ControllerBase
    {
        private readonly HttpClient _client;

        public UniversityController(IHttpClientFactory clientFactory)
        {
            if (clientFactory is null)
            {
                throw new ArgumentNullException(nameof(clientFactory));
            }
            _client = clientFactory.CreateClient("uni");
        }


        /// <summary>
        /// Returns all the websites of universities in a country
        /// <param name="Country">Country to Check</param>
        /// </summary>
        /// <returns>Universites and their corresponding websites</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetNumberAsync(string input)

        {
            var res = await _client.GetAsync("search?country=" + input);
            var content = await res.Content.ReadAsStringAsync();
            string output = "";
            int i = 1;

            Console.WriteLine(content);
            List<University> universities = JsonConvert.DeserializeObject<List<University>>(content);

            foreach (University current in universities)
            {
                output += i + ". " + current.printAll();
                i++;
            }

            return Ok(output);
        }



        /// <summary>
        /// Demonstrates posting action
        /// </summary>
        /// <returns>A 201 Created response</returns>
        [HttpPost]
        [ProducesResponseType(201)]
        public IActionResult DemonstratePost()
        {
            var pain = "";
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = "";

            SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_dynamic_cdecl());

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();

                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = "CREATE TABLE man(name VARCHAR(50)";
                tableCmd.ExecuteNonQuery();

                using (var transaction = connection.BeginTransaction())
                {
                    var insertCmd = connection.CreateCommand();
                    tableCmd.CommandText = "INSERT INTO man VALUES('PAI AND SUFFERING')";
                    tableCmd.ExecuteNonQuery();

                    tableCmd.CommandText = "INSERT INTO man VALUES('PsdfND SUFFERING')";
                    tableCmd.ExecuteNonQuery();

                    tableCmd.CommandText = "INSERT INTO man VALUES('PAI AasdfsdfERING')";
                    tableCmd.ExecuteNonQuery();

                    transaction.Commit();
                }

                var selectCmd = connection.CreateCommand();
                selectCmd.CommandText = "SELECT * FROM man";

                using (var reader = selectCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var result = reader.GetString(0);
                        Console.WriteLine(result);

                    }
                }
            }

            
            return Created(new Uri("https://www.google.com"), "Hi There");
        }

        /// <summary>
        /// Demonstrates put action
        /// </summary>
        /// <returns>A 201 Created Response></returns>
        [HttpPut]
        [ProducesResponseType(201)]
        public IActionResult DemonstratePut()
        {
            Console.WriteLine("I'm over-writing whatever was there in the first place...");

            return Created(new Uri("https://www.google.com"), "Hi There");
        }

        /// <summary>
        /// Demonstrates a delete action
        /// </summary>
        /// <returns>A 204 No Content Response</returns>
        [HttpDelete]
        [ProducesResponseType(204)]
        public IActionResult DemonstrateDelete()
        {
            Console.WriteLine("I'm removing something from the database...");

            return NoContent();
        }


    }
}