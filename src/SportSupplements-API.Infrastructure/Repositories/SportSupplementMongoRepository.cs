using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using SportSupplements_API.Core.Models;
using SportSupplements_API.Core.Repositories;

namespace SportSupplements_API.Infrastructure.Repositories;

public class SportSupplementMongoRepository : ISportSupplementRepository
{
    private readonly IMongoDatabase sportSupplementDb;
    private readonly IMongoCollection<SportSupplement> mycollection;

    public SportSupplementMongoRepository(string connectionString, string databaseName, string collectionName)
    {
        var client = new MongoClient(connectionString);

        this.sportSupplementDb = client.GetDatabase(databaseName);

        this.mycollection = this.sportSupplementDb.GetCollection<SportSupplement>(collectionName);
    }


    public async Task CreateAsync(SportSupplement sportSupplement)
    {
        await this.mycollection.InsertOneAsync(sportSupplement);
    }
    public async Task<IEnumerable<SportSupplement>?> GetAllAsync()
    {
        var sportSupplement = await this.mycollection.FindAsync(s => s.IsApproved == true);

        var allSportSupplement = sportSupplement.ToList();

        return allSportSupplement;
    }

    public async Task<SportSupplement> GetByIdAsync(int id)
    {
        var sportSupplement = await this.mycollection.FindAsync(s => s.Id == id);

        var searchForSportSupplement = sportSupplement.FirstOrDefault();

        return searchForSportSupplement;
    }
    public async Task DeleteAsync(int id)
    {
        await this.mycollection.FindOneAndDeleteAsync(s => s.Id == id);
    }


    public async Task UpdateAsync(int id, SportSupplement sportSupplementToUpdate)
    {
        await this.mycollection.ReplaceOneAsync<SportSupplement>(filter: s => s.Id == id, replacement: sportSupplementToUpdate);
    }
    public async Task ApproveAsync(int id)
    {
        var update = Builders<SportSupplement>.Update.Set(s => s.IsApproved, false);

        var options = new FindOneAndUpdateOptions<SportSupplement>();

        await this.mycollection.FindOneAndUpdateAsync<SportSupplement>(s => s.Id == id, update, options);
    }
}
