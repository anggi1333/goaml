﻿'------------------------------------------------------------------------------
' <auto-generated>
'    This code was generated from a template.
'
'    Manual changes to this file may cause unexpected behavior in your application.
'    Manual changes to this file will be overwritten if the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Imports System
Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure

Partial Public Class NawaDatadevEntities
    Inherits DbContext

    Public Sub New()
        MyBase.New("name=NawaDatadevEntities")
    End Sub

    Protected Overrides Sub OnModelCreating(modelBuilder As DbModelBuilder)
        Throw New UnintentionalCodeFirstException()
    End Sub

    Public Property AuditTrail_UserAccess() As DbSet(Of AuditTrail_UserAccess)
    Public Property AuditTrail_UserLogin() As DbSet(Of AuditTrail_UserLogin)
    Public Property AuditTrailStatus() As DbSet(Of AuditTrailStatu)
    Public Property ELMAH_Error() As DbSet(Of ELMAH_Error)
    Public Property EmailStatus() As DbSet(Of EmailStatu)
    Public Property EODSchedulers() As DbSet(Of EODScheduler)
    Public Property EODSchedulerDetails() As DbSet(Of EODSchedulerDetail)
    Public Property EODSchedulerLogs() As DbSet(Of EODSchedulerLog)
    Public Property EODTasks() As DbSet(Of EODTask)
    Public Property EODTaskDetails() As DbSet(Of EODTaskDetail)
    Public Property EODTaskDetailLogs() As DbSet(Of EODTaskDetailLog)
    Public Property EODTaskDetailTypes() As DbSet(Of EODTaskDetailType)
    Public Property EODTaskLogs() As DbSet(Of EODTaskLog)
    Public Property FileSecurities() As DbSet(Of FileSecurity)
    Public Property LogConsoleServices() As DbSet(Of LogConsoleService)
    Public Property MenuTemplates() As DbSet(Of MenuTemplate)
    Public Property MExtTypes() As DbSet(Of MExtType)
    Public Property MFieldTypes() As DbSet(Of MFieldType)
    Public Property MGroupMenuAccesses() As DbSet(Of MGroupMenuAccess)
    Public Property MGroupMenuSetttings() As DbSet(Of MGroupMenuSettting)
    Public Property Modules() As DbSet(Of [Module])
    Public Property ModuleActions() As DbSet(Of ModuleAction)
    Public Property ModuleApprovals() As DbSet(Of ModuleApproval)
    Public Property ModuleFields() As DbSet(Of ModuleField)
    Public Property ModuleFieldDefaults() As DbSet(Of ModuleFieldDefault)
    Public Property ModuleFieldRegexes() As DbSet(Of ModuleFieldRegex)
    Public Property ModuleTimes() As DbSet(Of ModuleTime)
    Public Property ModuleValidations() As DbSet(Of ModuleValidation)
    Public Property MRoles() As DbSet(Of MRole)
    Public Property MRole_Upload() As DbSet(Of MRole_Upload)
    Public Property MsEODPeriods() As DbSet(Of MsEODPeriod)
    Public Property MsEODStatus() As DbSet(Of MsEODStatu)
    Public Property MUserHistoryPasswords() As DbSet(Of MUserHistoryPassword)
    Public Property ReportQueries() As DbSet(Of ReportQuery)
    Public Property SystemParameters() As DbSet(Of SystemParameter)
    Public Property SystemParameterGroups() As DbSet(Of SystemParameterGroup)
    Public Property MonitoringDurations() As DbSet(Of MonitoringDuration)
    Public Property EmailTemplateDetails() As DbSet(Of EmailTemplateDetail)
    Public Property EmailTemplates() As DbSet(Of EmailTemplate)
    Public Property EmailTemplateSchedulers() As DbSet(Of EmailTemplateScheduler)
    Public Property MGroupMenus() As DbSet(Of MGroupMenu)
    Public Property EmailTemplateSchedulerDetails() As DbSet(Of EmailTemplateSchedulerDetail)
    Public Property MUsers() As DbSet(Of MUser)
    Public Property AuditTrailDetails() As DbSet(Of AuditTrailDetail)
    Public Property AuditTrailHeaders() As DbSet(Of AuditTrailHeader)
    Public Property GenerateFileTemplateDetails() As DbSet(Of GenerateFileTemplateDetail)
    Public Property SLIKParameters() As DbSet(Of SLIKParameter)
    Public Property GeneratedFileLists() As DbSet(Of GeneratedFileList)
    Public Property GenerateFileTemplates() As DbSet(Of GenerateFileTemplate)
    Public Property Vw_GeneratedFileList() As DbSet(Of Vw_GeneratedFileList)
    Public Property Web_ValidationReport_RowComplate() As DbSet(Of Web_ValidationReport_RowComplate)
    Public Property vw_ValidationReport() As DbSet(Of vw_ValidationReport)
    Public Property vw_TextFileTemporaryTable() As DbSet(Of vw_TextFileTemporaryTable)
    Public Property TextFileTemporaryTables() As DbSet(Of TextFileTemporaryTable)
    Public Property TextFileTemporaryTableDetails() As DbSet(Of TextFileTemporaryTableDetail)
    Public Property Web_ValidationReport_Row() As DbSet(Of Web_ValidationReport_Row)
    Public Property ValidationSummaries() As DbSet(Of ValidationSummary)
    Public Property SettingPersonals() As DbSet(Of SettingPersonal)
    Public Property GenerateFileTemplateAdditionals() As DbSet(Of GenerateFileTemplateAdditional)
    Public Property Vw_validationReport_List() As DbSet(Of Vw_validationReport_List)
    Public Property CleansingReports() As DbSet(Of CleansingReport)
    Public Property ORS_FormInfo() As DbSet(Of ORS_FormInfo)
    Public Property ORS_ValidationKey() As DbSet(Of ORS_ValidationKey)
    Public Property Ref_KategoriValidasi() As DbSet(Of Ref_KategoriValidasi)
    Public Property ValidationTypes() As DbSet(Of ValidationType)
    Public Property Ref_DataSet() As DbSet(Of Ref_DataSet)
    Public Property LastLogins() As DbSet(Of LastLogin)
    Public Property ReportofDisableNames() As DbSet(Of ReportofDisableName)
    Public Property vw_AuditTrail_UserAccess() As DbSet(Of vw_AuditTrail_UserAccess)
    Public Property Vw_KelompokInformasiRelation() As DbSet(Of Vw_KelompokInformasiRelation)
    Public Property vw_KodeCabangActive() As DbSet(Of vw_KodeCabangActive)
    Public Property Vw_ListAllCabang() As DbSet(Of Vw_ListAllCabang)
    Public Property vw_MappingCounterparty() As DbSet(Of vw_MappingCounterparty)
    Public Property vw_Menuall() As DbSet(Of vw_Menuall)
    Public Property vw_Ref_SandiBankAlias() As DbSet(Of vw_Ref_SandiBankAlias)
    Public Property vw_Ref_SandiNasabahLainDalamNegeriAlias() As DbSet(Of vw_Ref_SandiNasabahLainDalamNegeriAlias)
    Public Property Vw_SettingPersonal() As DbSet(Of Vw_SettingPersonal)
    Public Property Vw_ValidationReportSummary() As DbSet(Of Vw_ValidationReportSummary)
    Public Property Prefix_Module_Generator() As DbSet(Of Prefix_Module_Generator)
    Public Property ValidationParameters() As DbSet(Of ValidationParameter)
    Public Property vw_EODLog() As DbSet(Of vw_EODLog)

End Class
