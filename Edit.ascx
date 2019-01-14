<%@ Control Language="C#" Inherits="DevPCI.Modules.DDT_Org_Chart.Edit" AutoEventWireup="false" CodeBehind="Edit.ascx.cs" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="dnn" TagName="UrlControl" Src="~/controls/URLControl.ascx" %>
<%@ Register TagPrefix="dnn" Assembly="DotNetNuke.Web" Namespace="DotNetNuke.Web.UI.WebControls" %>
<%@ Register TagPrefix="dnn" Assembly="DotNetNuke" Namespace="DotNetNuke.UI.WebControls" %>
<%-- <%@ Register TagPrefix="dnn" TagName="FilePickerUploader" Src="~/controls/filepickeruploader.ascx" %> --%>
<%@ Import Namespace="DotNetNuke.Services.Localization" %>
<script type="text/javascript">
    var uploadedFilesCount = 0;
    var isEditMode;
    function validateRadUpload(source, e) {

        if (isEditMode == null || isEditMode == undefined) {
            e.IsValid = false;

            if (uploadedFilesCount > 0) {
                e.IsValid = true;
            }
        }
        isEditMode = null;
    }

    function OnClientFileUploaded(sender, eventArgs) {
        uploadedFilesCount++;
    }           
             
</script>
<asp:PlaceHolder ID="phdnn7filepickercsshack" runat="server" Visible="false">
<style type="text/css"> 
.dnnFilePicker {
    background-color: #F0F0F0;
    display: block;
    float: left;
    margin-bottom: 0;
    margin-right: 10px;
    padding-left: 15px;
    padding-top: 35px;
    width: 340px;
}
</style>
</asp:PlaceHolder>
<asp:Label ID="LblMode" runat="server" Text="Label" Visible="false"></asp:Label>
<asp:Label ID="LblDNNVersion" runat="server" Text="Label" Visible="false"></asp:Label>
<asp:PlaceHolder ID="PhMessageNotif3" runat="server" Visible="false">
    <div>
        <asp:Image ID="Image3" runat="server" ImageUrl="/DesktopModules/DDT_Org_Chart/images/red-error.gif" />
        <asp:Label ID="LblMessageNotif3" runat="server" Text="Label"></asp:Label>
    </div>
</asp:PlaceHolder>
<asp:PlaceHolder ID="PHSimple" runat="server" Visible="False">
<h1><asp:Label ID="lblItems" runat="server" Text="Items" ResourceKey="lblItems"></asp:Label></h1><telerik:RadGrid ID="RG_Items_Simple" runat="server" AllowAutomaticDeletes="True"
    AllowAutomaticInserts="True" AllowAutomaticUpdates="True" AllowMultiRowEdit="True" AllowMultiRowSelection="True"
    AutoGenerateColumns="False" CellSpacing="0" DataSourceID="LDS_Org_Chart_Items"
    GridLines="None" OnItemDataBound="RG_Items_Simple_ItemDataBound" OnLoad="RG_Items_Simple_Load" OnItemCreated="RG_Items_Simple_ItemCreated">
    <AlternatingItemStyle CssClass="rgAltRow"></AlternatingItemStyle>
    <GroupHeaderItemStyle CssClass="rgGroupHeader"></GroupHeaderItemStyle>
    <MasterTableView AutoGenerateColumns="False" DataKeyNames="ID_Org_Chart_Item" DataSourceID="LDS_Org_Chart_Items"
        CommandItemDisplay="TopAndBottom" EditMode="InPlace">
        <EditFormSettings>
            <EditColumn FilterControlAltText="Filter EditCommandColumn column">
            </EditColumn>
        </EditFormSettings>
        <CommandItemTemplate>
            <div style="padding: 5px 5px;">
                &#160;&#160;&#160;&#160;
                <asp:LinkButton ID="btnEditSelected12" runat="server" CommandName="EditSelected" Visible="<%# RG_Items_Simple.EditIndexes.Count == 0 %>"><img alt="" 
                            src="/DesktopModules/DDT_Org_Chart/images/Edit.gif" 
                            style="border:0px;vertical-align:middle;" /><%=LocalizeString("EditSelection") %></asp:LinkButton>&#160;&#160;
                <asp:LinkButton ID="btnUpdateEdited12" runat="server" CommandName="UpdateEdited" Visible="<%# RG_Items_Simple.EditIndexes.Count > 0 %>"><img alt="" 
                            src="/DesktopModules/DDT_Org_Chart/images/Update.gif" 
                            style="border:0px;vertical-align:middle;" /><%=LocalizeString("Update")%></asp:LinkButton>&#160;&#160;
                <asp:LinkButton ID="btnCancel12" runat="server" CommandName="CancelAll" Visible="<%# RG_Items_Simple.EditIndexes.Count > 0 || RG_Teams.MasterTableView.IsItemInserted %>"><img 
                            alt="" src="/DesktopModules/DDT_Org_Chart/images/Cancel.gif" 
                            style="border:0px;vertical-align:middle;" /><%=LocalizeString("Cancel")%></asp:LinkButton>&#160;&#160;
                <asp:LinkButton ID="LinkButton23" runat="server" CommandName="InitInsert" Visible="<%# !RG_Items_Simple.MasterTableView.IsItemInserted %>"><img 
                            alt="" src="/DesktopModules/DDT_Org_Chart/images/AddRecord.gif" 
                            style="border:0px;vertical-align:middle;" /><%=LocalizeString("AddRecord")%></asp:LinkButton>&#160;&#160;
                <asp:LinkButton ID="LinkButton24" runat="server" CommandName="PerformInsert" Visible="<%# RG_Items_Simple.MasterTableView.IsItemInserted %>"><img 
                            alt="" src="/DesktopModules/DDT_Org_Chart/images/Insert.gif" 
                            style="border:0px;vertical-align:middle;" /><%=LocalizeString("AddThisRecord")%></asp:LinkButton>&#160;&#160;
                <%--Code for Soft Delete--%><asp:LinkButton ID="LinkButton15" runat="server" OnClientClick='<%#ConfirmLocalize("ConfirmSoftDelete")%>'
                    OnCommand="RG_Items_Simple_SoftDeleteSelected"><img alt="" 
                            src="/DesktopModules/DDT_Org_Chart/images/Delete.gif" 
                            style="border:0px;vertical-align:middle;" /><%=LocalizeString("DeleteSelected")%></asp:LinkButton>&#160;&#160;
                <asp:LinkButton ID="LinkButton26" runat="server" CommandName="RebindGrid"><img alt="" 
                            src="/DesktopModules/DDT_Org_Chart/images/Refresh.gif" 
                            style="border:0px;vertical-align:middle;" /><%=LocalizeString("Refresh")%></asp:LinkButton>&#160;&#160;
                <%--code for Hard Delete--%><asp:LinkButton ID="LinkButton36" runat="server" CommandName="DeleteSelected"
                    OnClientClick=<%# String.Format("javascript:return confirm('{0}')", LocalizeString("ConfirmHardDelete"))%>
                    Visible="<%# CheckBoxIsSuperUser.Checked %>"><img alt="" 
                            src="/DesktopModules/DDT_Org_Chart/images/Delete.gif" 
                            style="border:0px;vertical-align:middle;" /><%=LocalizeString("HardDelete")%></asp:LinkButton></div>
        </CommandItemTemplate>
        <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
            <RowIndicatorColumn>
                <HeaderStyle CssClass="rgResizeCol"></HeaderStyle>
                <ItemStyle CssClass="rgResizeCol"></ItemStyle>
            </RowIndicatorColumn>
        <ExpandCollapseColumn>
        </ExpandCollapseColumn>
        <Columns>
            <telerik:GridBoundColumn DataField="ID_Org_Chart_Item" DataType="System.Int32" FilterControlAltText="Filter ID_Org_Chart_Item column"
                HeaderText="ID_Org_Chart_Item" ReadOnly="True" SortExpression="ID_Org_Chart_Item"
                UniqueName="ID_Org_Chart_Item" >
            </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn DataField="PortalID" DataType="System.Int32" FilterControlAltText="Filter PortalID column"
                    HeaderText="PortalID" SortExpression="PortalID" UniqueName="PortalID">
                    <EditItemTemplate>
                        <asp:TextBox ID="PortalIDTextBox" runat="server" Text='<%# Bind("PortalID")%>'></asp:TextBox></EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="PortalIDLabelTextBox" runat="server" Text='<%# Eval("PortalID")%>'></asp:Label></ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn DataField="ModuleID" DataType="System.Int32" FilterControlAltText="Filter ModuleID column"
                    HeaderText="ModuleID" SortExpression="ModuleID" UniqueName="ModuleID">
                    <EditItemTemplate>
                        <asp:TextBox ID="ModuleIDTextBox" runat="server" Text='<%# Bind("ModuleID")%>'></asp:TextBox></EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="ModuleIDLabelTextBox" runat="server" Text='<%# Eval("ModuleID")%>'></asp:Label></ItemTemplate>
                </telerik:GridTemplateColumn> 

