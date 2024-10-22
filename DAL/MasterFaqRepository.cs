using System.Data;
using DAL.Helper;
using Domain.Abstract.Repository;
using Domain.Models;

namespace DAL
{
	public class MasterFaqRepository : BaseRepository, IMasterFaqRepository
	{
		private readonly string tableBase = "master_faq";

		#region GetRows
		public async Task<List<MasterFaq>> GetRows(IDbTransaction transaction, string? keyword, int offset, int limit)
		{
			string p = db.Symbol();
		
			string query = $@"
							select
								{tableBase}.id as ID
								,{tableBase}.question as Question
								,{tableBase}.answer as Answer
								,{tableBase}.filename as Filename
								,{tableBase}.paths as Paths
								,{tableBase}.is_active as IsActive
							from
								{tableBase}
							where
								(
									lower({tableBase}.question) like lower({p}Keyword)
									or lower({tableBase}.answer) like lower({p}Keyword)
									or lower({tableBase}.filename) like lower({p}Keyword)
									or lower({tableBase}.paths) like lower({p}Keyword)
									or case {tableBase}.is_active
											when 1 then 'yes'
											else 'no'
										end like lower({p}Keyword)
								)
							order by
								{tableBase}.mod_date desc
					";
		
			query = QueryLimitOffset(query);
		
			object parameters = new
			{
				Keyword = $"%{keyword}%",
				Offset = offset,
				Limit = limit
			};
		
			List<MasterFaq> result = await _command.GetRows<MasterFaq>(transaction, query, parameters);
		
			return result;
		}
		#endregion

		#region GetRowByID
		public async Task<MasterFaq> GetRowByID(IDbTransaction transaction, string id)
		{
			string p = db.Symbol();
		
			string query = $@"
							select
								{tableBase}.id as ID
								,{tableBase}.question as Question
								,{tableBase}.answer as Answer
								,{tableBase}.filename as Filename
								,{tableBase}.paths as Paths
								,{tableBase}.is_active as IsActive
							from
								{tableBase}
							where
								{tableBase}.id = {p}ID
					";
		
			object parameters = new
			{
				ID = id
			};
		
			MasterFaq result = await _command.GetRow<MasterFaq>(transaction, query, parameters);
		
			return result;
		}
		#endregion

		#region Insert
		public async Task<int> Insert(IDbTransaction transaction, MasterFaq model)
		{
			string p = db.Symbol();
		
			string query = $@"insert into {tableBase}
							(
								id
								,cre_date
								,cre_by
								,cre_ip_address
								,mod_date
								,mod_by
								,mod_ip_address
								--
								,question
								,answer
								,filename
								,paths
								,is_active
							)
							values
							(
								{p}ID
								,{p}CreDate
								,{p}CreBy
								,{p}CreIPAddress
								,{p}ModDate
								,{p}ModBy
								,{p}ModIPAddress
								--
								,{p}Question
								,{p}Answer
								,{p}Filename
								,{p}Paths
								,{p}IsActive
							)
					";
		
			return await _command.Insert(transaction, query, model);
		}
		#endregion

		#region UpdateByID
		public async Task<int> UpdateByID(IDbTransaction transaction, MasterFaq model)
		{
			string p = db.Symbol();
		
			string query = $@"update {tableBase}
							set
								question = {p}Question
								,answer = {p}Answer
								,filename = {p}Filename
								,paths = {p}Paths
								,is_active = {p}IsActive
								--
								,mod_date = {p}ModDate
								,mod_by = {p}ModBy
								,mod_ip_address = {p}ModIPAddress
							where
								id = {p}ID
										";
		
			return await _command.Update(transaction, query, model);
		}
		#endregion

		#region DeleteByID
		public async Task<int> DeleteByID(IDbTransaction transaction, string id)
		{
			string p = db.Symbol();
		
			string query = $"delete from {tableBase} where id = {p}ID";
		
			return await _command.DeleteByID(transaction, query, id);
		}
		#endregion
		
		#region ChangeStatus
		public async Task<int> ChangeStatus(IDbTransaction transaction, MasterFaq model)
		{
			var p = db.Symbol();
		
			string query = $@"
				update {tableBase} 
				set
					is_active       = is_active * -1
					--
					,mod_date       = {p}ModDate
					,mod_by         = {p}ModBy
					,mod_ip_address = {p}ModIpAddress
				where
					id = {p}ID
			";
		
			return await _command.Update(transaction, query, model);
		}
		#endregion
	}
}