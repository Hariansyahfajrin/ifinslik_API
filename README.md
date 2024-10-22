# IFinancing360 API

---

## Domain Layer

### Model

Merupakan class respresentasi dari tabel pada basis data. gunakan PascalCase untuk penamaan Class Model dan Property. Model selalu **inherit ke BaseModel**

```csharp
public class BaseModel
{
	public string? ID { get; set; }
	public DateTime? CreDate { get; set; }
	public string? CreBy { get; set; }
	public string? CreIPAddress { get; set; }
	public DateTime? ModDate { get; set; }
	public string? ModBy { get; set; }
	public string? ModIPAddress { get; set; }
}
```

```csharp
public class Model : BaseModel
{
	public string? Property1 { get; set; }
	public int? Property2 { get; set; }
	public DateTime? Property3 { get; set; }
}
```

#### Model dengan relasi

```csharp
public class Model : BaseModel
{
	public string? Property1 { get; set; }
	public int? Property2 { get; set; }
	public DateTime? Property3 { get; set; }

	// Properti dengan Type Object Model Relasinya
	public OtherModel? Model { get; set; }
}

```

### Abstract

Abstract pada layer domain berisikan Interface yang merupakan sebuah kontrak implementasi method. Abstract pada layer domain dibagi menjadi Service dan Repository. IBaseService dan IBaseRepository berisikan deklarasi 5 method dasar. Setiap Interface repository dan interface service inherit terhadap Interface Base nya dengan Type Parameter yang sesuai dengan modelnya

```
Abstract/
	Service/
		IBaseService.cs
		...Interface Service Lainnya .cs

	Repository/
		IBaseRepository.cs
		...Interface Repository Lainnya .cs
```

#### Abstract.Repository

```csharp
public interface IBaseRepository<T> where T : BaseModel
{
	IDbTransaction BeginTransaction();
	Task<List<T>> GetRows(IDbTransaction transaction, string keyword, int offset, int limit);
	Task<T> GetRowByID(IDbTransaction transaction, string id);
	Task<int> Insert(IDbTransaction transaction, T model);
	Task<int> UpdateByID(IDbTransaction transaction, T model);
	Task<int> DeleteByID(IDbTransaction transaction, string id);
}
```

```Csharp
public interface ISysModuleRepository : IBaseRepository<SysModule>
{
	Task<List<SysModule>> GetRowsForLookupModule(IDbTransaction transaction, string keyword, int offset, int limit, bool withAll);
	Task<int> ChangeStatus(IDbTransaction transaction, string ID);
}
```

#### Abstract.Service

```Csharp
public interface IBaseService<T> where T : BaseModel
{
	Task<List<T>> GetRows(string keyword, int offset, int limit);
	Task<T> GetRowByID(string id);
	Task<int> Insert(T model);
	Task<int> UpdateByID(T model);
	Task<int> DeleteByID(string[] idList);
}
```

```csharp
public interface ISysModuleService : IBaseService<SysModule>
{
	Task<List<SysModule>> GetRowsForLookupModule(string keyword, int offset, int limit, bool withAll);
	Task<int> ChangeStatus(string ID);
}
```

---

## Data Access Layer (Repository)

Repository merupakan layer yang berisikan pengaksesan data. Oleh karena itu biasanya method berisikan **command sql / query**.

Repository haruslah **mengimplementasikan interface yang sebelumnya sudah dibuat pada Domain Layer** dan Setiap method pada repository hanya mengerjakan satu tugas.

```cs
public class SysModuleRepository : BaseRepository, ISysModuleRepository
{
	private readonly string tableBase = "sys_module";

	// Method implementasi dibawah ini
	// ...
}
```

### Implementasi Method Interface

#### GetRows

NOTE :

-   Gunakan **Case When** untuk where clause pencarian pada column yang is\_... (e.g is_active)
-   Gunakan **CAST** pada column yang bertipe data number (int, decimal, dll) contoh: `CAST(order_key as varchar)`
-   Gunakan method `QueryLimitOffset(query)` untuk menambahkan syntax query `offset {p}Offset rows fetch next {p}Limit rows only`

