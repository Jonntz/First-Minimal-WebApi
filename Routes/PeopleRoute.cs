using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstApi.Models;

namespace FirstApi.Routes
{
    public static class PeopleRoute
    {
        public static List<Person> Peoples =
        [
            new Person(Guid.NewGuid(), "Neymar"),
            new Person(Guid.NewGuid(), "Cristiano"),
            new Person(Guid.NewGuid(), "Messi")
        ];
        
        public static void MapPeopleRoutes(this WebApplication app) 
        {
            app.MapGet("/peoples", () => Peoples);
            app.MapGet("/peoples/{name}", (string name) => Peoples.Find(x => x.Name == name));
            app.MapPost("/peoples", (Person person) => 
            {
                if(person.Name == "")
                {
                    return Results.BadRequest();
                }

                Peoples.Add(person);
                return Results.Ok(Peoples);
            });

            app.MapPut("/peoples/{id}", (Guid id, Person person) => {
                var findPerson = Peoples.Find(x => x.Id == id);
                if(findPerson == null){
                    return Results.NotFound();
                }

                findPerson.Name = person.Name;
                return Results.Ok(findPerson);
            });

            app.MapDelete("/peoples/{id}", (Guid id) => {
                var findPerson = Peoples.Find(x => x.Id == id);
                if(findPerson == null){
                    return Results.NotFound();
                }

                Peoples.Remove(findPerson);
                return Results.Ok(findPerson);
            });
        }
    }
}