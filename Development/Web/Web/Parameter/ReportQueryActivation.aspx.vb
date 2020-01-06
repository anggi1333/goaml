﻿Imports Ext.Net
Partial Class ReportQueryActivation
    Inherits Parent

    Public Property StrUnikKey() As String
        Get
            Return Session("ReportQueryActivation.StrUnikKey")
        End Get
        Set(ByVal value As String)
            Session("ReportQueryActivation.StrUnikKey") = value
        End Set
    End Property

    Public Property objmodule() As NawaDAL.Module
        Get
            Return Session("ReportQueryActivation.objmodule")
        End Get
        Set(ByVal value As NawaDAL.Module)
            Session("ReportQueryActivation.objmodule") = value
        End Set
    End Property
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try



            Dim Moduleid As String = Request.Params("ModuleID")
            Dim IDData As String = Request.Params("ID")
            Dim intModuleid As Integer
            Try
                intModuleid = NawaBLL.Common.DecryptQueryString(Moduleid, NawaBLL.SystemParameterBLL.GetEncriptionKey)
                objmodule = NawaBLL.ModuleBLL.GetModuleByModuleID(intModuleid)

                If Not NawaBLL.ModuleBLL.GetHakAkses(NawaBLL.Common.SessionCurrentUser.FK_MGroupMenu_ID, objmodule.PK_Module_ID, NawaBLL.Common.ModuleActionEnum.Activation) Then
                    Dim strIDCode As String = 1
                    strIDCode = NawaBLL.Common.EncryptQueryString(strIDCode, NawaBLL.SystemParameterBLL.GetEncriptionKey)

                    Ext.Net.X.Redirect(String.Format(NawaBLL.Common.GetApplicationPath & "/UnAuthorizeAccess.aspx?ID={0}", strIDCode), "Loading...")
                    Exit Sub
                End If
                FormPanelInput.Title = objmodule.ModuleLabel & " Activation"
                Panelconfirmation.Title = objmodule.ModuleLabel & " Activation"
                StrUnikKey = NawaBLL.Common.DecryptQueryString(IDData, NawaBLL.SystemParameterBLL.GetEncriptionKey)

                If Not Ext.Net.X.IsAjaxRequest Then
                    NawaBLL.ReportQueryBLL.LoadPanelActivation(FormPanelInput, objmodule.ModuleName, StrUnikKey)
                End If


            Catch ex As Exception

            End Try

            'objFormModuleAdd.ModuleName = "MRole"

            'objFormModuleAdd.SettingFormAdd()


        Catch ex As Exception
            Elmah.ErrorSignal.FromCurrentContext.Raise(ex)
            Ext.Net.X.Msg.Alert("Error", ex.Message).Show()
        End Try

    End Sub

    Protected Sub Callback(sender As Object, e As DirectEventArgs)
        Try
            Select Case e.ExtraParams("command")
                Case "Activation"
                    If NawaBLL.ReportQueryBLL.IsDataValidDelete(StrUnikKey, objmodule) Then


                        If NawaBLL.Common.SessionCurrentUser.FK_MRole_ID = 1 OrElse Not objmodule.IsUseApproval Then

                            NawaBLL.ReportQueryBLL.activationTanpaapproval(StrUnikKey, objmodule)

                            Panelconfirmation.Hidden = False
                            LblConfirmation.Text = "Data  " & objmodule.ModuleLabel & " is changed."
                            FormPanelInput.Hidden = True

                        Else
                            NawaBLL.ReportQueryBLL.activationDenganapproval(StrUnikKey, objmodule)

                            Panelconfirmation.Hidden = False
                            LblConfirmation.Text = "Data " & objmodule.ModuleLabel & " Saved into Pending Approval"
                            FormPanelInput.Hidden = True

                        End If
                    Else
                        Throw New Exception("Data Report Query Already Exist in Pending Approval.")

                    End If




            End Select

        Catch ex As Exception
            Elmah.ErrorSignal.FromCurrentContext.Raise(ex)
            Ext.Net.X.Msg.Alert("Error", ex.Message).Show()
        End Try

    End Sub
    Private Sub BtnConfirmation_DirectClick(sender As Object, e As DirectEventArgs) Handles BtnConfirmation.DirectClick
        Try
            Dim Moduleid As String = Request.Params("ModuleID")

            Ext.Net.X.Redirect(NawaBLL.Common.GetApplicationPath & objmodule.UrlView & "?ModuleID=" & Moduleid)
        Catch ex As Exception
            Elmah.ErrorSignal.FromCurrentContext.Raise(ex)
            Ext.Net.X.Msg.Alert("Error", ex.Message).Show()

        End Try
    End Sub
    Protected Sub BtnCancel_Click(sender As Object, e As DirectEventArgs)
        Try
            Dim Moduleid As String = Request.Params("ModuleID")

            Ext.Net.X.Redirect(NawaBLL.Common.GetApplicationPath & objmodule.UrlView & "?ModuleID=" & Moduleid)
        Catch ex As Exception
            Elmah.ErrorSignal.FromCurrentContext.Raise(ex)
            Ext.Net.X.Msg.Alert("Error", ex.Message).Show()

        End Try
    End Sub
End Class
