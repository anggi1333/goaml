'------------------------------------------------------------------------------
' <auto-generated>
'    This code was generated from a template.
'
'    Manual changes to this file may cause unexpected behavior in your application.
'    Manual changes to this file will be overwritten if the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Imports System
Imports System.Collections.Generic

Partial Public Class AuditTrailDetail
    Public Property PK_AuditTrailDetail_id As Long
    Public Property FK_AuditTrailHeader_ID As Nullable(Of Long)
    Public Property FieldName As String
    Public Property OldValue As String
    Public Property NewValue As String

    Public Overridable Property AuditTrailHeader As AuditTrailHeader

End Class