<%--           <telerik:GridBoundColumn DataField="ID_Org_Chart_Node" FilterControlAltText="Filter ID_Org_Chart_Node column"
                HeaderText="ID_Org_Chart_Node" SortExpression="ID_Org_Chart_Node" UniqueName="ID_Org_Chart_Node"
                DataType="System.Int32">
            </telerik:GridBoundColumn>
--%>            
            <telerik:GridDropDownColumn DataField="ID_Org_Chart_Node" FilterControlAltText="Filter ID_Org_Chart_Node column"
                    HeaderText="Parent Item" ListTextField="ItemName_Org_Chart"
                    ListValueField="ID_Org_Chart_Item" SortExpression="ItemOrder_Org_Chart" DataSourceID="LDS_ParentItems_Select"
                    EnableEmptyListItem="true" HeaderStyle-Width="180px" UniqueName="TeamName_Org_Chart_Node">
            </telerik:GridDropDownColumn>

            <telerik:GridBoundColumn DataField="ItemName_Org_Chart" FilterControlAltText="Filter ItemName_Org_Chart column"
                HeaderText="Item Name" SortExpression="ItemName_Org_Chart" UniqueName="ItemName_Org_Chart" HeaderStyle-Width="180px">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="ItemTitle_Org_Chart" FilterControlAltText="Filter ItemTitle_Org_Chart column"
                HeaderText="Item Title" SortExpression="ItemTitle_Org_Chart" UniqueName="ItemTitle_Org_Chart" HeaderStyle-Width="180px">
            </telerik:GridBoundColumn>

<%--            <telerik:GridBoundColumn DataField="ItemImageUrl_Org_Chart" FilterControlAltText="Filter ItemImageUrl_Org_Chart column"
                HeaderText="ItemImageUrl_Org_Chart" SortExpression="ItemImageUrl_Org_Chart" UniqueName="ItemImageUrl_Org_Chart">
            </telerik:GridBoundColumn>
--%>
            <telerik:GridTemplateColumn DataField="ItemImageUrl_Org_Chart" FilterControlAltText="Filter ItemImageUrl_Org_Chart column" HeaderText="Item Image Url" SortExpression="ItemImageUrl_Org_Chart"
                    UniqueName="ItemImageUrl_Org_Chart" HeaderStyle-Width="340px">
                    <EditItemTemplate>
                        <dnn:DnnFilePicker runat="server" ID="FilePickerSimple" FilePath='<%# Bind("ItemImageUrl_Org_Chart")%>'
                        FileFilter="jpg,png,gif"  />
