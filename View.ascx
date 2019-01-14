<%@ Control Language="C#" Inherits="DevPCI.Modules.DDT_Org_Chart.View" AutoEventWireup="false"  CodeBehind="View.ascx.cs" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="dnn" Assembly="DotNetNuke" Namespace="DotNetNuke.UI.WebControls" %>
<%@ Import Namespace="DotNetNuke.Services.Localization" %>
<script type="text/javascript">
        var T$ = $telerik.$;
        function expandAllNodes() {
            T$(".rocExpandArrow").click();
        }

        function collapseAllNodes() {
            T$(".rocCollapseArrow").click();
        }

        function expandAllGroups() {
            T$(".rocExpandGroupArrow").click();
        }

        function collapseAllGroups() {
            T$(".rocCollapseGroupArrow").click();
        }

</script>
<asp:PlaceHolder ID="phInit" runat="server" Visible="false">
<b><%=LocalizeString("InitInfo")%></b>
</asp:PlaceHolder>
<asp:PlaceHolder ID="phReductSize25" runat="server" Visible="false">
<style type="text/css">
/* Reduce size by 25 % */
html .RadOrgChart .rocLineUp
{
    height:10px ;
    top: -10px;
}

html .RadOrgChart .rocLineDown
{
    height:10px;

}

html .RadOrgChart .rocLineHorizontal
{
    top: -10px;
}

html .RadOrgChart .rocNodeList
{
    margin: 20px auto 0;
}

.rocItem
{
    width:150px!important;
    height:70px!important;
}

.rocItemContent
{
    height:50px!important;
    font-size: 10px;
}
</style>
</asp:PlaceHolder>
<div id="DDT_Org_Chart_Div" runat="server">
    <asp:Label ID="LblMode" runat="server" Text="Label" Visible="false"></asp:Label>
    <asp:PlaceHolder ID="PHSimple" runat="server" Visible="false">
        <%--    <p>Simple</p>--%>
            <telerik:RadOrgChart ID="DDT_Org_Chart_Simple" runat="server" 
            EnableDragAndDrop="true" onnodedrop="DDT_Org_Chart_Simple_NodeDrop">
                <RenderedFields>
                    <NodeFields>
                        <telerik:OrgChartRenderedField DataField="ItemName_Org_Chart_Node" />
                    </NodeFields>
                    <ItemFields>
                        <telerik:OrgChartRenderedField DataField="ItemTitle_Org_Chart" />
                    </ItemFields>
                </RenderedFields>
            </telerik:RadOrgChart>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="PHWithGroup" runat="server" Visible="false">
    <%--    <p>With Team</p> --%>    
    <telerik:RadOrgChart ID="DDT_Org_Chart_WithGroup" runat="server" 
        Skin="Default" ongroupitemdrop="DDT_Org_Chart_WithGroup_GroupItemDrop" 
        onnodedrop="DDT_Org_Chart_WithGroup_NodeDrop" >
            <RenderedFields>
                <NodeFields>
                    <telerik:OrgChartRenderedField DataField="TeamName_Org_Chart_Node" />
                </NodeFields>
                <ItemFields>
                    <telerik:OrgChartRenderedField DataField="ItemTitle_Org_Chart" />
                </ItemFields>
            </RenderedFields>
        </telerik:RadOrgChart>
    </asp:PlaceHolder>
</div>

<asp:LinqDataSource ID="LDS_Org_Chart_Items_Simple" runat="server" ContextTypeName="DevPCI.Modules.DDT_Org_Chart.DDT_Org_Chart_LinqDataContext"
    EnableDelete="True" EnableInsert="True" EnableUpdate="True" 
    TableName="DDT_Org_Chart_Items" OrderBy="ItemOrder_Org_Chart"
    Where="IsActive == @IsActive &amp;&amp; IsDeleted == @IsDeleted  &amp;&amp; PortalID == @PortalID &amp;&amp; ModuleID == @ModuleID" 
    onselecting="LDS_Org_Chart_Items_Simple_Selecting">
    <WhereParameters>
        <asp:Parameter DefaultValue="true" Name="IsActive" Type="Boolean" />
        <asp:Parameter DefaultValue="false" Name="IsDeleted" Type="Boolean" />
        <asp:ControlParameter ControlID="lblPortalID" DefaultValue="2" Name="PortalID" PropertyName="Text"
            Type="Int32" />
        <asp:ControlParameter ControlID="lblModuleID" DefaultValue="2" Name="ModuleID" PropertyName="Text"
            Type="Int32" />
    </WhereParameters>
