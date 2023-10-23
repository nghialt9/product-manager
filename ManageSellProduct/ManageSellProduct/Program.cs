var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

#region Unitest
//*/ Unitest

/*/
//Add sellInvoice
SellInvoice sellInvoice = new SellInvoice
{ 
    Code = "SellInvoice002",
    InvoiceDate = DateTime.Now,
    DetailSellProducts = new DetailSellProduct[] { 
        new DetailSellProduct {
            Price = 12000,
            ProductName = "Banh bao",
            Quantity = 2,
            SellInvoiceCode = "SellInvoice002"
        },
        new DetailSellProduct {
            Price = 21000,
            ProductName = "Banh bao chien",
            Quantity = 3,
            SellInvoiceCode = "SellInvoice002"
        }
    }
};

//SellInvoiceProvider.GetSellInvoices();
//SellInvoiceProvider.GetDetailSellInvoices(sellInvoice);
//SellInvoiceProvider.AddSellInvoices(sellInvoice);
//SellInvoiceProvider.EditSellInvoices(sellInvoice);
SellInvoiceProvider.DeleteSellInvoices("SellInvoice002");
//*/

/*/
//Add product
Product product = new Product
{ 
    Code = "Banh002",
    Name = "Banh Bao",
    ExpiryDate = DateTime.Now, 
    Manufacturer = "Cong Ty ABC",
    ManufactureDate = DateTime.Now.AddDays(5),
    Category = "Banh",
    Price = 120000
};

//ProductProvider.AddProducts(product);
//ProductProvider.EditProducts(product);
ProductProvider.DeleteProducts("Banh002");
//*/


/*/
//Test save data
string[] data = new string[5];
for (int i = 0; i < data.Length; i++)
{
    data[i] = $"Name {i} | Age {i}";
}

string result = CommonFunction.SaveData(data, @"Test/test.txt");

//test get data
string[] a = CommonFunction.GetData(@"Test/test.txt");
//*/


#endregion

app.Run();
