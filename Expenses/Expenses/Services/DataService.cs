namespace Expenses.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Common.Models;
    using Interfaces;
    using Models;
    using SQLite;
    using Xamarin.Forms;


    public class DataService
    {
        #region Properties
        private SQLiteAsyncConnection connection;
        #endregion

        #region Constructors
        public DataService()
        {
            this.OpenOrCreateDB();
        }
        #endregion

        #region Methods
        private async Task OpenOrCreateDB()
        {
            var databasePath = DependencyService.Get<IPathService>().GetDatabasePath();
            this.connection = new SQLiteAsyncConnection(databasePath);
            await connection.CreateTableAsync<ExpenseLocal>().ConfigureAwait(false);
            //await connection.CreateTableAsync<MeasureUnitLocal>().ConfigureAwait(false);
            //await connection.CreateTableAsync<LocationLocal>().ConfigureAwait(false);
        }

        public async Task Insert<T>(T model)
        {
            await this.connection.InsertAsync(model);
        }

        public async Task Insert<T>(List<T> models)
        {
            await this.connection.InsertAllAsync(models);
        }

        public async Task Update<T>(T model)
        {
            await this.connection.UpdateAsync(model);
        }

        public async Task Update<T>(List<T> models)
        {
            await this.connection.UpdateAllAsync(models);
        }

        public async Task Delete<T>(T model)
        {
            await this.connection.DeleteAsync(model);
        }

        public async Task<List<ExpenseLocal>> GetAllExpenses()
        {
            try
            {
                var query = await this.connection.QueryAsync<ExpenseLocal>("select * from [Expense]");
                var array = query.ToArray();
                var list = array.Select(p => new ExpenseLocal
                {
                    Approved = p.Approved,
                    Comments = p.Comments,
                    Description = p.Description,
                    ExpenseDateEnd = p.ExpenseDateEnd,
                    ExpenseDateStart = p.ExpenseDateStart,
                    ExpenseId = p.ExpenseId,
                    UserId = p.UserId,
                }).ToList();
                return list;
            }
            catch (Exception e)
            {
                var errormessage = e.Message.ToString();
                return null;
            }
        }
        /*
        public async Task<List<MeasureUnitLocal>> GetAllMeasureUnits()
        {
            try
            {
                var query = await this.connection.QueryAsync<MeasureUnitLocal>("select * from [MeasureUnit]");
                var array = query.ToArray();
                var list = array.Select(p => new MeasureUnitLocal
                {
                    MeasureUnitId = p.MeasureUnitId,
                    Description = p.Description,
                }).ToList();
                return list;
            }
            catch (Exception e)
            {
                var errormessage = e.Message.ToString();
                return null;
            }
        }
        public async Task<List<LocationLocal>> GetAllLocations()
        {
            try
            {
                var query = await this.connection.QueryAsync<LocationLocal>("select * from [Location]");
                var array = query.ToArray();
                var list = array.Select(p => new LocationLocal
                {
                    LocationId = p.LocationId,
                    Description = p.Description,
                }).ToList();
                return list;
            }
            catch (Exception e)
            {
                var errormessage = e.Message.ToString();
                return null;
            }
        }
        */

        public async Task DeleteAllExpenses()
        {
            var query = await this.connection.QueryAsync<ExpenseLocal>("delete from [Expense]");
        }
        /*
        public async Task DeleteAllMeasureUnits()
        {
            var query = await this.connection.QueryAsync<MeasureUnit>("delete from [MeasureUnit]");
        }
        public async Task DeleteAllLocations()
        {
            var query = await this.connection.QueryAsync<Location>("delete from [Location]");
        }
        */
        #endregion
    }
}