<%--                        <dnn:FilePickerUploader runat="server" ID="FilePickerSimple" FilePath='<%# Bind("ItemImageUrl_Org_Chart")%>'
                        FileFilter="jpg,png,gif" />--%>  
                    </EditItemTemplate>
                    <ItemTemplate>
                        <img alt="<%# Eval("ItemImageUrl_Org_Chart")%>" src="/Portals/<%# Eval("PortalId") %>/<%# Eval("ItemImageUrl_Org_Chart")%>" />
                        <asp:LinkButton ID="DDT_Org_Chart_ItemDeleteImage" runat="server" CommandArgument='<%# Eval("ID_Org_Chart_Item") %>'
                        CommandName="DeleteItemImage" OnClick="DDT_Org_Chart_ItemDeleteImage_Click" Text="delete"><img alt="<%=LocalizeString("Delete")%>" 
                              src="/DesktopModules/DDT_Org_Chart/images/Delete.gif" 
                              style="border:0px;vertical-align:middle;" /></asp:LinkButton>
                    </ItemTemplate>
            </telerik:GridTemplateColumn>

            <telerik:GridTemplateColumn DataField="ItemOrder_Org_Chart" DataType="System.Int32"
                    FilterControlAltText="Filter ItemOrder_Org_Chart column" HeaderText="Order" SortExpression="ItemOrder_Org_Chart"
                    UniqueName="ItemOrder_Org_Chart">
                    <EditItemTemplate>
                        <asp:TextBox ID="OrderIDTextBox" runat="server" Text='<%# Bind("ItemOrder_Org_Chart")%>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="OrderIDLabelTextBox" runat="server" Text='<%# Eval("ItemOrder_Org_Chart")%>'></asp:Label>
                    </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridCheckBoxColumn DataField="Collapsed" DataType="System.Boolean" FilterControlAltText="Filter Collapsed column"
                HeaderText="Collapsed" SortExpression="Collapsed" UniqueName="Collapsed">
            </telerik:GridCheckBoxColumn>
                <telerik:GridCheckBoxColumn DataField="IsActive" DataType="System.Boolean" DefaultInsertValue="True"
                    HeaderText="Is Active" SortExpression="IsActive" UniqueName="IsActive">
                </telerik:GridCheckBoxColumn>
                <telerik:GridCheckBoxColumn DataField="IsDeleted" DataType="System.Boolean" FilterControlAltText="Filter IsDeleted column"
                    HeaderText="Is Deleted" SortExpression="IsDeleted" UniqueName="IsDeleted">
                </telerik:GridCheckBoxColumn>
            <telerik:GridTemplateColumn HeaderText="Order" UniqueName="Order" HeaderStyle-Width="40px">
                <ItemTemplate>
                    <asp:LinkButton ID="DDT_Org_Chart_ItemlnkOrderDown" runat="server" CommandArgument='<%# Eval("ID_Org_Chart_Item") %>'
                        CommandName="OrderDown" OnClick="DDT_Org_Chart_ItemlnkOrderDown_Click" Text="down"><img alt="" 
                              src="/DesktopModules/DDT_Org_Chart/images/MoveDown.gif" 
                              style="border:0px;vertical-align:middle;" /><%=LocalizeString("Down")%></asp:LinkButton><asp:LinkButton
                                  ID="DDT_Org_Chart_ItemlnkOrderUp" runat="server" CommandArgument='<%# Eval("ID_Org_Chart_Item") %>'
                                  CommandName="OrderUp" OnClick="DDT_Org_Chart_ItemlnkOrderUp_Click" Text="up"><img alt="" 
                              src="/DesktopModules/DDT_Org_Chart/images/MoveUp.gif" 
                              style="border:0px;vertical-align:middle;" /><%=LocalizeString("Up")%></asp:LinkButton></ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn DataField="CreatedByUserID" DataType="System.Int32" FilterControlAltText="Filter CreatedByUserID column"
                HeaderText="CreatedByUserID" SortExpression="CreatedByUserID" UniqueName="CreatedByUserID"
                Visible="False">
                <EditItemTemplate>
                    <asp:TextBox ID="CreatedByUserID_TB" runat="server" Text='<%# Bind("CreatedByUserID")%>'></asp:TextBox></EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="CreatedByUserIDLabel" runat="server" Text='<%# Eval("CreatedByUserID")%>'></asp:Label></ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn DataField="CreatedOnDate" DataType="System.DateTime"
                FilterControlAltText="Filter CreatedOnDate column" HeaderText="CreatedOnDate"
                SortExpression="CreatedOnDate" UniqueName="CreatedOnDate" Visible="false">
                <EditItemTemplate>
                    <asp:TextBox ID="CreatedOnDate_TB" runat="server" Text='<%# Bind("CreatedOnDate")%>'></asp:TextBox></EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="CreatedOnDateLabel" runat="server" Text='<%# Eval("CreatedOnDate")%>'></asp:Label></ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn DataField="LastModifiedByUserID" DataType="System.Int32"
                FilterControlAltText="Filter LastModifiedByUserID column" HeaderText="LastModifiedByUserID"
                SortExpression="LastModifiedByUserID" UniqueName="LastModifiedByUserID" Visible="false">
                <EditItemTemplate>
                    <asp:TextBox ID="LastModifiedByUserID_TB" runat="server" Text='<%# Bind("LastModifiedByUserID")%>'></asp:TextBox></EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="LastModifiedByUserIDLabel" runat="server" Text='<%# Eval("LastModifiedByUserID")%>'></asp:Label></ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn DataField="LastModifiedOnDate" DataType="System.DateTime"
                FilterControlAltText="Filter LastModifiedOnDate column" HeaderText="LastModifiedOnDate"
                SortExpression="LastModifiedOnDate" UniqueName="LastModifiedOnDate" Visible="false">
                <EditItemTemplate>
                    <asp:TextBox ID="LastModifiedOnDate_TB" runat="server" Text='<%# Bind("LastModifiedOnDate")%>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="LastModifiedOnDateLabel" runat="server" Text='<%# Eval("LastModifiedOnDate")%>'></asp:Label>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
    </Columns>
    </MasterTableView>
    <clientsettings><selecting allowrowselect="True" /></clientsettings>
    <EditItemStyle CssClass="rgEditRow"></EditItemStyle>
    <FooterStyle CssClass="rgFooter"></FooterStyle>
    <HeaderStyle CssClass="rgHeader"></HeaderStyle>
    <FilterItemStyle CssClass="rgFilterRow"></FilterItemStyle>
    <CommandItemStyle CssClass="rgCommandRow"></CommandItemStyle>
    <ActiveItemStyle CssClass="rgActiveRow"></ActiveItemStyle>
    <MultiHeaderItemStyle CssClass="rgMultiHeaderRow"></MultiHeaderItemStyle>
    <ItemStyle CssClass="rgRow"></ItemStyle>
    <PagerStyle CssClass="rgPager"></PagerStyle>
    <SelectedItemStyle CssClass="rgSelectedRow"></SelectedItemStyle>
    <FilterMenu EnableImageSprites="False">
    </FilterMenu>
