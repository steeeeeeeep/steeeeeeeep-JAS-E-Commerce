using JAS.Shared.Dto.Product;
using JAS.Shared.Dto.ProductCategory;
using JAS.Shared.Dtos.Product;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;

namespace JASApp.Client.Pages.Admin.Products;

public partial class GetProductList
{
    [Inject]
    protected HttpClient _httpClient { get; set; } = default!;
    [Inject]
    protected ILogger<GetProductList> _logger { get; set; } = default!;
    [Inject]
    protected IDialogService DialogService { get; set; } = default!;
    [Inject]
    protected ISnackbar Snackbar { get; set; } = default!;

    protected IEnumerable<ProductCategoryGetDto> ProductCategoryList { get; set; } = new List<ProductCategoryGetDto>();
    protected IEnumerable<ProductsListDto> ProductList { get; set; } = new List<ProductsListDto>();
    protected IEnumerable<ProductsListDto> FilteredProductList { get; set; } = new List<ProductsListDto>();
    public bool IsSaving { get; set; }
    private bool IsLoading { get; set; }
    public string SearchCategory = "Name";
    public string SearchText { get; set; }
    private List<string> SearchCategories { get; set; } = new List<string>() { "Name", "Price", "Quantity" };

    private string baseUrl = "/api/Product";

    protected override async Task OnParametersSetAsync()
    {
        IsLoading = true;
        await LoadData();
        IsLoading = false;
    }

    protected async Task LoadData()
    {
        try
        {
            var products = await _httpClient.GetFromJsonAsync<List<ProductsListDto>>(baseUrl+"/All");

            ProductList = products;
            FilteredProductList = products;
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Error, ex.Message);
        }
    }

    protected async Task ShowAddProduct()
    {
        DialogOptions options = new() { CloseOnEscapeKey = true, MaxWidth = MaxWidth.Medium };

        IDialogReference dialog = await DialogService.ShowAsync<AddOrEditProduct>("Add Product", options);

        DialogResult result = await dialog.Result;

        if (!result.Canceled)
        {
            await LoadData();
        }
    }

    protected async Task ShowUpdateProduct(long id)
    {
        DialogOptions options = new() { CloseOnEscapeKey = true, MaxWidth = MaxWidth.Medium };
        var parameters = new DialogParameters<AddOrEditProduct>
        {
            {x => x.IdParam, id.ToString() },
            {x => x.IsEdit, true }
        };

        IDialogReference dialog = await DialogService.ShowAsync<AddOrEditProduct>("Update Product", parameters, options);

        DialogResult result = await dialog.Result;

        if (!result.Canceled)
        {
            await LoadData();
        }
    }

    protected async Task ShowProductDetails(long id)
    {
        DialogOptions options = new() { CloseOnEscapeKey = true, MaxWidth = MaxWidth.Medium };
        var parameters = new DialogParameters<AddOrEditProduct>
        {
            {x => x.IdParam, id.ToString() },
            {x => x.IsViewing, true }
        };

        IDialogReference dialog = await DialogService.ShowAsync<AddOrEditProduct>("Product Details", parameters, options);

        DialogResult result = await dialog.Result;

        if (!result.Canceled)
        {
            await LoadData();
        }
    }

    protected async Task DeleteProduct(long id)
    {
        IsSaving = true;
        try
        {
            if (id == 0)
            {
                IsSaving = false;
                return;
            }

            string url = baseUrl + $"/Get/{id}";
            ProductDetailsDto productDto = await _httpClient.GetFromJsonAsync<ProductDetailsDto>(url);
            if (productDto != null) {

                bool? result = await DialogService.ShowMessageBox(
                    "Confirmation",
                    "Are you sure you want to delete this item?",
                    yesText: "Confirm",
                    cancelText: "Cancel"
                    );

                if (result.HasValue && result.Value == true)
                {
                    string deleteUrl = baseUrl + $"/Delete/{id}";
                    await _httpClient.PatchAsJsonAsync(deleteUrl, productDto);
                } 
            }
            else
            {
                Snackbar.Add("Unable to get product details");
            }
        }
        catch (Exception ex) {
            _logger.Log(LogLevel.Error, ex.Message);
        }
        finally
        {
            IsSaving = false;
            await LoadData();
        }
    }

    protected void HandleSearch()
    {
        if (string.IsNullOrEmpty(SearchText))
        {
            FilteredProductList = ProductList;
            return;
        }

        switch (SearchCategory)
        {
            case ("Name"):
                FilteredProductList = ProductList.Where(x => x.Name.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase)).ToList();
                break;
            case ("Price"):
                FilteredProductList = ProductList.Where(x => x.Price.ToString().Contains(SearchText, StringComparison.InvariantCultureIgnoreCase)).ToList();
                break;
            case ("Quantity"):
                FilteredProductList = ProductList.Where(x => x.Quantity.ToString().Contains(SearchText, StringComparison.InvariantCultureIgnoreCase)).ToList();
                break;
        }
    }

}
