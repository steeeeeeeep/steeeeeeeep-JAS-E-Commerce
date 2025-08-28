using JAS.Shared.Dto.Product;
using JAS.Shared.Dto.ProductCategory;
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

    protected IEnumerable<ProductCategoryGetDto> ProductCategoryList { get; set; } = new List<ProductCategoryGetDto>();
    protected IEnumerable<ProductsListDto> ProductList { get; set; } = new List<ProductsListDto>();

    private string baseUrl = "/api/Product/All";

    protected override async Task OnParametersSetAsync()
    {
        await LoadData();
    }

    protected async Task LoadData()
    {
        try
        {
            var products = await _httpClient.GetFromJsonAsync<List<ProductsListDto>>(baseUrl);

            ProductList = products;
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Error, ex.Message);
        }
    }

    protected async Task AddProduct()
    {
        DialogOptions options = new() { CloseOnEscapeKey = true, MaxWidth = MaxWidth.Medium };

        IDialogReference dialog = await DialogService.ShowAsync<AddOrEditProduct>("Add Product", options);

        DialogResult result = await dialog.Result;

        if (!result.Canceled)
        {
            await LoadData();
        }
    }
}
