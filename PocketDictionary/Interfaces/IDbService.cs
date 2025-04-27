using PocketDictionary.Models;
using SQLite;

namespace PocketDictionary.Interfaces;

/// <summary>
/// Interface for database service to manage SQLite connections.
/// </summary>
public interface IDbService
{
    /// <summary>
    /// Gets the SQLite connection instance.
    /// </summary>
    /// <returns>A <see cref="SQLiteConnection"/> object representing the database connection.</returns>
    SQLiteConnection GetConnection();
}