```cs
public async Task<List<SysModule>> GetRows(IDbTransaction transaction, string keyword, int offset, int limit)
{
	var p = db.Symbol();

	// Query
	string query =
	$@"
		select
				id					as ID
				,code				as Code
				,module_name		as Name
				,module_ip  		as IP
				,module_colour		as Colour
				,module_icon		as Icon
				,is_active			as IsActive
		from
				{tableBase}
		where
				(
					lower(code)				like	lower({p}Keyword)
					or lower(module_name)	like	lower({p}Keyword)
					or lower(module_ip)		like	lower({p}Keyword)
					or lower(module_colour)	like	lower({p}Keyword)
					or lower(module_icon)	like	lower({p}Keyword)
					or case is_active
							when 1 then 'yes'
							else		'no'
						end					like	lower({p}Keyword)
				)
		order by
				mod_date desc
	";

	// Gunakan QueryLimitOffset untuk menambahkan
	// query offset dan rows fetch next
	query = QueryLimitOffset(query);

	// Binding Parameters
	var parameters = new {
		Keyword = $"%{keyword}%",
		Offset = offset,
		Limit = limit
	}

	var result = await _command.GetRows<SysModule>(transaction, query, parameters);
	return result;
}
```

#### GetRowByID

```cs
public async Task<SysModule> GetRowByID(IDbTransaction transaction, string id)
{
	var p = db.Symbol();

	string query = $@"
		select
			id                  as ID
			,code               as Code
			,module_name        as Name
			,module_ip          as IP
			,module_colour      as Colour
			,module_icon        as Icon
			,order_key          as OrderKey
			,server_ip_address  as ServerIpAddress
			,is_active          as IsActive
		from
			{tableBase}
		where
			id = {p}ID
		";

	// Binding Parameters
	var parameters = new {
		ID = id
	}

	var result = await _command.GetRow<SysModule>(transaction, query, parameters);
	return result;
}
```

#### Insert

```cs
public async Task<int> Insert(IDbTransaction transaction, SysModule module)
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
				--
				,code
				,module_name
				,module_ip
				,module_colour
				,module_icon
				,order_key
				,server_ip_address
				,is_active
			)
			values
			(
				{p}ID
				,{p}CreDate
				,{p}CreBy
				,{p}CreIpAddress
				,{p}ModDate
				,{p}ModBy
				,{p}ModIpAddress
				--
				,{p}Code
				,{p}Name
				,{p}IP
				,{p}Colour
				,{p}Icon
				,{p}OrderKey
				,{p}ServerIpAddress
				,{p}IsActive
			)";

	return await _command.Insert(transaction, query, module);
}
```

#### UpdateByID

```cs
public async Task<int> UpdateByID(IDbTransaction transaction, SysModule module)
{
	var p = db.Symbol();

	string query = $@"
					update {tableBase}
					set
						module_name         = {p}Name
						,module_ip          = {p}IP
						,module_colour      = {p}Colour
						,module_icon        = {p}Icon
						,order_key          = {p}OrderKey
						,server_ip_address  = {p}ServerIpAddress
					where
						id = {p}ID";

	return await _command.Update(transaction, query, module);
}
```

#### DeleteByID

```cs
public async Task<int> DeleteByID(IDbTransaction transaction, string id)
{
	var p = db.Symbol();

	string query = $@"
					delete from {tableBase}
					where
						id = {p}ID";
	return await _command.DeleteByID(transaction, query, id);

}
```

### Contoh Implementasi Method diluar 5 Method Repo Dasar

#### GetRows Join

