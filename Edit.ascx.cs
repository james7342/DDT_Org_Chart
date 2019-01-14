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

using System;
using DotNetNuke.Services.Exceptions;
using System.Collections;
using System.Web.UI.WebControls;
using DotNetNuke.Entities.Users;
using Telerik.Web.UI;
using System.Collections.Generic;
using System.Linq;
using DotNetNuke.UI.WebControls;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Localization;
using DotNetNuke.UI.Utilities;
using DotNetNuke.Web.UI.WebControls;
using DevPCI.Modules.DDT_Org_Chart;
using DotNetNuke.Common;

namespace DevPCI.Modules.DDT_Org_Chart
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The EditDDT_Org_Chart class is used to manage content
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class Edit : DDT_Org_ChartModuleBase
    {
        public bool? IsRadAsyncValid
        {
            get
            {
                if (Session["IsRadAsyncValid"] == null)
                {
                    Session["IsRadAsyncValid"] = true;
                }

                return Convert.ToBoolean(Session["IsRadAsyncValid"].ToString());
            }
            set
            {
                Session["IsRadAsyncValid"] = value;
            }
        }
        
        #region Event Handlers

        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
            hlCancel.NavigateUrl = Globals.NavigateURL();
            var dnnversion = Convert.ToString(DotNetNuke.Application.DotNetNukeContext.Current.Application.Version);
            var mainV = dnnversion.Substring(0,dnnversion.IndexOf(".")).Trim();
            LblDNNVersion.Text = mainV;
            int v = Convert.ToInt32(mainV);
            if(v>6)
            {
                phdnn7filepickercsshack.Visible = true;
            }


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
                //DotNetNuke.Framework.jQuery.RequestRegistration();
                //Implement your edit logic for your module
                lblPortalID.Text = PortalId.ToString();
                lblModuleID.Text = ModuleId.ToString();
                PhMessageNotif3.Visible = false;

                //bool IsHost =  UserController.Instance.GetCurrentUserInfo().IsSuperUser;
                //lblIsSuperUser.Text = Convert.ToString( UserController.Instance.GetCurrentUserInfo().IsSuperUser);


                if (Settings["Skin"] != null)
                {
                    RG_Items_Simple.Skin = Convert.ToString(Settings["Skin"]);
                    RG_Teams.Skin = Convert.ToString(Settings["Skin"]);
                    RG_Items_WithGroup.Skin = Convert.ToString(Settings["Skin"]);
                }
                else
                {
                    RG_Items_Simple.Skin = "Default";
                    RG_Teams.Skin = "Default";
                    RG_Items_WithGroup.Skin = "Default";
                }

                CheckBoxIsSuperUser.Checked = UserController.Instance.GetCurrentUserInfo().IsSuperUser;
                if (Settings["Mode"] != null)
                {
                    LblMode.Text = Convert.ToString(Settings["Mode"]);
                }
                else { LblMode.Text = "Simple"; }

                if (LblMode.Text == "Simple") // Mode Simple Without Team
                {
                    PHSimple.Visible = true;
                    PHWithGroup.Visible = false;
                }
                else if (LblMode.Text == "WithGroup") // Mode With Team
                {
                    PHSimple.Visible = false;
                    PHWithGroup.Visible = true;
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

        protected void LDS_Org_Chart_Items_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            DDT_Org_Chart_LinqDataContext linqContext = new DDT_Org_Chart_LinqDataContext();
            var XItem = from DDT_Org_Chart_Item tmp in linqContext.DDT_Org_Chart_Items
                        where tmp.PortalID == PortalId && tmp.ModuleID == ModuleId
                        select new { tmp.ID_Org_Chart_Item, tmp.ID_Org_Chart_Node, tmp.IsActive, tmp.IsDeleted, tmp.ItemName_Org_Chart, tmp.ItemOrder_Org_Chart, tmp.ItemTitle_Org_Chart, tmp.LastModifiedByUserID, tmp.LastModifiedOnDate, tmp.ModuleID, tmp.PortalID, tmp.Collapsed, tmp.CreatedByUserID, tmp.CreatedOnDate, tmp.ItemImageUrl_Org_Chart };
            e.Result = XItem;
        }

        #endregion


        #region GetTelerikGridSelections
        // region pour permettre le soft delete
        public ArrayList GetTelerikGridSelections(Telerik.Web.UI.RadGrid grid)
        {
            ArrayList selectedItems = new ArrayList();
            if (grid.MasterTableView.DataKeyNames.Length > 0)
            {
                string key = grid.MasterTableView.DataKeyNames[0];
                for (int i = 0; i < grid.SelectedItems.Count; i++)
                {
                    selectedItems.Add(grid.MasterTableView.DataKeyValues[grid.SelectedItems[i].ItemIndex][key]);
                }
            }
            return selectedItems;
        }
        //fin region
        #endregion 

        #region Action RG_Items_Simple

        protected void RG_Items_Simple_SoftDeleteSelected(object sender, CommandEventArgs e)
        {
            ArrayList MaListDeSelected = GetTelerikGridSelections(RG_Items_Simple);
            int nb = MaListDeSelected.Count;
            for (int i = 0; i < nb; i++)
            {
                int IdASoftDelete = Convert.ToInt32(MaListDeSelected[i]);
                DDT_Org_Chart_LinqDataContext linqContext = new DDT_Org_Chart_LinqDataContext();
                var XItem = from DDT_Org_Chart_Item tmp in linqContext.DDT_Org_Chart_Items
                             where tmp.ID_Org_Chart_Item == IdASoftDelete
                             select tmp;
                DDT_Org_Chart_Item ItemSoftDelete = XItem.First();

                var XTeam = from DDT_Org_Chart_Item tmp in linqContext.DDT_Org_Chart_Items
                            where tmp.ID_Org_Chart_Node == IdASoftDelete && tmp.IsDeleted == false
                            select tmp;

                if (XTeam.Count() != 0)
                {
                    PhMessageNotif3.Visible = true;
                    LblMessageNotif3.Text = "Le noeud ne peut pas être supprimé tant qu'il y a des items attachés a ce noeud";
                }
                else
                {
                    ItemSoftDelete.IsDeleted = true;
                    linqContext.SubmitChanges();
                    PhMessageNotif3.Visible = false;
                }
            }
            RG_Items_Simple.DataBind();
            SynchronizeModuleLastContentModifiedOnDate();
        }

        protected void RG_Items_Simple_Load(object sender, EventArgs e)
        {
            //Gestion de l'affichage des champs uniquement destinés au Hosts.
            //this.RG_Items_Simple.Columns.FindByUniqueName("ID_Org_Chart_Item").Visible =  UserController.Instance.GetCurrentUserInfo().IsSuperUser;
            //this.RG_Items_Simple.Columns.FindByUniqueName("PortalID").Visible =  UserController.Instance.GetCurrentUserInfo().IsSuperUser;
            //this.RG_Items_Simple.Columns.FindByUniqueName("ModuleID").Visible =  UserController.Instance.GetCurrentUserInfo().IsSuperUser;
            //this.RG_Items_Simple.Columns.FindByUniqueName("ItemOrder_Org_Chart").Visible =  UserController.Instance.GetCurrentUserInfo().IsSuperUser;
            this.RG_Items_Simple.Columns.FindByUniqueName("ID_Org_Chart_Item").Visible = false;
            this.RG_Items_Simple.Columns.FindByUniqueName("PortalID").Visible = false;
            this.RG_Items_Simple.Columns.FindByUniqueName("ModuleID").Visible = false;
            this.RG_Items_Simple.Columns.FindByUniqueName("ItemOrder_Org_Chart").Visible = false;
            //this.RG_Items_Simple.Columns.FindByUniqueName("IsActive").Visible =  UserController.Instance.GetCurrentUserInfo().IsSuperUser;
            //this.RG_Items_Simple.Columns.FindByUniqueName("IsDeleted").Visible =  UserController.Instance.GetCurrentUserInfo().IsSuperUser;
            // Champs CreatedByUserID CreatedOnDate LastModifiedByUserID LastModifiedOnDate Non visibles
            this.RG_Items_Simple.Columns.FindByUniqueName("CreatedByUserID").Visible = false;
            this.RG_Items_Simple.Columns.FindByUniqueName("CreatedOnDate").Visible = false;
            this.RG_Items_Simple.Columns.FindByUniqueName("LastModifiedByUserID").Visible = false;
            this.RG_Items_Simple.Columns.FindByUniqueName("LastModifiedOnDate").Visible = false;
        }

        protected void DDT_Org_Chart_ItemlnkOrderDown_Click(object sender, EventArgs e)
        {
            LinkButton linkButton = sender as LinkButton;
            int id = int.Parse(linkButton.CommandArgument.ToString());
            DDT_Org_Chart_ItemApplyOrder(id, +1);
            this.RG_Items_Simple.Rebind();
            SynchronizeModuleLastContentModifiedOnDate();
        }

        protected void DDT_Org_Chart_ItemlnkOrderUp_Click(object sender, EventArgs e)
        {
            LinkButton linkButton = sender as LinkButton;
            int id = int.Parse(linkButton.CommandArgument.ToString());
            DDT_Org_Chart_ItemApplyOrder(id, -1);
            this.RG_Items_Simple.Rebind();
            SynchronizeModuleLastContentModifiedOnDate();
        }
        //initialisation a 0 du count
        int countDDT_Org_Chart_Items = 0;
        // Mise en place du mecanisme d'affichage des liens Move Up et Move Down
        protected void RG_Items_Simple_ItemDataBound(object sender, GridItemEventArgs e)
        {
            // Mecanisme de traitement permettant de remplir les champs PortalId et Order en automatique lors de l ajout
            // e.Item.DataSetIndex == -1 permet de verifier que l on est dans un cas d insert d une nouvelle ligne et pas de update de lignes
            if ((e.Item.IsInEditMode) && (e.Item.DataSetIndex == -1))
            {
                //Code avant le chargement du formulaire (permet de charger les différents composants du formulaire)
                //le PortailId
                TextBox box = e.Item.FindControl("PortalIDTextBox") as TextBox;
                box.Text = Convert.ToString(PortalId);
                //le ModuleID
                TextBox box2 = e.Item.FindControl("ModuleIDTextBox") as TextBox;
                box2.Text = Convert.ToString(ModuleId);

                // un order qui soit egal au dernier +1
                DDT_Org_Chart_LinqDataContext linqContext = new DDT_Org_Chart_LinqDataContext();
                var MyOrderDDT_Org_Chart_Item = from DDT_Org_Chart_Item tmp in linqContext.DDT_Org_Chart_Items
                                                where tmp.PortalID == PortalId
                                                //  orderby tmp.Order_Secteurs descending. on a donc la valeur la plus forte en Premier (first)
                                                orderby tmp.ItemOrder_Org_Chart descending
                                                select tmp.ItemOrder_Org_Chart;
                int? lastorder = 0;
                if (MyOrderDDT_Org_Chart_Item.Count() > 0)
                {
                    lastorder = MyOrderDDT_Org_Chart_Item.First();
                }
                //  e.Result = comptelist;

                TextBox boxOrder = e.Item.FindControl("OrderIDTextBox") as TextBox;
                boxOrder.Text = Convert.ToString(lastorder + 1);

                //Insertions Champs CreatedByUserID CreatedOnDate LastModifiedByUserID LastModifiedOnDate
                TextBox tbCreatedByUserID = e.Item.FindControl("CreatedByUserID_TB") as TextBox;
                tbCreatedByUserID.Text = Convert.ToString(UserController.Instance.GetCurrentUserInfo().UserID);

                TextBox tbCreatedOnDate = e.Item.FindControl("CreatedOnDate_TB") as TextBox;
                tbCreatedOnDate.Text = Convert.ToString(DateTime.Now);

                TextBox tbLastModifiedByUserID = e.Item.FindControl("LastModifiedByUserID_TB") as TextBox;
                tbLastModifiedByUserID.Text = Convert.ToString(UserController.Instance.GetCurrentUserInfo().UserID);

                TextBox tbLastModifiedOnDate = e.Item.FindControl("LastModifiedOnDate_TB") as TextBox;
                tbLastModifiedOnDate.Text = Convert.ToString(DateTime.Now);

            }
            //cas de l'update Mise a jour des champs LastModifiedByUserID et LastModifiedOnDate
            else if ((e.Item.IsInEditMode) && (e.Item.DataSetIndex != -1))
            {
                TextBox tbLastModifiedByUserID = e.Item.FindControl("LastModifiedByUserID_TB") as TextBox;
                tbLastModifiedByUserID.Text = Convert.ToString(UserController.Instance.GetCurrentUserInfo().UserID);

                TextBox tbLastModifiedOnDate = e.Item.FindControl("LastModifiedOnDate_TB") as TextBox;
                tbLastModifiedOnDate.Text = Convert.ToString(DateTime.Now);

                // fix filepicker V S
                DnnFilePicker myfileP = e.Item.FindControl("FilePickerSimple") as DnnFilePicker;  
                if(myfileP.FilePath.Contains("//"))
                {
                    myfileP.FilePath = myfileP.FilePath.Replace("//", "/");
                }
                // fin fix filepicker V S
                SynchronizeModuleLastContentModifiedOnDate();

            }

            // Fin Mecanisme de traitement permettant de remplir les champs PortalId et Order en automatique

            // Mise en place du mecanisme d'affichage des liens Move Up et Move Down
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                //test si countSecteurs = 0, si oui, requete linq 
                if (countDDT_Org_Chart_Items == 0)
                {
                    DDT_Org_Chart_LinqDataContext linqContext = new DDT_Org_Chart_LinqDataContext();
                    var lDDT_Org_Chart_Items = from DDT_Org_Chart_Item DDT_Org_Chart_Item in linqContext.DDT_Org_Chart_Items
                                               where DDT_Org_Chart_Item.PortalID == PortalId && DDT_Org_Chart_Item.IsDeleted == false && DDT_Org_Chart_Item.ModuleID == ModuleId
                                               || DDT_Org_Chart_Item.PortalID == PortalId && DDT_Org_Chart_Item.IsDeleted == UserController.Instance.GetCurrentUserInfo().IsSuperUser && DDT_Org_Chart_Item.ModuleID == ModuleId
                                               select DDT_Org_Chart_Item;
                    //suivi du .Count() pour en avoir le nombre.
                    countDDT_Org_Chart_Items = lDDT_Org_Chart_Items.Count();
                }
                // on initialise index et on le recupere du RagGrid (index de la premiere ligne est 0)
                int index = e.Item.ItemIndex;

                //  Traitement de l affichage du bouton down (toutes les lignes sauf la derniere (index != countSecteurs-1))
                LinkButton DDT_Org_Chart_ItemlnkOrderDown = e.Item.FindControl("DDT_Org_Chart_ItemlnkOrderDown") as LinkButton;
                if (DDT_Org_Chart_ItemlnkOrderDown != null)
                {
                    DDT_Org_Chart_ItemlnkOrderDown.Visible = (index != countDDT_Org_Chart_Items - 1);
                }

                //  Traitement de l affichage du bouton down (toutes les lignes sauf la premiere (index != 0))
                LinkButton DDT_Org_Chart_ItemlnkOrderUp = e.Item.FindControl("DDT_Org_Chart_ItemlnkOrderUp") as LinkButton;
                if (DDT_Org_Chart_ItemlnkOrderUp != null)
                {
                    DDT_Org_Chart_ItemlnkOrderUp.Visible = (index != 0);
                }
                // Fin mecanisme d'affichage des liens Move Up et Move Down
            }
        }

        private void DDT_Org_Chart_ItemApplyOrder(int id, int step) // Apply Order avec invertion des valeurs d'ordre
        {
            DDT_Org_Chart_LinqDataContext linqContext = new DDT_Org_Chart_LinqDataContext();
            var lDDT_Org_Chart_Items = from DDT_Org_Chart_Item DDT_Org_Chart_Item in linqContext.DDT_Org_Chart_Items
                                       where DDT_Org_Chart_Item.PortalID == PortalId && DDT_Org_Chart_Item.ModuleID == ModuleId
               //                      where DDT_Org_Chart_Item.PortalID == PortalId && DDT_Org_Chart_Item.IsDeleted == false && DDT_Org_Chart_Item.ModuleID == ModuleId
               //                      || DDT_Org_Chart_Item.PortalID == PortalId && DDT_Org_Chart_Item.IsDeleted ==  UserController.Instance.GetCurrentUserInfo().IsSuperUser && DDT_Org_Chart_Item.ModuleID == ModuleId
                                       orderby DDT_Org_Chart_Item.ItemOrder_Org_Chart
                                       select DDT_Org_Chart_Item;

            if ((lDDT_Org_Chart_Items != null) && (lDDT_Org_Chart_Items.Count() > 0))
            {
                List<DDT_Org_Chart_Item> DDT_Org_Chart_Items = lDDT_Org_Chart_Items.ToList<DDT_Org_Chart_Item>();
                // initialisation de l'index
                int index = 0;
                int orderNullable = 0;
                // on boucle sur la liste
                foreach (DDT_Org_Chart_Item DDT_Org_Chart_Item in DDT_Org_Chart_Items)
                {
                    if (DDT_Org_Chart_Item.ID_Org_Chart_Item == id)
                    {
                        if (DDT_Org_Chart_Items.ElementAt(index + step).ItemOrder_Org_Chart != null)
                            //{
                            orderNullable = Convert.ToInt32(DDT_Org_Chart_Item.ItemOrder_Org_Chart);
                        DDT_Org_Chart_Item.ItemOrder_Org_Chart = DDT_Org_Chart_Items.ElementAt(index + step).ItemOrder_Org_Chart;
                        DDT_Org_Chart_Items.ElementAt(index + step).ItemOrder_Org_Chart = orderNullable;
                        // envoi linq des modifs à la BDD
                        linqContext.SubmitChanges();
                        //}
                    }
                    index++;
                }
            }
            SynchronizeModuleLastContentModifiedOnDate();
        }

        #endregion

        #region Action RG_Teams

        protected void RG_Teams_SoftDeleteSelected(object sender, CommandEventArgs e)
        {
            ArrayList MaListDeSelected = GetTelerikGridSelections(RG_Teams);
            int nb = MaListDeSelected.Count;
            for (int i = 0; i < nb; i++)
            {
                int IdASoftDelete = Convert.ToInt32(MaListDeSelected[i]);
                DDT_Org_Chart_LinqDataContext linqContext = new DDT_Org_Chart_LinqDataContext();
                var XNode = from DDT_Org_Chart_Node tmp in linqContext.DDT_Org_Chart_Nodes
                                    where tmp.ID_Org_Chart_Node == IdASoftDelete
                                    select tmp;
                DDT_Org_Chart_Node NodeSoftDelete = XNode.First();

                var XTeam = from DDT_Org_Chart_Item tmp in linqContext.DDT_Org_Chart_Items
                                 where tmp.ID_Org_Chart_Node == IdASoftDelete && tmp.IsDeleted == false
                                 select tmp;

                if (XTeam.Count() != 0)
                {
                    PhMessageNotif3.Visible = true;
                    LblMessageNotif3.Text = "Le noeud ne peut pas être supprimé tant qu'il y a des items attachés a ce noeud";
                }
                else
                {
                    NodeSoftDelete.IsDeleted = true;
                    linqContext.SubmitChanges();
                    PhMessageNotif3.Visible = false;
                }
            }
            RG_Teams.DataBind();
            SynchronizeModuleLastContentModifiedOnDate();
        }

        protected void RG_Teams_Load(object sender, EventArgs e)
        {
            //Gestion de l'affichage des champs uniquement destinés au Hosts.
            //this.RG_Teams.Columns.FindByUniqueName("ID_Org_Chart_Node").Visible =  UserController.Instance.GetCurrentUserInfo().IsSuperUser;
            //this.RG_Teams.Columns.FindByUniqueName("PortalID").Visible =  UserController.Instance.GetCurrentUserInfo().IsSuperUser;
            //this.RG_Teams.Columns.FindByUniqueName("ModuleID").Visible =  UserController.Instance.GetCurrentUserInfo().IsSuperUser;
            //this.RG_Teams.Columns.FindByUniqueName("NodeOrder_Org_Chart").Visible =  UserController.Instance.GetCurrentUserInfo().IsSuperUser;
            this.RG_Teams.Columns.FindByUniqueName("ID_Org_Chart_Node").Visible = false;
            this.RG_Teams.Columns.FindByUniqueName("PortalID").Visible = false;
            this.RG_Teams.Columns.FindByUniqueName("ModuleID").Visible = false;
            this.RG_Teams.Columns.FindByUniqueName("NodeOrder_Org_Chart").Visible = false;
            //this.RG_Teams.Columns.FindByUniqueName("IsActive").Visible =  UserController.Instance.GetCurrentUserInfo().IsSuperUser;
            //this.RG_Teams.Columns.FindByUniqueName("IsDeleted").Visible =  UserController.Instance.GetCurrentUserInfo().IsSuperUser;
            // Champs CreatedByUserID CreatedOnDate LastModifiedByUserID LastModifiedOnDate Non visibles
            this.RG_Teams.Columns.FindByUniqueName("CreatedByUserID").Visible = false;
            this.RG_Teams.Columns.FindByUniqueName("CreatedOnDate").Visible = false;
            this.RG_Teams.Columns.FindByUniqueName("LastModifiedByUserID").Visible = false;
            this.RG_Teams.Columns.FindByUniqueName("LastModifiedOnDate").Visible = false;


        }

        protected void DDT_Org_Chart_NodelnkOrderDown_Click(object sender, EventArgs e)
        {
            LinkButton linkButton = sender as LinkButton;
            int id = int.Parse(linkButton.CommandArgument.ToString());
            DDT_Org_Chart_NodeApplyOrder(id, +1);
            this.RG_Teams.Rebind();
            SynchronizeModuleLastContentModifiedOnDate();
        }

        protected void DDT_Org_Chart_NodelnkOrderUp_Click(object sender, EventArgs e)
        {
            LinkButton linkButton = sender as LinkButton;
            int id = int.Parse(linkButton.CommandArgument.ToString());
            DDT_Org_Chart_NodeApplyOrder(id, -1);
            this.RG_Teams.Rebind();
            SynchronizeModuleLastContentModifiedOnDate();
        }
        //initialisation a 0 du count
        int countDDT_Org_Chart_Nodes = 0;
        // Mise en place du mecanisme d'affichage des liens Move Up et Move Down
        protected void RG_Teams_ItemDataBound(object sender, GridItemEventArgs e)
        {
            // Mecanisme de traitement permettant de remplir les champs PortalId et Order en automatique lors de l ajout
            // e.Item.DataSetIndex == -1 permet de verifier que l on est dans un cas d insert d une nouvelle ligne et pas de update de lignes
            if ((e.Item.IsInEditMode) && (e.Item.DataSetIndex == -1))
            {
                //Code avant le chargement du formulaire (permet de charger les différents composants du formulaire)
                //le PortailId
                TextBox box = e.Item.FindControl("PortalIDTextBox") as TextBox;
                box.Text = Convert.ToString(PortalId);
                //le ModuleID
                TextBox box2 = e.Item.FindControl("ModuleIDTextBox") as TextBox;
                box2.Text = Convert.ToString(ModuleId);

                // un order qui soit egal au dernier +1
                DDT_Org_Chart_LinqDataContext linqContext = new DDT_Org_Chart_LinqDataContext();
                var MyOrderDDT_Org_Chart_Node = from DDT_Org_Chart_Node tmp in linqContext.DDT_Org_Chart_Nodes
                                              where tmp.PortalID == PortalId
                                              //  orderby tmp.Order_Secteurs descending. on a donc la valeur la plus forte en Premier (first)
                                              orderby tmp.NodeOrder_Org_Chart descending
                                              select tmp.NodeOrder_Org_Chart;
                int? lastorder = 0;
                if (MyOrderDDT_Org_Chart_Node.Count() > 0)
                {
                    lastorder = MyOrderDDT_Org_Chart_Node.First();
                }
                //  e.Result = comptelist;

                TextBox boxOrder = e.Item.FindControl("OrderIDTextBox") as TextBox;
                boxOrder.Text = Convert.ToString(lastorder + 1);

                //Insertions Champs CreatedByUserID CreatedOnDate LastModifiedByUserID LastModifiedOnDate
                TextBox tbCreatedByUserID = e.Item.FindControl("CreatedByUserID_TB") as TextBox;
                tbCreatedByUserID.Text = Convert.ToString(UserController.Instance.GetCurrentUserInfo().UserID);

                TextBox tbCreatedOnDate = e.Item.FindControl("CreatedOnDate_TB") as TextBox;
                tbCreatedOnDate.Text = Convert.ToString(DateTime.Now);

                TextBox tbLastModifiedByUserID = e.Item.FindControl("LastModifiedByUserID_TB") as TextBox;
                tbLastModifiedByUserID.Text = Convert.ToString(UserController.Instance.GetCurrentUserInfo().UserID);

                TextBox tbLastModifiedOnDate = e.Item.FindControl("LastModifiedOnDate_TB") as TextBox;
                tbLastModifiedOnDate.Text = Convert.ToString(DateTime.Now);

            }
            //cas de l'update Mise a jour des champs LastModifiedByUserID et LastModifiedOnDate
            else if ((e.Item.IsInEditMode) && (e.Item.DataSetIndex != -1))
            {
                TextBox tbLastModifiedByUserID = e.Item.FindControl("LastModifiedByUserID_TB") as TextBox;
                tbLastModifiedByUserID.Text = Convert.ToString(UserController.Instance.GetCurrentUserInfo().UserID);

                TextBox tbLastModifiedOnDate = e.Item.FindControl("LastModifiedOnDate_TB") as TextBox;
                tbLastModifiedOnDate.Text = Convert.ToString(DateTime.Now);

                SynchronizeModuleLastContentModifiedOnDate();

            }

            // Fin Mecanisme de traitement permettant de remplir les champs PortalId et Order en automatique

            // Mise en place du mecanisme d'affichage des liens Move Up et Move Down
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                //test si countSecteurs = 0, si oui, requete linq 
                if (countDDT_Org_Chart_Nodes == 0)
                {
                    DDT_Org_Chart_LinqDataContext linqContext = new DDT_Org_Chart_LinqDataContext();
                    var lDDT_Org_Chart_Nodes = from DDT_Org_Chart_Node DDT_Org_Chart_Node in linqContext.DDT_Org_Chart_Nodes
                                             where DDT_Org_Chart_Node.PortalID == PortalId && DDT_Org_Chart_Node.IsDeleted == false && DDT_Org_Chart_Node.ModuleID == ModuleId
                                             || DDT_Org_Chart_Node.PortalID == PortalId && DDT_Org_Chart_Node.IsDeleted == UserController.Instance.GetCurrentUserInfo().IsSuperUser && DDT_Org_Chart_Node.ModuleID == ModuleId
                                             select DDT_Org_Chart_Node;
                    //suivi du .Count() pour en avoir le nombre.
                    countDDT_Org_Chart_Nodes = lDDT_Org_Chart_Nodes.Count();
                }
                // on initialise index et on le recupere du RagGrid (index de la premiere ligne est 0)
                int index = e.Item.ItemIndex;

                //  Traitement de l affichage du bouton down (toutes les lignes sauf la derniere (index != countSecteurs-1))
                LinkButton DDT_Org_Chart_NodelnkOrderDown = e.Item.FindControl("DDT_Org_Chart_NodelnkOrderDown") as LinkButton;
                if (DDT_Org_Chart_NodelnkOrderDown != null)
                {
                    DDT_Org_Chart_NodelnkOrderDown.Visible = (index != countDDT_Org_Chart_Nodes - 1);
                }

                //  Traitement de l affichage du bouton down (toutes les lignes sauf la premiere (index != 0))
                LinkButton DDT_Org_Chart_NodelnkOrderUp = e.Item.FindControl("DDT_Org_Chart_NodelnkOrderUp") as LinkButton;
                if (DDT_Org_Chart_NodelnkOrderUp != null)
                {
                    DDT_Org_Chart_NodelnkOrderUp.Visible = (index != 0);
                }
                // Fin mecanisme d'affichage des liens Move Up et Move Down
            }
        }

        private void DDT_Org_Chart_NodeApplyOrder(int id, int step) // Apply Order avec invertion des valeurs d'ordre
        {
            DDT_Org_Chart_LinqDataContext linqContext = new DDT_Org_Chart_LinqDataContext();
            var lDDT_Org_Chart_Nodes = from DDT_Org_Chart_Node DDT_Org_Chart_Node in linqContext.DDT_Org_Chart_Nodes
                                       where DDT_Org_Chart_Node.PortalID == PortalId && DDT_Org_Chart_Node.ModuleID == ModuleId
           //                          where DDT_Org_Chart_Node.PortalID == PortalId && DDT_Org_Chart_Node.IsDeleted == false && DDT_Org_Chart_Node.ModuleID == ModuleId
           //                        || DDT_Org_Chart_Node.PortalID == PortalId && DDT_Org_Chart_Node.IsDeleted ==  UserController.Instance.GetCurrentUserInfo().IsSuperUser && DDT_Org_Chart_Node.ModuleID == ModuleId
                                     orderby DDT_Org_Chart_Node.NodeOrder_Org_Chart
                                     select DDT_Org_Chart_Node;

            if ((lDDT_Org_Chart_Nodes != null) && (lDDT_Org_Chart_Nodes.Count() > 0))
            {
                List<DDT_Org_Chart_Node> DDT_Org_Chart_Nodes = lDDT_Org_Chart_Nodes.ToList<DDT_Org_Chart_Node>();
                // initialisation de l'index
                int index = 0;
                int orderNullable = 0;
                // on boucle sur la liste
                foreach (DDT_Org_Chart_Node DDT_Org_Chart_Node in DDT_Org_Chart_Nodes)
                {
                    if (DDT_Org_Chart_Node.ID_Org_Chart_Node == id)
                    {
                        if (DDT_Org_Chart_Nodes.ElementAt(index + step).NodeOrder_Org_Chart != null)
                            //{
                            orderNullable = Convert.ToInt32(DDT_Org_Chart_Node.NodeOrder_Org_Chart);
                        DDT_Org_Chart_Node.NodeOrder_Org_Chart = DDT_Org_Chart_Nodes.ElementAt(index + step).NodeOrder_Org_Chart;
                        DDT_Org_Chart_Nodes.ElementAt(index + step).NodeOrder_Org_Chart = orderNullable;
                        // envoi linq des modifs à la BDD
                        linqContext.SubmitChanges();
                        //}
                    }
                    index++;
                }

            }
            SynchronizeModuleLastContentModifiedOnDate();
        }


        #endregion
        
        #region Action RG_Items_WithGroup

        protected void DDT_Org_Chart_ItemDeleteImage_Click (object sender, EventArgs e)
        {
                LinkButton linkButton = sender as LinkButton;
                int IdADelete = int.Parse(linkButton.CommandArgument.ToString());

                DDT_Org_Chart_LinqDataContext linqContext = new DDT_Org_Chart_LinqDataContext();
                var XItem = from DDT_Org_Chart_Item tmp in linqContext.DDT_Org_Chart_Items
                             where tmp.ID_Org_Chart_Item == IdADelete
                             select tmp;
                DDT_Org_Chart_Item ItemSoftDelete = XItem.First();
                ItemSoftDelete.ItemImageUrl_Org_Chart = null;
                linqContext.SubmitChanges();
                RG_Items_WithGroup.DataBind();
                RG_Items_Simple.DataBind();
                SynchronizeModuleLastContentModifiedOnDate();
        }

        protected void RG_Items_WithGroup_SoftDeleteSelected(object sender, CommandEventArgs e)
        {
            ArrayList MaListDeSelected = GetTelerikGridSelections(RG_Items_WithGroup);
            int nb = MaListDeSelected.Count;
            for (int i = 0; i < nb; i++)
            {
                int IdASoftDelete = Convert.ToInt32(MaListDeSelected[i]);
                DDT_Org_Chart_LinqDataContext linqContext = new DDT_Org_Chart_LinqDataContext();
                var XItem = from DDT_Org_Chart_Item tmp in linqContext.DDT_Org_Chart_Items
                            where tmp.ID_Org_Chart_Item == IdASoftDelete
                            select tmp;
                DDT_Org_Chart_Item ItemSoftDelete = XItem.First();
                ItemSoftDelete.IsDeleted = true;
                linqContext.SubmitChanges();
                PhMessageNotif3.Visible = false;
            }
            RG_Items_WithGroup.DataBind();
            SynchronizeModuleLastContentModifiedOnDate();
        }

        protected void RG_Items_WithGroup_Load(object sender, EventArgs e)
        {
            //Gestion de l'affichage des champs uniquement destinés au Hosts.
            //this.RG_Items_WithGroup.Columns.FindByUniqueName("ID_Org_Chart_Item").Visible =  UserController.Instance.GetCurrentUserInfo().IsSuperUser;
            //this.RG_Items_WithGroup.Columns.FindByUniqueName("PortalID").Visible =  UserController.Instance.GetCurrentUserInfo().IsSuperUser;
            //this.RG_Items_WithGroup.Columns.FindByUniqueName("ModuleID").Visible =  UserController.Instance.GetCurrentUserInfo().IsSuperUser;
            //this.RG_Items_WithGroup.Columns.FindByUniqueName("ItemOrder_Org_Chart").Visible =  UserController.Instance.GetCurrentUserInfo().IsSuperUser;
            this.RG_Items_WithGroup.Columns.FindByUniqueName("ID_Org_Chart_Item").Visible = false;
            this.RG_Items_WithGroup.Columns.FindByUniqueName("PortalID").Visible = false;
            this.RG_Items_WithGroup.Columns.FindByUniqueName("ModuleID").Visible = false;
            this.RG_Items_WithGroup.Columns.FindByUniqueName("ItemOrder_Org_Chart").Visible = false;
            //this.RG_Items_WithGroup.Columns.FindByUniqueName("IsActive").Visible =  UserController.Instance.GetCurrentUserInfo().IsSuperUser;
            //this.RG_Items_WithGroup.Columns.FindByUniqueName("IsDeleted").Visible =  UserController.Instance.GetCurrentUserInfo().IsSuperUser;
            // Champs CreatedByUserID CreatedOnDate LastModifiedByUserID LastModifiedOnDate Non visibles
            this.RG_Items_WithGroup.Columns.FindByUniqueName("CreatedByUserID").Visible = false;
            this.RG_Items_WithGroup.Columns.FindByUniqueName("CreatedOnDate").Visible = false;
            this.RG_Items_WithGroup.Columns.FindByUniqueName("LastModifiedByUserID").Visible = false;
            this.RG_Items_WithGroup.Columns.FindByUniqueName("LastModifiedOnDate").Visible = false;


        }

        protected void DDT_Org_Chart_Item2lnkOrderDown_Click(object sender, EventArgs e)
        {
            LinkButton linkButton = sender as LinkButton;
            int id = int.Parse(linkButton.CommandArgument.ToString());
            DDT_Org_Chart_Item2ApplyOrder(id, +1);
            this.RG_Items_WithGroup.Rebind();
            SynchronizeModuleLastContentModifiedOnDate();
        }

        protected void DDT_Org_Chart_Item2lnkOrderUp_Click(object sender, EventArgs e)
        {
            LinkButton linkButton = sender as LinkButton;
            int id = int.Parse(linkButton.CommandArgument.ToString());
            DDT_Org_Chart_Item2ApplyOrder(id, -1);
            this.RG_Items_WithGroup.Rebind();
            SynchronizeModuleLastContentModifiedOnDate();
        }
        //initialisation a 0 du count
        int countDDT_Org_Chart_Items2 = 0;
        // Mise en place du mecanisme d'affichage des liens Move Up et Move Down
        protected void RG_Items_WithGroup_ItemDataBound(object sender, GridItemEventArgs e)
        {
            // Mecanisme de traitement permettant de remplir les champs PortalId et Order en automatique lors de l ajout
            // e.Item.DataSetIndex == -1 permet de verifier que l on est dans un cas d insert d une nouvelle ligne et pas de update de lignes
            if ((e.Item.IsInEditMode) && (e.Item.DataSetIndex == -1))
            {
                //Code avant le chargement du formulaire (permet de charger les différents composants du formulaire)
                //le PortailId
                TextBox box = e.Item.FindControl("PortalIDTextBox") as TextBox;
                box.Text = Convert.ToString(PortalId);
                //le ModuleID
                TextBox box2 = e.Item.FindControl("ModuleIDTextBox") as TextBox;
                box2.Text = Convert.ToString(ModuleId);

                // un order qui soit egal au dernier +1
                DDT_Org_Chart_LinqDataContext linqContext = new DDT_Org_Chart_LinqDataContext();
                var MyOrderDDT_Org_Chart_Item = from DDT_Org_Chart_Item tmp in linqContext.DDT_Org_Chart_Items
                                                where tmp.PortalID == PortalId
                                                //  orderby tmp.Order_Secteurs descending. on a donc la valeur la plus forte en Premier (first)
                                                orderby tmp.ItemOrder_Org_Chart descending
                                                select tmp.ItemOrder_Org_Chart;

                int? lastorder = 0;
                if (MyOrderDDT_Org_Chart_Item.Count() > 0)
                {
                    lastorder = MyOrderDDT_Org_Chart_Item.First();
                }
                //  e.Result = comptelist;

                TextBox boxOrder = e.Item.FindControl("OrderIDTextBox") as TextBox;
                boxOrder.Text = Convert.ToString(lastorder + 1);

                //Insertions Champs CreatedByUserID CreatedOnDate LastModifiedByUserID LastModifiedOnDate
                TextBox tbCreatedByUserID = e.Item.FindControl("CreatedByUserID_TB") as TextBox;
                tbCreatedByUserID.Text = Convert.ToString(UserController.Instance.GetCurrentUserInfo().UserID);

                TextBox tbCreatedOnDate = e.Item.FindControl("CreatedOnDate_TB") as TextBox;
                tbCreatedOnDate.Text = Convert.ToString(DateTime.Now);

                TextBox tbLastModifiedByUserID = e.Item.FindControl("LastModifiedByUserID_TB") as TextBox;
                tbLastModifiedByUserID.Text = Convert.ToString(UserController.Instance.GetCurrentUserInfo().UserID);

                TextBox tbLastModifiedOnDate = e.Item.FindControl("LastModifiedOnDate_TB") as TextBox;
                tbLastModifiedOnDate.Text = Convert.ToString(DateTime.Now);

            }
            //cas de l'update Mise a jour des champs LastModifiedByUserID et LastModifiedOnDate
            else if ((e.Item.IsInEditMode) && (e.Item.DataSetIndex != -1))
            {
                TextBox tbLastModifiedByUserID = e.Item.FindControl("LastModifiedByUserID_TB") as TextBox;
                tbLastModifiedByUserID.Text = Convert.ToString(UserController.Instance.GetCurrentUserInfo().UserID);

                TextBox tbLastModifiedOnDate = e.Item.FindControl("LastModifiedOnDate_TB") as TextBox;
                tbLastModifiedOnDate.Text = Convert.ToString(DateTime.Now);

                // fix filepicker V S
                // DnnFilePicker myfileP = e.Item.FindControl("FilePickerSimple") as DnnFilePicker;
                DnnFilePicker myfileP = e.Item.FindControl("FilePickerSimple") as DnnFilePicker;
                if (myfileP.FilePath.Contains("//"))
                {
                    myfileP.FilePath = myfileP.FilePath.Replace("//", "/");
                }
                // fin fix filepicker V S

                SynchronizeModuleLastContentModifiedOnDate();


            }

            // Fin Mecanisme de traitement permettant de remplir les champs PortalId et Order en automatique

            // Mise en place du mecanisme d'affichage des liens Move Up et Move Down
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                //test si countSecteurs = 0, si oui, requete linq 
                if (countDDT_Org_Chart_Items2 == 0)
                {
                    DDT_Org_Chart_LinqDataContext linqContext = new DDT_Org_Chart_LinqDataContext();
                    var lDDT_Org_Chart_Items = from DDT_Org_Chart_Item DDT_Org_Chart_Item in linqContext.DDT_Org_Chart_Items
                                               where DDT_Org_Chart_Item.PortalID == PortalId && DDT_Org_Chart_Item.ModuleID == ModuleId
                    //                           where DDT_Org_Chart_Item.PortalID == PortalId && DDT_Org_Chart_Item.IsDeleted == false && DDT_Org_Chart_Item.ModuleID == ModuleId
                    //                           || DDT_Org_Chart_Item.PortalID == PortalId && DDT_Org_Chart_Item.IsDeleted ==  UserController.Instance.GetCurrentUserInfo().IsSuperUser && DDT_Org_Chart_Item.ModuleID == ModuleId
                                               select DDT_Org_Chart_Item;
                    //suivi du .Count() pour en avoir le nombre.
                    countDDT_Org_Chart_Items2 = lDDT_Org_Chart_Items.Count();
                }
                // on initialise index et on le recupere du RagGrid (index de la premiere ligne est 0)
                int index = e.Item.ItemIndex;

                //  Traitement de l affichage du bouton down (toutes les lignes sauf la derniere (index != countSecteurs-1))
                LinkButton DDT_Org_Chart_Item2lnkOrderDown = e.Item.FindControl("DDT_Org_Chart_Item2lnkOrderDown") as LinkButton;
                if (DDT_Org_Chart_Item2lnkOrderDown != null)
                {
                    DDT_Org_Chart_Item2lnkOrderDown.Visible = (index != countDDT_Org_Chart_Items2 - 1);
                }

                //  Traitement de l affichage du bouton down (toutes les lignes sauf la premiere (index != 0))
                LinkButton DDT_Org_Chart_Item2lnkOrderUp = e.Item.FindControl("DDT_Org_Chart_Item2lnkOrderUp") as LinkButton;
                if (DDT_Org_Chart_Item2lnkOrderUp != null)
                {
                    DDT_Org_Chart_Item2lnkOrderUp.Visible = (index != 0);
                }
                // Fin mecanisme d'affichage des liens Move Up et Move Down
            }
        }

        private void DDT_Org_Chart_Item2ApplyOrder(int id, int step) // Apply Order avec invertion des valeurs d'ordre
        {
            DDT_Org_Chart_LinqDataContext linqContext = new DDT_Org_Chart_LinqDataContext();
            var lDDT_Org_Chart_Items = from DDT_Org_Chart_Item DDT_Org_Chart_Item in linqContext.DDT_Org_Chart_Items
                                       where DDT_Org_Chart_Item.PortalID == PortalId && DDT_Org_Chart_Item.IsDeleted == false && DDT_Org_Chart_Item.ModuleID == ModuleId
                                       || DDT_Org_Chart_Item.PortalID == PortalId && DDT_Org_Chart_Item.IsDeleted == UserController.Instance.GetCurrentUserInfo().IsSuperUser && DDT_Org_Chart_Item.ModuleID == ModuleId
                                       orderby DDT_Org_Chart_Item.ItemOrder_Org_Chart
                                       select DDT_Org_Chart_Item;

            if ((lDDT_Org_Chart_Items != null) && (lDDT_Org_Chart_Items.Count() > 0))
            {
                List<DDT_Org_Chart_Item> DDT_Org_Chart_Items = lDDT_Org_Chart_Items.ToList<DDT_Org_Chart_Item>();
                // initialisation de l'index
                int index = 0;
                int orderNullable = 0;
                // on boucle sur la liste
                foreach (DDT_Org_Chart_Item DDT_Org_Chart_Item in DDT_Org_Chart_Items)
                {
                    if (DDT_Org_Chart_Item.ID_Org_Chart_Item == id)
                    {
                        if (DDT_Org_Chart_Items.ElementAt(index + step).ItemOrder_Org_Chart != null)
                            //{
                            orderNullable = Convert.ToInt32(DDT_Org_Chart_Item.ItemOrder_Org_Chart);
                        DDT_Org_Chart_Item.ItemOrder_Org_Chart = DDT_Org_Chart_Items.ElementAt(index + step).ItemOrder_Org_Chart;
                        DDT_Org_Chart_Items.ElementAt(index + step).ItemOrder_Org_Chart = orderNullable;
                        // envoi linq des modifs à la BDD
                        linqContext.SubmitChanges();
                        //}
                    }
                    index++;
                }

            }
            SynchronizeModuleLastContentModifiedOnDate();
        }

        #endregion


        # region localization
        protected string ConfirmLocalize(string key)
        {
            return String.Format("javascript:return confirm('{0}')", LocalizeString(key));
        }

        protected void RG_Items_Simple_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridHeaderItem)
            {
                GridHeaderItem header = (GridHeaderItem)e.Item;
                header["ItemName_Org_Chart"].Text = LocalizeString("ItemName").ToString();
                header["ItemTitle_Org_Chart"].Text = LocalizeString("ItemTitle").ToString();
                header["ItemImageUrl_Org_Chart"].Text = LocalizeString("ItemImageUrl").ToString();
                header["IsActive"].Text = LocalizeString("IsActive").ToString();
                header["IsDeleted"].Text = LocalizeString("IsDeleted").ToString();
                header["Order"].Text = LocalizeString("Order").ToString();
                header["TeamName_Org_Chart_Node"].Text = LocalizeString("ParentItem").ToString();
                header["Collapsed"].Text = LocalizeString("Collapsed").ToString();
            }
        }

        protected void RG_Teams_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridHeaderItem)
            {
                GridHeaderItem header = (GridHeaderItem)e.Item;
                header["ParentID_Org_Chart_Node"].Text = LocalizeString("ParentGroup").ToString();
                header["TeamName_Org_Chart_Node"].Text = LocalizeString("GroupName").ToString();
                header["Collapsed"].Text = LocalizeString("Collapsed").ToString();
                header["GroupCollapsed"].Text = LocalizeString("GroupCollapsed").ToString();
                header["ColumnCount"].Text = LocalizeString("ColumnCount").ToString();
                header["IsActive"].Text = LocalizeString("IsActive").ToString();
                header["IsDeleted"].Text = LocalizeString("IsDeleted").ToString();
                header["Order"].Text = LocalizeString("Order").ToString();
            }
        }

        protected void RG_Items_WithGroup_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridHeaderItem)
            {
                GridHeaderItem header = (GridHeaderItem)e.Item;
                header["TeamName_Org_Chart_Node"].Text = LocalizeString("GroupName").ToString();
                header["ItemName_Org_Chart"].Text = LocalizeString("ItemName").ToString();
                header["ItemTitle_Org_Chart"].Text = LocalizeString("ItemTitle").ToString();
                header["ItemImageUrl_Org_Chart"].Text = LocalizeString("ItemImageUrl").ToString();
                header["IsActive"].Text = LocalizeString("IsActive").ToString();
                header["IsDeleted"].Text = LocalizeString("IsDeleted").ToString();
                header["Order"].Text = LocalizeString("Order").ToString();
            }
        }


        #endregion














    }

}