namespace Kendo.Mvc.UI.Tests.Chart
{
    using Kendo.Mvc.UI;
    using Kendo.Mvc.UI.Fluent;
    using Xunit;

    public class ChartOHLCSeriesBuilderTests
    {
        private IChartOHLCSeries series;
        private ChartOHLCSeriesBuilder<OHLCData> builder;

        public ChartOHLCSeriesBuilderTests()
        {
            var chart = ChartTestHelper.CreateChart<OHLCData>();
            series = new ChartOHLCSeries<OHLCData, decimal>(chart, s => s.Open, s => s.High, s => s.Low, s => s.Close, s => s.Color, s => s.BaseColor);
            builder = new ChartOHLCSeriesBuilder<OHLCData>(series);
        }

        [Fact]
        public void Aggregate_should_set_Aggregate()
        {
            builder.Aggregates(ChartSeriesAggregate.Max);
            series.Aggregates.Open.ShouldEqual(ChartSeriesAggregate.Max);
        }

        [Fact]
        public void Aggregate_should_return_builder()
        {
            builder.Aggregates(ChartSeriesAggregate.Max).ShouldBeSameAs(builder);
        }

        [Fact]
        public void Gap_should_set_gap()
        {
            builder.Gap(1);
            series.Gap.ShouldEqual(1);
        }

        [Fact]
        public void Spacing_should_set_spacing()
        {
            builder.Spacing(1);
            series.Spacing.ShouldEqual(1);
        }

        [Fact]
        public void Border_sets_border_properties()
        {
            builder.Border(1, "red", ChartDashType.Dot);
            series.Border.Color.ShouldEqual("red");
            series.Border.Width.ShouldEqual(1);
            series.Border.DashType.ShouldEqual(ChartDashType.Dot);
        }

        [Fact]
        public void Line_sets_line_properties()
        {
            builder.Line(1, "red", ChartDashType.Dot);
            series.Line.Color.ShouldEqual("red");
            series.Line.Width.ShouldEqual(1);
            series.Line.DashType.ShouldEqual(ChartDashType.Dot);
        }
    }
}