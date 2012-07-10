namespace Kendo.Mvc.UI
{
    using System.Collections.Generic;
    using System.Linq;
    using Moq;
    using Kendo.Mvc.UI.Html;
    using Kendo.Mvc.UI.Tests;
    using Xunit;

    public class GridCustomActionCommandTests
    {
        private GridCustomActionCommand<Customer> command;
        private readonly Mock<IGridUrlBuilder> urlBuilder;
        private readonly Mock<IGridHtmlHelper> htmlHelper;

        public GridCustomActionCommandTests()
        {
            command = new GridCustomActionCommand<Customer>();
            
            command.ControllerName = "Home";
            command.ActionName = "Index";

            urlBuilder = new Mock<IGridUrlBuilder>();
            urlBuilder.Setup(u => u.GetDataKeys()).Returns(new IDataKey[0]).Verifiable();
            htmlHelper = new Mock<IGridHtmlHelper>();
        }

        [Fact]
        public void Should_serialize_custom_click_handler()
        {
            command.Click.HandlerName = "myCustomClickHandler";

            ((ClientHandlerDescriptor)Serialize()["click"]).HandlerName.ShouldEqual("myCustomClickHandler");
        }

        [Fact]
        public void Should_not_serialize_custom_click_handler()
        {
            Serialize().ContainsKey("click").ShouldBeFalse();
        }

        [Fact]
        public void Should_set_use_url_of_the_button_builder()
        {
            command.Text = "Custom";

            var button = Button();

            button.Url.ShouldNotBeNull();
        }

        [Fact]
        public void Should_use_navigatable_and_url_builder_to_build_url()
        {
            urlBuilder.Setup(u => u.Url(It.IsAny<INavigatable>(), true)).Verifiable();

            var button = Button();

            button.Create(null);

            urlBuilder.Verify();
        }

        [Fact]
        public void Should_not_copy_route_values_if_send_state_is_false()
        {
            command.SendState = false;

            urlBuilder.Setup(u => u.Url(It.IsAny<INavigatable>(), false)).Verifiable();

            var button = Button();

            button.Create(null);

            urlBuilder.Verify();
        }
        [Fact]
        public void Should_use_route_keys_to_build_the_url()
        {
            var key = new Mock<IGridDataKey<Customer>>();

            key.Setup(k => k.GetValue(It.IsAny<object>())).Verifiable();
            key.SetupGet(k => k.RouteKey).Returns("foo");

            command.DataRouteValues.Add(key.Object);

            var button = Button();

            button.Create(null);

            key.Verify();
        }

        [Fact]
        public void Should_use_data_keys_to_build_the_url()
        {
            var button = Button();

            button.Create(null);

            urlBuilder.Verify();
        }

        [Fact]
        public void Should_not_use_data_keys_to_build_the_url_if_send_data_keys_is_false()
        {
            command.SendDataKeys = false;

            var button = Button();

            button.Create(null);

            urlBuilder.Verify(u => u.GetDataKeys(), Times.Never());
        }

        private IDictionary<string, object> Serialize()
        {
            return command.Serialize(urlBuilder.Object);
        }

        private IGridButtonBuilder Button()
        {
           return command.CreateDisplayButtons(urlBuilder.Object, htmlHelper.Object).First();
        }
    }
}
