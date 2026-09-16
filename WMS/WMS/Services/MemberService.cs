using Microsoft.EntityFrameworkCore;
using WMS.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WMS.Services
{
    public class MemberService
    {
        private readonly WarehouseDbContext _dbContext;
        //DI自动注入DbContext
        public MemberService(WarehouseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>查询全部会员</summary>
        public async Task<List<Member>> GetAllMemberAsync()
        {
            return await _dbContext.Members.ToListAsync();
        }

        /// <summary>新增会员</summary>
        public async Task AddMemberAsync(Member member)
        {
            _dbContext.Members.Add(member);
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>修改会员</summary>
        public async Task UpdateMemberAsync(Member member)
        {
            _dbContext.Members.Update(member);
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>删除会员</summary>
        public async Task DeleteMemberAsync(int id)
        {
            var model = await _dbContext.Members.FindAsync(id);
            if (model != null)
            {
                _dbContext.Members.Remove(model);
                await _dbContext.SaveChangesAsync();
            }
        }

        /// <summary>根据ID查询会员</summary>
        public async Task<Member?> GetMemberByIdAsync(int id)
        {
            return await _dbContext.Members.FindAsync(id);
        }
    }
}