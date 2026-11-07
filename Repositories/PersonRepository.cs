using SQLite;
using mpalloS5.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace mpalloS5.Repositories
{
    public class PersonRepository
    {
        string _dbPath;
        private SQLiteConnection _conn;

        public string StatusMessage { get; set; }
        public PersonRepository(string dbPath)
        {
            _dbPath = dbPath;
        }

        private void Init()
        {
            if (_conn is not null)
                return;
            _conn = new(_dbPath);
            _conn.CreateTable<Persona>();
        }

        //Insert
        public void AddNewPerson(string name)
        {
            int result = 0;
            try
            {
                Init();
                if (string.IsNullOrEmpty(name))
                    throw new Exception("Nombre es requerido");
                Persona persona = new() { Name = name };
                result = _conn.Insert(persona);
                StatusMessage = string.Format("Dato Ingresado");
            }
            catch(Exception ex)
            {
                StatusMessage = string.Format("Error " + ex.Message);
            }
        }

        //select
        public List<Persona>GetAllPerson()
        {
            try
            {
                Init();
                return _conn.Table<Persona>().ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Error " + ex.Message);
            }
            return new List<Persona>();

        }

        //Delete

        public void DeletePerson(int id)
        {
            try
            {
                Init();
                _conn.Delete<Persona>(id);
                StatusMessage = string.Format("Dato Eliminado");
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Error " + ex.Message);
            }
        }

        //Update

        public void UpdatePerson(int id, string newName)
        {
            try
            {
                Init();
                if (string.IsNullOrEmpty(newName))
                    throw new Exception("El nombre es requerido");

                // Buscar a la persona por su ID
                Persona persona = _conn.Find<Persona>(id);

                if (persona != null)
                {
                    //Asignamos nuevo nombre y guardamos cambios
                    persona.Name = newName;
                    _conn.Update(persona);
                    StatusMessage = string.Format("Dato Actualizado");
                }
                else
                {
                    StatusMessage = string.Format("Error: Persona no encontrada");
                }
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Error " + ex.Message);
            }
        }

    }
}
