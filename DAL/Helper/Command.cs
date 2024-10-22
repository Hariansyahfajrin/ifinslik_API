using Dapper;
using System.Data;

namespace DAL.Helper
{
	public class Command
	{
		private void Mapping(Type[] types)
		{
			foreach (var type in types)
			{
				SqlMapper.SetTypeMap(type, new CustomPropertyTypeMap(type, (obj, columnName) =>
								{
									return obj.GetProperties().Where(x => (obj.Name + x.Name).ToLower() == columnName.ToLower() || x.Name.ToLower() == columnName.ToLower()).FirstOrDefault();
								}));

			}
		}
		#region GetRows
		public async Task<List<T>> GetRows<T>(IDbTransaction transaction, string query, object parameters = null)
		{
			try
			{

				var param = new DynamicParameters(parameters);

				var result = await transaction.Connection.QueryAsync<T>(query, param: param, transaction: transaction);
				return result.ToList();
			}
			catch (Exception)
			{
				throw;
			}
		}
		public async Task<List<TReturn>> GetRows<TFirst, TSecond, TReturn>(IDbTransaction transaction, string query, Func<TFirst, TSecond, TReturn> map, string splitOn = "ID", object parameters = null)
		{
			try
			{

				Mapping([typeof(TSecond)]);

				var param = new DynamicParameters(parameters);
				var result = await transaction.Connection.QueryAsync(query, map, splitOn: splitOn, param: param, transaction: transaction);
				return result.ToList();
			}
			catch (Exception)
			{
				throw;
			}
		}
		public async Task<List<TReturn>> GetRows<TFirst, TSecond, TThird, TReturn>(IDbTransaction transaction, string query, Func<TFirst, TSecond, TThird, TReturn> map, string splitOn = "ID", object parameters = null)
		{
			try
			{
				Mapping([typeof(TSecond), typeof(TThird)]);

				var param = new DynamicParameters(parameters);

				var result = await transaction.Connection.QueryAsync(query, map, splitOn: splitOn, param: param, transaction: transaction);
				return result.ToList();
			}
			catch (Exception)
			{
				throw;
			}
		}

		#endregion

		#region GetRow
		public async Task<T> GetRow<T>(IDbTransaction transaction, string query, object parameters = null)
		{
			try
			{
				var param = new DynamicParameters(parameters);

				var result = await transaction.Connection.QueryFirstOrDefaultAsync<T>(query, param: param, transaction: transaction);
				return result;
			}
			catch (Exception)
			{
				throw;
			}
		}
		public async Task<TReturn> GetRow<TFirst, TSecond, TReturn>(IDbTransaction transaction, string query, Func<TFirst, TSecond, TReturn> map, string splitOn = "ID", object parameters = null)
		{
			try
			{
				Mapping([typeof(TSecond)]);

				var param = new DynamicParameters(parameters);

				var result = await transaction.Connection.QueryAsync(query, map, splitOn: splitOn, param: param, transaction: transaction);
				return result.First();
			}
			catch (Exception)
			{
				throw;
			}
		}
		public async Task<TReturn> GetRow<TFirst, TSecond, TThird, TReturn>(IDbTransaction transaction, string query, Func<TFirst, TSecond, TThird, TReturn> map, string splitOn = "ID", object parameters = null)
		{
			try
			{
				Mapping([typeof(TSecond), typeof(TThird)]);

				var param = new DynamicParameters(parameters);

				var result = await transaction.Connection.QueryAsync(query, map, splitOn: splitOn, param: param, transaction: transaction);
				return result.First();
			}
			catch (Exception)
			{
				throw;
			}
		}
		#endregion

		#region Insert
		public async Task<int> Insert(IDbTransaction transaction, string query, object parameter)
		{
			try
			{
				var parameters = new DynamicParameters(parameter);

				var result = await transaction.Connection.ExecuteAsync(query, parameters, transaction: transaction);

				return result;
			}
			catch (Exception)
			{
				throw;
			}
		}
		#endregion

		#region Update
		public async Task<int> Update(IDbTransaction transaction, string query, object parameter)
		{
			try
			{
				var parameters = new DynamicParameters(parameter);

				return await transaction.Connection.ExecuteAsync(query, parameters, transaction: transaction);
			}
			catch (Exception)
			{
				throw;
			}
		}
		#endregion

		#region Delete
		public async Task<int> DeleteByID(IDbTransaction transaction, string query, string id)
		{
			try
			{
				var parameters = new
				{
					ID = id
				};

				return await transaction.Connection.ExecuteAsync(query, parameters, transaction: transaction);
			}
			catch (Exception)
			{
				throw;
			}
		}
		#endregion

		#region UploadFile
		public void UploadFile(string query, string filePath)
		{
			try
			{
				using (var connection = new Database().Connection)
				{

					using (var transaction = connection.BeginTransaction())
					{
						try
						{
							var parameters = new
							{
								Image = filePath
							};
							connection.Execute(query, parameters, transaction: transaction);
							transaction.Commit();
						}
						catch (Exception)
						{
							transaction.Rollback();
							throw;
						}
					}
				}
			}
			catch (Exception)
			{
				throw;
			}
		}
		#endregion
	}
}