</telerik:RadGrid>
</asp:PlaceHolder>
<asp:PlaceHolder ID="PHWithGroup" runat="server" Visible="False">
<h1><asp:Label ID="lblGroups" runat="server" Text="Groups" ResourceKey="lblGroups"></asp:Label></h1> 
<telerik:RadGrid ID="RG_Teams" runat="server" AllowAutomaticDeletes="True" AllowAutomaticInserts="True"
        AllowAutomaticUpdates="True" AllowMultiRowEdit="True" AllowMultiRowSelection="True"
        AutoGenerateColumns="False" CellSpacing="0" DataSourceID="LDS_Org_Chart_Nodes"
        GridLines="None" OnItemDataBound="RG_Teams_ItemDataBound" OnLoad="RG_Teams_Load" OnItemCreated="RG_Teams_ItemCreated">
        <AlternatingItemStyle CssClass="rgAltRow"></AlternatingItemStyle>
        <GroupHeaderItemStyle CssClass="rgGroupHeader"></GroupHeaderItemStyle>
        <MasterTableView AutoGenerateColumns="False" DataKeyNames="ID_Org_Chart_Node"
            DataSourceID="LDS_Org_Chart_Nodes" CommandItemDisplay="Top" EditMode="InPlace">
            <EditFormSettings>
                <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                </EditColumn>
            </EditFormSettings>
            <CommandItemTemplate>
                <div style="padding: 5px 5px;">
                    &#160;&#160;&#160;&#160;
                    <asp:LinkButton ID="btnEditSelected2" runat="server" CommandName="EditSelected" Visible="<%# RG_Teams.EditIndexes.Count == 0 %>"><img alt="" 
                            src="/DesktopModules/DDT_Org_Chart/images/Edit.gif" 
                            style="border:0px;vertical-align:middle;" /><%=LocalizeString("EditSelection") %></asp:LinkButton>&#160;&#160;
                    <asp:LinkButton ID="btnUpdateEdited2" runat="server" CommandName="UpdateEdited" Visible="<%# RG_Teams.EditIndexes.Count > 0 %>"><img alt="" 
                            src="/DesktopModules/DDT_Org_Chart/images/Update.gif" 
                            style="border:0px;vertical-align:middle;" /><%=LocalizeString("Update")%></asp:LinkButton>&#160;&#160;
                    <asp:LinkButton ID="btnCancel2" runat="server" CommandName="CancelAll" Visible="<%# RG_Teams.EditIndexes.Count > 0 || RG_Teams.MasterTableView.IsItemInserted %>"><img 
                            alt="" src="/DesktopModules/DDT_Org_Chart/images/Cancel.gif" 
                            style="border:0px;vertical-align:middle;" /><%=LocalizeString("Cancel")%></asp:LinkButton>&#160;&#160;
                    <asp:LinkButton ID="LinkButton13" runat="server" CommandName="InitInsert" Visible="<%# !RG_Teams.MasterTableView.IsItemInserted %>"><img 
                            alt="" src="/DesktopModules/DDT_Org_Chart/images/AddRecord.gif" 
                            style="border:0px;vertical-align:middle;" /><%=LocalizeString("AddRecord")%></asp:LinkButton>&#160;&#160;
                    <asp:LinkButton ID="LinkButton14" runat="server" CommandName="PerformInsert" Visible="<%# RG_Teams.MasterTableView.IsItemInserted %>"><img 
                            alt="" src="/DesktopModules/DDT_Org_Chart/images/Insert.gif" 
                            style="border:0px;vertical-align:middle;" /><%=LocalizeString("AddThisRecord")%></asp:LinkButton>&#160;&#160;
                    <%--Code pour Soft delete--%><asp:LinkButton ID="LinkButton15" runat="server" OnClientClick='<%#ConfirmLocalize("ConfirmSoftDelete")%>'
                        OnCommand="RG_Teams_SoftDeleteSelected"><img alt="" 
                            src="/DesktopModules/DDT_Org_Chart/images/Delete.gif" 
                            style="border:0px;vertical-align:middle;" /><%=LocalizeString("DeleteSelected")%></asp:LinkButton>&#160;&#160;
                    <asp:LinkButton ID="LinkButton16" runat="server" CommandName="RebindGrid"><img alt="" 
                            src="/DesktopModules/DDT_Org_Chart/images/Refresh.gif" 
                            style="border:0px;vertical-align:middle;" /><%=LocalizeString("Refresh")%></asp:LinkButton>&#160;&#160;
                    <%--code hard delete--%><asp:LinkButton ID="LinkButton35" runat="server" CommandName="DeleteSelected"
                        OnClientClick=<%# String.Format("javascript:return confirm('{0}')", LocalizeString("ConfirmHardDelete"))%>
                        Visible="<%# CheckBoxIsSuperUser.Checked %>"><img alt="" 
                            src="/DesktopModules/DDT_Org_Chart/images/Delete.gif" 
                            style="border:0px;vertical-align:middle;" /><%=LocalizeString("HardDelete")%></asp:LinkButton></div>
            </CommandItemTemplate>
            <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
            <RowIndicatorColumn>
                <HeaderStyle CssClass="rgResizeCol"></HeaderStyle>
                <ItemStyle CssClass="rgResizeCol"></ItemStyle>
            </RowIndicatorColumn>
            <ExpandCollapseColumn>
            </ExpandCollapseColumn>
            <Columns>
                <telerik:GridBoundColumn DataField="ID_Org_Chart_Node" DataType="System.Int32" FilterControlAltText="Filter ID_Org_Chart_Node column"
                    HeaderText="ID_Org_Chart_Node" ReadOnly="True" SortExpression="ID_Org_Chart_Node"
                    UniqueName="ID_Org_Chart_Node">
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn DataField="PortalID" DataType="System.Int32" FilterControlAltText="Filter PortalID column"
                    HeaderText="PortalID" SortExpression="PortalID" UniqueName="PortalID">
                    <EditItemTemplate>
                        <asp:TextBox ID="PortalIDTextBox" runat="server" Text='<%# Bind("PortalID")%>'></asp:TextBox></EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="PortalIDLabelTextBox" runat="server" Text='<%# Eval("PortalID")%>'></asp:Label></ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn DataField="ModuleID" DataType="System.Int32" FilterControlAltText="Filter ModuleID column"
                    HeaderText="ModuleID" SortExpression="ModuleID" UniqueName="ModuleID">
                    <EditItemTemplate>
                        <asp:TextBox ID="ModuleIDTextBox" runat="server" Text='<%# Bind("ModuleID")%>'></asp:TextBox></EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="ModuleIDLabelTextBox" runat="server" Text='<%# Eval("ModuleID")%>'></asp:Label></ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridDropDownColumn DataField="ParentID_Org_Chart_Node" FilterControlAltText="Filter ParentID_Org_Chart_Node column"
                    HeaderText="Parent Group" ListTextField="TeamName_Org_Chart_Node"
                    ListValueField="ID_Org_Chart_Node" SortExpression="TeamName_Org_Chart_Node" DataSourceID="LDS_ParentNodes_Select"
                    EnableEmptyListItem="true" HeaderStyle-Width="180px" UniqueName="ParentID_Org_Chart_Node">
                </telerik:GridDropDownColumn>
                <telerik:GridBoundColumn DataField="TeamName_Org_Chart_Node" FilterControlAltText="Filter TeamName_Org_Chart_Node column"
                    HeaderText="Group Name" SortExpression="TeamName_Org_Chart_Node"
                    UniqueName="TeamName_Org_Chart_Node" HeaderStyle-Width="180px">
                </telerik:GridBoundColumn>
                <telerik:GridCheckBoxColumn DataField="Collapsed" DataType="System.Boolean" FilterControlAltText="Filter Collapsed column"
                    HeaderText="Collapsed" SortExpression="Collapsed" UniqueName="Collapsed">
                </telerik:GridCheckBoxColumn>
                <telerik:GridCheckBoxColumn DataField="GroupCollapsed" DataType="System.Boolean"
                    FilterControlAltText="Filter GroupCollapsed column" HeaderText="GroupCollapsed"
                    SortExpression="Group Collapsed" UniqueName="GroupCollapsed">
                </telerik:GridCheckBoxColumn>
                <telerik:GridTemplateColumn DataField="ColumnCount" DataType="System.Int32" FilterControlAltText="Filter ColumnCount column"
                    HeaderText="Column Count" SortExpression="ColumnCount" UniqueName="ColumnCount">
                    <EditItemTemplate>
                        <asp:TextBox ID="ColumnCountTextBox" runat="server" Text='<%# Bind("ColumnCount")%>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="ColumnCountLabelTextBox" runat="server" Text='<%# Eval("ColumnCount")%>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn DataField="NodeOrder_Org_Chart" DataType="System.Int32"
                    FilterControlAltText="Filter NodeOrder_Org_Chart column" HeaderText="Order" SortExpression="NodeOrder_Org_Chart"
                    UniqueName="NodeOrder_Org_Chart">
                    <EditItemTemplate>
                        <asp:TextBox ID="OrderIDTextBox" runat="server" Text='<%# Bind("NodeOrder_Org_Chart")%>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="OrderIDLabelTextBox" runat="server" Text='<%# Eval("NodeOrder_Org_Chart")%>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridCheckBoxColumn DataField="IsActive" DataType="System.Boolean" DefaultInsertValue="True"
                    HeaderText="Is Active" SortExpression="IsActive" UniqueName="IsActive">
                </telerik:GridCheckBoxColumn>
                <telerik:GridCheckBoxColumn DataField="IsDeleted" DataType="System.Boolean" FilterControlAltText="Filter IsDeleted column"
                    HeaderText="Is Deleted" SortExpression="IsDeleted" UniqueName="IsDeleted">
                </telerik:GridCheckBoxColumn>
                <telerik:GridTemplateColumn HeaderText="Order" UniqueName="Order" HeaderStyle-Width="40px" >
                    <ItemTemplate>
                        <asp:LinkButton ID="DDT_Org_Chart_NodelnkOrderDown" runat="server" CommandArgument='<%# Eval("ID_Org_Chart_Node") %>'
                            CommandName="OrderDown" OnClick="DDT_Org_Chart_NodelnkOrderDown_Click" Text="down"><img alt="" 
                              src="/DesktopModules/DDT_Org_Chart/images/MoveDown.gif" 
                              style="border:0px;vertical-align:middle;" /><%=LocalizeString("Down")%></asp:LinkButton><asp:LinkButton
                                  ID="DDT_Org_Chart_NodelnkOrderUp" runat="server" CommandArgument='<%# Eval("ID_Org_Chart_Node") %>'
                                  CommandName="OrderUp" OnClick="DDT_Org_Chart_NodelnkOrderUp_Click" Text="up"><img alt="" 
                              src="/DesktopModules/DDT_Org_Chart/images/MoveUp.gif" 
                              style="border:0px;vertical-align:middle;" /><%=LocalizeString("Up")%></asp:LinkButton></ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn DataField="CreatedByUserID" DataType="System.Int32" FilterControlAltText="Filter CreatedByUserID column"
                    HeaderText="CreatedByUserID" SortExpression="CreatedByUserID" UniqueName="CreatedByUserID"
                    Visible="False">
                    <EditItemTemplate>
                        <asp:TextBox ID="CreatedByUserID_TB" runat="server" Text='<%# Bind("CreatedByUserID")%>'></asp:TextBox></EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="CreatedByUserIDLabel" runat="server" Text='<%# Eval("CreatedByUserID")%>'></asp:Label></ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn DataField="CreatedOnDate" DataType="System.DateTime"
                    FilterControlAltText="Filter CreatedOnDate column" HeaderText="CreatedOnDate"
                    SortExpression="CreatedOnDate" UniqueName="CreatedOnDate" Visible="false">
                    <EditItemTemplate>
                        <asp:TextBox ID="CreatedOnDate_TB" runat="server" Text='<%# Bind("CreatedOnDate")%>'></asp:TextBox></EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="CreatedOnDateLabel" runat="server" Text='<%# Eval("CreatedOnDate")%>'></asp:Label></ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn DataField="LastModifiedByUserID" DataType="System.Int32"
                    FilterControlAltText="Filter LastModifiedByUserID column" HeaderText="LastModifiedByUserID"
                    SortExpression="LastModifiedByUserID" UniqueName="LastModifiedByUserID" Visible="false">
                    <EditItemTemplate>
                        <asp:TextBox ID="LastModifiedByUserID_TB" runat="server" Text='<%# Bind("LastModifiedByUserID")%>'></asp:TextBox></EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="LastModifiedByUserIDLabel" runat="server" Text='<%# Eval("LastModifiedByUserID")%>'></asp:Label></ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn DataField="LastModifiedOnDate" DataType="System.DateTime"
                    FilterControlAltText="Filter LastModifiedOnDate column" HeaderText="LastModifiedOnDate"
                    SortExpression="LastModifiedOnDate" UniqueName="LastModifiedOnDate" Visible="false">
                    <EditItemTemplate>
                        <asp:TextBox ID="LastModifiedOnDate_TB" runat="server" Text='<%# Bind("LastModifiedOnDate")%>'></asp:TextBox></EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="LastModifiedOnDateLabel" runat="server" Text='<%# Eval("LastModifiedOnDate")%>'></asp:Label></ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
        <clientsettings><selecting allowrowselect="True" /></clientsettings>
        <EditItemStyle CssClass="rgEditRow"></EditItemStyle>
        <FooterStyle CssClass="rgFooter"></FooterStyle>
        <HeaderStyle CssClass="rgHeader"></HeaderStyle>
        <FilterItemStyle CssClass="rgFilterRow"></FilterItemStyle>
        <CommandItemStyle CssClass="rgCommandRow"></CommandItemStyle>
        <ActiveItemStyle CssClass="rgActiveRow"></ActiveItemStyle>
        <MultiHeaderItemStyle CssClass="rgMultiHeaderRow"></MultiHeaderItemStyle>
        <ItemStyle CssClass="rgRow"></ItemStyle>
        <PagerStyle CssClass="rgPager"></PagerStyle>
        <SelectedItemStyle CssClass="rgSelectedRow"></SelectedItemStyle>
        <FilterMenu EnableImageSprites="False">
        </FilterMenu>
    </telerik:RadGrid>

