using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avalanchain
{
    class DashBoardDetailViewModel
    {
        public PlotModel PieModel { get; set; }

        public PlotModel AreaModel { get; set; }
        public PlotModel BarModel { get; set; }
        public PlotModel StackedBarModel { get; set; }
        public PlotModel ScatterSeriesModel { get; set; }
        public PlotModel StemSeriesModel { get; set; }
        public DashBoardDetailViewModel()
        {
            PieModel = CreatePieChart();
            //AreaModel = CreateAreaChart();
            StackedBarModel = CreateBarChart(true, "Transactions");
            //BarModel = CreateBarChart(false, "Un-Stacked Bar");
            ScatterSeriesModel = CreateScatterSeries();
            StemSeriesModel = CreateStemSeries();
        }
        private PlotModel CreatePieChart()
        {
            var model = new PlotModel {
                Title = "Accounts",
                LegendTitleColor = OxyColor.Parse("#C7413E"),
            };

            var ps = new PieSeries
            {
                Diameter = 0.7,
                InnerDiameter = 0.5,
                StrokeThickness = .25,
                InsideLabelPosition = .25,
                AngleSpan = 360,
                StartAngle = 0,
                Font = "Ostrich",
                FontWeight = FontWeights.Bold,
                Selectable = true
            };
            var accounts = SampleData.Accounts;
            var colors = SamplesDefinition.CategoriesColors;
            var i = 0;
            foreach (Account account in accounts)
            {
                var label = account.Name + " (" + account.AccountNumberSubName + ")";
                ps.Slices.Add(new PieSlice(label, (double)account.Amount) { IsExploded = false, Fill = OxyColor.Parse(colors[i]) });
                i++;
            }

            model.Series.Add(ps);
            return model;
        }

        private PlotModel CreateBarChart(bool stacked, string title)
        {
            var model = new PlotModel
            {
                //Title = title,
                LegendPlacement = LegendPlacement.Outside,
                LegendPosition = LegendPosition.BottomCenter,
                LegendOrientation = LegendOrientation.Horizontal,
                LegendBorderThickness = 0,
                LegendFontSize = 15,
                PlotAreaBorderColor = OxyColor.Parse("#ffffff"),
            };
            var colors = SamplesDefinition.CategoriesColors;
            var s1 = new BarSeries
            {
                Title = "Sent",
                IsStacked = stacked,
                StrokeColor = OxyColor.Parse("#ffffff"),
                FillColor = OxyColor.Parse("#413ec7"),
                //Background = OxyColor.Parse("#C7413E"),
                StrokeThickness = 0,
            };
            s1.Items.Add(new BarItem { Value = 25, Color = OxyColor.Parse("#413ec7") });
            s1.Items.Add(new BarItem { Value = 137, Color = OxyColor.Parse("#413ec7") });
            s1.Items.Add(new BarItem { Value = 18, Color = OxyColor.Parse("#413ec7") });
            s1.Items.Add(new BarItem { Value = 40, Color = OxyColor.Parse("#413ec7") });

            var s2 = new BarSeries
            {
                Title = "Recieved",
                FontWeight = 25,
                StrokeColor = OxyColor.Parse("#ffffff"),
                //ColorField = OxyColor.Parse(colors[4]),
                FillColor = OxyColor.Parse("#c7413e"),
                IsStacked = stacked,
                StrokeThickness = 0
            };
            s2.Items.Add(new BarItem { Value = 12, Color = OxyColor.Parse("#c7413e") });
            s2.Items.Add(new BarItem { Value = 14, Color = OxyColor.Parse("#c7413e") });
            s2.Items.Add(new BarItem { Value = 120, Color = OxyColor.Parse("#c7413e") });
            s2.Items.Add(new BarItem { Value = 26, Color = OxyColor.Parse("#c7413e") });

            var categoryAxis = new CategoryAxis { Position = CategoryAxisPosition() };
            var accounts = SampleData.Cards;

            foreach (Account account in accounts)
            {
                var label = account.Name + " (" + account.AccountNumberSubName + ")";
                categoryAxis.Labels.Add(label);
            }
            var valueAxis = new LinearAxis
            {
                Position = ValueAxisPosition(),
                MinimumPadding = 0,
                MaximumPadding = 0.06,
                AbsoluteMinimum = 0,
            };
            model.Series.Add(s1);
            model.Series.Add(s2);
            model.Axes.Add(categoryAxis);
            model.Axes.Add(valueAxis);
            return model;
        }
        private PlotModel CreateScatterSeries()
        {
            var model = new PlotModel { Title = "ScatterSeries" };
            var scatterSeries = new ScatterSeries { MarkerType = MarkerType.Circle };
            var r = new Random(314);
            for (int i = 0; i < 100; i++)
            {
                var x = r.NextDouble();
                var y = r.NextDouble();
                var size = r.Next(5, 15);
                var colorValue = r.Next(100, 1000);
                scatterSeries.Points.Add(new ScatterPoint(x, y, size, colorValue));
            }

            model.Series.Add(scatterSeries);
            model.Axes.Add(new LinearColorAxis { Position = AxisPosition.Right, Palette = OxyPalettes.Jet(200) });
            return model;
        }
        private PlotModel CreateStemSeries()
        {
            var model = new PlotModel { Title = "Trigonometric functions" };

            var start = -Math.PI;
            var end = Math.PI;
            var step = 0.1;
            int steps = (int)((Math.Abs(start) + Math.Abs(end)) / step);

            //generate points for functions
            var sinData = new DataPoint[steps];
            for (int i = 0; i < steps; ++i)
            {
                var x = (start + step * i);
                sinData[i] = new DataPoint(x, Math.Sin(x));
            }

            //sin(x)
            var sinStemSeries = new StemSeries
            {
                MarkerStroke = OxyColors.Green,
                MarkerType = MarkerType.Circle
            };
            sinStemSeries.Points.AddRange(sinData);

            model.Series.Add(sinStemSeries);
            return model;
        }
        public void GetData()
        {
            var transactions = SampleData.Transactions;
            //var s1 = new BarSeries
            //{
            //    Title = "Series 1",
            //    IsStacked = stacked,
            //    StrokeColor = OxyColors.Black,
            //    StrokeThickness = 1
            //};
            //s1.Items.Add(new BarItem { Value = 25 });
            //s1.Items.Add(new BarItem { Value = 137 });
            //s1.Items.Add(new BarItem { Value = 18 });
            //s1.Items.Add(new BarItem { Value = 40 });

            //var s2 = new BarSeries
            //{
            //    Title = "Series 2",
            //    IsStacked = stacked,
            //    StrokeColor = OxyColors.Black,
            //    StrokeThickness = 1
            //};
            //s2.Items.Add(new BarItem { Value = 12 });
            //s2.Items.Add(new BarItem { Value = 14 });
            //s2.Items.Add(new BarItem { Value = 120 });
            //s2.Items.Add(new BarItem { Value = 26 });
        }
        private AxisPosition CategoryAxisPosition()
        {
            if (typeof(BarSeries) == typeof(ColumnSeries))
            {
                return AxisPosition.Bottom;
            }

            return AxisPosition.Left;
        }

        private AxisPosition ValueAxisPosition()
        {
            if (typeof(BarSeries) == typeof(ColumnSeries))
            {
                return AxisPosition.Left;
            }

            return AxisPosition.Bottom;
        }
    }
}
