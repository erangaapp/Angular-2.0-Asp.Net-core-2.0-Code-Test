
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MD = CT.Models;


namespace CT.Data
{
    public static class DbInitializer
    {

        public static void Initialize(CTDbContext context)//SchoolContext is EF context
        {

            context.Database.EnsureCreated();//if db is not exist ,it will create database .but ,do nothing .

            if (!context.PetTypes.Any())
            {
                var petTypes = new MD.PetType[]
                {
                    new MD.PetType(){ Type = "Cat" },
                    new MD.PetType(){ Type = "Dog" },
                    new MD.PetType(){ Type = "Fish" }
                };

                foreach (MD.PetType petType in petTypes)
                {
                    context.PetTypes.Add(petType);
                }
                context.SaveChanges();
            }


            if (!context.Owners.Any())
            {
                var _cat = context.PetTypes.Where(w => w.Type.Equals("Cat")).FirstOrDefault();
                var _dog = context.PetTypes.Where(w => w.Type.Equals("Dog")).FirstOrDefault();
                var _fish = context.PetTypes.Where(w => w.Type.Equals("Fish")).FirstOrDefault();

                var owners = new MD.Owner[] {
                    new MD.Owner
                    {
                        Age = 30,
                        Gender = MD.Enum.Gender.Male,
                        Name = "Bob",
                        Pets = new List<MD.Pet>(){ new MD.Pet(){ Name = "Garfield", PetType = _cat }, new MD.Pet(){ Name = "Fido", PetType = _dog } }
                    },
                    new MD.Owner
                    {
                        Age = 18,
                        Gender = MD.Enum.Gender.Female,
                        Name = "Jennifer",
                        Pets = new List<MD.Pet>(){ new MD.Pet(){ Name = "Garfield", PetType = _cat } }
                    },
                    new MD.Owner
                    {
                        Age = 45,
                        Gender = MD.Enum.Gender.Male,
                        Name = "Steve"
                    },
                    new MD.Owner
                    {
                        Age = 40,
                        Gender = MD.Enum.Gender.Male,
                        Name = "Fred",
                        Pets = new List<MD.Pet>(){ new MD.Pet(){ Name = "Tom", PetType = _cat }, new MD.Pet() { Name = "Max", PetType = _cat }, new MD.Pet() { Name = "Sam", PetType = _dog }, new MD.Pet() { Name = "Jim", PetType = _cat } }
                    },
                    new MD.Owner
                    {
                        Age = 40,
                        Gender = MD.Enum.Gender.Female,
                        Name = "Samantha",
                        Pets = new List<MD.Pet>(){ new MD.Pet(){ Name = "Tabby", PetType = _cat } }
                    },
                    new MD.Owner
                    {
                        Age = 64,
                        Gender = MD.Enum.Gender.Female,
                        Name = "Alice",
                        Pets = new List<MD.Pet>(){ new MD.Pet(){ Name = "Simba", PetType = _cat }, new MD.Pet() { Name = "Nemo", PetType = _fish } }
                    },

                };
                foreach (MD.Owner owner in owners)
                {
                    context.Owners.Add(owner);
                }
                context.SaveChanges();

            }

        }
    }
}
