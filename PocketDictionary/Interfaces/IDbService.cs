using PocketDictionary.Models;
using SQLite;

namespace PocketDictionary.Interfaces;

public interface IDbService
{
    SQLiteConnection GetConnection();
}
