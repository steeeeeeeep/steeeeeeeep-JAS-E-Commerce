using MudBlazor;

namespace JASApp.Client.Pages.Admin;

public partial class Dashboard
{
    private int _index = -1; //default value cannot be 0 -> first selectedindex is 0.
    private string _width = "650px";
    private string _height = "350px";

    int dataSize = 4;
    public double[] data = { 50, 25, 20, 5 };
    public string[] labels = { "Fossil", "Nuclear", "Solar", "Wind", "Oil", "Coal", "Gas", "Biomass",
                                "Hydro", "Geothermal", "Nuclear Fusion", "Pumped Storage", "Solar", "Wind", "Oil",
                                "Coal", "Gas", "Biomass", "Hydro", "Geothermal" };

    Random random = new Random();

    private AxisChartOptions _axisChartOptions = new AxisChartOptions();


    private ChartOptions options = new ChartOptions();
    public List<ChartSeries> Series = new List<ChartSeries>()
    {
        new ChartSeries() { Name = "Series 1", Data = new double[] { 90, 79, 72, 69, 62, 62, 55, 65, 70 } },
        new ChartSeries() { Name = "Series 2", Data = new double[] { 35, 41, 35, 51, 49, 62, 69, 91, 148 } },
    };
    public string[] XAxisLabels = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep" };


    private List<ChartSeries> _series = new List<ChartSeries>()
    {
        new ChartSeries() { Name = "United States", Data = new double[] { 40, 20, 25, 27, 46, 60, 48, 80, 15 } },
        new ChartSeries() { Name = "Germany", Data = new double[] { 19, 24, 35, 13, 28, 15, 13, 16, 31 } },
        new ChartSeries() { Name = "Sweden", Data = new double[] { 8, 6, 11, 13, 4, 16, 10, 16, 18 } },
    };
    private string[] _xAxisLabels = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep" };


    void AddDataSize()
    {
        if (dataSize < 20)
        {
            dataSize = dataSize + 1;
            RandomizeData();
        }
    }

    void RemoveDataSize()
    {
        if (dataSize > 0)
        {
            dataSize = dataSize - 1;
            RandomizeData();
        }
    }

    protected override void OnInitialized()
    {
        options.InterpolationOption = InterpolationOption.NaturalSpline;
        options.YAxisFormat = "c2";
    }

    public void RandomizeData()
    {
        foreach (var series in Series)
        {
            for (int i = 0; i < series.Data.Length - 1; i++)
            {
                series.Data[i] = random.NextDouble() * 100 + 10;
            }
        }

        StateHasChanged();
    }

    void OnClickMenu(InterpolationOption interpolationOption)
    {
        options.InterpolationOption = interpolationOption;
        StateHasChanged();
    }
}