```cs
public async Task<List<SysMenu>> GetRows(IDbTransaction transaction, string keyword, int offset, int limit)
{
	string query = QueryLimitOffset(
		$@"
			select
				{tableBase}.id             	 as ID
				,{tableBase}.code            as Code
				,{tableBase}.name            as Name
				,{tableBase}.is_active       as IsActive
				,{tableBase}.parent_menu_id  as ParentMenuID
				,{tableBase}.module_id		 as ModuleID
				-- SysModule
				,{tableModule}.id			 as SysModuleID
				,{tableModule}.module_name   as SysModuleName
				-- SysMenu (Parent)
				,mm.id                     	 as SysMenuID
				,mm.name                     as SysMenuName
			from
				{tableBase}
			left join
				{tableBase} as mm on mm.id = {tableBase}.parent_menu_id
			inner join
				{tableModule} on {tableModule}.id = {tableBase}.module_id
			where
				(
					lower({tableBase}.code)				like	lower({p}Keyword)
					or lower({tableBase}.module_name)	like	lower({p}Keyword)
					or lower({tableBase}.module_ip)		like	lower({p}Keyword)
					or lower({tableBase}.module_colour)	like	lower({p}Keyword)
					or lower({tableBase}.module_icon)	like	lower({p}Keyword)
					or case {tableBase}.is_active
							when 1 then 'yes'
							else		'no'
						end								like	lower({p}Keyword)
					or lower({tableModule}.module_name)	like	lower({p}Keyword)
					or lower(mm.name)					like	lower({p}Keyword)
				)

			"
	);

	// Binding Parameters
	var parameters = new
	{
		Keyword = $"%{keyword}%",
		Offset = offset,
		Limit = limit
	}

	// Penulisan Query harus berkelompok sesuai dengan Table nya
	// Contoh id sampai is_active merupakan kelompok SysMenu
	// 					SysModuleID merupakan kelompok SysModule
	// 					SysMenuID merupakan kelompok SysMenu (Parent)
	// Hal ini dilakukan Karna Split On bekerja secara mengurut

	var map = (menu, module, parent) =>
	{
		menu.Parent = parent;
		menu.Module = module;
		return menu;
	}

	var result = await _command.GetRows<SysMenu, SysModule, SysMenu, SysMenu>(transaction, query, map, "SysModuleID, SysMenuID", parameters);

	// Penulisan split on ditujukan untuk membagi select mengurut
	// Dalam Kasus ini SysModuleID ke bawah akan di bind ke TSecond (SysModule)
	// Dalam Kasus ini SysMenuID ke bawah akan di bind ke TThird (SysMenu (Parent))

	return result;
}
```

#### ChangeStatus

method ini berisikan perintah untuk update status is_active dengan melakukan negasi terhadap column is_active. untuk is\_ lain seperti is_editable atau is_wapu maka nama method bisa `ChangeEditableStatus` atau `ChangeWAPUStatus`

```cs
public async Task<int> ChangeStatus(IDbTransaction transaction, SysMenu menu)
{
	var p = db.Symbol();

	// Negasi is_active dengan mengalikan dengan -1
	string query = $@"
                update {tableBase}
                  set
                      is_active          = is_active * -1
                      --
                      ,mod_date           = {p}ModDate
                      ,mod_by             = {p}ModBy
                      ,mod_ip_address     = {p}ModIpAddress
                  where
                      id = {p}ID";
	return await _command.Update(transaction, query, menu);
}
```

---

## Buisness Layer (Service)

Service merupakan layer bisnis yang berarti didalam service berisikan logika dari suatu proses bisnis. Setiap Service akan melakukan **injeksi Repository** yang akan digunakan pada constructornya. Setiap Service haruslah Inherit terhadap `BaseService` dan mengimplementasikan `Interface` dari Service tersebut

```cs
public class SysModuleService : BaseService, ISysModuleService
{
	private readonly ISysModuleRepository _repo;
	private readonly ISysMenuRepository _menuRepo;

	// Inject SysModuleRepository dan SysMenuRepository
	public SysModuleService(ISysModuleRepository repo, ISysMenuRepository menuRepo)
	{
		_repo = repo;
		_menuRepo = menuRepo;
	}

	// Implementasi Method dibawah ini
	// ...
}
```

### Implementasi 5 Method Dasar

Proses pada method service haruslah berada pada **Transaction**. Tidak lupa juga untuk melakukan **Rollback Transaction** sehingga ketika ada kegagalan pada suatu proses, keseluruhan proses akan dibatalkan.

#### GetRows

```cs
public async Task<List<SysModule>> GetRows(string keyword, int offset, int limit)
{
	using (var transaction = _repo.BeginTransaction())
	{
		try
		{
			var result = await _repo.GetRows(transaction, keyword, offset, limit);
			transaction.Commit();
			return result;
		}
		catch (Exception)
		{
			transaction.Rollback();
			throw;
		}
	}
}
```

#### GetRowByID

