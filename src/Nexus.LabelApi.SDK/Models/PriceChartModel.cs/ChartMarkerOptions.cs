namespace Nexus.LabelApi.SDK.Models.PriceChartModel
{
    public class ChartMarkerOptions
    {
        private bool _enabled = false;
        private int _radius = 3;

        public bool enabled
        {
            get
            {
                return _enabled;
            }
            set
            {
                if (value != _enabled)
                {
                    _enabled = value;
                }
            }
        }

        public int radius
        {
            get
            {
                return _radius;
            }
            set
            {
                if (value != _radius && value > 0)
                {
                    _radius = value;
                }
            }
        }
    }
}
