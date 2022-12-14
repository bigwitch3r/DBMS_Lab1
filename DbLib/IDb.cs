using System.Collections.Generic;
using System.Dynamic;

namespace DbLib
{
    /// <summary>
    /// Интерфейс работы с базой данных
    /// </summary>
    public interface IDb
    {
        /*void AddMech(Mech mech);
        void DeleteMech(int id);
        void Create(int mechId, int coreId);
        List<Mech> GetMech();
        List<Core> GetCore(string searchQuery = "");
        List<Corpus> GetCorpus();*/

        void AddPerson(Person person);
        void DeletePerson(int id);
        void CreateDbObject(int personId, int professionId);
        List<Person> GetPersons();
    }
}