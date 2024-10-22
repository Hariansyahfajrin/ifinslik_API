using Domain.Models;
using DAL.Helper;
using System.Data;
using Domain.Abstract.Repository;


namespace DAL
{
	public class MasterTemplateDetailRepository : BaseRepository, IMasterTemplateDetailRepository
	{

		private readonly string tableBase = "master_template_detail";
		private readonly string tableGeneralSubCode = "sys_general_subcode";

		public async Task<List<MasterTemplateDetail>> GetRows(IDbTransaction transaction, string keyword, int offset, int limit, string masterTemplateID)
		{
			var p = db.Symbol();

			string query = QueryLimitOffset(
				$@"
					select
						    	{tableBase}.id		        					as		ID
								,{tableBase}.format_type						as		FormatType 
								,{tableBase}.order_row							as		OrderRow
								,{tableBase}.description	    				as 		Description 
								,{tableBase}.is_fix								as		IsFix 
								,{tableBase}.is_active							as		IsActive       				
					from
						{tableBase}
					where
										 	{tableBase}.master_template_id 	 = {p}MasterTemplateID
                    and
						(
                            lower({tableBase}.format_type)          				like	lower({p}Keyword)
							or	cast ({tableBase}.order_row as varchar)          		like	lower({p}Keyword)
                            or	lower({tableBase}.description)          				like	lower({p}Keyword)
                            or  cast ({tableBase}.is_fix as varchar)		    		like	lower({p}Keyword)
 							or case {tableBase}.is_active
									when 1 then 'yes'
									else		'no'
							end					    						like	lower({p}Keyword)
						)
					order by
						{tableBase}.mod_date DESC
					"
			);

			return await _command.GetRows<MasterTemplateDetail>(transaction, query, new
			{
				MasterTemplateID = masterTemplateID,
				Keyword = $"%{keyword}%",
				Limit = limit,
				Offset = offset
			});
		}

		public async Task<MasterTemplateDetail> GetRowByID(IDbTransaction transaction, string id)
		{
			var p = db.Symbol();

			var query = $@"
						    select

								{tableBase}.id		        					as	ID
								,{tableBase}.template_code						as 	TemplateCode
								,{tableBase}.master_template_id					as	MasterTemplateID
								,{tableBase}.patch_periode    					as 	PatchPeriode 
								,{tableBase}.format_type						as	FormatType 
								,{tableBase}.order_row							as	OrderRow
								,{tableBase}.description	    				as 	Description 
								,{tableBase}.field_name							as	FieldName
								,{tableBase}.refference_type_code				as  RefferenceTypeCode
								,{tableBase}.is_fix								as	IsFix 
								,{tableBase}.fix_length							as	FixLength
								,{tableBase}.fix_length_filler					as	FixLengthFiller 
								,{tableBase}.fix_length_filler_position			as	FixLengthFillerPosition 
								,{tableBase}.is_active							as	IsActive     
								--
								,sgsrtc.description								as  SysGeneralSubCodeRefferenceTypeCode
								,sgstft.description								as  SysGeneralSubCodeFormatType	

							from
								{tableBase}
							left join
								{tableGeneralSubCode} as sgsrtc on sgsrtc.code = {tableBase}.refference_type_code	
							left join
								{tableGeneralSubCode} as sgstft on sgstft.code = {tableBase}.format_type
							where
								{tableBase}.id = {p}ID
										";

			var result = await _command.GetRow<MasterTemplateDetail>(transaction, query, new { ID = id });

			return result;
		}


		public async Task<int> Insert(IDbTransaction transaction, MasterTemplateDetail model)
		{
			var p = db.Symbol();

			string query = $@"
									insert into {tableBase}
									(
										id
										,cre_date		
										,cre_by		    
										,cre_ip_address	
										,mod_date		
										,mod_by		    
										,mod_ip_address	
										,template_code		        			
										,master_template_id			
										,patch_periode    			
										,format_type				
										,order_row					
										,description	    		
										,field_name		
										,refference_type_code			
										,is_fix						
										,fix_length					
										,fix_length_filler			
										,fix_length_filler_position	
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
										,{p}TemplateCode
										,{p}MasterTemplateID
										,{p}PatchPeriode 
										,{p}FormatType 
										,{p}OrderRow
										,{p}Description 
										,{p}FieldName
										,{p}RefferenceTypeCode
										,{p}IsFix 
										,{p}FixLength 
										,{p}FixLengthFiller 
										,{p}FixLengthFillerPosition 
										,{p}IsActive       				
									)
								";

			return await _command.Insert(transaction, query, model);
		}

		public async Task<int> UpdateByID(IDbTransaction transaction, MasterTemplateDetail model)
		{
			var p = db.Symbol();

			string query = $@"
								update 
									{tableBase}
								set
								patch_periode						 = {p}PatchPeriode
								,format_type	 	 				 = {p}FormatType
								,order_row 		   					 = {p}OrderRow
								,description						 = {p}Description
								,field_name 						 = {p}FieldName	
								,refference_type_code				 = {p}RefferenceTypeCode
								,is_fix								 = {p}IsFix
								,fix_length 						 = {p}FixLength 	
								,fix_length_filler 					 = {p}FixLengthFiller
								,fix_length_filler_position 		 = {p}FixLengthFillerPosition
								,mod_date       					 = {p}ModDate
								,mod_by								 = {p}ModBy
								,mod_ip_address 					 = {p}ModIPAddress
								

								where
										id = {p}ID
							";

			return await _command.Update(transaction, query, model);
		}

		public async Task<int> DeleteByID(IDbTransaction transaction, string id)
		{
			var p = db.Symbol();

			string query = $@"
									delete from {tableBase}
									where 
										id = {p}ID
									";

			return await _command.DeleteByID(transaction, query, id.ToString());
		}
		public async Task<int> ChangeStatus(IDbTransaction transaction, MasterTemplateDetail model)
		{
			var p = db.Symbol();
			string query = $@"
                update {tableBase}
                  set
                      	is_active     	  = is_active * -1
					 	,mod_date        = {p}ModDate
						,mod_by			 = {p}ModBy
						,mod_ip_address  = {p}ModIPAddress
                  where
                      id = {p}ID";
			return await _command.Update(transaction, query, model);
		}

		public Task<List<MasterTemplateDetail>> GetRows(IDbTransaction transaction, string keyword, int offset, int limit)
		{
			throw new NotImplementedException();
		}
	}
}

