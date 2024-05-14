using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SportSupplements_API.Core.Models;
using SportSupplements_API.Core.Repositories;
using SportSupplements_API.Infrastructure.Data;

namespace SportSupplements_API.Infrastructure.Repositories;

public class SportSupplementSqlRepository : ISportSupplementRepository
{
    private readonly SportSupplementDbContext dbContext;

    public SportSupplementSqlRepository(SportSupplementDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<SportSupplement>>? GetAllAsync()
    {
        var sportSupplement = this.dbContext.SportSupplements.AsEnumerable();

        return sportSupplement;
    }

    public async Task CreateAsync(SportSupplement sportSupplement)
    {
        await this.dbContext.SportSupplements.AddAsync(sportSupplement);

        await this.dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var deleteSupplement = await this.dbContext.SportSupplements.FirstOrDefaultAsync(sportSupplement => sportSupplement.Id == id);

        this.dbContext.Remove<SportSupplement>(deleteSupplement);

        await this.dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, SportSupplement sportSupplement)
    {
        var oldSupplement = await this.dbContext.SportSupplements.FirstOrDefaultAsync(s => s.Id == id);
        {
            oldSupplement.Name = sportSupplement.Name;
            oldSupplement.Description = sportSupplement.Description;
            oldSupplement.ManufactureCountry = sportSupplement.ManufactureCountry;
            oldSupplement.Quantity = sportSupplement.Quantity;


            this.dbContext.SportSupplements.Update(oldSupplement);

            await this.dbContext.SaveChangesAsync();
        }

    }
    public async Task<SportSupplement> GetByIdAsync(int id)
    {
        var searchedForSupplement = await this.dbContext.SportSupplements.FirstOrDefaultAsync(supplement => supplement.Id == id);

        return searchedForSupplement!;
    }


}

