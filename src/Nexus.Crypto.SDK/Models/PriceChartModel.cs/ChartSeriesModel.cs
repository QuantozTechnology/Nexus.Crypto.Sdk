using System.Collections;
using Newtonsoft.Json;
using Nexus.Crypto.SDK.Helpers;

namespace Nexus.Crypto.SDK.Models.PriceChartModel.cs;

public class ChartSeriesModelPT
{
    private string _step = "false";
    private ChartMarkerOptions chartMarkerOptions;

    public ChartSeriesModelPT()
    {
        chartMarkerOptions = new ChartMarkerOptions();
        LineWidth = 2;
        FillOpacity = 0.75;

        Data = new ArrayList();
    }

    public string Color { get; set; }
    public bool Visible { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public int LineWidth { get; set; }
    public double FillOpacity { get; set; }

    public string Step
    {
        get
        {
            return _step;
        }
        set
        {
            if (value.Equals("false") || value.Equals("true"))
            {
                _step = value;
            }
        }
    }

    public ChartMarkerOptions Marker
    {
        get
        {
            return chartMarkerOptions;
        }
        set
        {
            if (value != chartMarkerOptions)
            {
                chartMarkerOptions = value;
            }
        }
    }

    [JsonConverter(typeof(ArrayListConverter<string[]>))]
    public ArrayList Data { get; set; }
}

public class ChartSeriesModelLinkedPT : ChartSeriesModelPT
{
    public string LinkedTo { get; set; }
}