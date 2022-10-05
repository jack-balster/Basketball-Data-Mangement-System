<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Basketball.aspx.vb" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <br />
        <asp:LinkButton ID="LinkButton1" runat="server">LinkButton</asp:LinkButton>
        <asp:LinkButton ID="LinkButton2" runat="server">LinkButton</asp:LinkButton>
        <asp:LinkButton ID="LinkButton3" runat="server">LinkButton</asp:LinkButton>
        <asp:MultiView ID="MultiView1" runat="server">
            <asp:View ID="View1" runat="server">
                <table class="auto-style1">
                    <tr>
                        <td>Player Name</td>
                        <td>
                            <asp:DropDownList ID="DropDownList2" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>Name</td>
                        <td>
                            <asp:TextBox ID="TextBox10" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Position</td>
                        <td>
                            <asp:TextBox ID="TextBox11" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Phone</td>
                        <td>
                            <asp:TextBox ID="TextBox12" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Height</td>
                        <td>
                            <asp:TextBox ID="TextBox13" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Weight</td>
                        <td>
                            <asp:TextBox ID="TextBox14" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Player Summary</td>
                        <td>
                            <asp:TextBox ID="TextBox15" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Contract Start</td>
                        <td>
                            <asp:TextBox ID="TextBox16" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Contract End</td>
                        <td>
                            <asp:TextBox ID="TextBox17" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Contract Amount</td>
                        <td>
                            <asp:TextBox ID="TextBox18" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="Button5" runat="server" Text="Button" />
                        </td>
                        <td>
                            <asp:Button ID="Button6" runat="server" Text="Button" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="Button7" runat="server" Text="Button" />
                        </td>
                        <td>
                            <asp:Button ID="Button8" runat="server" Text="Button" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Image ID="Image2" runat="server" />
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="GridView2" runat="server">
                            </asp:GridView>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="View2" runat="server">
                <table class="auto-style1">
                    <tr>
                        <td>Player Name</td>
                        <td>
                            <asp:DropDownList ID="DropDownList3" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>Name</td>
                        <td>
                            <asp:TextBox ID="TextBox19" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Position</td>
                        <td>
                            <asp:TextBox ID="TextBox20" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Phone</td>
                        <td>
                            <asp:TextBox ID="TextBox21" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Height</td>
                        <td>
                            <asp:TextBox ID="TextBox22" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Weight</td>
                        <td>
                            <asp:TextBox ID="TextBox23" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Player Summary</td>
                        <td>
                            <asp:TextBox ID="TextBox24" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Contract Start</td>
                        <td>
                            <asp:TextBox ID="TextBox25" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Contract End</td>
                        <td>
                            <asp:TextBox ID="TextBox26" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Contract Amount</td>
                        <td>
                            <asp:TextBox ID="TextBox27" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="Button9" runat="server" Text="Button" />
                        </td>
                        <td>
                            <asp:Button ID="Button10" runat="server" Text="Button" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="Button11" runat="server" Text="Button" />
                        </td>
                        <td>
                            <asp:Button ID="Button12" runat="server" Text="Button" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Image ID="Image3" runat="server" />
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="GridView3" runat="server">
                            </asp:GridView>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="View3" runat="server">
                <table class="auto-style1">
                    <tr>
                        <td>Select Player Name</td>
                        <td>
                            <asp:DropDownList ID="DropDownList4" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td>Game ID</td>
                        <td>
                            <asp:DropDownList ID="DropDownList5" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>Game Date</td>
                        <td>
                            <asp:TextBox ID="TextBox28" runat="server"></asp:TextBox>
                        </td>
                        <td>Player ID</td>
                        <td>
                            <asp:TextBox ID="TextBox35" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Points</td>
                        <td>
                            <asp:TextBox ID="TextBox29" runat="server"></asp:TextBox>
                        </td>
                        <td>Player Name</td>
                        <td>
                            <asp:TextBox ID="TextBox36" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Assists</td>
                        <td>
                            <asp:TextBox ID="TextBox30" runat="server"></asp:TextBox>
                        </td>
                        <td>Game Date</td>
                        <td>
                            <asp:TextBox ID="TextBox37" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Rebounds</td>
                        <td>
                            <asp:TextBox ID="TextBox31" runat="server"></asp:TextBox>
                        </td>
                        <td>Points</td>
                        <td>
                            <asp:TextBox ID="TextBox38" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Steals</td>
                        <td>
                            <asp:TextBox ID="TextBox32" runat="server"></asp:TextBox>
                        </td>
                        <td>Assists</td>
                        <td>
                            <asp:TextBox ID="TextBox39" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Blocks</td>
                        <td>
                            <asp:TextBox ID="TextBox33" runat="server"></asp:TextBox>
                        </td>
                        <td>Rebounds</td>
                        <td>
                            <asp:TextBox ID="TextBox40" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Turnovers</td>
                        <td>
                            <asp:TextBox ID="TextBox34" runat="server"></asp:TextBox>
                        </td>
                        <td>Steals</td>
                        <td>
                            <asp:TextBox ID="TextBox41" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>Blocks</td>
                        <td>
                            <asp:TextBox ID="TextBox42" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="Button13" runat="server" Text="Button" />
                            <asp:Button ID="Button14" runat="server" Text="Button" />
                        </td>
                        <td>&nbsp;</td>
                        <td>Turnovers</td>
                        <td>
                            <asp:TextBox ID="TextBox43" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Image ID="Image4" runat="server" />
                        </td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Button ID="Button15" runat="server" Text="Button" />
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="GridView4" runat="server">
                            </asp:GridView>
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </asp:View>
        </asp:MultiView>
        <p>
            &nbsp;</p>
    </form>
</body>
</html>

    

