using WMSWarehouse.Models;
using Microsoft.EntityFrameworkCore;

namespace WMSWarehouse.Services;

public class WarehouseService
{
    private readonly WarehouseDbContext _dbContext;
    public WarehouseService(WarehouseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // 查询全部仓库
    public async Task<List<Warehouse>> GetAllWarehouseAsync()
    {
        return await _dbContext.Warehouses.ToListAsync();
    }

    // 根据Id查询单个仓库
    public async Task<Warehouse?> GetWarehouseByIdAsync(int id)
    {
        return await _dbContext.Warehouses.FindAsync(id);
    }

    // 新增仓库
    public async Task AddWarehouseAsync(Warehouse warehouse)
    {
        warehouse.InsertDate = DateTime.Now;
        _dbContext.Warehouses.Add(warehouse);
        await _dbContext.SaveChangesAsync();
    }

    // 修改仓库
    public async Task UpdateWarehouseAsync(Warehouse warehouse)
    {
        _dbContext.Warehouses.Update(warehouse);
        await _dbContext.SaveChangesAsync();
    }

    // 删除仓库
    public async Task DeleteWarehouseAsync(int id)
    {
        var warehouse = await GetWarehouseByIdAsync(id);
        if (warehouse != null)
        {
            _dbContext.Warehouses.Remove(warehouse);
            await _dbContext.SaveChangesAsync();
        }
    }
}