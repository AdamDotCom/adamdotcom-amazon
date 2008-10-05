<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SampleWebApp._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>SampleWebApp</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>

        <h3>My Review List</h3>
        <div style="overflow: scroll; height: 300px;">

            <asp:XmlDataSource ID="XmlReviews" runat="server" DataFile="/Xml/Reviews.xml" XPath="ArrayOfReview/Review" />

            <asp:Repeater ID="AmazonReviews" runat="server" DataSourceID="XmlReviews">
                <ItemTemplate>
                    <div style="background-color: #cccccc; margin-bottom: 15px;">
                        <div style="float: right;">
                            <a href="<%# XPath("Url") %>"><img src="<%# XPath("ImageUrl")%>" alt="<%# XPath("Title") %> Image" /></a>
                        </div>
                        <div>
                             <span style="font-weight: bold;"><%# XPath("Title") %></span>
                             <span style="font-style: italic;">by <%# XPath("Authors") %>.</span>
                        </div>
                        <div>
                            <%# XPath("Content") %>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>

        <h3>My To Read List</h3>
        <div style="overflow: scroll; height: 300px;">
            <ul>

            <asp:XmlDataSource ID="XmlReadingList" runat="server" DataFile="/Xml/Products.xml" XPath="ArrayOfProduct/Product" />

            <asp:Repeater ID="AmazonReadingList" runat="server" DataSourceID="XmlReadingList">
                <ItemTemplate>
                    <li>
                        <%# XPath("Authors") %>&nbsp;<a href="<%# XPath("Url") %>"><%# XPath("Title") %>.</a>&nbsp;
                        <%# XPath("Publisher") %>.&nbsp;
			        </li>
                </ItemTemplate>
            </asp:Repeater>
            </ul>
        </div>
    
    </div>
    </form>
</body>
</html>
