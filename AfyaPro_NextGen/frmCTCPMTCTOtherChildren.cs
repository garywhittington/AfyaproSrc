using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraLayout;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System.IO;
using System.Xml.Serialization;

namespace AfyaPro_NextGen
{
    public partial class frmCTCPMTCTOtherChildren : DevExpress.XtraEditors.XtraForm
    {
        #region declaration

        private AfyaPro_MT.clsCTCClients pMdtCTCClients;
        private AfyaPro_Types.clsResult pResult = new AfyaPro_Types.clsResult();

        private Type pType;
        private string pClassName = "";

        private DataTable pDtChildren = null;

        #endregion

        #region frmCTCPMTCTOtherChildren
        public frmCTCPMTCTOtherChildren(DataTable mDtChildren)
        {
            InitializeComponent();

            pType = this.GetType();
            pClassName = pType.FullName;
            string mFunctionName = "frmCTCPMTCTOtherChildren";

            try
            {
                this.Icon = Program.gMdiForm.Icon;

                this.pDtChildren = mDtChildren;

                pMdtCTCClients = (AfyaPro_MT.clsCTCClients)Activator.GetObject(
                    typeof(AfyaPro_MT.clsCTCClients),
                    Program.gMiddleTier + "clsCTCClients");

                grdPMTCTOtherChildren.DataSource = pDtChildren;

                layoutControl1.AllowCustomizationMenu = Program.GrantDeny_FunctionAccess(
                    AfyaPro_Types.clsSystemFunctions.FunctionKeys.ctcpmtctantenatal_customizelayout.ToString());
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region Load_Controls
        private void Load_Controls()
        {
            List<Object> mObjectsList = new List<object>();

            mObjectsList.Add(txbDeliveryDate);
            mObjectsList.Add(cmdAdd);
            mObjectsList.Add(cmdDelete);
            mObjectsList.Add(cmdClose);

            Program.Apply_Language(this.Name, mObjectsList);
        }

        #endregion

        #region frmCTCPMTCTOtherChildren_Load
        private void frmCTCPMTCTOtherChildren_Load(object sender, EventArgs e)
        {
            Program.Restore_FormLayout(layoutControl1, this.Name);
            Program.Restore_FormSize(this);
            Program.Restore_GridLayout(grdPMTCTOtherChildren, grdPMTCTOtherChildren.Name);

            this.Load_Controls();

            this.Details_Clear();

            Program.Center_Screen(this);

            this.Append_ShortcutKeys();
        }
        #endregion

        #region frmCTCPMTCTOtherChildren_FormClosing
        private void frmCTCPMTCTOtherChildren_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //layout
                if (layoutControl1.IsModified == true)
                {
                    Program.Save_FormLayout(this, layoutControl1, this.Name);
                }

                Program.Save_GridLayout(grdPMTCTOtherChildren, grdPMTCTOtherChildren.Name);
            }
            catch { }
        }
        #endregion

        #region Append_ShortcutKeys
        private void Append_ShortcutKeys()
        {
            //cmdAdd.Text = cmdAdd.Text + " (" + Program.KeyCode_ItemAdd.ToString() + ")";
            //cmdDelete.Text = cmdDelete.Text + " (" + Program.KeyCode_ItemRemove.ToString() + ")";
            //cmdUpdate.Text = cmdUpdate.Text + " (" + Program.KeyCode_ItemUpdate.ToString() + ")";
            //cmdClear.Text = cmdClear.Text + " (" + Program.KeyCode_RchClear.ToString() + ")";
        }
        #endregion

        #region viewChildren_RowClick
        private void viewChildren_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle <= -1)
            {
                return;
            }

