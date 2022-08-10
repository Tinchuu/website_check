using Microsoft.AspNetCore.Mvc;
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
    public class DemonstrationController : ControllerBase
    {
        private readonly HttpClient _client;

        public DemonstrationController(IHttpClientFactory clientFactory)
        {
            if (clientFactory is null)
            {
                throw new ArgumentNullException(nameof(clientFactory));
            }
            _client = clientFactory.CreateClient("uni");
        }

        /// <summary>
        /// Returns all universities in a country
        /// <param name="Country">Country to Check</param>
        /// </summary>
        /// <returns>The universities</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetNumberAsync(string input)

        {
            var res = await _client.GetAsync("search?country=" + input);
            var content = await res.Content.ReadAsStringAsync();

            Console.WriteLine(content);
            List<University> universities = JsonConvert.DeserializeObject<List<University>>(content);

            return Ok(universities);
        }

        /// <summary>
        /// Demonstrates posting action
        /// </summary>
        /// <returns>A 201 Created response</returns>
        [HttpPost]
        [ProducesResponseType(201)]
        public IActionResult DemonstratePost()
        {
            Console.WriteLine("I'm doing some work right now to create a new thing...");

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