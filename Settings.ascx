<%@ Control Language="C#" AutoEventWireup="false" Inherits="DevPCI.Modules.DDT_Org_Chart.Settings" Codebehind="Settings.ascx.cs" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/labelcontrol.ascx" %>
<style type="text/css"> 
.dnnFormItem textarea {width:50px;}
.dnnFormItem input[type="text"], .dnnFormItem textarea {
    min-width: 50px;}
.dnnFormMessage {
    display: block;
}

</style>  
<div class="dnnForm">
    <asp:Label ID="LblIntro" runat="server" CssClass="dnnFormMessage dnnFormInfo" ResourceKey="Intro" />
    <fieldset>
    <div class="dnnFormItem">
            <dnn:Label runat="server" ControlName="ModeRadioButtons" ResourceKey="Mode" Text="Mode" suffix=":"/>
            <asp:RadioButtonList runat="server" ID="rbMode"
                RepeatDirection="Horizontal" CssClass="dnnFormRadioButtons" 
                onselectedindexchanged="rbMode_SelectedIndexChanged" AutoPostBack="True" CausesValidation="true" ValidationGroup="Group1">
                <asp:ListItem Text="Simple" Value="Simple" Selected="True" ResourceKey="Simple"/>
                <asp:ListItem Text="With Group" Value="WithGroup" ResourceKey="WithGroup"/>
            </asp:RadioButtonList>
            <script type="text/javascript">
                function ConfirmChangeMode(source, arguments) {
                    arguments.IsValid = confirm("Are you sure?");
                }
        </script>
        <asp:CustomValidator ID="ConfirmChangeModeValidator" runat="server"
            ClientValidationFunction="ConfirmChangeMode" Display="Dynamic" ValidationGroup="Group1"  />
    </div>
<div class="dnnFormItem">
<div class="dnnFormItem">
    <dnn:Label runat="server" ControlName="SkinDropDownList" ResourceKey="Skin" Text="Skin" suffix=":"/>
    <asp:DropDownList ID="ddlSkin" runat="server">
        <asp:ListItem>Black</asp:ListItem>
        <asp:ListItem>BlackMetroTouch</asp:ListItem>
        <asp:ListItem Selected="True">Default</asp:ListItem>
        <asp:ListItem>Glow</asp:ListItem>
        <asp:ListItem>Metro</asp:ListItem>
        <asp:ListItem>MetroTouch</asp:ListItem>
        <asp:ListItem>Office2007</asp:ListItem>
        <asp:ListItem>Office2010Black</asp:ListItem>
        <asp:ListItem>Office2010Blue</asp:ListItem>
        <asp:ListItem>Office2010Silver</asp:ListItem>
        <asp:ListItem>Outlook</asp:ListItem>
        <asp:ListItem>Silk</asp:ListItem>
        <asp:ListItem>Telerik</asp:ListItem>        
        <asp:ListItem>Vista</asp:ListItem>
        <asp:ListItem>Web20</asp:ListItem>
        <asp:ListItem>WebBlue</asp:ListItem>
        <asp:ListItem>Windows7</asp:ListItem>
    </asp:DropDownList>
</div>
<div class="dnnFormItem">
        <dnn:Label runat="server" ControlName="EnableCollapsingCB" ResourceKey="EnableCollapsing" Text="EnableCollapsing" suffix=":"/>
<asp:CheckBox ID="cbEnableCollapsing" runat="server" />

    </div>
    <div class="dnnFormItem">
        <dnn:Label runat="server" ControlName="DisableDefaultImageCB" ResourceKey="DisableDefaultImage" Text="DisableDefaultImage" suffix=":"/>
<asp:CheckBox ID="cbDisableDefaultImage" runat="server" />

    </div>
         <div class="dnnFormItem">
        <dnn:Label runat="server" ControlName="DefaultImageUrl" ResourceKey="DefaultImageUrl" Text="Default Image Url" suffix=":"/>
            <asp:TextBox ID="tbDefaultImageUrl" runat="server" ></asp:TextBox>
        </div>
    <%--    <div class="dnnFormItem">
        <dnn:Label runat="server" ControlName="ExpandCollapseAllNodes" ResourceKey="ExpandCollapseAllNodes" Text="Expand / Collapse All Nodes" suffix=":"/>
        <asp:RadioButtonList runat="server" ID="ExpandCollapseAllNodesRB"
                RepeatDirection="Horizontal" CssClass="dnnFormRadioButtons">
                <asp:ListItem Text="None" Selected="True"/>
                <asp:ListItem Text="Expand All Nodes" />
                <asp:ListItem Text="Collapse All Nodes" />
            </asp:RadioButtonList>
    </div>
