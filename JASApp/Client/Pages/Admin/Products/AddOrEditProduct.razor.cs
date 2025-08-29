using JAS.Shared.Dtos.Product;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System.Net.Http.Json;

namespace JASApp.Client.Pages.Admin.Products;

public partial class AddOrEditProduct
{
    [Parameter]
    public string IdParam { get; set; }
    [Parameter]
    public bool IsViewing { get; set; } = false;
    [Parameter]
    public bool IsEdit { get; set; } = false;

    [Inject]
    protected HttpClient HttpClient { get; set; } = default!;
    [Inject]
    protected ILogger<AddOrEditProduct> Logger { get; set; } = default!;
    [Inject]
    protected ISnackbar Snackbar { get; set; } = default!;

    [CascadingParameter]
    protected IMudDialogInstance MudDialog { get; set; } = default!;

    protected ProductDetailsDto ProductDetailsDto { get; set; } = new();
    protected ProductCreateOrUpdateDto ProductDto { get; set; } = new();

    private readonly List<IBrowserFile> files = [];

    private string baseUrl = "/api/Product";
    protected MudForm MudForm { get; set; }
    public bool IsSaving { get; private set; }
    protected long ProductId;

    protected override async Task OnParametersSetAsync()
    {
        if (!string.IsNullOrEmpty(IdParam))
        {
            if (long.TryParse(IdParam, out long productId))
            {
                ProductId = productId;
                await LoadData();
            }
        }
    }

    private async Task LoadData()
    {
        try
        {
            string url = baseUrl + $"/Get/{ProductId}";
            ProductDetailsDto getProduct = await HttpClient.GetFromJsonAsync<ProductDetailsDto>(url);

            ProductDetailsDto = getProduct;

            ProductDto.Id = ProductDetailsDto.Id;
            ProductDto.Name = ProductDetailsDto.Name;
            ProductDto.Price = ProductDetailsDto.Price;
            ProductDto.DiscountPrice = ProductDetailsDto.DiscountPrice.GetValueOrDefault();
            ProductDto.Description = ProductDetailsDto.Description;
            ProductDto.IsDeleted = ProductDetailsDto.IsDeleted;
            ProductDto.IsActive = ProductDetailsDto.IsActive;
            ProductDto.Quantity = ProductDetailsDto.Quantity;
            ProductDto.Image = ProductDetailsDto.Image;
            ProductDto.ImageUrl = ProductDetailsDto.ImageUrl;
        }
        catch (Exception ex)
        {
            Logger.Log(LogLevel.Error, ex.Message);
        }
        
    }

    protected async Task AddProductAsync()
    {
        IsSaving = true;
        await MudForm.Validate();
        if (!MudForm.IsValid)
        {
            IsSaving = false;
            return;
        }

        try
        {
            if (ProductDto != null)
            {
                await HttpClient.PostAsJsonAsync(baseUrl, ProductDto);
                MudDialog.Close();
            }
            else
            {
                IsSaving = false;
                return;
            }
        }
        catch (Exception ex)
        {
            Logger.Log(LogLevel.Error, ex.Message);
        }
        finally
        {
            Snackbar.Add("Product Added successfully!");
            IsSaving = false;
        }
    }

    protected async Task UpdateProductAsync()
    {
        IsSaving = true;
        try
        {
            ProductDetailsDto.Name = ProductDto.Name;
            ProductDetailsDto.Price = ProductDto.Price;
            ProductDetailsDto.Description = ProductDto.Description;

            var update = await HttpClient.PatchAsJsonAsync(baseUrl + $"/Update/{ProductId}", ProductDto);
        }
        catch (Exception ex)
        {
            Logger.Log(LogLevel.Error, ex.Message);
        }
        finally
        {
            Snackbar.Add("Product updated successfully!");
            IsSaving = false;
            MudDialog.Close();
        }
    }

    string Validate(decimal param)
    {
        if(param <= 0)
        {
            return "Must not be 0";
        }

        return null;
    }

    private void UploadFiles(IReadOnlyList<IBrowserFile> files)
    {
        foreach (var file in files)
        {
            this.files.Add(file);
        }

    }

    protected void EnableEditing() => IsViewing = !IsViewing;

    protected void Cancel() => MudDialog.Cancel();
}
