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
using System.Linq;
using DotNetNuke.Services.Localization;


namespace DevPCI.Modules.DDT_Org_Chart
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Settings class manages Module Settings
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class Settings : DDT_Org_ChartSettingsBase
    {

        #region Base Method Implementations

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// LoadSettings loads the settings from the Database and displays them
        /// </summary>
        /// -----------------------------------------------------------------------------
        public override void LoadSettings()
        {
            try
            {
                if (Page.IsPostBack == false)
                {
                    lblPortalID.Text = PortalId.ToString();
                    lblModuleID.Text = ModuleId.ToString();

                    //Check for existing settings and use those on this page
                    //Settings["SettingName"]
                    if (Settings["Mode"] != null)
                    {
                        rbMode.SelectedValue = Convert.ToString(Settings["Mode"]);
                        if (rbMode.SelectedValue == "Simple")
                        { PHGroup.Visible = false; }
                        else 
                        { PHGroup.Visible = true; }
                    }
                    else { PHGroup.Visible = false;
                    }
                    if (Settings["Skin"] != null)
                    {
                        ddlSkin.SelectedValue = Convert.ToString(Settings["Skin"]);
                    }
                    if (Settings["GroupColumnCount"] != null)
                    {
                        tbGroupColumnCount.Text = Convert.ToString(Settings["GroupColumnCount"]);
                    }
                    if (Settings["DisableDefaultImage"] != null)
                    {
                        cbDisableDefaultImage.Checked = Convert.ToBoolean(Settings["DisableDefaultImage"]);
                    }
                    if (Settings["DefaultImageUrl"] != null)
                    {
                        tbDefaultImageUrl.Text = Convert.ToString(Settings["DefaultImageUrl"]);
                    }

                    if (Settings["EnableCollapsing"] != null)
                    {
                        cbEnableCollapsing.Checked = Convert.ToBoolean(Settings["EnableCollapsing"]);
                    }
                    if (Settings["EnableGroupCollapsing"] != null)
                    {
                        cbEnableGroupCollapsing.Checked = Convert.ToBoolean(Settings["EnableGroupCollapsing"]);
                    }
                    if (Settings["LoadOnDemand"] != null)
                    {
                        ddlLoadOnDemand.SelectedValue = Convert.ToString(Settings["LoadOnDemand"]);
                    }
                    if (Settings["EnableDrillDown"] != null)
                    {
                        cbEnableDrillDown.Checked = Convert.ToBoolean(Settings["EnableDrillDown"]);
                    }
                    //if (Settings["ExpandCollapseAllNodes"] != null)
                    //{
                    //    ExpandCollapseAllNodesRB.Text = Convert.ToString(Settings["ExpandCollapseAllNodes"]);
                    //}
                    //if (Settings["ExpandCollapseAllGroups"] != null)
                    //{
                    //    ExpandCollapseAllGroupsRB.Text = Convert.ToString(Settings["ExpandCollapseAllGroups"]);
                    //}
                    if (Settings["ItemTitle"] != null)
                    {
                        TextBoxItemTitle.Text = Convert.ToString(Settings["ItemTitle"]);
                    }
                    if (Settings["NodeLabel"] != null)
                    {
                        TextBoxNodeLabel.Text = Convert.ToString(Settings["NodeLabel"]);
                    }
                    if (Settings["ReductSize25"] != null)
                    {
                        cbReductSize25.Checked = Convert.ToBoolean(Settings["ReductSize25"]);
                    }
                    if (Settings["ShowExpandCollapseNodeButton"] != null)
                    {
                        cbShowExpandCollapseNodeButton.Checked = Convert.ToBoolean(Settings["ShowExpandCollapseNodeButton"]);
                    }
                    if (Settings["ShowExpandCollapseGroupButton"] != null)
                    {
                        cbShowExpandCollapseGroupButton.Checked = Convert.ToBoolean(Settings["ShowExpandCollapseGroupButton"]);
                    }

               }
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// UpdateSettings saves the modified settings to the Database
        /// </summary>
        /// -----------------------------------------------------------------------------
        public override void UpdateSettings()
        {
            try
            {

                string mode = Convert.ToString(Settings["Mode"]);
                string mode2 = rbMode.SelectedValue;
                if (mode != mode2)
                {
                    //purge relation parent
                    DDT_Org_Chart_LinqDataContext linqContext = new DDT_Org_Chart_LinqDataContext();
                    var items = from DDT_Org_Chart_Item tmp in linqContext.DDT_Org_Chart_Items
                                where tmp.PortalID == PortalId && tmp.ModuleID == ModuleId
                                select tmp;

                    if (items != null)
                    {
                        foreach (var item in items)
                        {
                            item.ID_Org_Chart_Node = null;
                        }
                        linqContext.SubmitChanges();
                    }

                }
                ModuleController modules = new ModuleController();
                //modules.UpdateTabModuleSetting(this.TabModuleId, "ModuleSetting", (control.value ? "true" : "false"));
                //modules.UpdateModuleSetting(this.TabModuleId, "LogBreadCrumb", (control.value ? "true" : "false"));
                modules.UpdateTabModuleSetting(this.TabModuleId, "Mode", rbMode.SelectedValue);
                modules.UpdateTabModuleSetting(this.TabModuleId, "Skin", ddlSkin.SelectedValue);
                modules.UpdateTabModuleSetting(this.TabModuleId, "DisableDefaultImage", (cbDisableDefaultImage.Checked ? "true" : "false"));
                modules.UpdateTabModuleSetting(this.TabModuleId, "DefaultImageUrl", tbDefaultImageUrl.Text);
                modules.UpdateTabModuleSetting(this.TabModuleId, "GroupColumnCount", tbGroupColumnCount.Text);
                modules.UpdateTabModuleSetting(this.TabModuleId, "EnableCollapsing", (cbEnableCollapsing.Checked ? "true" : "false"));
                modules.UpdateTabModuleSetting(this.TabModuleId, "EnableGroupCollapsing", (cbEnableGroupCollapsing.Checked ? "true" : "false"));
                modules.UpdateTabModuleSetting(this.TabModuleId, "LoadOnDemand", ddlLoadOnDemand.SelectedValue);
                modules.UpdateTabModuleSetting(this.TabModuleId, "EnableDrillDown", (cbEnableDrillDown.Checked ? "true" : "false"));
                //modules.UpdateTabModuleSetting(this.TabModuleId, "ExpandCollapseAllNodes", ExpandCollapseAllNodesRB.Text);
                //modules.UpdateTabModuleSetting(this.TabModuleId, "ExpandCollapseAllGroups", ExpandCollapseAllGroupsRB.Text);
                modules.UpdateTabModuleSetting(this.TabModuleId, "ItemTitle", TextBoxItemTitle.Text);
                modules.UpdateTabModuleSetting(this.TabModuleId, "NodeLabel", TextBoxNodeLabel.Text);
                modules.UpdateTabModuleSetting(this.TabModuleId, "ReductSize25", (cbReductSize25.Checked ? "true" : "false"));
                modules.UpdateTabModuleSetting(this.TabModuleId, "ShowExpandCollapseNodeButton", (cbShowExpandCollapseNodeButton.Checked ? "true" : "false"));
                modules.UpdateTabModuleSetting(this.TabModuleId, "ShowExpandCollapseGroupButton", (cbShowExpandCollapseGroupButton.Checked ? "true" : "false"));
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        #endregion

        protected void rbMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            //don't work here (not client side on)
            //rbMode.Attributes.Add("SelectedIndexChanged", "javascript:return confirm('" + Localization.GetString("ConfirmChangeMode") + "');");
            if (rbMode.SelectedValue == "Simple")
            {
                PHGroup.Visible = false;
            }
            else { PHGroup.Visible = true; }

            
        }


    }

}