```cs
public async Task<SysModule> GetRowByID(string id)
{
	using (var transaction = _repo.BeginTransaction())
	{
		try
		{
			var result = await _repo.GetRowByID(transaction, id);
			transaction.Commit();
			return result;
		}
		catch (Exception)
		{
			transaction.Rollback();
			throw;
		}
	}
}
```

#### Insert

```cs
public async Task<int> Insert(SysModule model)
{
	using (var transaction = _repo.BeginTransaction())
		try
		{
			int result = await _repo.Insert(transaction, model);
			transaction.Commit();
			return result;
		}
		catch (Exception)
		{
			transaction.Rollback();
			throw;
		}
}
```

#### GenerateCode Method

Terkandang sebuah tabel memiliki column `Code` yang berisikan code unique berurut dengan format tertentu sehingga diperlukanlah `GenerateCode` Method. GenerateCode merupakan Method dari class `BaseService` untuk membuat Code unik baru berdasarkan code last record. Method `GenerateCode` ini memerlukan 3 argumen :

-   FormatCode
    ```cs
    public class FormatCode
    {
    	public string Prefix { get; set; } = "";
    	public DateTime? Date { get; set; }
    	public int RunNumberLen { get; set; }
    	public string Delimiter { get; set; } = "";
    	public string DateFormat { get; set; } = "yyMM";
    }
    ```
-   Object Last Record
-   string propertyName

##### Contoh Implementasi pada Insert SysMenu

```cs
public async Task<int> Insert(SysMenu model)
{

	using (var transaction = _repo.BeginTransaction())
	{
		try
		{
			// Mengambil Last Record SysMenu
			// GetTop Method berikut berisikan query
			// `select code from SysMenu` dengan limit bergantung pada arg 2.
			// contoh dibawah melakukan limit 1
			List<SysMenu> lastRow = await _repo.GetTop(transaction, 1);

			// Memasukkan nilai Code ke objek model senilai dengan nilai balik GenerateCode
			model.Code = GenerateCode(
				// Arg 1 : FormatCode. contoh dibawah menghasilkan code dengan format "M0000001"
				new FormatCode
				{
					Prefix = "M",
					RunNumberLen = 7
				},
				// Arg 2 : Last Record
				lastRow.FirstOrDefault(),
				// Arg 3 : Nama Property yang digunakan
				"Code"
			);

			var result = await _repo.Insert(transaction, model);
			transaction.Commit();
			return result;
		}
		catch (Exception)
		{
			transaction.Rollback();
			throw;
		}
	}
}
```

Dari contoh code diatas diperlukan pemanggilan `GetTop` dari repository untuk mendapatkan last record dari `SysMenu`. Sebelum melakukan Insert, property `Code` dari `model` akan di assign dengan nilai yang diterima dari method `GenerateCode`.

#### UpdateByID

```cs
public async Task<int> UpdateByID(SysModule module)
{
	using (var transaction = _repo.BeginTransaction())
		try
		{
			int result = await _repo.UpdateByID(transaction, module);
			transaction.Commit();
			return result;
		}
		catch (Exception)
		{
			transaction.Rollback();
			throw;
		}
}
```

#### DeleteByID

```cs
public async Task<int> DeleteByID(string[] idList)
{
	using (var transaction = _repo.BeginTransaction())
		try
		{
			int countResult = 0;
			foreach (string id in idList)
			{
				var result = await _repo.DeleteByID(transaction, id);
				if (result > 0)
				{
					countResult += result;
				}
			}
			transaction.Commit();

			return countResult;
		}
		catch (Exception)
		{
			transaction.Rollback();
			throw;
		}
}
```

### Contoh Implementasi Method Lain

#### Change Status Module

Karena setiap module itu memiliki banyak menu (One to Many) sehingga ketika status is_active dirubah maka is_active pada setiap menu dari module tersebut juga berubah mengikuti is_active dari modulenya.

```cs
public async Task<int> ChangeStatus(SysModule module)
{
  using var connection = _repo.GetDbConnection();
using var transaction = connection.BeginTransaction();

  try
  {
    int result = await _repo.ChangeStatus(transaction, module);

    if (result > 0)
    {
      module = await _repo.GetRowByID(transaction, module.ID ?? "");

      result += await _menuRepo.ChangeStatusByModule(transaction, module);
    }

    transaction.Commit();
    return result;
  }
  catch (Exception)
  {
    transaction.Rollback();
    throw;
  }
}
```