--%>    <div class="dnnFormItem"><dnn:Label runat="server" ControlName="ItemLTitleTxt" ResourceKey="ItemTitle" Text="Item Title" suffix=":"/>
    <asp:TextBox ID="TextBoxItemTitle" runat="server" ></asp:TextBox>
    </div>
    <div class="dnnFormItem">
            <dnn:Label runat="server" ControlName="EnableDrillDownCB" ResourceKey="EnableDrillDown" Text="EnableDrillDown" suffix=":"/>
            <asp:CheckBox ID="cbEnableDrillDown" runat="server" />
    </div>
        <div class="dnnFormItem">
            <dnn:Label runat="server" ControlName="ShowExpandCollapseNodeButton" ResourceKey="ShowExpandCollapseNodeButton" Text="Show Expand/Collapse Node Button" suffix=":"/>
            <asp:CheckBox ID="cbShowExpandCollapseNodeButton" runat="server" />
    </div>

<asp:PlaceHolder ID="PHGroup" runat="server">    <div class="dnnFormItem"><dnn:Label runat="server" ControlName="NodeLabelTxt" ResourceKey="NodeLabel" Text="Node Label" suffix=":"/>
    <asp:TextBox ID="TextBoxNodeLabel" runat="server" ></asp:TextBox>
    </div>
<div class="dnnFormItem">
    <dnn:Label runat="server" ControlName="GroupColumnCountTxt" ResourceKey="GroupColumnCount" Text="GroupColumnCount" suffix=":"/>
    <asp:TextBox ID="tbGroupColumnCount" runat="server" Columns="3" class="CountTextArea" ></asp:TextBox>
    </div>
    <div class="dnnFormItem">
    <dnn:Label runat="server" ControlName="EnableGroupCollapsingCB" ResourceKey="EnableGroupCollapsing" Text="EnableGroupCollapsing" suffix=":"/>
    <asp:CheckBox ID="cbEnableGroupCollapsing" runat="server" />
    </div>
<%--        <div class="dnnFormItem">
        <dnn:Label runat="server" ControlName="ExpandCollapseAllGroups" ResourceKey="ExpandCollapseAllGroups" Text="Expand / Collapse All Groups" suffix=":"/>
        <asp:RadioButtonList runat="server" ID="ExpandCollapseAllGroupsRB"
                RepeatDirection="Horizontal" CssClass="dnnFormRadioButtons">
                <asp:ListItem Text="None" Selected="True"/>
                <asp:ListItem Text="Expand All Groups" />
                <asp:ListItem Text="Collapse All Groups" />
            </asp:RadioButtonList>
    </div>
--%>
        <div class="dnnFormItem">
            <dnn:Label runat="server" ControlName="ShowExpandCollapseGroupButton" ResourceKey="ShowExpandCollapseGroupButton" Text="Show Expand/Collapse Group Button" suffix=":"/>
            <asp:CheckBox ID="cbShowExpandCollapseGroupButton" runat="server" />
    </div>

</asp:PlaceHolder>

    <div class="dnnFormItem">
    <dnn:Label runat="server" ControlName="LoadOnDemandDDL" ResourceKey="LoadOnDemand" Text="LoadOnDemand" suffix=":"/>
    <asp:DropDownList ID="ddlLoadOnDemand" runat="server">
        <asp:ListItem Selected="True">None</asp:ListItem>
        <asp:ListItem>Nodes</asp:ListItem>
        <asp:ListItem>Groups</asp:ListItem>
        <asp:ListItem>NodesAndGroups</asp:ListItem>
    </asp:DropDownList>
    </div>
    <div class="dnnFormItem">
            <dnn:Label runat="server" ControlName="lblReductSize25" ResourceKey="ReductSize25" Text="Reduct Size of 25 %" suffix=":"/>
            <asp:CheckBox ID="cbReductSize25" runat="server" />
    </div>

    </div>
    </fieldset>
</div>
<asp:Label ID="lblPortalID" runat="server" Text="Label" Visible="false"></asp:Label>
<asp:Label ID="lblModuleID" runat="server" Text="Label" Visible="false"></asp:Label>