            this.Display_RowDetails(viewPMTCTOtherChildren.GetDataRow(e.RowHandle));
        }
        #endregion

        #region viewChildren_GotFocus
        private void viewChildren_GotFocus(object sender, EventArgs e)
        {
            if (viewPMTCTOtherChildren.FocusedRowHandle <= -1)
            {
                return;
            }

            this.Display_RowDetails(viewPMTCTOtherChildren.GetDataRow(viewPMTCTOtherChildren.FocusedRowHandle));
        }
        #endregion

        #region Details_Clear
        private void Details_Clear()
        {
            txtDeliveryDate.EditValue = null;
            cboResult.SelectedIndex = 0;
        }
        #endregion

        #region Display_RowDetails
        private void Display_RowDetails(DataRow mSelectedRow)
        {
            txtDeliveryDate.EditValue = Convert.ToDateTime(mSelectedRow["birthdate"]);
            cboResult.SelectedIndex = Convert.ToInt32(mSelectedRow["hivstatus"]);
        }
        #endregion

        #region viewChildren_FocusedRowChanged
        private void viewChildren_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle <= -1)
            {
                return;
            }

            this.Display_RowDetails(viewPMTCTOtherChildren.GetDataRow(e.FocusedRowHandle));
        }
        #endregion

        #region cmdAdd_Click
        private void cmdAdd_Click(object sender, EventArgs e)
        {
            AfyaPro_Types.clsResult mResult = new AfyaPro_Types.clsResult();

            string mFunctionName = "cmdAdd_Click";

            #region validation

            if (Program.IsNullDate(txtDeliveryDate.EditValue) == true)
            {
                Program.Display_Error("Please enter date of birth");
                txtDeliveryDate.Focus();
                return;
            }

            if (cboResult.SelectedIndex == -1)
            {
                Program.Display_Error("Please select HIV Status", false);
                cboResult.Focus();
                return;
            }

            #endregion

            try
            {
                DateTime mBirthDate = Convert.ToDateTime(txtDeliveryDate.EditValue);
                int mHIVStatus = cboResult.SelectedIndex;

                DataRow mNewRow = pDtChildren.NewRow();
                mNewRow["autocode"] = 0;
                mNewRow["birthdate"] = mBirthDate;
                mNewRow["hivstatus"] = mHIVStatus;
                mNewRow["hivstatusdescription"] = cboResult.Text;
                pDtChildren.Rows.Add(mNewRow);
                pDtChildren.AcceptChanges();

                //refresh
                this.Details_Clear();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region cmdUpdate_Click
        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            string mFunctionName = "cmdUpdate_Click";

            if (viewPMTCTOtherChildren.FocusedRowHandle <= -1)
            {
                return;
            }

            DataRow mSelectedRow = viewPMTCTOtherChildren.GetDataRow(viewPMTCTOtherChildren.FocusedRowHandle);

            #region validation

            if (Program.IsNullDate(txtDeliveryDate.EditValue) == true)
            {
                Program.Display_Error("Please enter date of birth");
                txtDeliveryDate.Focus();
                return;
            }

            if (cboResult.SelectedIndex == -1)
            {
                Program.Display_Error("Please select HIV Status", false);
                cboResult.Focus();
                return;
            }

            #endregion

            try
            {
                DateTime mBirthDate = Convert.ToDateTime(txtDeliveryDate.EditValue);
                int mHIVStatus = cboResult.SelectedIndex;

                mSelectedRow.BeginEdit();

                mSelectedRow["birthdate"] = mBirthDate;
                mSelectedRow["hivstatus"] = mHIVStatus;
                mSelectedRow["hivstatusdescription"] = cboResult.Text;

                mSelectedRow.EndEdit();

                //refresh
                this.Details_Clear();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
        }
        #endregion

        #region cmdDelete_Click
        private void cmdDelete_Click(object sender, EventArgs e)
        {
            string mFunctionName = "cmdDelete_Click";

            this.Cursor = Cursors.WaitCursor;

            try
            {
                if (viewPMTCTOtherChildren.FocusedRowHandle <= -1)
                {
                    return;
                }

                DataRow mSelectedRow = viewPMTCTOtherChildren.GetDataRow(viewPMTCTOtherChildren.FocusedRowHandle);

                DialogResult mResp = Program.Confirm_Deletion("'"
                    + Convert.ToDateTime(mSelectedRow["birthdate"]).Date.ToString("d") + "'   '"
                    + mSelectedRow["hivstatusdescription"].ToString().Trim() + "'");

                if (mResp != DialogResult.Yes)
                {
                    return;
                }

                if (Convert.ToInt32(mSelectedRow["autocode"]) != 0)
                {
                    AfyaPro_Types.clsCtcClient mResult = pMdtCTCClients.Delete_PMTCTOtherChild(
                                                                Convert.ToInt32(mSelectedRow["autocode"]),
                                                                Program.gMachineName,
                                                                Program.gMachineUser,
                                                                Program.gCurrentUser.Code);

                    if (mResult.Exe_Result == 0)
                    {
                        Program.Display_Error(mResult.Exe_Message);
                        return;
                    }
                    if (mResult.Exe_Result == -1)
                    {
                        Program.Display_Server_Error(mResult.Exe_Message);
                        return;
                    }
                }

                viewPMTCTOtherChildren.DeleteSelectedRows();
                pDtChildren.AcceptChanges();
            }
            catch (Exception ex)
            {
                Program.Display_Error(pClassName, mFunctionName, ex.Message);
                return;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        #region cmdClear_Click
        private void cmdClear_Click(object sender, EventArgs e)
        {
            this.Details_Clear();
        }
        #endregion

        #region cmdClose_Click
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region frmCTCPMTCTOtherChildren_KeyDown
        void frmCTCPMTCTOtherChildren_KeyDown(object sender, KeyEventArgs e)
        {
            //switch (e.KeyCode)
            //{
            //    case Program.KeyCode_ItemAdd:
            //        {
            //            this.cmdAdd_Click(cmdAdd, e);
            //        }
            //        break;
            //    case Program.KeyCode_ItemRemove:
            //        {
            //            this.cmdDelete_Click(cmdDelete, e);
            //        }
            //        break;
            //    case Program.KeyCode_ItemUpdate:
            //        {
            //            this.cmdUpdate_Click(cmdUpdate, e);
            //        }
            //        break;
            //    case Program.KeyCode_RchClear:
            //        {
            //            this.cmdClear_Click(cmdClear, e);
            //        }
            //        break;
            //}
        }
        #endregion
    }
}