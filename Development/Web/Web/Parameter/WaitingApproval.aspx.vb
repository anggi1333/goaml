﻿Imports Ext.Net
Imports OfficeOpenXml
Partial Class WaitingApproval
    Inherits Parent
    Public objFormModuleApproval As NawaBLL.FormModuleWaiting
    Public Property ObjModule As NawaDAL.Module
        Get
            Return Session("WaitingApproval.ObjModule")
        End Get
        Set(ByVal value As NawaDAL.Module)
            Session("WaitingApproval.ObjModule") = value
        End Set
    End Property
    Public Property strWhereClause() As String
        Get
            Return Session("WaitingApproval.strWhereClause")
        End Get
        Set(ByVal value As String)
            Session("WaitingApproval.strWhereClause") = value
        End Set
    End Property
    Public Property strOrder() As String
        Get
            Return Session("WaitingApproval.strSort")
        End Get
        Set(ByVal value As String)
            Session("WaitingApproval.strSort") = value
        End Set
    End Property
    Public Property indexStart() As String
        Get
            Return Session("WaitingApproval.indexStart")
        End Get
        Set(ByVal value As String)
            Session("WaitingApproval.indexStart") = value
        End Set
    End Property
    Private Sub Parameterview_Init(sender As Object, e As EventArgs) Handles Me.Init
        objFormModuleApproval = New NawaBLL.FormModuleWaiting(Me.GridpanelView)
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Dim Moduleid As String = Request.Params("ModuleID")
            Dim intModuleid As Integer
            Try
                intModuleid = NawaBLL.Common.DecryptQueryString(Moduleid, NawaBLL.SystemParameterBLL.GetEncriptionKey)
                ObjModule = NawaBLL.ModuleBLL.GetModuleByModuleID(intModuleid)

                If Not NawaBLL.ModuleBLL.GetHakAkses(NawaBLL.Common.SessionCurrentUser.FK_MGroupMenu_ID, ObjModule.PK_Module_ID, NawaBLL.Common.ModuleActionEnum.view) Then
                    Dim strIDCode As String = 1
                    strIDCode = NawaBLL.Common.EncryptQueryString(strIDCode, NawaBLL.SystemParameterBLL.GetEncriptionKey)

                    Ext.Net.X.Redirect(String.Format(NawaBLL.Common.GetApplicationPath & "/UnAuthorizeAccess.aspx?ID={0}", strIDCode), "Loading...")
                    Exit Sub
                End If

                objFormModuleApproval.SettingFormApproval(intModuleid)

                If Not objmodule.IsUseDesigner Then
                    objFormModuleApproval.urlwaitingapproval = objmodule.UrlApprovalDetail
                End If
            Catch ex As Exception

            End Try

            If Not Ext.Net.X.IsAjaxRequest Then
                cboExportExcel.SelectedItem.Text = "Excel"
            End If





        Catch ex As Exception
            Elmah.ErrorSignal.FromCurrentContext.Raise(ex)
            Ext.Net.X.Msg.Alert("Error", ex.Message).Show()
        End Try
    End Sub
    Protected Sub Store_ReadData(sender As Object, e As StoreReadDataEventArgs)
        Try
            Dim intStart As Integer = e.Start
            'If intStart = 0 Then intStart = 1
            Dim intLimit As Int16 = e.Limit
            Dim inttotalRecord As Integer
            Dim strfilter As String = NawaBLL.Nawa.BLL.NawaFramework.GetWhereClauseHeader(e) 'objFormModuleApproval.GetWhereClauseHeader(e)

            strfilter = strfilter.Replace("CreatedDate", "ModuleApproval.CreatedDate")
            strfilter = strfilter.Replace("CreatedBy", "ModuleApproval.CreatedBy")
            Dim strsort As String = ""
            For Each item As DataSorter In e.Sort
                strsort += item.Property & " " & item.Direction.ToString
            Next
            Me.indexStart = intStart
            Me.strWhereClause = strfilter

            Dim strparam As String = Request.Params("ExtraParam")

            If strWhereClause.Trim.Length = 0 Then
                strWhereClause += " ModuleApproval.modulename='" & ObjModule.ModuleName & "' and ModuleApproval.createdby in ( SELECT m.UserID FROM MUser m WHERE m.FK_MRole_ID IN ( SELECT c.FK_MRole_ID FROM muser c WHERE c.UserID='" & NawaBLL.Common.SessionCurrentUser.UserID & "') AND m.UserID='" & NawaBLL.Common.SessionCurrentUser.UserID & "')"
            Else
                strWhereClause += " and  ModuleApproval.modulename='" & ObjModule.ModuleName & "' and ModuleApproval.createdby in ( SELECT m.UserID FROM MUser m WHERE m.FK_MRole_ID IN ( SELECT c.FK_MRole_ID FROM muser c WHERE c.UserID='" & NawaBLL.Common.SessionCurrentUser.UserID & "') AND m.UserID='" & NawaBLL.Common.SessionCurrentUser.UserID & "')"
            End If

            Me.strOrder = strsort
            ' Dim DataPaging As Data.DataTable = objFormModuleApproval.getDataPaging(strWhereClause, strsort, intStart - 1, intLimit, inttotalRecord)

            Dim DataPaging As Data.DataTable = NawaDAL.SQLHelper.ExecuteTabelPaging(" ModuleApproval iNNER JOIN ModuleAction ON moduleapproval.PK_ModuleAction_ID=moduleaction.PK_ModuleAction_ID inner join module on module.modulename=moduleapproval.modulename", "PK_ModuleApproval_ID, ModuleLabel, ModuleKey,moduleapproval.CreatedDate, moduleaction.ModuleActionName   ,moduleapproval.CreatedBy", strWhereClause, strsort, intStart, intLimit, inttotalRecord)
            ''-- start paging ------------------------------------------------------------
            Dim limit As Integer = e.Limit
            If (e.Start + e.Limit) > inttotalRecord Then
                limit = inttotalRecord - e.Start
            End If
            'Dim rangeData As List(Of Object) = If((e.Start < 0 OrElse limit < 0), data, data.GetRange(e.Start, limit))
            ''-- end paging ------------------------------------------------------------
            e.Total = inttotalRecord
            GridpanelView.GetStore.DataSource = DataPaging
            GridpanelView.GetStore.DataBind()

        Catch ex As Exception
            Elmah.ErrorSignal.FromCurrentContext.Raise(ex)
            Ext.Net.X.Msg.Alert("Error", ex.Message).Show()
        End Try
    End Sub
    Protected Sub ExportAllExcel(sender As Object, e As EventArgs)
        Dim objfileinfo As IO.FileInfo
        Try
            If Not cboExportExcel.SelectedItem Is Nothing Then
                If cboExportExcel.SelectedItem.Value = "Excel" Then
                    Dim tempfilexls As String = Guid.NewGuid.ToString & ".xlsx"
                    objfileinfo = New IO.FileInfo(Server.MapPath("~\temp\" & tempfilexls))
                    Using objtbl As Data.DataTable = NawaDAL.SQLHelper.ExecuteTabelPaging(" ModuleApproval iNNER JOIN ModuleAction ON moduleapproval.PK_ModuleAction_ID=moduleaction.PK_ModuleAction_ID inner join module on module.modulename=moduleapproval.modulename", "PK_ModuleApproval_ID as Id, ModuleLabel, ModuleKey,CONVERT(VARCHAR, ModuleApproval.CreatedDate, 106) CreatedDate, moduleaction.ModuleActionName   ,moduleapproval.CreatedBy", Me.strWhereClause, Me.strOrder, 0, Integer.MaxValue, 0)
                        Using resource As New ExcelPackage(objfileinfo)
                            Dim ws As ExcelWorksheet = resource.Workbook.Worksheets.Add("Approval")
                            ws.Cells("A1").LoadFromDataTable(objtbl, True)
                            'Dim dateformat As String = NawaBLL.SystemParameterBLL.GetDateFormat
                            'Dim intcolnumber As Integer = 0
                            'For Each item As System.Data.DataColumn In objtbl.Columns

                            '    intcolnumber = intcolnumber + 1
                            '    If item.DataType = GetType(Date) Then

                            '        ws.Column(intcolnumber).Style.Numberformat.Format = dateformat
                            '    End If
                            'Next
                            ws.Cells(ws.Dimension.Address).AutoFitColumns()
                            resource.Save()
                            Response.Clear()
                            Response.ClearHeaders()
                            Response.ContentType = "application/vnd.ms-excel"
                            Response.AddHeader("content-disposition", "attachment;filename=" & objFormModuleApproval.objSchemaModule.ModuleLabel.Replace(" ", "") & ".xlsx")
                            Response.Charset = ""
                            Response.AddHeader("cache-control", "max-age=0")
                            Me.EnableViewState = False
                            Response.ContentType = "ContentType"
                            Response.BinaryWrite(IO.File.ReadAllBytes(objfileinfo.FullName))
                            Response.End()
                        End Using
                    End Using
                ElseIf cboExportExcel.SelectedItem.Value = "CSV" Then
                    Dim tempfilexls As String = Guid.NewGuid.ToString & ".csv"
                    objfileinfo = New IO.FileInfo(Server.MapPath("~\temp\" & tempfilexls))
                    Dim stringWriter_Temp = New IO.StreamWriter(objfileinfo.FullName)


                    Using objtbl As Data.DataTable = NawaDAL.SQLHelper.ExecuteTabelPaging(" ModuleApproval iNNER JOIN ModuleAction ON moduleapproval.PK_ModuleAction_ID=moduleaction.PK_ModuleAction_ID inner join module on module.modulename=moduleapproval.modulename", "PK_ModuleApproval_ID as Id, ModuleLabel, ModuleKey,CONVERT(VARCHAR, ModuleApproval.CreatedDate, 106) CreatedDate, moduleaction.ModuleActionName   ,moduleapproval.CreatedBy", Me.strWhereClause, Me.strOrder, 0, Integer.MaxValue, 0)
                        For k As Integer = 0 To objtbl.Columns.Count - 1
                            'add separator 
                            stringWriter_Temp.Write(objtbl.Columns(k).ColumnName + ","c)
                        Next
                        'append new line 
                        stringWriter_Temp.Write(vbCr & vbLf)
                        For i As Integer = 0 To objtbl.Rows.Count - 1
                            For k As Integer = 0 To objtbl.Columns.Count - 1
                                'add separator 
                                stringWriter_Temp.Write("""" & objtbl.Rows(i).Item(k).ToString & """" + ","c)
                            Next
                            'append new line 
                            stringWriter_Temp.Write(vbCr & vbLf)
                        Next
                        stringWriter_Temp.Close()
                        Response.Clear()
                        Response.AddHeader("content-disposition", "attachment;filename=" & objFormModuleApproval.objSchemaModule.ModuleLabel.Replace(" ", "") & ".csv")
                        Response.Charset = ""
                        'Response.Cache.SetCacheability(HttpCacheability.NoCache)
                        Me.EnableViewState = False
                        'Response.ContentType = "application/ms-excel"
                        Response.ContentType = "text/csv"
                        Response.BinaryWrite(System.IO.File.ReadAllBytes(objfileinfo.FullName))
                        Response.End()
                    End Using
                Else
                    Ext.Net.X.MessageBox.Alert("error", "Please Choose Format").Show()
                End If
            Else
                Ext.Net.X.MessageBox.Alert("error", "Please Choose Format").Show()
            End If
        Catch ex As Exception
            Elmah.ErrorSignal.FromCurrentContext.Raise(ex)
            Ext.Net.X.Msg.Alert("Error", ex.Message).Show()
        End Try
    End Sub
    Protected Sub ExportExcel(sender As Object, e As EventArgs)
        Dim objfileinfo As IO.FileInfo
        Try
            If Not cboExportExcel.SelectedItem Is Nothing Then
                If cboExportExcel.SelectedItem.Value = "Excel" Then
                    Dim tempfilexls As String = Guid.NewGuid.ToString & ".xlsx"
                    objfileinfo = New IO.FileInfo(Server.MapPath("~\temp\" & tempfilexls))

                    Using objtbl As Data.DataTable = NawaDAL.SQLHelper.ExecuteTabelPaging(" ModuleApproval iNNER JOIN ModuleAction ON moduleapproval.PK_ModuleAction_ID=moduleaction.PK_ModuleAction_ID inner join module on module.modulename=moduleapproval.modulename", "PK_ModuleApproval_ID as Id, ModuleLabel, ModuleKey,CONVERT(VARCHAR, ModuleApproval.CreatedDate, 106) CreatedDate, moduleaction.ModuleActionName   ,moduleapproval.CreatedBy", Me.strWhereClause, Me.strOrder, Me.indexStart, NawaBLL.SystemParameterBLL.GetPageSize, 0)
                        '                        objFormModuleView.changeheaderName(objtbl)
                        Using resource As New ExcelPackage(objfileinfo)
                            Dim ws As ExcelWorksheet = resource.Workbook.Worksheets.Add("Approval")
                            ws.Cells("A1").LoadFromDataTable(objtbl, True)
                            'Dim dateformat As String = NawaBLL.SystemParameterBLL.GetDateFormat
                            'Dim intcolnumber As Integer = 0
                            'For Each item As System.Data.DataColumn In objtbl.Columns
                            '    intcolnumber = intcolnumber + 1
                            '    If item.DataType = GetType(Date) Then

                            '        ws.Column(intcolnumber).Style.Numberformat.Format = dateformat
                            '    End If
                            'Next
                            ws.Cells(ws.Dimension.Address).AutoFitColumns()
                            resource.Save()
                            Response.Clear()
                            Response.ClearHeaders()
                            Response.ContentType = "application/vnd.ms-excel"
                            Response.AddHeader("content-disposition", "attachment;filename=" & objFormModuleApproval.objSchemaModule.ModuleLabel.Replace(" ", "") & ".xlsx")
                            Response.Charset = ""
                            Response.AddHeader("cache-control", "max-age=0")
                            Me.EnableViewState = False
                            Response.ContentType = "ContentType"
                            Response.BinaryWrite(IO.File.ReadAllBytes(objfileinfo.FullName))
                            Response.End()
                        End Using
                    End Using
                    'Dim json As String = Me.Hidden1.Value.ToString()
                    'Dim eSubmit As New StoreSubmitDataEventArgs(json, Nothing)
                    'Dim xml As XmlNode = eSubmit.Xml
                    'Me.Response.Clear()
                    'Me.Response.ContentType = "application/vnd.ms-excel"
                    'Me.Response.AddHeader("Content-Disposition", "attachment; filename=submittedData.xls")
                    'Dim xtExcel As New Xsl.XslCompiledTransform()
                    'xtExcel.Load(Server.MapPath("Excel.xsl"))
                    'xtExcel.Transform(xml, Nothing, Me.Response.OutputStream)
                    'Me.Response.[End]()
                ElseIf cboExportExcel.SelectedItem.Value = "CSV" Then
                    Dim tempfilexls As String = Guid.NewGuid.ToString & ".csv"
                    objfileinfo = New IO.FileInfo(Server.MapPath("~\temp\" & tempfilexls))
                    Dim stringWriter_Temp = New IO.StreamWriter(objfileinfo.FullName)

                    Using objtbl As Data.DataTable = NawaDAL.SQLHelper.ExecuteTabelPaging(" ModuleApproval iNNER JOIN ModuleAction ON moduleapproval.PK_ModuleAction_ID=moduleaction.PK_ModuleAction_ID inner join module on module.modulename=moduleapproval.modulename", "PK_ModuleApproval_ID as Id, ModuleLabel, ModuleKey,CONVERT(VARCHAR, ModuleApproval.CreatedDate, 106) CreatedDate, moduleaction.ModuleActionName   ,moduleapproval.CreatedBy", Me.strWhereClause, Me.strOrder, Me.indexStart, NawaBLL.SystemParameterBLL.GetPageSize, 0)
                        For k As Integer = 0 To objtbl.Columns.Count - 1
                            'add separator 
                            stringWriter_Temp.Write(objtbl.Columns(k).ColumnName + ","c)
                        Next
                        'append new line 
                        stringWriter_Temp.Write(vbCr & vbLf)
                        For i As Integer = 0 To objtbl.Rows.Count - 1
                            For k As Integer = 0 To objtbl.Columns.Count - 1
                                'add separator 
                                stringWriter_Temp.Write("""" & objtbl.Rows(i).Item(k).ToString & """" + ","c)
                            Next
                            'append new line 
                            stringWriter_Temp.Write(vbCr & vbLf)
                        Next
                        stringWriter_Temp.Close()
                        Response.Clear()
                        Response.AddHeader("content-disposition", "attachment;filename=" & objFormModuleApproval.objSchemaModule.ModuleLabel.Replace(" ", "") & ".csv")
                        Response.Charset = ""
                        'Response.Cache.SetCacheability(HttpCacheability.NoCache)
                        Me.EnableViewState = False
                        'Response.ContentType = "application/ms-excel"
                        Response.ContentType = "text/csv"
                        Response.BinaryWrite(System.IO.File.ReadAllBytes(objfileinfo.FullName))
                        Response.End()
                    End Using
                Else
                    Ext.Net.X.MessageBox.Alert("error", "Please Choose Format").Show()
                End If
            Else
                Ext.Net.X.MessageBox.Alert("error", "Please Choose Format").Show()
            End If
        Catch ex As Exception
            Ext.Net.X.MessageBox.Alert("error", "Please Choose Format").Show()
        Finally
            If Not objfileinfo Is Nothing Then
                objfileinfo.Delete()
            End If
        End Try
    End Sub
End Class

