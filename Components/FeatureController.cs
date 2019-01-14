/*
' Copyright (c) 2019 DevPCI
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

using System.Collections.Generic;
//using System.Xml;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Search;
using System.Xml;
using System.Linq;
using System;

namespace DevPCI.Modules.DDT_Org_Chart.Components
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Controller class for DDT_Org_Chart
    /// </summary>
    /// -----------------------------------------------------------------------------

    //uncomment the interfaces to add the support.
    public class FeatureController : IPortable //: IPortable, ISearchable, IUpgradeable
    {
        DDT_Org_Chart_LinqDataContext linqContext = new DDT_Org_Chart_LinqDataContext();

        #region Optional Interfaces

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// ExportModule implements the IPortable ExportModule Interface
        /// </summary>
        /// <param name="ModuleID">The Id of the module to be exported</param>
        /// -----------------------------------------------------------------------------
        public string ExportModule(int ModuleID)
        {
            ModuleController mc = new ModuleController();
            var mi = mc.GetModule(ModuleID);
            var sett = mi.TabModuleSettings;
            string mode = Convert.ToString(sett["Mode"]);

            string strXML = string.Format("<ddt_org_charts mode=\"{0}\" >", mode);
                
            // export setting
            
            List<DDT_Org_Chart_Item> colDDT_Org_Charts_I = (from DDT_Org_Chart_Item tmp in linqContext.DDT_Org_Chart_Items where tmp.ModuleID == ModuleID select tmp).ToList();
            List<DDT_Org_Chart_Node> colDDT_Org_Charts_N = (from DDT_Org_Chart_Node tmp in linqContext.DDT_Org_Chart_Nodes where tmp.ModuleID == ModuleID select tmp).ToList();
            List<DDT_Org_Chart_Item> colDDT_Org_Charts_Iroot = (from DDT_Org_Chart_Item tmp in linqContext.DDT_Org_Chart_Items where tmp.ModuleID == ModuleID && tmp.ID_Org_Chart_Node == null select tmp).ToList();
            List<DDT_Org_Chart_Node> colDDT_Org_Charts_Nroot = (from DDT_Org_Chart_Node tmp in linqContext.DDT_Org_Chart_Nodes where tmp.ModuleID == ModuleID && tmp.ParentID_Org_Chart_Node == null select tmp).ToList();
            //cas du module en mode simple
            #region simple
            if (colDDT_Org_Charts_I.Count != 0 && mode == "Simple")
            {
                strXML += "<ddt_org_chart_root>";
                foreach (DDT_Org_Chart_Item objDDT_Org_Chart in colDDT_Org_Charts_Iroot)
                {
                    strXML += ExportChildItemModeSimple(colDDT_Org_Charts_I, objDDT_Org_Chart, true);
                }
                strXML += "</ddt_org_chart_root>";
            }
            #endregion
            #region with group
            //cas module mode "With Group"
            else if (colDDT_Org_Charts_I.Count != 0 && colDDT_Org_Charts_N.Count != 0 && mode == "WithGroup")
            {
                strXML += "<ddt_org_chart_root>";
                foreach (DDT_Org_Chart_Node currentNode in colDDT_Org_Charts_Nroot)
                {
                    strXML += ExportChildItemModeWithGroup(colDDT_Org_Charts_N,colDDT_Org_Charts_I, currentNode, true);
                }
                strXML += "</ddt_org_chart_root>";
            }
            #endregion

            strXML += "</ddt_org_charts>";

            return strXML;

            //throw new System.NotImplementedException("The method or operation is not implemented.");
        }

        private static string ExportChildItemModeSimple(List<DDT_Org_Chart_Item> colDDT_Org_Charts_I, DDT_Org_Chart_Item currentItem, bool lookchild)
        {
            string strXML = string.Empty;
            strXML += "<ddt_org_chart_i>";
            strXML += "<itemname_org_chart>" + DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(Convert.ToString(currentItem.ItemName_Org_Chart)) + "</itemname_org_chart>";
            strXML += "<itemtitle_org_chart>" + DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(Convert.ToString(currentItem.ItemTitle_Org_Chart)) + "</itemtitle_org_chart>";
            strXML += "<itemimageurl_org_chart>" + DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(Convert.ToString(currentItem.ItemImageUrl_Org_Chart)) + "</itemimageurl_org_chart>";
            strXML += "<itemorder_org_chart>" + DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(Convert.ToString(currentItem.ItemOrder_Org_Chart)) + "</itemorder_org_chart>";
            strXML += "<collapsed>" + DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(Convert.ToString(currentItem.Collapsed)) + "</collapsed>";
            strXML += "<isactive>" + DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(Convert.ToString(currentItem.IsActive)) + "</isactive>";
            strXML += "<isdeleted>" + DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(Convert.ToString(currentItem.IsDeleted)) + "</isdeleted>";

            //recherche des enfants
            if (lookchild)
            {

                var childItems = colDDT_Org_Charts_I.Where(a => a.ID_Org_Chart_Node == currentItem.ID_Org_Chart_Item);
                if (childItems.Count() > 0)
                {
                    strXML += "<ddt_org_chart_is>";
                    foreach (DDT_Org_Chart_Item childItem in childItems)
                    {
                        strXML += ExportChildItemModeSimple(colDDT_Org_Charts_I, childItem, lookchild);
                        
                    }
                    strXML += "</ddt_org_chart_is>";
                }
            }
            strXML += "</ddt_org_chart_i>";
            return strXML;
        }

        private static string ExportChildItemModeWithGroup(List<DDT_Org_Chart_Node> colDDT_Org_Charts_N, List<DDT_Org_Chart_Item> colDDT_Org_Charts_I, DDT_Org_Chart_Node currentItem, bool lookchild)
        {
            string strXML = string.Empty;
            strXML += "<ddt_org_chart_n>";

            strXML += "<teamname_org_chart_node>" + DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(Convert.ToString(currentItem.TeamName_Org_Chart_Node)) + "</teamname_org_chart_node>";
            strXML += "<collapsed>" + DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(Convert.ToString(currentItem.Collapsed)) + "</collapsed>";
            strXML += "<groupcollapsed>" + DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(Convert.ToString(currentItem.GroupCollapsed)) + "</groupcollapsed>";
            strXML += "<columncount>" + DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(Convert.ToString(currentItem.ColumnCount)) + "</columncount>";
            strXML += "<isactive>" + DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(Convert.ToString(currentItem.IsActive)) + "</isactive>";
            strXML += "<isdeleted>" + DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(Convert.ToString(currentItem.IsDeleted)) + "</isdeleted>";

            //recherche des enfants type items
            var childItems = colDDT_Org_Charts_I.Where(a => a.ID_Org_Chart_Node == currentItem.ID_Org_Chart_Node);
            if (childItems.Count() > 0)
            {
                strXML += "<ddt_org_chart_is>";
                foreach (DDT_Org_Chart_Item childItem in childItems)
                {
                    strXML += ExportChildItemModeSimple(null, childItem, false);

                }
                strXML += "</ddt_org_chart_is>";
            }
 
            //recherche des enfants type nodes
            if (lookchild)
            {
                var childNodes = colDDT_Org_Charts_N.Where(a => a.ParentID_Org_Chart_Node == currentItem.ID_Org_Chart_Node);
                if (childNodes.Count() > 0)
                {
                    strXML += "<ddt_org_chart_ns>";
                    foreach (DDT_Org_Chart_Node childItem in childNodes)
                    {
                        strXML += ExportChildItemModeWithGroup(colDDT_Org_Charts_N, colDDT_Org_Charts_I, childItem, true);

                    }
                    strXML += "</ddt_org_chart_ns>";
                }
            }
            strXML += "</ddt_org_chart_n>";
            return strXML;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// ImportModule implements the IPortable ImportModule Interface
        /// </summary>
        /// <param name="ModuleID">The Id of the module to be imported</param>
        /// <param name="Content">The content to be imported</param>
        /// <param name="Version">The version of the module to be imported</param>
        /// <param name="UserID">The Id of the user performing the import</param>
        /// -----------------------------------------------------------------------------
        public void ImportModule(int ModuleID, string Content, string Version, int UserID)
        {
            ModuleController mc = new ModuleController();
            var mi = mc.GetModule(ModuleID);
            var sett = mi.TabModuleSettings;
            //string mode = Convert.ToString(sett["Mode"]);

            XmlNode xmlDDT_Org_Charts = DotNetNuke.Common.Globals.GetContent(Content, "ddt_org_charts");
            string mode = xmlDDT_Org_Charts.Attributes["mode"].InnerText;
            XmlNode nodeRoot = xmlDDT_Org_Charts.SelectSingleNode("ddt_org_chart_root");
            
            if (mode == "Simple")
            {
                ImportChildItemSimpleMode(ModuleID, UserID, mi.PortalID, nodeRoot, null, true);
            }
            if (mode == "WithGroup")
            {
                ImportChildItemModeWithGroup(ModuleID, UserID, mi.PortalID, nodeRoot, null);
            }
            //throw new System.NotImplementedException("The method or operation is not implemented.");

            // Import mode setting
            mc.UpdateTabModuleSetting(mi.TabModuleID, "Mode", mode);
        }


        protected void ImportChildItemSimpleMode(int ModuleID, int UserID, int PortalId, XmlNode rootXml, int? parentItemId, bool lookupchild)
        {
            int o1 = 1;
            foreach (XmlNode xmlDDT_Org_Chart_I in rootXml.SelectNodes("ddt_org_chart_i")) // node ROOT
            {
                DDT_Org_Chart_Item objDDT_Org_Chart_I = new DDT_Org_Chart_Item();
                objDDT_Org_Chart_I.ModuleID = ModuleID;
                objDDT_Org_Chart_I.PortalID = PortalId;
                objDDT_Org_Chart_I.ItemName_Org_Chart = xmlDDT_Org_Chart_I.SelectSingleNode("itemname_org_chart").InnerText;
                objDDT_Org_Chart_I.ItemTitle_Org_Chart = xmlDDT_Org_Chart_I.SelectSingleNode("itemtitle_org_chart").InnerText;
                objDDT_Org_Chart_I.ItemImageUrl_Org_Chart = xmlDDT_Org_Chart_I.SelectSingleNode("itemimageurl_org_chart").InnerText;
                objDDT_Org_Chart_I.ItemOrder_Org_Chart = o1;
                o1 += o1;
                objDDT_Org_Chart_I.Collapsed = Convert.ToBoolean(xmlDDT_Org_Chart_I.SelectSingleNode("collapsed").InnerText);
                objDDT_Org_Chart_I.IsActive = Convert.ToBoolean(xmlDDT_Org_Chart_I.SelectSingleNode("isactive").InnerText);
                objDDT_Org_Chart_I.IsDeleted = Convert.ToBoolean(xmlDDT_Org_Chart_I.SelectSingleNode("isdeleted").InnerText);
                objDDT_Org_Chart_I.CreatedByUserID = UserID;
                objDDT_Org_Chart_I.CreatedOnDate = DateTime.Now;
                objDDT_Org_Chart_I.LastModifiedByUserID = UserID;
                objDDT_Org_Chart_I.LastModifiedOnDate = DateTime.Now;

                if (parentItemId != null && parentItemId.HasValue)
                {
                    objDDT_Org_Chart_I.ID_Org_Chart_Node = parentItemId;
                }

                linqContext.DDT_Org_Chart_Items.InsertOnSubmit(objDDT_Org_Chart_I);
                linqContext.SubmitChanges();

                if (lookupchild)
                {
                    //recherche des enfants
                    XmlNode nodeIS = xmlDDT_Org_Chart_I.SelectSingleNode("ddt_org_chart_is");
                    if (nodeIS != null)
                    {
                        ImportChildItemSimpleMode(ModuleID, UserID, PortalId, nodeIS, objDDT_Org_Chart_I.ID_Org_Chart_Item, lookupchild);
                    }
                }
            }

        }

        protected void ImportChildItemModeWithGroup(int ModuleID, int UserID, int PortalId, XmlNode rootXml, DDT_Org_Chart_Node parentNode)
        {
            int o1 = 1;

            foreach (XmlNode xmlDDT_Org_Chart_N in rootXml.SelectNodes("ddt_org_chart_n")) // node ROOT
            {
                DDT_Org_Chart_Node objDDT_Org_Chart_N = new DDT_Org_Chart_Node();
                objDDT_Org_Chart_N.ModuleID = ModuleID;
                objDDT_Org_Chart_N.PortalID = PortalId;
                objDDT_Org_Chart_N.TeamName_Org_Chart_Node = xmlDDT_Org_Chart_N.SelectSingleNode("teamname_org_chart_node").InnerText;
                
                string groupcollapsed = xmlDDT_Org_Chart_N.SelectSingleNode("groupcollapsed").InnerText;
                bool bGroupCollapsed = false;
                if (bool.TryParse(groupcollapsed, out bGroupCollapsed))
                {
                    objDDT_Org_Chart_N.GroupCollapsed = bGroupCollapsed;
                }

                string columncount = xmlDDT_Org_Chart_N.SelectSingleNode("columncount").InnerText;
                int icolumncount = 0;
                if (int.TryParse(columncount, out icolumncount))
                {
                    objDDT_Org_Chart_N.ColumnCount = icolumncount;
                }

                objDDT_Org_Chart_N.NodeOrder_Org_Chart = o1++;
                //o1 += o1;

                string collapsed = xmlDDT_Org_Chart_N.SelectSingleNode("collapsed").InnerText;
                bool bcollapsed = false;
                if (bool.TryParse(collapsed, out bcollapsed))
                {
                    objDDT_Org_Chart_N.Collapsed = bcollapsed;
                }

                string isactive = xmlDDT_Org_Chart_N.SelectSingleNode("isactive").InnerText;
                bool bisactive = false;
                if (bool.TryParse(isactive, out bisactive))
                {
                    objDDT_Org_Chart_N.IsActive = bisactive;
                }

                string isdeleted = xmlDDT_Org_Chart_N.SelectSingleNode("isdeleted").InnerText;
                bool bisdeleted = false;
                if (bool.TryParse(isdeleted, out bisdeleted))
                {
                    objDDT_Org_Chart_N.IsDeleted = bisdeleted;
                }

                objDDT_Org_Chart_N.CreatedByUserID = UserID;
                objDDT_Org_Chart_N.CreatedOnDate = DateTime.Now;
                objDDT_Org_Chart_N.LastModifiedByUserID = UserID;
                objDDT_Org_Chart_N.LastModifiedOnDate = DateTime.Now;

                if (parentNode != null)
                {
                    objDDT_Org_Chart_N.ParentID_Org_Chart_Node = parentNode.ID_Org_Chart_Node;
                }

                linqContext.DDT_Org_Chart_Nodes.InsertOnSubmit(objDDT_Org_Chart_N);
                linqContext.SubmitChanges();

                //recherche des enfants type items
                XmlNode nodeIS = xmlDDT_Org_Chart_N.SelectSingleNode("ddt_org_chart_is");
                if (nodeIS != null)
                {
                    ImportChildItemSimpleMode(ModuleID, UserID, PortalId, nodeIS, objDDT_Org_Chart_N.ID_Org_Chart_Node, false);
                }

                //recherche des enfants type nodes
                XmlNode nodeNS = xmlDDT_Org_Chart_N.SelectSingleNode("ddt_org_chart_ns");
                if (nodeNS != null)
                {
                    ImportChildItemModeWithGroup(ModuleID, UserID, PortalId, nodeNS, objDDT_Org_Chart_N);
                }
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// GetSearchItems implements the ISearchable Interface
        /// </summary>
        /// <param name="ModInfo">The ModuleInfo for the module to be Indexed</param>
        /// -----------------------------------------------------------------------------
        /// Depreciated in DNN 7.1 and later. Committed out during initial project resurection in 2019 for DNN 9.2
        /// 
        /* public DotNetNuke.Services.Search.SearchItemInfoCollection GetSearchItems(DotNetNuke.Entities.Modules.ModuleInfo ModInfo)
        {
            //SearchItemInfoCollection SearchItemCollection = new SearchItemInfoCollection();

            //List<DDT_Org_ChartInfo> colDDT_Org_Charts = GetDDT_Org_Charts(ModInfo.ModuleID);

            //foreach (DDT_Org_ChartInfo objDDT_Org_Chart in colDDT_Org_Charts)
            //{
            //    SearchItemInfo SearchItem = new SearchItemInfo(ModInfo.ModuleTitle, objDDT_Org_Chart.Content, objDDT_Org_Chart.CreatedByUser, objDDT_Org_Chart.CreatedDate, ModInfo.ModuleID, objDDT_Org_Chart.ItemId.ToString(), objDDT_Org_Chart.Content, "ItemId=" + objDDT_Org_Chart.ItemId.ToString());
            //    SearchItemCollection.Add(SearchItem);
            //}

            //return SearchItemCollection;

             throw new System.NotImplementedException("The method or operation is not implemented.");
        } */

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// UpgradeModule implements the IUpgradeable Interface
        /// </summary>
        /// <param name="Version">The current version of the module</param>
        /// -----------------------------------------------------------------------------
        public string UpgradeModule(string Version)
        {
            throw new System.NotImplementedException("The method or operation is not implemented.");
        }

        #endregion

    }

}