<%--Edit WIth Group Contols--%>

    <h1><asp:Label ID="lblItems2" runat="server" Text="Items" ResourceKey="lblItems"></asp:Label></h1>  
    <telerik:RadGrid ID="RG_Items_WithGroup" runat="server" AllowAutomaticDeletes="True"
    AllowAutomaticInserts="True" AllowAutomaticUpdates="True" AllowMultiRowEdit="True" AllowMultiRowSelection="True"
    AutoGenerateColumns="False" CellSpacing="0" DataSourceID="LDS_Org_Chart_Items"
    GridLines="None" OnItemDataBound="RG_Items_WithGroup_ItemDataBound" OnLoad="RG_Items_WithGroup_Load" OnItemCreated="RG_Items_WithGroup_ItemCreated">
    <AlternatingItemStyle CssClass="rgAltRow"></AlternatingItemStyle>
    <GroupHeaderItemStyle CssClass="rgGroupHeader"></GroupHeaderItemStyle>
    <MasterTableView AutoGenerateColumns="False" DataKeyNames="ID_Org_Chart_Item" DataSourceID="LDS_Org_Chart_Items"
        CommandItemDisplay="TopAndBottom" EditMode="InPlace">
        <EditFormSettings>
            <EditColumn FilterControlAltText="Filter EditCommandColumn column">
            </EditColumn>
        </EditFormSettings>
        <CommandItemTemplate>
            <div style="padding: 5px 5px;">
                &#160;&#160;&#160;&#160;
                <asp:LinkButton ID="btnEditSelected12" runat="server" CommandName="EditSelected" Visible="<%# RG_Items_WithGroup.EditIndexes.Count == 0 %>"><img alt="" 
                            src="/DesktopModules/DDT_Org_Chart/images/Edit.gif" 
                            style="border:0px;vertical-align:middle;" /><%=LocalizeString("EditSelection") %></asp:LinkButton>&#160;&#160;
                <asp:LinkButton ID="btnUpdateEdited12" runat="server" CommandName="UpdateEdited" Visible="<%# RG_Items_WithGroup.EditIndexes.Count > 0 %>"><img alt="" 
                            src="/DesktopModules/DDT_Org_Chart/images/Update.gif" 
                            style="border:0px;vertical-align:middle;" /><%=LocalizeString("Update")%></asp:LinkButton>&#160;&#160;
                <asp:LinkButton ID="btnCancel12" runat="server" CommandName="CancelAll" Visible="<%# RG_Items_WithGroup.EditIndexes.Count > 0 || RG_Teams.MasterTableView.IsItemInserted %>"><img 
                            alt="" src="/DesktopModules/DDT_Org_Chart/images/Cancel.gif" 
                            style="border:0px;vertical-align:middle;" /><%=LocalizeString("Cancel")%></asp:LinkButton>&#160;&#160;
                <asp:LinkButton ID="LinkButton23" runat="server" CommandName="InitInsert" Visible="<%# !RG_Items_WithGroup.MasterTableView.IsItemInserted %>"><img 
                            alt="" src="/DesktopModules/DDT_Org_Chart/images/AddRecord.gif" 
                            style="border:0px;vertical-align:middle;" /><%=LocalizeString("AddRecord")%></asp:LinkButton>&#160;&#160;
                <asp:LinkButton ID="LinkButton24" runat="server" CommandName="PerformInsert" Visible="<%# RG_Items_WithGroup.MasterTableView.IsItemInserted %>"><img 
                            alt="" src="/DesktopModules/DDT_Org_Chart/images/Insert.gif" 
                            style="border:0px;vertical-align:middle;" /><%=LocalizeString("AddThisRecord")%></asp:LinkButton>&#160;&#160;
                <%--Code pour Soft delete--%><asp:LinkButton ID="LinkButton15" runat="server" OnClientClick='<%#ConfirmLocalize("ConfirmSoftDelete")%>'
                    OnCommand="RG_Items_WithGroup_SoftDeleteSelected"><img alt="" 
                            src="/DesktopModules/DDT_Org_Chart/images/Delete.gif" 
                            style="border:0px;vertical-align:middle;" /><%=LocalizeString("DeleteSelected")%></asp:LinkButton>&#160;&#160;
                <asp:LinkButton ID="LinkButton26" runat="server" CommandName="RebindGrid"><img alt="" 
                            src="/DesktopModules/DDT_Org_Chart/images/Refresh.gif" 
                            style="border:0px;vertical-align:middle;" /><%=LocalizeString("Refresh")%></asp:LinkButton>&#160;&#160;
                <%--code hard delete--%><asp:LinkButton ID="LinkButton36" runat="server" CommandName="DeleteSelected"
                    OnClientClick=<%# String.Format("javascript:return confirm('{0}')", LocalizeString("ConfirmHardDelete"))%>
                    Visible="<%# CheckBoxIsSuperUser.Checked %>"><img alt="" 
                            src="/DesktopModules/DDT_Org_Chart/images/Delete.gif" 
                            style="border:0px;vertical-align:middle;" /><%=LocalizeString("HardDelete")%></asp:LinkButton></div>
        </CommandItemTemplate>
        <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
            <RowIndicatorColumn>
                <HeaderStyle CssClass="rgResizeCol"></HeaderStyle>
                <ItemStyle CssClass="rgResizeCol"></ItemStyle>
            </RowIndicatorColumn>
        <ExpandCollapseColumn>
        </ExpandCollapseColumn>
        <Columns>
            <telerik:GridBoundColumn DataField="ID_Org_Chart_Item" DataType="System.Int32" FilterControlAltText="Filter ID_Org_Chart_Item column"
                HeaderText="ID_Org_Chart_Item" ReadOnly="True" SortExpression="ID_Org_Chart_Item"
                UniqueName="ID_Org_Chart_Item">
            </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn DataField="PortalID" DataType="System.Int32" FilterControlAltText="Filter PortalID column"
                    HeaderText="PortalID" SortExpression="PortalID" UniqueName="PortalID">
                    <EditItemTemplate>
                        <asp:TextBox ID="PortalIDTextBox" runat="server" Text='<%# Bind("PortalID")%>'></asp:TextBox></EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="PortalIDLabelTextBox" runat="server" Text='<%# Eval("PortalID")%>'></asp:Label></ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn DataField="ModuleID" DataType="System.Int32" FilterControlAltText="Filter ModuleID column"
                    HeaderText="ModuleID" SortExpression="ModuleID" UniqueName="ModuleID">
                    <EditItemTemplate>
                        <asp:TextBox ID="ModuleIDTextBox" runat="server" Text='<%# Bind("ModuleID")%>'></asp:TextBox></EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="ModuleIDLabelTextBox" runat="server" Text='<%# Eval("ModuleID")%>'></asp:Label></ItemTemplate>
                </telerik:GridTemplateColumn> 

