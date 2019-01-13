/*
' Copyright (c) 2011  DevPCI
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

using System;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Localization;
using DotNetNuke.Security;
using System.Data;
using Telerik.Web.UI;
using DotNetNuke.UI;
using System.Web.UI;
using System.Linq;
using DotNetNuke.Security.Permissions;


namespace DevPCI.Modules.DDT_Org_Chart
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The ViewDDT_Org_Chart class displays the content
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : DDT_Org_ChartModuleBase, IActionable
    {
        #region Event Handlers

        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Page_Load runs when the control is loaded
        /// </summary>
        /// -----------------------------------------------------------------------------
        private void Page_Load(object sender, System.EventArgs e)
        {
            try
            {
                //get the moduleconfiguration
                ModuleInfo conf = this.ModuleConfiguration;
                ModulePermissionCollection myPermissionCollection = ModuleConfiguration.ModulePermissions;
                //read out the custom data editright of the global constant
                bool bCustomEditDataRights = ModulePermissionController.HasModulePermission(myPermissionCollection, "EDIT");

                //DDT_Org_Chart_Simple.NodeDataBound += new Telerik.Web.UI.OrgChartNodeDataBoundEventHandler(DDT_Org_Chart_Simple_NodeDataBound);
                DDT_Org_Chart_WithGroup.NodeDataBound += new Telerik.Web.UI.OrgChartNodeDataBoundEventHandler(DDT_Org_Chart_WithGroup_NodeDataBound);
                DDT_Org_Chart_WithGroup.NodeDrop += new Telerik.Web.UI.OrgChartNodeDropEventHandler(DDT_Org_Chart_WithGroup_NodeDrop);
                DDT_Org_Chart_WithGroup.GroupItemDrop += new Telerik.Web.UI.OrgChartGroupItemDropEventHandler(DDT_Org_Chart_WithGroup_GroupItemDrop);

                lblPortalID.Text = PortalId.ToString();
                lblModuleID.Text = ModuleId.ToString();

                DDT_Org_Chart_Div.Attributes.Add("class", "DDT_Org_Chart_Class");

                string mode = Convert.ToString(Settings["Mode"]); 
                if (string.IsNullOrEmpty(mode))
                {
                    phInit.Visible = true;
                }
                if (string.IsNullOrEmpty(mode)==false)
                {
                    if (mode == "Simple") // Mode Simple Without Team
                    {
                        PHSimple.Visible = true;
                        PHWithGroup.Visible = false;
                        DDT_Org_Chart_Simple.DataSourceID = "LDS_Org_Chart_Items_Simple";
                        DDT_Org_Chart_Simple.DataTextField = "ItemName_Org_Chart";
                        DDT_Org_Chart_Simple.DataFieldID = "ID_Org_Chart_Item";
                        DDT_Org_Chart_Simple.DataFieldParentID = "ID_Org_Chart_Node";
                        DDT_Org_Chart_Simple.DataImageUrlField = "MyBuildImageUrl";
                        DDT_Org_Chart_Simple.EnableDragAndDrop = bCustomEditDataRights;
                        DDT_Org_Chart_Simple.DataCollapsedField = "Collapsed";

                    }
                    else if (mode == "WithGroup") // Mode With Team
                    {
                        PHSimple.Visible = false;
                        PHWithGroup.Visible = true;

                        DDT_Org_Chart_WithGroup.GroupEnabledBinding.NodeBindingSettings.DataFieldID = "ID_Org_Chart_Node";
                        DDT_Org_Chart_WithGroup.GroupEnabledBinding.NodeBindingSettings.DataFieldParentID = "ParentID_Org_Chart_Node";
                        DDT_Org_Chart_WithGroup.GroupEnabledBinding.NodeBindingSettings.DataSourceID = "LDS_Org_Chart_Nodes";
                        DDT_Org_Chart_WithGroup.GroupEnabledBinding.NodeBindingSettings.DataCollapsedField = "Collapsed";
                        DDT_Org_Chart_WithGroup.GroupEnabledBinding.NodeBindingSettings.DataGroupCollapsedField = "GroupCollapsed";
                        DDT_Org_Chart_WithGroup.GroupEnabledBinding.GroupItemBindingSettings.DataFieldNodeID = "ID_Org_Chart_Node";
                        DDT_Org_Chart_WithGroup.GroupEnabledBinding.GroupItemBindingSettings.DataFieldID = "ID_Org_Chart_Item";
                        DDT_Org_Chart_WithGroup.GroupEnabledBinding.GroupItemBindingSettings.DataTextField = "ItemName_Org_Chart";
                        DDT_Org_Chart_WithGroup.GroupEnabledBinding.GroupItemBindingSettings.DataSourceID = "LDS_Org_Chart_Items_WithGroup";
                        DDT_Org_Chart_WithGroup.GroupEnabledBinding.GroupItemBindingSettings.DataImageUrlField = "MyBuildImageUrl";

                        DDT_Org_Chart_WithGroup.EnableDragAndDrop = bCustomEditDataRights;
                        //DDT_Org_Chart_WithGroup.DataBind();

                }

                }
                if (Settings["Skin"] != null)
                {
                    DDT_Org_Chart_Simple.Skin = Convert.ToString(Settings["Skin"]);
                    DDT_Org_Chart_WithGroup.Skin = Convert.ToString(Settings["Skin"]);
                }
                else { DDT_Org_Chart_Simple.Skin = "Default";}

                string s = Convert.ToString(Settings["GroupColumnCount"]);
                if (string.IsNullOrEmpty(s) == false)
                {
                    DDT_Org_Chart_Simple.GroupColumnCount = Convert.ToInt32(s);
                    DDT_Org_Chart_WithGroup.GroupColumnCount = Convert.ToInt32(s);
                }
                if (Settings["DisableDefaultImage"] != null)
                {
                    DDT_Org_Chart_Simple.DisableDefaultImage = Convert.ToBoolean(Settings["DisableDefaultImage"]);
                    DDT_Org_Chart_WithGroup.DisableDefaultImage = Convert.ToBoolean(Settings["DisableDefaultImage"]);
                }
                if (Settings["DefaultImageUrl"] != null)
                {
                    DDT_Org_Chart_Simple.DefaultImageUrl = Convert.ToString(Settings["DefaultImageUrl"]);
                    DDT_Org_Chart_WithGroup.DefaultImageUrl = Convert.ToString(Settings["DefaultImageUrl"]);
                }

                if (Settings["EnableCollapsing"] != null)
                {
                    DDT_Org_Chart_Simple.EnableCollapsing = Convert.ToBoolean(Settings["EnableCollapsing"]);
                    DDT_Org_Chart_WithGroup.EnableCollapsing = Convert.ToBoolean(Settings["EnableCollapsing"]);
                }
                if (Settings["EnableGroupCollapsing"] != null)
                {
                    DDT_Org_Chart_Simple.EnableGroupCollapsing = Convert.ToBoolean(Settings["EnableGroupCollapsing"]);
                    DDT_Org_Chart_WithGroup.EnableGroupCollapsing = Convert.ToBoolean(Settings["EnableGroupCollapsing"]);
                }
                if (Settings["LoadOnDemand"] != null)
                    {
                        lblLoadOnDemand.Text = Convert.ToString(Settings["LoadOnDemand"]);
                        if (lblLoadOnDemand.Text == "None")
                    {
                            DDT_Org_Chart_Simple.LoadOnDemand = OrgChartLoadOnDemand.None;
                            DDT_Org_Chart_WithGroup.LoadOnDemand = OrgChartLoadOnDemand.None;
                    }
                        if (lblLoadOnDemand.Text == "Nodes")
                    {
                            DDT_Org_Chart_Simple.LoadOnDemand = OrgChartLoadOnDemand.Nodes;
                            DDT_Org_Chart_WithGroup.LoadOnDemand = OrgChartLoadOnDemand.Nodes;
                    }
                        if (lblLoadOnDemand.Text == "Groups")
                    {
                            DDT_Org_Chart_Simple.LoadOnDemand = OrgChartLoadOnDemand.Groups;
                            DDT_Org_Chart_WithGroup.LoadOnDemand = OrgChartLoadOnDemand.Groups;
                    }
                        if (lblLoadOnDemand.Text == "NodesAndGroups")
                    {
                            DDT_Org_Chart_Simple.LoadOnDemand = OrgChartLoadOnDemand.NodesAndGroups;
                            DDT_Org_Chart_WithGroup.LoadOnDemand = OrgChartLoadOnDemand.NodesAndGroups;
                    }
                 }
                if (Settings["EnableDrillDown"] != null)
                {
                    DDT_Org_Chart_Simple.EnableDrillDown = Convert.ToBoolean(Settings["EnableDrillDown"]);
                    DDT_Org_Chart_WithGroup.EnableDrillDown = Convert.ToBoolean(Settings["EnableDrillDown"]);
                }
                if (Settings["ExpandCollapseAllNodes"] != null)
                {
                    if ((string)Settings["ExpandCollapseAllNodes"] == "Expand All Nodes")
                    {
                        DDT_Org_Chart_Simple.ExpandAllNodes();
                        DDT_Org_Chart_WithGroup.ExpandAllNodes();
                    }
                    else if ((string)Settings["ExpandCollapseAllNodes"] == "Collapse All Nodes")
                    {
                        DDT_Org_Chart_Simple.CollapseAllNodes();
                        DDT_Org_Chart_WithGroup.CollapseAllNodes();
                    }
                }
                if ((string)Settings["ExpandCollapseAllGroups"] != null)
                {
                    if ((string)Settings["ExpandCollapseAllGroups"] == "Expand All Groups")
                    {
                        DDT_Org_Chart_Simple.ExpandAllGroups();
                        DDT_Org_Chart_WithGroup.ExpandAllGroups();
                    }
                    else if ((String)Settings["ExpandCollapseAllGroups"] == "Collapse All Groups")
                    {
                        DDT_Org_Chart_Simple.CollapseAllGroups();
                        DDT_Org_Chart_WithGroup.CollapseAllGroups();
                    }
                    if (Settings["NodeLabel"] != null)
                    {
                        DDT_Org_Chart_WithGroup.RenderedFields.NodeFields.First().Label = Convert.ToString(Settings["NodeLabel"]);
                    }
                    if (Settings["ItemLabel"] != null)
                    {
                        DDT_Org_Chart_WithGroup.RenderedFields.ItemFields.First().Label = Convert.ToString(Settings["ItemLabel"]);
                    }
                    if (Settings["ItemTitle"] != null)
                    {
                        DDT_Org_Chart_Simple.RenderedFields.ItemFields.First().Label = Convert.ToString(Settings["ItemTitle"]);
                        DDT_Org_Chart_WithGroup.RenderedFields.ItemFields.First().Label = Convert.ToString(Settings["ItemTitle"]);
                    }
                }
                if (Settings["ReductSize25"] != null)
                {
                    string tt = Convert.ToString(Settings["ReductSize25"]);
                    if (tt == "true")
                    {phReductSize25.Visible = true; }
                    else
                    { phReductSize25.Visible = false; }
                }
                if (Settings["ShowExpandCollapseNodeButton"] != null)
                {
                    string tt = Convert.ToString(Settings["ShowExpandCollapseNodeButton"]);
                    if (tt == "true")
                    { phShowExpandCollapseNodeButton.Visible = true; }
                    else
                    { phShowExpandCollapseNodeButton.Visible = false; }
                }
                if (Settings["ShowExpandCollapseGroupButton"] != null)
                {
                    string tt = Convert.ToString(Settings["ShowExpandCollapseGroupButton"]);
                    if (tt == "true")
                    { phShowExpandCollapseGroupButton.Visible = true; }
                    else
                    { phShowExpandCollapseGroupButton.Visible = false; }
                }
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }
        
        private void SynchronizeModuleLastContentModifiedOnDate()
        {
            //Synchronise module (update Modules LastContentModifiedOnDate)
            ModuleController.SynchronizeModule(this.ModuleId);
            //clear page cache
            ModuleController modules = new ModuleController();
            modules.ClearCache(TabId);
        }

        //manage individual column count
        void DDT_Org_Chart_WithGroup_NodeDataBound(object sender, Telerik.Web.UI.OrgChartNodeDataBoundEventArguments e)
        {
            DDT_Org_Chart_LinqDataContext linqContext = new DDT_Org_Chart_LinqDataContext();
            var nodeid = from DDT_Org_Chart_Node tmp in linqContext.DDT_Org_Chart_Nodes
                         where tmp.ID_Org_Chart_Node == Convert.ToInt32(e.Node.ID)
                         select new { IDCC=tmp.ID_Org_Chart_Node, CC=tmp.ColumnCount };
            if (nodeid.Count() > 0)
            {
                if (nodeid.First().CC != null)
                {
                    string idcc = Convert.ToString(nodeid.First().IDCC);
                    int cc = Convert.ToInt32(nodeid.First().CC);
                    if (e.Node.ID == idcc)
                        e.Node.ColumnCount = cc;

                }

            }
        }
        #endregion

        #region Optional Interfaces

        public ModuleActionCollection ModuleActions
        {
            get
            {
                ModuleActionCollection Actions = new ModuleActionCollection();
                Actions.Add(GetNextActionID(), Localization.GetString("EditModule", this.LocalResourceFile), "", "", "", EditUrl(), false, SecurityAccessLevel.Edit, true, false);
                return Actions;
            }
        }

        #endregion

        protected void DDT_Org_Chart_Simple_NodeDrop(object sender, OrgChartNodeDropEventArguments e)
        {
            DDT_Org_Chart_LinqDataContext linqContext = new DDT_Org_Chart_LinqDataContext();
            var rows = from DDT_Org_Chart_Item myRow in linqContext.DDT_Org_Chart_Items 
                       where myRow.ID_Org_Chart_Item == Convert.ToInt32(e.SourceNode.ID) 
                       select new { values = myRow };
            foreach (var row in rows)
            {
                row.values.ID_Org_Chart_Node = Convert.ToInt32(e.DestinationNode.ID);
                linqContext.SubmitChanges();
                DDT_Org_Chart_Simple.DataBind();
            }
            SynchronizeModuleLastContentModifiedOnDate();
        }


        protected void DDT_Org_Chart_WithGroup_GroupItemDrop(object sender, OrgChartGroupItemDropEventArguments e)
        {
            DDT_Org_Chart_LinqDataContext linqContext = new DDT_Org_Chart_LinqDataContext();
            var rows = from DDT_Org_Chart_Item myRow in linqContext.DDT_Org_Chart_Items
                       where myRow.ID_Org_Chart_Item == Convert.ToInt32(e.SourceGroupItem.ID)
                       select new { values = myRow };
            foreach (var row in rows)
            {
                row.values.ID_Org_Chart_Node = Convert.ToInt32(e.DestinationNode.ID);
                linqContext.SubmitChanges();
                DDT_Org_Chart_WithGroup.DataBind();
            }
            SynchronizeModuleLastContentModifiedOnDate();
        }

        protected void DDT_Org_Chart_WithGroup_NodeDrop(object sender, OrgChartNodeDropEventArguments e)
        {
            DDT_Org_Chart_LinqDataContext linqContext = new DDT_Org_Chart_LinqDataContext();
            var rows = from DDT_Org_Chart_Node myRow in linqContext.DDT_Org_Chart_Nodes
                       where myRow.ID_Org_Chart_Node == Convert.ToInt32(e.SourceNode.ID)
                       select new { values = myRow };
            foreach (var row in rows)
            {
                row.values.ParentID_Org_Chart_Node = Convert.ToInt32(e.DestinationNode.ID);
                linqContext.SubmitChanges();
                DDT_Org_Chart_WithGroup.DataBind();
            }
            SynchronizeModuleLastContentModifiedOnDate();
        }

        protected void LDS_Org_Chart_Items_Simple_Selecting(object sender, System.Web.UI.WebControls.LinqDataSourceSelectEventArgs e)
        {
            DDT_Org_Chart_LinqDataContext linqContext = new DDT_Org_Chart_LinqDataContext();
            var XItem = from DDT_Org_Chart_Item tmp in linqContext.DDT_Org_Chart_Items
                        where tmp.PortalID == PortalId && tmp.ModuleID == ModuleId && tmp.IsActive == true && tmp.IsDeleted == false
                        orderby tmp.ItemOrder_Org_Chart
                        select new { tmp.ID_Org_Chart_Item, tmp.ID_Org_Chart_Node, tmp.IsActive, tmp.IsDeleted, tmp.ItemName_Org_Chart, tmp.ItemOrder_Org_Chart, tmp.ItemTitle_Org_Chart, tmp.LastModifiedByUserID, tmp.LastModifiedOnDate, tmp.ModuleID, tmp.PortalID, tmp.Collapsed, tmp.CreatedByUserID, tmp.CreatedOnDate, MyBuildImageUrl = "/Portals/" + tmp.PortalID + "/" + tmp.ItemImageUrl_Org_Chart.Replace("//", "/"), tmp.ItemImageUrl_Org_Chart };
            e.Result = XItem;

        }

        protected void LDS_Org_Chart_Items_WithGroup_Selecting(object sender, System.Web.UI.WebControls.LinqDataSourceSelectEventArgs e)
        {
            DDT_Org_Chart_LinqDataContext linqContext = new DDT_Org_Chart_LinqDataContext();
            var XItem = from DDT_Org_Chart_Item tmp in linqContext.DDT_Org_Chart_Items
                        where tmp.ID_Org_Chart_Node > 0 && tmp.PortalID == PortalId && tmp.ModuleID == ModuleId && tmp.IsActive == true && tmp.IsDeleted == false
                        orderby tmp.ItemOrder_Org_Chart
                        select new { tmp.ID_Org_Chart_Item, tmp.ID_Org_Chart_Node, tmp.IsActive, tmp.IsDeleted, tmp.ItemName_Org_Chart, tmp.ItemOrder_Org_Chart, tmp.ItemTitle_Org_Chart, tmp.LastModifiedByUserID, tmp.LastModifiedOnDate, tmp.ModuleID, tmp.PortalID, tmp.Collapsed, tmp.CreatedByUserID, tmp.CreatedOnDate, MyBuildImageUrl = "/Portals/" + tmp.PortalID + "/" + tmp.ItemImageUrl_Org_Chart.Replace("//", "/"), tmp.ItemImageUrl_Org_Chart };
            e.Result = XItem;
        }

        protected void GetBuildImageUrl(string ImageUrlToBuild)
        {
            ImageUrlToBuild = "/Portals/" + PortalId + "/" + ImageUrlToBuild.Replace("//", "/");
        }

    }

}
