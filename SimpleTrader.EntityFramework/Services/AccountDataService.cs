using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SimpleTrader.EntityFramework.Services.Common;

namespace SimpleTrader.EntityFramework.Services
{
    public class AccountDataService : IDataService<Account>
    {
        private readonly SimpleTraderDbContextFactory _contextFactory;
        private readonly NonQueryDataService<Account> _nonQueryDataService;

        public AccountDataService(SimpleTraderDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
            _nonQueryDataService = new NonQueryDataService<Account>(contextFactory);
        }

        public async Task<Account> Create(Account account)
        {
            return await _nonQueryDataService.Create(account);
        }

        public async Task<bool> Delete(int id)
        {
            return await _nonQueryDataService.Delete(id);
        }

        public async Task<Account> Get(int id)
        {
            using (SimpleTraderDbContext context = _contextFactory.CreateDbContext())
            {
                Account? account = await context.Accounts.Include(a => a.AssetTransactions).FirstOrDefaultAsync(e => e.Id == id);

                return account ?? throw new NullReferenceException();
            }
        }

        public async Task<IEnumerable<Account>> GetAll()
        {
            using (SimpleTraderDbContext context = _contextFactory.CreateDbContext())
            {
                IEnumerable<Account> accounts = await context.Accounts.Include(a => a.AssetTransactions).ToListAsync();

                return accounts;
            }
        }

        public async Task<Account> Update(int id, Account account)
        {
            return await _nonQueryDataService.Update(id, account);
        }
    }
}
