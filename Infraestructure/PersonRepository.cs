using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.IO;
using QueryApi.Domain;
using System.Threading.Tasks;

namespace QueryApi.Repositories
{
    public class PersonRepository
    {
        List<Person> _persons;

        public PersonRepository()
        {
            var fileName = "dummy.data.queries.json";
            if(File.Exists(fileName))
            {
                var json = File.ReadAllText(fileName);
                _persons = JsonSerializer.Deserialize<IEnumerable<Person>>(json).ToList();
            }
        }

        // Escribe un método en el cual se retorne la información de todas las personas.
        public IEnumerable<Person> GetAll()
        {
            var query = _persons.Select(person => person);
            return query;
        }
        //Escribe un método en el cual se retorne únicamente el nombre completo de las personas, el correo y el año de nacimiento.
        public IEnumerable<Object> GetFullName()
        {
            var query = _persons.Select(person => new{
                NombreCompleto = $"{person.FirstName} {person.LastName}",
                AnioNacimiento = DateTime.Now.AddYears(person.Age * -1).Year,
                CorreoElectronico = person.Email
            });
            return query;
        }
        // Escribe un método que retorne la información de todas las personas cuyo genero sea Femenino
        public IEnumerable<Person> GetGender(char gender)
        {
            var query = _persons.Where(person=> person.Gender == gender);
            return query;
        }
        //Escribe un método que retorne la información de todas las personas cuya edad se encuentre entre los 20 y 30 años.
        public IEnumerable<Person> GetVeinteYTreinta(int age, int agee)
        {
            var query =_persons.Where(person => person.Age >= age && person.Age <= agee);
            return query;
        }
        //Escribe un método que retorne la información de los diferentes trabajos que tienen las personas
        public IEnumerable<string> AllJobs()
        {
            var query =_persons.Select(person => person.Job).Distinct();
            return query;
        }
        //  Escribe un método que retorne la información de las personas cuyo nombre contenga la palabra “ar”
        public IEnumerable<Person> FindWord(string word)
        {
            var query =_persons.Where(person => person.Job.Contains(word));
            return query;
        }
        //Escribe un método que retorna la información de las personas cuyas edades sean 25, 35 y 45 años
        public IEnumerable<Person> GetByAges(List<int> ages)
        {
            var query = _persons.Where(person => ages.Contains(person.Age));
            return query;
        }
        // Escribe un método que retorne la información ordenas por edad para las personas que sean mayores a 40 años
        public IEnumerable<Person> GetByAgees(int age)
        {
            var query =_persons.Where(person => person.Age > age).OrderBy(person => person.Age);
            return query;
        }
        // Escribe un método que retorne la información ordenada de manera descendente para todas las personas de género masculino y que se encuentren entre los 20 y 30 años de edad
        public IEnumerable<Person> GetListOrdered(int minAge, int maxAge, char gender)
        {

            var query = _persons.Where(person => person.Age >= minAge && person.Age < maxAge && person.Gender == gender).
            OrderByDescending(person => person.Age);
            return query;
        }
        // Escribe un método que retorne la cantidad de personas con género femenino.
        public int CountGender(char gender)
        {
            var query =_persons.Count(person => person.Gender == gender);

            return query;
        }
        
        //Escribe un método que retorna si existen personas con el apellido “Shemelt”.
        public bool AnyPerson(string lastName)
        {
            var query = _persons.Exists(p=> p.LastName == lastName);
            return query;
            
        }
        
        //Escribe un método que retorne únicamente una persona cuyo trabajo sea “SoftwareConsultant” y tenga 25 años de edad
        public Person GetJobSoftware(int age, string job)
        {
            var query = _persons.Single(p => p.Age == age && p.Job == job);
            return query;
        }

        // Escribe un método que retorne la información de las primeras 3 personas cuyo puesto de trabajo sea “Software Consultant”

         public IEnumerable<Person> TakeJobs(string job, int take)
         {
            var query = _persons.Where(person => person.Job == job).Take(take);
            return query;
         }

         //Escribe un método que omita la información de las primeras 3 personas cuyo puesto de trabajo sea “Software Consultant”
         public IEnumerable<Person> Skipjobs(string job, int take, int skip)
         {
            var query = _persons.Where(person => person.Job == job).Skip(skip).Take(take);
            return query;
         }
    }
}