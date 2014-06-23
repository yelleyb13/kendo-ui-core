﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div class="box">
    <div class="box-col">
        <h4>Get selected action</h4>
        <ul class="options">
            <li>
                <button class="getSelected k-button">Get selected action</button>
            </li>
            <li>
                Selected: <span id="selectedFromGroup"></span>
            </li>
        </ul>
    </div>
    <div class="box-col">
        <h4>Enable / Disable</h4>
        <ul class="options">
            <li>
                <button class="toggleRepeat k-button">Enable/Disable Repeat</button>
            </li>
        </ul>
    </div>
    <div class="box-col">
        <h4>Add / Remove</h4>
        <ul class="options">
            <li>
                <button class="removeItem k-button">Remove by ID</button>
                <input type="text" value="#delete" id="forRemoval" class="k-textbox"/> 
            </li>
            <li>
                <button class="addItem k-button">Add new button</button>
                <label>Text: <input type="text" value="New Button" id="btnText" class="k-textbox"/></label>
                <label>ID: <input type="text" value="newButton" id="btnID" class="k-textbox"/></label>
                <label>Togglable: <input type="checkbox" id="btnTogglable"/></label>
            </li>
            <li>
                <button class="addSplitButton k-button">Add new SplitButton</button>
            </li> 
            <li>
                <button class="addButtonGroup k-button">Add new ButtonGroup</button>
            </li>
        </ul>
    </div>
</div>

<%= Html.Kendo().ToolBar()
    .Name("ToolBar")
    .Items(items => {
        items.Add().Type(CommandType.ButtonGroup).Buttons(buttons =>
        {
            buttons.Add().Text("play").Id("play").Togglable(true).Group("player");
            buttons.Add().Text("pause").Id("pause").Togglable(true).Group("player").Selected(true);
            buttons.Add().Text("stop").Id("stop").Togglable(true).Group("player");
        });
        items.Add().Type(CommandType.Button).Id("repeat").Text("repeat").Togglable(true);
        items.Add().Type(CommandType.SplitButton).Text("Save").Id("save").MenuButtons(menuButtons =>
        {
            menuButtons.Add().Text("Add to favourites").Id("favourites");
            menuButtons.Add().Text("Listen later").Id("later");
            menuButtons.Add().Text("Download").Id("download");
        });
        items.Add().Type(CommandType.Button).Id("delete").Text("Delete");
    })
%>

<script>
    $(document).ready(function () {

        $(".getSelected").click(function () {
            var toolbar = $("#ToolBar").data("kendoToolBar"),
                selected;

            selected = toolbar.getSelectedFromGroup("player");
            $("#selectedFromGroup").text(selected.text());
        });

        $(".toggleRepeat").click(function () {
            var toolbar = $("#ToolBar").data("kendoToolBar"),
                repeatButton = $("#repeat"),
                isDisabled = repeatButton.hasClass("k-state-disabled");

            toolbar.enable(repeatButton, isDisabled);
        });

        $(".removeItem").click(function () {
            var toolbar = $("#ToolBar").data("kendoToolBar"),
                id = $("#forRemoval").val();

            toolbar.remove(id);
        });

        $(".addItem").click(function () {
            var toolbar = $("#ToolBar").data("kendoToolBar"),
                text = $("#btnText").val(),
                id = $("#btnID").val(),
                togglable = $("#btnTogglable").prop("checked");

            toolbar.add({ type: "button", text: text, id: id, togglable: togglable });
        });

        $(".addSplitButton").click(function () {
            var toolbar = $("#ToolBar").data("kendoToolBar");

            toolbar.add({
                type: "splitButton",
                text: "New SplitButton",
                menuButtons: [
                    { text: "Option 1" },
                    { text: "Option 2" }
                ]
            });
        });

        $(".addButtonGroup").click(function () {
            var toolbar = $("#ToolBar").data("kendoToolBar");

            toolbar.add({
                type: "buttonGroup",
                buttons: [
                    { text: "Left" },
                    { text: "Middle" },
                    { text: "Right" }
                ]
            });
        });

    });
</script>

</asp:Content>