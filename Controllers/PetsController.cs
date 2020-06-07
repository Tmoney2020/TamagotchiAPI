using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TamagotchiAPI.Models;

namespace TamagotchiAPI
{
    [ApiController]
    [Route("[controller]")]
    public class PetsController : ControllerBase
    {
        private readonly DatabaseContext _context;
        public PetsController(DatabaseContext context)
        {
            _context = context;
        }
        // All Pets
        [HttpGet]
        public ActionResult<IEnumerable<Pet>> GetAllPets()
        {
            return Ok(_context.Pets);
        }
        // Get pet by Id
        [HttpGet("{id}")]
        public ActionResult<Pet> PickAPet(int id)
        {
            var foundPet = FindPet(id);

            if (foundPet == null)
            {
                return NotFound();
            }
            return Ok(foundPet);
        }
        // Create a Pet
        [HttpPost]
        public ActionResult<Pet> Create(Pet petToCreate)
        {
            petToCreate.Birthday = DateTime.Now;
            petToCreate.HungerLevel = 0;
            petToCreate.HappinessLevel = 0;

            _context.Pets.Add(petToCreate);
            _context.SaveChanges();

            return CreatedAtAction(null, null, petToCreate);
        }
        //  POST /pets/{id}/feedings, should find the pet by id and subtract 5 from its hungry level and 3 from its happiness level.
        [HttpPost("{id}/feedings")]
        public ActionResult<Pet> Feeding(int id)
        {
            var foundPet = FindPet(id);

            if (foundPet == null)
            {
                return NotFound();
            }

            foundPet.HungerLevel -= 5;
            foundPet.HappinessLevel -= 3;

            _context.SaveChanges();
            return Ok(foundPet);
        }
        //  POST /pets/{id}/playtimes, should find the pet by id and add 5 to its happiness level and 3 to its hungry level.
        [HttpPost("{id}/playtimes")]
        public ActionResult<Pet> Playtime(int id)
        {
            var foundPet = FindPet(id);

            if (foundPet == null)
            {
                return NotFound();
            }

            foundPet.HappinessLevel += 5;
            foundPet.HungerLevel += 3;

            _context.SaveChanges();
            return Ok(foundPet);
        }
        //  POST /pets/{id}/scoldings, should find the pet by id and subtract 5 from its happiness level.
        [HttpPost("{id}/scoldings")]
        public ActionResult<Pet> Scolding(int id)
        {
            var foundPet = FindPet(id);

            if (foundPet == null)
            {
                return NotFound();
            }
            foundPet.HappinessLevel -= 5;

            _context.SaveChanges();
            return Ok(foundPet);
        }
        [HttpDelete("{id}")]
        public ActionResult<Pet> DeletePet(int id)
        {
            var foundPet = FindPet(id);

            if (foundPet == null)
            {
                return NotFound();
            }
            _context.Remove(foundPet);
            _context.SaveChanges();

            return Ok(foundPet);
        }
        private Pet FindPet(int id)
        {
            return _context.Pets.FirstOrDefault(pet => pet.Id == id);

        }
    }
}