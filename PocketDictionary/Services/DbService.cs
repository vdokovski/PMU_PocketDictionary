using Microsoft.Maui.Storage;
using PocketDictionary.Interfaces;
using PocketDictionary.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketDictionary.Services;
/// <summary>
/// Service for managing database operations related to flashcards.
/// </summary>
public class DbService : IDbService
{
    private SQLiteConnection _db;

    /// <summary>
    /// Initializes the database connection and creates the Flashcard table if it does not exist.
    /// </summary>
    public DbService()
    {
        if (_db != null)
            return;

        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "flashcards.db3");
        _db = new SQLiteConnection(dbPath);

        // Create tables
        _db.CreateTable<Flashcard>();
        _db.CreateTable<Deck>();
    }
    /// <summary>
    /// Gets the SQLite asynchronous connection instance.
    /// </summary>
    /// <returns>The SQLiteAsyncConnection instance.</returns>
    public SQLiteConnection GetConnection()
    {
        return _db;
    }
}
