<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PackageAnalyser.aspx.cs" Inherits="PackageDependencyCheckerApp.PackageAnalyser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #txtDependencies {
            height: 233px;
            width: 584px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <textarea id="txtDependencies" runat="server"></textarea>
        <p>
        <asp:Button ID="btnAnalyse" runat="server" Text="Get Sorted Dependencies" OnClick="btnAnalyse_Click" />
            </p>
    </div>
        <p>
            Output is :
            <asp:Label ID="sortedPackages" runat="server" Text=""></asp:Label>
        </p>
    </form>
</body>
</html>