<%--           <telerik:GridBoundColumn DataField="ID_Org_Chart_Node" FilterControlAltText="Filter ID_Org_Chart_node column"
                HeaderText="ID_Org_Chart_Node" SortExpression="ID_Org_Chart_Node" UniqueName="ID_Org_Chart_Node"
                DataType="System.Int32">
            </telerik:GridBoundColumn>
--%>           
               <telerik:GridDropDownColumn DataField="ID_Org_Chart_Node" FilterControlAltText="Filter ParentID_Org_Chart_Node column"
                    HeaderText="Group" ListTextField="TeamName_Org_Chart_Node"
                    ListValueField="ID_Org_Chart_Node" SortExpression="TeamName_Org_Chart_Node" DataSourceID="LDS_ParentNodes_Select"
                    EnableEmptyListItem="true"  HeaderStyle-Width="180px" UniqueName="TeamName_Org_Chart_Node">
               </telerik:GridDropDownColumn>
<%--            <telerik:GridDropDownColumn DataField="ID_Org_Chart_Item" FilterControlAltText="Filter ID_Org_Chart_Item column"
                    HeaderText="ID_Org_Chart_Item" ListTextField="ItemName_Org_Chart"
                    ListValueField="ID_Org_Chart_Item" SortExpression="ItemOrder_Org_Chart" DataSourceID="LDS_ParentItems_Select"
                    EnableEmptyListItem="true">
                </telerik:GridDropDownColumn>