</asp:LinqDataSource>
<asp:LinqDataSource ID="LDS_Org_Chart_Nodes" runat="server" ContextTypeName="DevPCI.Modules.DDT_Org_Chart.DDT_Org_Chart_LinqDataContext"
    EnableDelete="True" EnableInsert="True" EnableUpdate="True" TableName="DDT_Org_Chart_Nodes"
    OrderBy="NodeOrder_Org_Chart" Where="IsActive == @IsActive &amp;&amp; IsDeleted == @IsDeleted &amp;&amp; PortalID == @PortalID &amp;&amp; ModuleID == @ModuleID">
    <WhereParameters>
        <asp:Parameter DefaultValue="true" Name="IsActive" Type="Boolean" />
        <asp:Parameter DefaultValue="false" Name="IsDeleted" Type="Boolean" />
        <asp:ControlParameter ControlID="lblPortalID" DefaultValue="2" Name="PortalID" PropertyName="Text"
            Type="Int32" />
        <asp:ControlParameter ControlID="lblModuleID" DefaultValue="2" Name="ModuleID" PropertyName="Text"
            Type="Int32" />
    </WhereParameters>
</asp:LinqDataSource>
<asp:LinqDataSource ID="LDS_Org_Chart_Items_WithGroup" runat="server" ContextTypeName="DevPCI.Modules.DDT_Org_Chart.DDT_Org_Chart_LinqDataContext"
    EnableDelete="True" EnableInsert="True" EnableUpdate="True" 
    TableName="DDT_Org_Chart_Items" OrderBy="ItemOrder_Org_Chart"
    Where="ID_Org_Chart_Node &gt; @ID_Org_Chart_Node &amp;&amp; IsActive == @IsActive &amp;&amp; IsDeleted == @IsDeleted &amp;&amp; PortalID == @PortalID &amp;&amp; ModuleID == @ModuleID" 
    onselecting="LDS_Org_Chart_Items_WithGroup_Selecting">
    <WhereParameters>
        <asp:Parameter DefaultValue="0" Name="ID_Org_Chart_Node" Type="Int32" />
        <asp:Parameter DefaultValue="true" Name="IsActive" Type="Boolean" />
        <asp:Parameter DefaultValue="false" Name="IsDeleted" Type="Boolean" />
        <asp:ControlParameter ControlID="lblPortalID" DefaultValue="2" Name="PortalID" PropertyName="Text"
            Type="Int32" />
        <asp:ControlParameter ControlID="lblModuleID" DefaultValue="2" Name="ModuleID" PropertyName="Text"
            Type="Int32" />
    </WhereParameters>
</asp:LinqDataSource>
<asp:Label ID="lblPortalID" runat="server" Text="1" Visible="false"></asp:Label>
<asp:Label ID="lblModuleID" runat="server" Text="2" Visible="false"></asp:Label>
<asp:Label ID="lblLoadOnDemand" runat="server" Text="2" Visible="false"></asp:Label>
<div id="Org_Chart_Buttons" style="text-align:center; margin-top:10px;">
<asp:PlaceHolder ID="phShowExpandCollapseNodeButton" runat="server" Visible="False">
        <asp:LinkButton ID="LinkButton1" runat="server" onclientclick="expandAllNodes()" CssClass="dnnPrimaryAction" Text="Expand all nodes" ResourceKey="Expandallnodes" ></asp:LinkButton>&nbsp;
        <asp:LinkButton ID="LinkButton4" runat="server" onclientclick="collapseAllNodes()" CssClass="dnnPrimaryAction" Text="Collapse all nodes" ResourceKey="Collapseallnodes"></asp:LinkButton>
</asp:PlaceHolder>
<asp:PlaceHolder ID="phShowExpandCollapseGroupButton" runat="server" 
        Visible="False">
        <asp:LinkButton ID="LinkButton2" runat="server" onclientclick="expandAllGroups()" CssClass="dnnPrimaryAction" Text="Expand all groups" ResourceKey="Expandallgroups"></asp:LinkButton>&nbsp;
        <asp:LinkButton ID="LinkButton3" runat="server" onclientclick="collapseAllGroups()" CssClass="dnnPrimaryAction" Text="Collapse all groups" ResourceKey="Collapseallgroups"></asp:LinkButton>
</asp:PlaceHolder>
</div>