---

## API

API berisikan controller yang menangani request. API akan menerima request dari client web dan akan mengembalikan (return) hasil dari service dalam bentuk JSON.

Sebuah Class Controller harus inherit terhadap Class `BaseController` dan memiliki attribute `Route("api/[controller]")`. Controller juga harus melakukan Injection Service yang akan digunakan serta Configuration

```cs
namespace API.Controllers
{
	[Route("/api/[controller]")]
	[ApiController]
	public class SysModuleController : BaseController
	{
		private readonly ISysModuleService _service;
		private readonly IConfiguration _configuration;

		// Inject Service dan Configuration
		public SysModuleController(ISysModuleService service, IConfiguration configuration) : base(configuration)
		{
			_service = service;
			_configuration = configuration;
		}
	}

	// Method HTTP Handler dibawah ini
	// ...
}
```

### Implementasi Method

Note Route Attribute :

-   `HttpGet` untuk API yang bertujuan melakukan **pengambilan** data contoh: `GetRows` atau `GetRowByID`
-   `HttpPost` untuk API yang bertujuan melakukan **penambahan** data contoh: `Insert`
-   `HttpPut` untuk API yang bertujuan melakukan **perubahan** data contoh: `UpdateByID`
-   `HttpDelete` untuk API yang bertujuan melakukan **penghapusan** data contoh: `DeleteByID`

#### GetRows

```cs
[HttpGet("GetRows")]
public async Task<ActionResult> GetRows(string keyword, int offset, int limit)
{
	try
	{
		var data = await _service.GetRows(keyword, offset, limit);
		return ResponseSuccess(data);
	}
	catch (Exception ex)
	{
		return ResponseError(ex);
	}
}
```

#### GetRowByID

```cs
[HttpGet("GetRowByID")]
public async Task<ActionResult> GetRowByID(string id)
{
	try
	{
		var data = await _service.GetRowByID(id);
		return ResponseSuccess(data);
	}
	catch (Exception ex)
	{
		return ResponseError(ex);
	}
}
```

#### Insert

**Insert Mengembalikan ID dari data yang ditambahkan**

```cs
[HttpPost("Insert")]
public async Task<ActionResult> Insert(SysModule module)
{
	try
	{
		// SetBaseModelProperties untuk mengisi ID, CreDate, CreBy, CreIPAddress, ModDate, ModBy, ModIPAddress
		SetBaseModelProperties(module);

		return ResponseSuccess(new { module.ID }, await _service.Insert(module));
	}
	catch (Exception ex)
	{
		return ResponseError(ex);
	}
}
```

#### UpdateByID

```cs
[HttpPut("UpdateByID")]
public async Task<ActionResult> UpdateByID(SysModule module)
{
	try
	{
		// SetBaseModelProperties untuk mengisi ID, CreDate, CreBy, CreIPAddress, ModDate, ModBy, ModIPAddress
		SetBaseModelProperties(module);

		return ResponseSuccess(new { }, await _service.UpdateByID(module));
	}
	catch (Exception ex)
	{
		return ResponseError(ex);
	}
}
```

#### DeleteByID

```cs
[HttpDelete("DeleteByID")]
public async Task<ActionResult> DeleteByID([FromBody] string[] code)
{
	try
	{
		return ResponseSuccess(new { }, await _service.DeleteByID(code));
	}
	catch (Exception ex)
	{
		return ResponseError(ex);
	}
}
```

---

### Registrasi Service dan Repository

Untuk dapat melakukan injection yang dilakukan baik pada controller atau service. Perlu dilakukan penambahan scoped pada `API/ServiceRegister/ServiceRegister.cs`

Penambahan dilakukan menggunakan method `AddScoped<Interface, Implementasi>();` pada method AddService

#### Contoh :

```cs
// ServiceRegister.cs
public static class ServiceRegister
{
	public static void AddService(this IServiceCollection services)
	{
		// Penambahan Depedancy Repository
		services.AddScoped<ISysModuleRepository, SysModuleRepository>();

		// Penambahan Depedancy Service
		services.AddScoped<ISysModuleService, SysModuleService>();
	}
}
```