--%>
            <telerik:GridBoundColumn DataField="ItemName_Org_Chart" FilterControlAltText="Filter ItemName_Org_Chart column"
                HeaderText="Item Name" SortExpression="ItemName_Org_Chart" UniqueName="ItemName_Org_Chart" HeaderStyle-Width="180px">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn ColumnEditorID="ItemTitle_Org_Chart_EditBox_Style" DataField="ItemTitle_Org_Chart" FilterControlAltText="Filter ItemTitle_Org_Chart column"
                HeaderText="Item Title" SortExpression="ItemTitle_Org_Chart" UniqueName="ItemTitle_Org_Chart"  HeaderStyle-Width="180px">
            </telerik:GridBoundColumn>        
<%--        <telerik:GridBoundColumn DataField="ItemImageUrl_Org_Chart" FilterControlAltText="Filter ItemImageUrl_Org_Chart column"
                HeaderText="ItemImageUrl_Org_Chart" SortExpression="ItemImageUrl_Org_Chart" UniqueName="ItemImageUrl_Org_Chart">
            </telerik:GridBoundColumn>--%>
            <telerik:GridTemplateColumn DataField="ItemImageUrl_Org_Chart" FilterControlAltText="Filter ItemImageUrl_Org_Chart column" HeaderText="Item Image Url" SortExpression="ItemImageUrl_Org_Chart"
                    UniqueName="ItemImageUrl_Org_Chart" HeaderStyle-Width="280px">
                    <EditItemTemplate>
                    <dnn:DnnFilePicker runat="server" ID="FilePickerSimple" FilePath='<%# Bind("ItemImageUrl_Org_Chart")%>'
                        FileFilter="jpg,png,gif" />
<%--                <dnn:DnnFilePickerUploader runat="server" ID="FilePickerSimple" FilePath='<%# Bind("ItemImageUrl_Org_Chart")%>'
                        FileFilter="jpg,png,gif" /> --%>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <img alt="<%# Eval("ItemImageUrl_Org_Chart")%>" src="/Portals/<%# Eval("PortalId") %>/<%# Eval("ItemImageUrl_Org_Chart")%>" />
                        <asp:LinkButton ID="DDT_Org_Chart_ItemDeleteImage2" runat="server" CommandArgument='<%# Eval("ID_Org_Chart_Item") %>'
                        CommandName="DeleteItemImage" OnClick="DDT_Org_Chart_ItemDeleteImage_Click" Text="delete"><img alt="<%=LocalizeString("Delete")%>" 
                              src="/DesktopModules/DDT_Org_Chart/images/Delete.gif" 
                              style="border:0px;vertical-align:middle;" /></asp:LinkButton>
                    </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn DataField="ItemOrder_Org_Chart" DataType="System.Int32"
                    FilterControlAltText="Filter ItemOrder_Org_Chart column" HeaderText="Order" SortExpression="ItemOrder_Org_Chart"
                    UniqueName="ItemOrder_Org_Chart">
                    <EditItemTemplate>
                        <asp:TextBox ID="OrderIDTextBox" runat="server" Text='<%# Bind("ItemOrder_Org_Chart")%>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="OrderIDLabelTextBox" runat="server" Text='<%# Eval("ItemOrder_Org_Chart")%>'></asp:Label>                 
                    </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridCheckBoxColumn DataField="Collapsed" DataType="System.Boolean" FilterControlAltText="Filter Collapsed column"
                HeaderText="Collapsed" SortExpression="Collapsed" UniqueName="Collapsed" Visible="false">
            </telerik:GridCheckBoxColumn>
                <telerik:GridCheckBoxColumn DataField="IsActive" DataType="System.Boolean" DefaultInsertValue="True"
                    HeaderText="Is Active" SortExpression="IsActive" UniqueName="IsActive">
                </telerik:GridCheckBoxColumn>
                <telerik:GridCheckBoxColumn DataField="IsDeleted" DataType="System.Boolean" FilterControlAltText="Filter IsDeleted column"
                    HeaderText="Is Deleted" SortExpression="IsDeleted" UniqueName="IsDeleted">
                </telerik:GridCheckBoxColumn>
            <telerik:GridTemplateColumn HeaderText="Order" UniqueName="Order" HeaderStyle-Width = "10px">
                <ItemTemplate>
                    <asp:LinkButton ID="DDT_Org_Chart_Item2lnkOrderDown" runat="server" CommandArgument='<%# Eval("ID_Org_Chart_Item") %>'
                        CommandName="OrderDown" OnClick="DDT_Org_Chart_Item2lnkOrderDown_Click" Text="down"><img alt="" 
                              src="/DesktopModules/DDT_Org_Chart/images/MoveDown.gif" 
                              style="border:0px;vertical-align:middle;" /><%=LocalizeString("Down")%></asp:LinkButton><asp:LinkButton
                                  ID="DDT_Org_Chart_Item2lnkOrderUp" runat="server" CommandArgument='<%# Eval("ID_Org_Chart_Item") %>'
                                  CommandName="OrderUp" OnClick="DDT_Org_Chart_Item2lnkOrderUp_Click" Text="up"><img alt="" 
                              src="/DesktopModules/DDT_Org_Chart/images/MoveUp.gif" 
                              style="border:0px;vertical-align:middle;" /><%=LocalizeString("Up")%></asp:LinkButton></ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn DataField="CreatedByUserID" DataType="System.Int32" FilterControlAltText="Filter CreatedByUserID column"
                HeaderText="CreatedByUserID" SortExpression="CreatedByUserID" UniqueName="CreatedByUserID"
                Visible="False">
                <EditItemTemplate>
                    <asp:TextBox ID="CreatedByUserID_TB" runat="server" Text='<%# Bind("CreatedByUserID")%>'></asp:TextBox></EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="CreatedByUserIDLabel" runat="server" Text='<%# Eval("CreatedByUserID")%>'></asp:Label></ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn DataField="CreatedOnDate" DataType="System.DateTime"
                FilterControlAltText="Filter CreatedOnDate column" HeaderText="CreatedOnDate"
                SortExpression="CreatedOnDate" UniqueName="CreatedOnDate" Visible="false">
                <EditItemTemplate>
                    <asp:TextBox ID="CreatedOnDate_TB" runat="server" Text='<%# Bind("CreatedOnDate")%>'></asp:TextBox></EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="CreatedOnDateLabel" runat="server" Text='<%# Eval("CreatedOnDate")%>'></asp:Label></ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn DataField="LastModifiedByUserID" DataType="System.Int32"
                FilterControlAltText="Filter LastModifiedByUserID column" HeaderText="LastModifiedByUserID"
                SortExpression="LastModifiedByUserID" UniqueName="LastModifiedByUserID" Visible="false">
                <EditItemTemplate>
                    <asp:TextBox ID="LastModifiedByUserID_TB" runat="server" Text='<%# Bind("LastModifiedByUserID")%>'></asp:TextBox></EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="LastModifiedByUserIDLabel" runat="server" Text='<%# Eval("LastModifiedByUserID")%>'></asp:Label></ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn DataField="LastModifiedOnDate" DataType="System.DateTime"
                FilterControlAltText="Filter LastModifiedOnDate column" HeaderText="LastModifiedOnDate"
                SortExpression="LastModifiedOnDate" UniqueName="LastModifiedOnDate" Visible="false">
                <EditItemTemplate>
                    <asp:TextBox ID="LastModifiedOnDate_TB" runat="server" Text='<%# Bind("LastModifiedOnDate")%>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="LastModifiedOnDateLabel" runat="server" Text='<%# Eval("LastModifiedOnDate")%>'></asp:Label>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
    </Columns>
    </MasterTableView>
    <clientsettings><selecting allowrowselect="True" /></clientsettings>
    <EditItemStyle CssClass="rgEditRow"></EditItemStyle>
    <FooterStyle CssClass="rgFooter"></FooterStyle>
    <HeaderStyle CssClass="rgHeader"></HeaderStyle>
    <FilterItemStyle CssClass="rgFilterRow"></FilterItemStyle>
    <CommandItemStyle CssClass="rgCommandRow"></CommandItemStyle>
    <ActiveItemStyle CssClass="rgActiveRow"></ActiveItemStyle>
    <MultiHeaderItemStyle CssClass="rgMultiHeaderRow"></MultiHeaderItemStyle>
    <ItemStyle CssClass="rgRow"></ItemStyle>
    <PagerStyle CssClass="rgPager"></PagerStyle>
    <SelectedItemStyle CssClass="rgSelectedRow"></SelectedItemStyle>
    <FilterMenu EnableImageSprites="False">
    </FilterMenu>
