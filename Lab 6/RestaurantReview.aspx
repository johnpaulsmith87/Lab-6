<%@ Page Title="" Language="C#" MasterPageFile="~/ACMasterPage.master" AutoEventWireup="true" CodeFile="RestaurantReview.aspx.cs" Inherits="RestaurantReview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Lab 6 - John-Paul Smith</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlSelectRestaurant">
                <asp:Label runat="server" ID="lblChooseRestaurant" Text="Select a Restaurant:"></asp:Label>
                <asp:DropDownList runat="server" ID="ddlSelectRestaurant" AutoPostBack="true" CssClass="dropdown" OnSelectedIndexChanged="ddlSelectRestaurant_SelectedIndexChanged">
                    <asp:ListItem Text="Please Select a Restaurant.." Value="-1"></asp:ListItem>
                </asp:DropDownList>
            </asp:Panel>
            <br />
            <asp:Panel runat="server" ID="pnlRestaurantDetails">
                <asp:Label runat="server" ID="lblAddress" Text="Address:"></asp:Label>
                <br />
                <asp:TextBox runat="server" ID="txtAddress" ReadOnly="true" CssClass="textBox" TextMode="MultiLine"></asp:TextBox>
                <br />
                <asp:Label runat="server" Text="Summary:"></asp:Label>
                <br />
                <asp:TextBox runat="server" ID="txtSummary" CssClass="textBox" TextMode="MultiLine"></asp:TextBox>
                <br />
                <asp:Label runat="server" ID="lblRating" Text="Rating: "></asp:Label>
                <asp:DropDownList runat="server" ID="ddlRating" CssClass="dropdown">
                    <asp:ListItem Text="1"></asp:ListItem>
                    <asp:ListItem Text="2"></asp:ListItem>
                    <asp:ListItem Text="3"></asp:ListItem>
                    <asp:ListItem Text="4"></asp:ListItem>
                    <asp:ListItem Text="5"></asp:ListItem>
                </asp:DropDownList>
                <br />
                <br />
                <asp:Button runat="server" CssClass="btn" Text="Save Changes" ID="btnSaveChanges" OnClick="btnSaveChanges_Click" />
            </asp:Panel>
            <br />
            <br />
            <asp:Panel runat="server" ID="pnlSave" Visible="false">
                <asp:Label runat="server" ID="lblResult" ></asp:Label>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSaveChanges" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