</telerik:RadGrid>

<%-- Set ItemText Box Size
    <telerik:GridTextBoxColumnEditor ID="ItemTitle_Org_Chart_EditBox_Style" runat="server">
                 <TextBoxStyle Height="200px"/>
    </telerik:GridTextBoxColumnEditor>--%>

</asp:PlaceHolder>
<asp:LinqDataSource ID="LDS_Org_Chart_Nodes" runat="server" ContextTypeName="DevPCI.Modules.DDT_Org_Chart.DDT_Org_Chart_LinqDataContext"
    EnableDelete="True" EnableInsert="True" EnableUpdate="True" TableName="DDT_Org_Chart_Nodes"
    Where="PortalID == @PortalID &amp;&amp; ModuleID == @ModuleID" OrderBy="NodeOrder_Org_Chart">
    <WhereParameters>
        <asp:ControlParameter ControlID="lblPortalID" Name="PortalID" PropertyName="Text"
            Type="Int32" />
        <asp:ControlParameter ControlID="lblModuleID" Name="ModuleID" PropertyName="Text"
            Type="Int32" />
    </WhereParameters>
</asp:LinqDataSource>
<asp:LinqDataSource ID="LDS_ParentNodes_Select" runat="server" ContextTypeName="DevPCI.Modules.DDT_Org_Chart.DDT_Org_Chart_LinqDataContext"
    EnableDelete="True" EnableInsert="True" EnableUpdate="True" OrderBy="NodeOrder_Org_Chart"
    TableName="DDT_Org_Chart_Nodes" 
    Where="PortalID == @PortalID &amp;&amp; ModuleID == @ModuleID">
    <WhereParameters>
        <asp:ControlParameter ControlID="lblPortalID" DefaultValue="2" Name="PortalID" PropertyName="Text"
            Type="Int32" />
        <asp:ControlParameter ControlID="lblModuleID" DefaultValue="2" Name="ModuleID" PropertyName="Text"
            Type="Int32" />
    </WhereParameters>
</asp:LinqDataSource>
<asp:LinqDataSource ID="LDS_ParentItems_Select" runat="server" ContextTypeName="DevPCI.Modules.DDT_Org_Chart.DDT_Org_Chart_LinqDataContext"
    EnableDelete="True" EnableInsert="True" EnableUpdate="True" OrderBy="ItemOrder_Org_Chart"
    TableName="DDT_Org_Chart_Items" 
    Where="PortalID == @PortalID &amp;&amp; ModuleID == @ModuleID">
    <WhereParameters>
        <asp:ControlParameter ControlID="lblPortalID" DefaultValue="2" Name="PortalID" PropertyName="Text"
            Type="Int32" />
        <asp:ControlParameter ControlID="lblModuleID" DefaultValue="2" Name="ModuleID" PropertyName="Text"
            Type="Int32" />
    </WhereParameters>
</asp:LinqDataSource>
<asp:LinqDataSource ID="LDS_Org_Chart_Items" runat="server" ContextTypeName="DevPCI.Modules.DDT_Org_Chart.DDT_Org_Chart_LinqDataContext"
    EnableDelete="True" EnableInsert="True" EnableUpdate="True" TableName="DDT_Org_Chart_Items"
    Where="PortalID == @PortalID &amp;&amp; ModuleID == @ModuleID" 
    OrderBy="ItemOrder_Org_Chart" >
    <WhereParameters>
        <asp:ControlParameter ControlID="lblPortalID" DefaultValue="2" Name="PortalID" PropertyName="Text"
            Type="Int32" />
        <asp:ControlParameter ControlID="lblModuleID" DefaultValue="2" Name="ModuleID" PropertyName="Text"
            Type="Int32" />
    </WhereParameters>
</asp:LinqDataSource>
<br />
<asp:HyperLink id="hlCancel" runat="server" class="dnnPrimaryAction" resourcekey="cmdExit" />


<asp:Label ID="lblPortalID" runat="server" Text="1" Visible="false"></asp:Label>
<asp:Label ID="lblModuleID" runat="server" Text="2" Visible="false"></asp:Label>
<asp:CheckBox ID="CheckBoxIsSuperUser" runat="server" Visible="false" />

<%--<%=LocalizeString("ConfirmSoftDelete")%>--%>
<%--<%= GetBuildImageUrl("ItemImageUrl_Org_Chart")%>--%>